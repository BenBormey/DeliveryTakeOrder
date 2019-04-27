Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Data.OleDb
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters

Public Class FrmProcessTakeOrder
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private MyData As New DatabaseFramework_MySQL
    Private Todate As Date
    Private Printer As New PrintToPrinter
    Private Export As New ConvertReport
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Private Report As LocalReport
    Private RParameter As ReportParameter
    Private DatabaseName As String
    Private query As String
    Private lists As DataTable
    Private Const InvoiceName As String = ""
    Private vCheckBox As CheckBox

    Private Sub DrawCheckBoxInDataGridView()
        Dim rect As Rectangle = DgvShow.GetCellDisplayRectangle(0, -1, True)
        rect.Y = 3
        rect.X = rect.Location.X + (rect.Width / 10)
        vCheckBox = New CheckBox()
        With vCheckBox
            .BackColor = Color.Transparent
        End With

        vCheckBox.Name = "Checker"
        vCheckBox.Size = New Size(18, 18)
        vCheckBox.Location = rect.Location
        AddHandler vCheckBox.CheckedChanged, AddressOf vCheckBox_CheckedChanged
        DgvShow.Controls.Add(vCheckBox)
    End Sub

    Private Sub vCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        Dim headerBox As CheckBox = DirectCast(DgvShow.Controls.Find("Checker", True)(0), CheckBox)
        Dim vSelected As Integer = 0
        For Each row As DataGridViewRow In DgvShow.Rows
            row.Cells(0).Value = headerBox.Checked
            If CBool(row.Cells(0).Value) = True Then vSelected += 1
            If vSelected >= 21 Then Exit For
        Next
        LblSeletedRow.Text = String.Format("||  Selected Row : {0}", vSelected)
    End Sub

    Private Sub LoadingInitialized()
        Initialized.LoadingInitialized(Data, App)
        DatabaseName = String.Format("{0}{1}", Data.PrefixDatabase, Data.DatabaseName)
    End Sub

    Private Sub DataSources(ByVal ComboBoxName As ComboBox, ByVal DTable As DataTable, ByVal DisplayMember As String, ByVal ValueMember As String)
        ComboBoxName.DataSource = DTable
        ComboBoxName.DisplayMember = DisplayMember
        ComboBoxName.ValueMember = ValueMember
        ComboBoxName.SelectedIndex = -1
    End Sub

    Private Sub FrmProcessTakeOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmProcessTakeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        RequiredDateLoading.Enabled = True
        DrawCheckBoxInDataGridView()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TimerCustomerLoading_Tick(sender As Object, e As EventArgs) Handles TimerCustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCustomerLoading.Enabled = False
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vRequiredDate As Date = Todate
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then
            vRequiredDate = Todate
        Else
            If CmbRequiredDate.Text.Trim().Equals("") = True Then
                vRequiredDate = Todate
            Else
                vRequiredDate = CmbRequiredDate.SelectedValue
            End If
        End If
        query = <SQL>
                    <![CDATA[
                DECLARE @vRequiredDate AS DATE = N'{1:yyyy-MM-dd}';
                SELECT [CusNum],[CusName],[Customer]
                FROM (
                    --SELECT 0 AS [Index], N'CUS00000' AS [CusNum],N'All Customers' AS [CusName],N'All Customers' AS [Customer]
                    --UNION ALL
                    SELECT 1 AS [Index], [CusNum],[CusName],ISNULL([CusNum],'') + SPACE(3) + ISNULL([CusName],'') AS [Customer]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE (CONVERT(DATE,[DateRequired]) = @vRequiredDate)
                    GROUP BY [CusNum],[CusName],ISNULL([CusNum],'') + SPACE(3) + ISNULL([CusName],'')
                ) LISTS
                ORDER BY [Index],[CusName];
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vRequiredDate)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbCustomer, lists, "Customer", "CusNum")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TimerTakeOrderLoading_Tick(sender As Object, e As EventArgs) Handles TimerTakeOrderLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerTakeOrderLoading.Enabled = False
        CmbTakeOrderNo.DataSource = Nothing
        Dim CusNum As String = ""
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then
            CusNum = ""
        Else
            If CmbCustomer.Text.Trim().Equals("") = True Then
                CusNum = ""
            Else
                CusNum = CmbCustomer.SelectedValue
            End If
        End If
        Dim vDeltoId As Decimal = 0
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then
            vDeltoId = 0
        Else
            If CmbDelto.Text.Trim().Equals("") = True Then
                vDeltoId = 0
            Else
                vDeltoId = CmbDelto.SelectedValue
            End If
        End If
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vRequiredDate As Date = Todate
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then
            vRequiredDate = Todate
        Else
            If CmbRequiredDate.Text.Trim().Equals("") = True Then
                vRequiredDate = Todate
            Else
                vRequiredDate = CmbRequiredDate.SelectedValue
            End If
        End If
        query = <SQL>
                    <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vDateRequired AS DATE = N'{3:yyyy-MM-dd}';
                    SELECT [TakeOrderNumber]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE ([CusNum] = @vCusNum OR N'CUS00000' = @vCusNum) 
                    AND ([DelToId] = @vDeltoId OR 0 = @vDeltoId)
                    AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0 OR DATEDIFF(DAY,GETDATE(),@vDateRequired) = 0)
                    GROUP BY [TakeOrderNumber]
                    ORDER BY [TakeOrderNumber];
                ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CusNum, vDeltoId, vRequiredDate)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbTakeOrderNo, lists, "TakeOrderNumber", "TakeOrderNumber")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCustomer.SelectedIndexChanged
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then Exit Sub
        If CmbCustomer.Text.Trim().Equals("") = True Then Exit Sub
        'Initialized.R_AllUnpaid = True
        'If CmbCustomer.SelectedValue.Equals("CUS00000") = True Then
        '    Dim Frm As New FrmPODutchmillSelected
        '    If Frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'End If
        DeltoLoading.Enabled = True
        TimerDisplayLoading.Enabled = True
    End Sub

    Private Sub CmbTakeOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTakeOrderNo.SelectedIndexChanged
        If TypeOf CmbTakeOrderNo.SelectedValue Is DataRowView Or CmbTakeOrderNo.SelectedValue Is Nothing Then Exit Sub
        If CmbTakeOrderNo.Text.Trim().Equals("") = True Then Exit Sub
        TimerDisplayLoading.Enabled = True
    End Sub

    Private Sub TimerDisplayLoading_Tick(sender As Object, e As EventArgs) Handles TimerDisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerDisplayLoading.Enabled = False
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vRequiredDate As Date = Todate
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then
            vRequiredDate = Todate
        Else
            If CmbRequiredDate.Text.Trim().Equals("") = True Then
                vRequiredDate = Todate
            Else
                vRequiredDate = CmbRequiredDate.SelectedValue
            End If
        End If

        Dim CusNum As String = ""
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then
            CusNum = ""
        Else
            If CmbCustomer.Text.Trim().Equals("") = True Then
                CusNum = ""
            Else
                CusNum = CmbCustomer.SelectedValue
            End If
        End If

        Dim vDeltoId As Decimal = 0
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then
            vDeltoId = 0
        Else
            If CmbDelto.Text.Trim().Equals("") = True Then
                vDeltoId = 0
            Else
                vDeltoId = CmbDelto.SelectedValue
            End If
        End If
        
        Dim TakeOrderNo As Decimal = 0
        If TypeOf CmbTakeOrderNo.SelectedValue Is DataRowView Or CmbTakeOrderNo.SelectedValue Is Nothing Then
            TakeOrderNo = 0
        Else
            If CmbTakeOrderNo.Text.Trim().Equals("") = True Then
                TakeOrderNo = 0
            Else
                TakeOrderNo = CmbTakeOrderNo.SelectedValue
            End If
        End If
        query = <SQL><![CDATA[
DECLARE @vRequiredDate AS DATE = N'{1:yyyy-MM-dd}';
DECLARE @vCusNum AS NVARCHAR(8) = N'{2}';
DECLARE @vDeltoId AS DECIMAL(18, 0) = {3};
DECLARE @TakeOrderNo AS DECIMAL(18, 0) = {4};
WITH    v AS ( SELECT   [Id] ,
                        [CusNum] ,
                        [CusName] ,
                        [DelToId] ,
                        [DelTo] ,
                        [DateOrd] ,
                        [DateRequired] ,
                        [UnitNumber] ,
                        [Barcode] ,
                        CONVERT(NVARCHAR, N'') [CusCode] ,
                        [ProName] ,
                        [Size] ,
                        [QtyPCase] ,
                        [QtyPPack] ,
                        [Category] ,
                        [PcsFree] ,
                        [PcsOrder] ,
                        [PackOrder] ,
                        [CTNOrder] ,
                        [TotalPcsOrder] ,
                        [PONumber] ,
                        [LogInName] ,
                        [TakeOrderNumber] ,
                        [PromotionMachanic] ,
                        [ItemDiscount] ,
                        [Remark] ,
                        [Saleman] ,
                        [CreatedDate]
               FROM     [DBUNTWHOLESALECOLTD].[dbo].[TblDeliveryTakeOrders_Dutchmill]
               WHERE    ( CONVERT(DATE, [DateRequired]) = @vRequiredDate )
                        AND ( [CusNum] = @vCusNum
                              OR N'' = @vCusNum
                            )
                        AND ( [DelToId] = @vDeltoId
                              OR 0 = @vDeltoId
                            )
                        AND ( ( [TakeOrderNumber] = @TakeOrderNo )
                              OR ( 0 = @TakeOrderNo )
                            )
             )
    SELECT  v.*
    INTO    #DeliveryTakeorders
    FROM    v;

UPDATE  i
SET     i.[CusCode] = x.[CusCode]
FROM    #DeliveryTakeorders i
        INNER JOIN [DBUNTWHOLESALECOLTD].[dbo].[TblCustomerCodes] x ON ( x.CusNum = i.CusNum )
                                                              AND ( x.[Barcode] = i.[Barcode] );

SELECT  v.[Id] ,
        v.[TakeOrderNumber] ,
        v.[PONumber] ,
        v.[CusNum] ,
        v.[CusName] ,
        v.[DelToId] ,
        v.[DelTo] ,
        v.[DateOrd] ,
        v.[DateRequired] ,
        v.[UnitNumber] ,
        v.[Barcode] ,
        v.[CusCode] ,
        v.[ProName] ,
        v.[Size] ,
        v.[QtyPCase] ,
        v.[QtyPPack] ,
        v.[Category] ,
        v.[PcsFree] ,
        v.[PcsOrder] ,
        v.[PackOrder] ,
        v.[CTNOrder] ,
        v.[TotalPcsOrder] ,
        v.[LogInName] ,
        v.[PromotionMachanic] ,
        v.[ItemDiscount] ,
        v.[Remark] ,
        v.[Saleman] ,
        v.[CreatedDate]
FROM    #DeliveryTakeorders v
ORDER BY [CusName] ,
        [TakeOrderNumber];
            ]]></SQL>
        query = String.Format(query, DatabaseName, vRequiredDate, CusNum, vDeltoId, TakeOrderNo, IIf(Initialized.R_AllUnpaid = True, 0, 1), Initialized.R_DateFrom, Initialized.R_DateTo)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0:N0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnProcessTakeOrder_Click(sender As Object, e As EventArgs) Handles BtnProcessTakeOrder.Click
        If CmbCustomer.Text.Trim().Equals("") = True Or _
            CmbCustomer.Text.Trim().Equals("CUS00000") = True Then
            MessageBox.Show("Please select any customer first.", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbCustomer.Focus()
            Exit Sub
        ElseIf CmbDelto.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any delto.", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbDelto.Focus()
            Exit Sub
        ElseIf CmbRequiredDate.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any required date.", "Select Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbRequiredDate.Focus()
            Exit Sub
        ElseIf DgvShow.Rows.Count <= 0 Then
            MessageBox.Show("No record to process finish.", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        REM Check Required Date
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim oMinRequiredDate As Date = Todate
        Dim oDayAllow As Decimal = 0
        query = <SQL>
                    <![CDATA[
                DECLARE @oCusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @oDelToId AS DECIMAL(18,0) = {2};
                SELECT ISNULL(MIN([DateRequired]),CAST(GETDATE() AS DATE)) AS [DateRequired]
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                WHERE [CusNum] = @oCusNum AND [DelToId] = @oDelToId;
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CmbCustomer.SelectedValue, CmbDelto.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                oMinRequiredDate = CDate(IIf(IsDBNull(lists.Rows(0).Item("DateRequired")) = True, Todate, lists.Rows(0).Item("DateRequired")))
            End If
        End If
        oDayAllow = DateDiff(DateInterval.Day, CDate(CmbRequiredDate.Text), CDate(oMinRequiredDate))
        If oDayAllow >= 0 Then oDayAllow = DateDiff(DateInterval.Day, CDate(CmbRequiredDate.Text), CDate(Todate))
        If oDayAllow <= -6 Then
            If MessageBox.Show("Please check required date again." & vbCrLf & "The required date not allow to process this week." & vbCrLf & "Do you want to go ahead?(Yes/No)", "Not Allow Required Date", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                CmbRequiredDate.Focus()
                Exit Sub
            End If
        End If
        Dim vId As String = ""
        For Each v As DataGridViewRow In DgvShow.Rows
            If CBool(v.Cells("Tick").Value) = True Then
                vId &= String.Format("{0},", CDec(IIf(IsDBNull(v.Cells("Id").Value) = True, 0, v.Cells("Id").Value)))
                If CDec(IIf(DBNull.Value.Equals(v.Cells("TotalPcsOrder").Value) = True, 0, v.Cells("TotalPcsOrder").Value)) = 0 Then
                    MessageBox.Show("The Barcode (" & Trim(IIf(DBNull.Value.Equals(v.Cells("Barcode").Value) = True, "", v.Cells("Barcode").Value)) & "), Total Order Pcs is zero." & vbCrLf & "Please check it again...", "Invalid Processing", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
        Next
        If vId.Trim().Equals("") = True Then
            MessageBox.Show("Please tick the row(s) which you want to process the take order.", "Tick Row", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DgvShow.Focus()
            Exit Sub
        Else
            vId = vId.Trim()
            vId = Trim(Mid(vId, 1, vId.Length - 1))
        End If
        Dim CusNum As String = Trim(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value) = True, "", DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value))
        If MessageBox.Show("Are you sure, you already to process this take order?(Yes/No)", "Confirm Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Initialized.R_MessageAlert = ""
        Initialized.R_DocumentNumber = ""
        Initialized.R_LineCode = ""
        Initialized.R_DeptCode = ""
        Dim vPONumber As String = Trim(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("PONumber").Value) = True, "", DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("PONumber").Value))
        Dim vDeliveryDate As Date = Todate.Date
        Dim vFrm As New FrmDeliveryTakeOrderMessage With {.vPONumber = vPONumber, .vTodate = Todate}
        vFrm.DTPDeliveryDate.Value = Todate.Date
        If vFrm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        vPONumber = vFrm.vPONumber
        vDeliveryDate = vFrm.vDeliveryDate
        FrmDeliveryTakeOrderInfoAeon.ShowDialog()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vCusNum As String = ""
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then
            vCusNum = ""
        Else
            If CmbCustomer.Text.Trim() = "" Then
                vCusNum = ""
            Else
                vCusNum = CmbCustomer.SelectedValue
            End If
        End If
        Dim vDeltoId As Decimal = 0
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then
            vDeltoId = 0
        Else
            If CmbDelto.Text.Trim() = "" Then
                vDeltoId = 0
            Else
                vDeltoId = CmbDelto.SelectedValue
            End If
        End If
        Dim vRequiredDate As Date = Todate
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then
            vRequiredDate = Todate
        Else
            If CmbRequiredDate.Text.Trim() = "" Then
                vRequiredDate = Todate
            Else
                vRequiredDate = CmbRequiredDate.SelectedValue
            End If
        End If
        REM Check Invoice Number
        Dim vInvNo As Long = 0
        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RCom.Connection = RCon
        RCom.CommandType = CommandType.Text
        RCom.CommandText = String.Format("SELECT * FROM [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo]", DatabaseName)
        lists = New DataTable
        lists.Load(RCom.ExecuteReader())
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                If CBool(IIf(IsDBNull(lists.Rows(0).Item("IsBusy")) = True, 0, lists.Rows(0).Item("IsBusy"))) = True Then
                    RCon.Close()
                    MessageBox.Show("Printer is busy!" & vbCrLf & "Please wait a few minutes..." & vbCrLf & "Another PC is using...", "Printer Is Busy - Invoice Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    vInvNo = CLng(IIf(IsDBNull(lists.Rows(0).Item("PrintInvNo")) = True, 0, lists.Rows(0).Item("PrintInvNo")))
                    RCom.CommandText = String.Format("UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 1", DatabaseName)
                    RCom.ExecuteNonQuery()
                End If
            Else
                GoTo Err_Insert
            End If
        Else
Err_Insert:
            RCom.CommandText = String.Format("INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo]([PrintInvNo],[IsBusy]) VALUES(0,0)", DatabaseName)
            RCom.ExecuteNonQuery()
            vInvNo = 0
        End If
        RCon.Close()
        vInvNo += 1

        'Key
        Dim Key As Long = 0
        query = <SQL>
                    <![CDATA[
                DECLARE @ProgramName AS NVARCHAR(100) = 'TakeOrderKeyNumber';
                UPDATE [{0}].[dbo].[TblAutoNumber] 
                SET [AutoNumber] = 0, [CreatedDate] = GETDATE() 
                WHERE [ProgramName] = @ProgramName AND DATEDIFF(DAY,[CreatedDate],GETDATE()) <> 0;

                UPDATE [{0}].[dbo].[TblAutoNumber] 
                SET [AutoNumber] = ISNULL([AutoNumber],0) + 1, [CreatedDate] = GETDATE() 
                WHERE [ProgramName] = @ProgramName;

                SELECT [AutoNumber] 
                FROM [{0}].[dbo].[TblAutoNumber] 
                WHERE [ProgramName] = @ProgramName;
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                Key = CLng(IIf(IsDBNull(lists.Rows(0).Item("AutoNumber")) = True, 0, lists.Rows(0).Item("AutoNumber")))
            End If
        End If
        Dim RelatedKey As String = String.Format("{0:yyMMdd}{1}", Todate, Key)

        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RTran = RCon.BeginTransaction()
        Try
            RCom.Transaction = RTran
            RCom.Connection = RCon
            RCom.CommandType = CommandType.Text
            query = <SQL>
                        <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vRequiredDate AS DATE = N'{3:yyyy-MM-dd}';
                    DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {4};
                    DECLARE @RelatedKey AS NVARCHAR(10) = N'{5}';
                    DECLARE @vPONumber AS NVARCHAR(50) = N'{7}';
                    DECLARE @vDeliveryDate AS NVARCHAR(MAX) = N'{8}';
                    DECLARE @vMessageAlert AS NVARCHAR(MAX) = N'{9}';

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeliveryDate])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],@vPONumber AS [PONumber],[LogInName],@TakeOrderNumber AS [TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],GETDATE(),CASE WHEN (@vDeliveryDate = N'') THEN CONVERT(DATE,[DateRequired]) ELSE CONVERT(DATE,@vDeliveryDate) END
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    --WHERE ([CusNum] = @vCusNum) AND ([DelToId] = @vDeltoId) AND (CONVERT(DATE,[DateRequired]) = @vRequiredDate);
                    WHERE [Id] IN ({6});

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_Finish]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[FinishDate],[DeliveryDate])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],@vPONumber AS [PONumber],[LogInName],@TakeOrderNumber AS [TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],GETDATE(),CASE WHEN (@vDeliveryDate = N'') THEN CONVERT(DATE,[DateRequired]) ELSE CONVERT(DATE,@vDeliveryDate) END
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    --WHERE ([CusNum] = @vCusNum) AND ([DelToId] = @vDeltoId) AND (CONVERT(DATE,[DateRequired]) = @vRequiredDate);
                    WHERE [Id] IN ({6});

                    INSERT INTO [DBPickers].[dbo].[.tbldeliverytakeorders.dutchmill]([cusnum],[cusname],[deltoid],[delto],[dateorder],[daterequired],[deliverydate],[unitnumber],[barcode],[proname],[size],[qtypercase],[qtyperpack],[category],[pcsfree],[pcsorder],[packorder],[ctnorder],[totalpcsorder],[ponumber],[loginname],[takeordernumber],[promotionmachanic],[itemdiscount],[remark],[saleman],[note],[createddate])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[DeliveryDate],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],@vMessageAlert,[CreatedDate]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                    WHERE ([TakeOrderNumber] = @TakeOrderNumber);

                    -- Old Takeorder
                    --INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrder]([CusName],[CusNum],[DelTo],[DateOrd],[DateRequired],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderInvoiceNumber],[PrintInvoiceNumber],[PromotionMachanic],[ProCat],[ItemDiscount],[TranDate],[RemarkExpiry],[RelatedKey],[Saleman],[DeliveryDate])
                    --SELECT [CusName],[CusNum],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[ProName],[Size],[QtyPCase],[PcsFree],(ISNULL([PcsOrder],0) + (ISNULL([PackOrder],0) * ISNULL([QtyPPack],1))),[CTNOrder],[TotalPcsOrder],@vPONumber AS [PONumber],[LogInName],@TakeOrderNumber AS [TakeOrderNumber],NULL,[PromotionMachanic],[Category],[ItemDiscount],[CreatedDate],[Remark],@RelatedKey,[Saleman],CASE WHEN (@vDeliveryDate = N'') THEN CONVERT(DATE,[DateRequired]) ELSE CONVERT(DATE,@vDeliveryDate) END
                    -- FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                    --WHERE [TakeOrderNumber] = @TakeOrderNumber;

                    INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrderServiceLevel]([CusNum],[CusName],[DelTo],[DateOrd],[ShipDate],[Barcode],[ProName],[Size],[QtyPerCase],[PcsFree],[PcsOrder],[CTNOrder],[ActualOrder],[ActualDelivered],[TakeOrderNo],[TakeOrderDate],[PrintInvNo],[PrintDate],[InvoiceNo],[InvoiceDate],[RemarkStatus])
                    SELECT [CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[ProName],[Size],[QtyPCase],[PcsFree],(ISNULL([PcsOrder],0) + (ISNULL([PackOrder],0) * ISNULL([QtyPPack],1))),[CTNOrder],[TotalPcsOrder],0,[TakeOrderNumber],[CreatedDate],NULL,NULL,NULL,NULL,[Remark]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                    WHERE [TakeOrderNumber] = @TakeOrderNumber;

                    INSERT INTO [DBStockHistory].[dbo].[TblProcessHistory]([UnitNumber],[PackNumber],[CaseNumber],[ProName],[Size],[QtyPerCase],[Supplier],[ProgramName],[TransDate],[BeforeStock],[TransQty],[EndStock],[InvNumber],[Name],[Batchcode],[Location],[RelatedKey],[CreatedDate])  
                    SELECT v.[ProNumY],v.[ProNumYP],v.[ProNumYC],v.[ProName],v.[ProPacksize],v.[ProQtyPCase],v.[Sup1],N'Delivery Take Order' AS [ProgramName],v.[TransDate],v.[BeforeStock], x.[TotalPcsOrder]  AS [TransQty],v.[EndStock],@TakeOrderNumber AS [InvNumber],x.[CusNum] + SPACE(3) + x.[CusName] AS [Name],NULL AS [Batchcode],NULL AS [Location],@RelatedKey,GETDATE()  
                    FROM (  
	                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([ProTotQty],0) AS [BeforeStock],ISNULL([ProTotQty],0) AS [EndStock]  
	                    FROM [Stock].[dbo].[TPRProducts]  
	                    WHERE (ISNULL([ProNumY],'') IN (SELECT [UnitNumber] FROM [{0}].[dbo].[TblDeliveryTakeOrders] WHERE [TakeOrderNumber] = @TakeOrderNumber))
	                    UNION ALL  
	                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([ProTotQty],0) AS [BeforeStock],ISNULL([ProTotQty],0) AS [EndStock]  
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]  
	                    WHERE (ISNULL([ProNumY],'') IN (SELECT [UnitNumber] FROM [{0}].[dbo].[TblDeliveryTakeOrders] WHERE [TakeOrderNumber] = @TakeOrderNumber))
	                    UNION ALL  
	                    SELECT [OldProNumy] AS [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([Stock],0) AS [BeforeStock],ISNULL([Stock],0) AS [EndStock]  
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]  
	                    WHERE B.[OldProNumy] IN (SELECT [UnitNumber] FROM [{0}].[dbo].[TblDeliveryTakeOrders] WHERE [TakeOrderNumber] = @TakeOrderNumber)
	                    UNION ALL  
	                    SELECT [OldProNumy] AS [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([Stock],0) AS [BeforeStock],ISNULL([Stock],0) AS [EndStock]  
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]  
	                    WHERE B.[OldProNumy] IN (SELECT [UnitNumber] FROM [{0}].[dbo].[TblDeliveryTakeOrders] WHERE [TakeOrderNumber] = @TakeOrderNumber)
                    ) v INNER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS x ON v.[ProNumY] = x.[UnitNumber];

                    DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    --WHERE ([CusNum] = @vCusNum) AND ([DelToId] = @vDeltoId) AND (CONVERT(DATE,[DateRequired]) = @vRequiredDate);
                    WHERE [Id] IN ({6});
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vRequiredDate, vInvNo, RelatedKey, vId, vPONumber, IIf(DBNull.Value.Equals(vDeliveryDate) = True, "", String.Format("{0:yyyy-MM-dd}", vDeliveryDate)), Initialized.R_MessageAlert.Trim())
            RCom.CommandText = query
            RCom.ExecuteNonQuery()

            If Initialized.R_MessageAlert.Trim() <> "" Then
                query = <SQL>
                            <![CDATA[
                        DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                        DECLARE @Message AS NVARCHAR(100) = N'{2}';
                        INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrderAlertToQsDelivery]([InvNo],[CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[Message],[RegisterDate])
                        SELECT [TakeOrderNumber],[CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],@Message,GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                        WHERE [TakeOrderNumber] = @TakeOrderNumber;
                    ]]>
                        </SQL>
                query = String.Format(query, DatabaseName, vInvNo, Initialized.R_MessageAlert.Trim())
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
            End If

            If Initialized.R_DocumentNumber.Trim() <> "" And Initialized.R_LineCode.Trim() <> "" And Initialized.R_DeptCode.Trim <> "" Then
                query = <SQL>
                            <![CDATA[
                        DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                        DECLARE @CusNum AS NVARCHAR(8) = N'{2}';
                        DECLARE @DocumentNumber AS NVARCHAR(20) = N'{3}';
                        DECLARE @LineCode AS NVARCHAR(14) = N'{4}';
                        DECLARE @DeptCode AS NVARCHAR(14) = N'{5}';
                        INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrder_ForAEON]([SupervisorID],[CusNum],[DocumentNumber],[LineCode],[DeptCode],[TakeOrderID],[DeliveryID])
                        VALUES(NULL,@CusNum,@DocumentNumber,@LineCode,@DeptCode,@TakeOrderNumber,NULL);
                    ]]>
                        </SQL>
                query = String.Format(query, DatabaseName, vInvNo, CusNum, Initialized.R_DocumentNumber.Trim(), Initialized.R_LineCode.Trim(), Initialized.R_DeptCode.Trim())
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
            End If
            RCom.CommandText = "UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 0,[PrintInvNo] = " & vInvNo & "; "
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()
            MessageBox.Show("The processing have been finished.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TimerTakeOrderLoading.Enabled = True
            TimerDisplayLoading.Enabled = True
        Catch ex As SqlException
            RTran.Rollback()
            RCon.Close()
            RCon.Open()
            RCom.CommandType = CommandType.Text
            RCom.CommandText = "UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 0"
            RCom.ExecuteNonQuery()
            RCon.Close()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            RTran.Rollback()
            RCon.Close()
            RCon.Open()
            RCom.CommandType = CommandType.Text
            RCom.CommandText = "UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 0"
            RCom.ExecuteNonQuery()
            RCon.Close()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnExportToExcel_Click(sender As Object, e As EventArgs) Handles BtnExportToExcel.Click
        Dim Frm As New FrmPODutchmillDate
        If Frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim RequiredDate As Date = Frm.iRequiredDate
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        '    query =
        '    <SQL>
        '        <![CDATA[
        '            DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
        '            DECLARE @oQuery AS NVARCHAR(MAX) = N'';
        '            DECLARE @vQuery2 AS NVARCHAR(MAX) = N'';
        '            SELECT @vQuery2 += N'
        '            INSERT INTO @oPaymentStatement ([InvNumber],[CusNum],[Division])
        '            SELECT v.[InvNumber],v.[CusNum],N''' + o.[Division] + ''' [Division]
        '            FROM [Stock].[dbo].[' + o.[TableName] + '] v
        '            WHERE (v.[CusNum] IN (SELECT [CusNum] FROM #oDutchmill GROUP BY [CusNum]));
        '            '
        '            FROM [Stock].[dbo].[AAllDivisionStatement] o;
        '            SELECT @oQuery += N'
        '            INSERT INTO  @oPayment ([PONumber],[InvNumber],[ShipDate],[CusNum],[CusCom],[DelTo],[GrandTotal],[DatePaid],[PAID],[Division])
        '            SELECT v.[PONumber],v.[InvNumber],v.[ShipDate],v.[CusNum],v.[CusCom],v.[DelTo],v.[GrandTotal],v.[DatePaid],v.[PAID],N''' + v.[Division] + N''' [Division]
        '            FROM [Stock].[dbo].[' + v.[TableName] + N'] v
        '            WHERE (ISNUMERIC(v.[DatePaid]) <> 0) AND 
        '            (v.[PAID] <> N''PAID'') AND 
        '            (v.[CusNum] IN (SELECT [CusNum] FROM #oDutchmill GROUP BY [CusNum]));
        '            '
        '            FROM [Stock].[dbo].[AAllDivisionPayment] v
        '            WHERE v.[Division] NOT IN ('Division8', 'Division8 VAT', 'Division9','Division9 VAT', 'Division11', 'Division11 VAT')
        '            ORDER BY [v].[Division];

        '            SET @oQuery = N'
        '            DECLARE @DateRequired AS DATE = N''{1:yyyy-MM-dd}'';
        '            DECLARE @oPayment AS  TABLE 
        '            (
        '             [PONumber] [NVARCHAR](15) NULL,
        '             [InvNumber] [DECIMAL](18, 0) NULL,
        '             [ShipDate] [DATETIME] NULL,
        '             [CusNum] [NVARCHAR](15) NULL,
        '             [CusCom] [NVARCHAR](100) NULL,
        '             [DelTo] [NVARCHAR](100) NULL,
        '             [GrandTotal] [MONEY] NULL,
        '             [DatePaid] [NVARCHAR](20) NULL,
        '             [PAID] [NVARCHAR](13) NULL,
        '             [Division] [NVARCHAR](25) NULL
        '            );
        '            DECLARE @oPaymentStatement AS  TABLE 
        '            (
        '             [InvNumber] [DECIMAL](18, 0) NULL,
        '             [CusNum] [NVARCHAR](15) NULL,
        '             [Division] [NVARCHAR](25) NULL
        '            );
        '            SELECT [CusNum],[CusName]
        '            INTO #oDutchmill
        '            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
        '            WHERE (CONVERT(DATE,[DateRequired]) = @DateRequired)
        '            GROUP BY [CusNum],[CusName]
        '            ORDER BY [CusName];

        '            ' + @oQuery + @NewLineChar + @vQuery2 + N'

        '            WITH o AS (
        '             SELECT o.[PONumber],o.[InvNumber],o.[ShipDate],o.[CusNum],o.[CusCom],o.[DelTo],SUM(o.[GrandTotal]) [GrandTotal],o.[Division]
        '             FROM @oPayment o
        '             WHERE o.[InvNumber] NOT IN (SELECT v.[InvNumber] FROM @oPaymentStatement v WHERE (o.[Division] = v.[Division]) AND (o.[CusNum] = v.[CusNum]) AND (o.[InvNumber] = v.[InvNumber]))
        '             GROUP BY o.[PONumber],o.[InvNumber],o.[ShipDate],o.[CusNum],o.[CusCom],o.[DelTo],o.[Division]
        '            )
        '            SELECT o.PONumber,o.InvNumber,o.ShipDate,o.CusNum,o.CusCom,o.DelTo,o.GrandTotal,o.Division
        '            INTO #oPayment
        '            FROM o;

        '            DECLARE @oDateFrom AS DATE = DATEADD(MONTH,-3,CONVERT(DATE,GETDATE()));
        'DECLARE @oDateTo AS DATE = DATEADD(MONTH,0,CONVERT(DATE,GETDATE()));
        '            SELECT o.CusNum,ISNULL(SUM(o.GrandTotal),0) [GrandTotal],CONVERT(MONEY,(ISNULL(SUM(o.GrandTotal),0)/3)) [Average]
        'INTO #oAverage
        'FROM #oPayment o
        'WHERE CONVERT(DATE,o.ShipDate) BETWEEN @oDateFrom AND @oDateTo
        '            GROUP BY o.CusNum;

        '            SELECT o.CusNum,o.CusCom,o.[GrandTotal],DATEDIFF(DAY,MIN(o.[ShipDate]),GETDATE()) [Overday]
        '            ,ISNULL(v.[CreditLimitAllow],0) [CreditLimitAllow],ISNULL(CONVERT(DECIMAL,v.[MaxMonthAllow]),0) [MaxMonthAllow]
        '            ,x.[Average]
        '            FROM #oPayment o
        '            INNER JOIN [Stock].[dbo].[TPRCustomer] v ON (o.CusNum = v.CusNum) 
        '            LEFT OUTER JOIN #oAverage x ON (o.CusNum = x.CusNum) 
        '            GROUP BY o.CusNum,o.CusCom,o.[GrandTotal],ISNULL(v.[CreditLimitAllow],0),ISNULL(CONVERT(DECIMAL,v.[MaxMonthAllow]),0),x.[Average]
        '            HAVING ((SUM(o.[GrandTotal]) >= ISNULL(v.[CreditLimitAllow],0)) OR (ISNULL(DATEDIFF(DAY,MIN(o.[ShipDate]),GETDATE()),0) >= ISNULL(CONVERT(DECIMAL,v.[MaxMonthAllow]),0)))
        '            ORDER BY DATEDIFF(DAY,MIN(o.[ShipDate]),GETDATE()) DESC,SUM(o.[GrandTotal]) DESC,o.CusCom;

        '            DROP TABLE #oDutchmill;
        '            DROP TABLE #oPayment;
        '            DROP TABLE #oAverage;
        '            ';
        '            EXEC (@oQuery);
        '        ]]>
        '    </SQL>
        '    query = String.Format(query, DatabaseName, RequiredDate)
        query = <SQL>
                    <![CDATA[                
                DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
                DECLARE @oQuery AS NVARCHAR(MAX) = N'';
                DECLARE @vQuery2 AS NVARCHAR(MAX) = N'';
				DECLARE @vQuery3 AS NVARCHAR(MAX) = N'';

                SELECT @vQuery2 += N'
                INSERT INTO @oPaymentStatement ([InvNumber],[CusNum],[Division])
                SELECT v.[InvNumber],v.[CusNum],N''' + o.[Division] + ''' [Division]
                FROM [Stock].[dbo].[' + o.[TableName] + '] v
                WHERE (v.[CusNum] IN (SELECT [CusNum] FROM #oDutchmill GROUP BY [CusNum]));
                '
                FROM [Stock].[dbo].[AAllDivisionStatement] o;
                SELECT @oQuery += N'
                INSERT INTO  @oPayment ([PONumber],[InvNumber],[ShipDate],[CusNum],[CusCom],[DelTo],[GrandTotal],[DatePaid],[PAID],[Division])
                SELECT v.[PONumber],v.[InvNumber],v.[ShipDate],v.[CusNum],v.[CusCom],v.[DelTo],v.[GrandTotal],v.[DatePaid],v.[PAID],N''' + v.[Division] + N''' [Division]
                FROM [Stock].[dbo].[' + v.[TableName] + N'] v
                WHERE (ISNUMERIC(v.[DatePaid]) <> 0) AND 
                (v.[PAID] <> N''PAID'') AND 
                (v.[CusNum] IN (SELECT [CusNum] FROM #oDutchmill GROUP BY [CusNum]));
                '
                FROM [Stock].[dbo].[AAllDivisionPayment] v
                WHERE v.[Division] NOT IN ('Division8', 'Division8 VAT', 'Division9','Division9 VAT', 'Division11', 'Division11 VAT')
                ORDER BY [v].[Division];

				SELECT @vQuery3 += N'
				INSERT INTO @oDetail ([InvNumber],[ShipDate],[CusNum],[DeltoId],[SupNum],[Amount],[Dis],[Vat],[Division])
				SELECT v.[InvNumber],CONVERT(DATE,v.ShipDate) ShipDate,v.[CusNum],v.[DeltoId],v.[SupNum],SUM(v.[Amount]) [Amount],SUM(v.[DiscountAmount]) [DiscountAmount],SUM(v.[VatAmount]) [VatAmount],N''' + v.[Division] + ''' [Division]
				FROM [Stock].[dbo].[' + v.[TableName] + '] v
				WHERE v.[InvNumber] IN (SELECT [InvNumber] FROM #oPayment WHERE ([Division] = N''' + v.[Division] + '''))
				GROUP BY v.[InvNumber],CONVERT(DATE,v.ShipDate),v.[CusNum],v.[DeltoId],v.[SupNum];
				'
				FROM [Stock].[dbo].[AAllMainDivision] v
				ORDER BY v.[Division];

                SET @oQuery = N'
                DECLARE @DateRequired AS DATE = N''{1:yyyy-MM-dd}'';
                DECLARE @oPayment AS  TABLE 
                (
	                [PONumber] [NVARCHAR](100) NULL,
	                [InvNumber] [DECIMAL](18, 0) NULL,
	                [ShipDate] [DATETIME] NULL,
	                [CusNum] [NVARCHAR](15) NULL,
	                [CusCom] [NVARCHAR](100) NULL,
	                [DelTo] [NVARCHAR](100) NULL,
	                [GrandTotal] [MONEY] NULL,
	                [DatePaid] [NVARCHAR](20) NULL,
	                [PAID] [NVARCHAR](13) NULL,
	                [Division] [NVARCHAR](25) NULL
                );
                DECLARE @oPaymentStatement AS  TABLE 
                (
	                [InvNumber] [DECIMAL](18, 0) NULL,
	                [CusNum] [NVARCHAR](15) NULL,
	                [Division] [NVARCHAR](25) NULL
                );
				DECLARE @oDetail AS  TABLE 
                (
	                [InvNumber] [DECIMAL](18, 0) NULL,
					[ShipDate] [DATE] NULL,
	                [CusNum] [NVARCHAR](15) NULL,
					[DeltoId] [DECIMAL] (18,0) NULL,
					[SupNum] [NVARCHAR](8) NULL,
					[Amount] [MONEY] NULL,
					[Dis] [MONEY] NULL,
					[Vat] [MONEY] NULL,
	                [Division] [NVARCHAR](25) NULL
                );
                SELECT [CusNum],[CusName]
                INTO #oDutchmill
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                WHERE (CONVERT(DATE,[DateRequired]) = @DateRequired)
                GROUP BY [CusNum],[CusName]
                ORDER BY [CusName];

                ' + @oQuery + @NewLineChar + @vQuery2 + N'

				WITH v AS (
					SELECT v.[ID],v.[SalesmanCode],v.[SalesmanName],v.[SalesmanNumber],v.[MobileNumber],v.[PositionName],v.[PositionValue],v.[CallcardRule],v.[EmployeeNumber],v.[ManagerId]
					,o.[DeltoID],o.[NSMID],o.[WeekDay],o.[Status]
					,x.[SupNum],x.[SupName]
					FROM [{0}].[dbo].[TblSetSalesmanToSalesManager] v
					INNER JOIN [{0}].[dbo].[TblSetSaleManagerToSupplier] x ON (x.[SaleManagerID] = v.[ManagerId])
					INNER JOIN [{0}].[dbo].[TblSetDailyVisitCustomerForSalesman] o  ON (o.SalesmanNumber = v.SalesmanNumber)
				)
				SELECT v.*
				INTO #oDailyVisit
				FROM v
				ORDER BY v.SalesmanName;

                WITH o AS (
	                SELECT o.[PONumber],o.[InvNumber],o.[ShipDate],o.[CusNum],o.[CusCom],o.[DelTo],SUM(o.[GrandTotal]) [GrandTotal],o.[Division]
	                FROM @oPayment o
	                WHERE o.[InvNumber] NOT IN (SELECT v.[InvNumber] FROM @oPaymentStatement v WHERE (o.[Division] = v.[Division]) AND (o.[CusNum] = v.[CusNum]) AND (o.[InvNumber] = v.[InvNumber]))
	                GROUP BY o.[PONumber],o.[InvNumber],o.[ShipDate],o.[CusNum],o.[CusCom],o.[DelTo],o.[Division]
                )
                SELECT o.PONumber,o.InvNumber,o.ShipDate,o.CusNum,o.CusCom,o.DelTo,o.GrandTotal,o.Division
                INTO #oPayment
                FROM o;

				' + @NewLineChar + @vQuery3 + N'

                DECLARE @oDateFrom AS DATE = DATEADD(MONTH,-3,CONVERT(DATE,GETDATE()));
				DECLARE @oDateTo AS DATE = DATEADD(MONTH,0,CONVERT(DATE,GETDATE()));
                SELECT o.CusNum,ISNULL(SUM(o.GrandTotal),0) [GrandTotal],CONVERT(MONEY,(ISNULL(SUM(o.GrandTotal),0)/3)) [Average]
				INTO #oAverage
				FROM #oPayment o
				WHERE CONVERT(DATE,o.ShipDate) BETWEEN @oDateFrom AND @oDateTo
                GROUP BY o.CusNum;

				SELECT o.CusNum,ISNULL(SUM(o.GrandTotal),0) [GrandTotal],DATEDIFF(DAY,MIN(o.[ShipDate]),GETDATE()) [Overday]
				INTO #oGrandTotal
				FROM #oPayment o
                GROUP BY o.CusNum;

				SELECT o.CusNum,o.CusCom,x.[GrandTotal],x.[Overday]
				,v.InvNumber,v.ShipDate,v.DeltoId,o.Delto,v.SupNum,v.Amount,v.Dis,v.Vat,v.Division
				,DATEDIFF(DAY,v.ShipDate,GETDATE()) [Period]
				INTO #oFinalPayment
                FROM #oPayment o
				INNER JOIN  @oDetail v ON ((v.InvNumber = o.InvNumber) AND (v.CusNum = o.CusNum) AND (v.Division = o.Division))
				INNER JOIN #oGrandTotal x ON (o.CusNum = x.CusNum) 
                GROUP BY o.CusNum,o.CusCom,x.[GrandTotal],x.[Overday]
				,v.InvNumber,v.ShipDate,v.DeltoId,o.Delto,v.SupNum,v.Amount,v.Dis,v.Vat,v.Division
				,DATEDIFF(DAY,v.ShipDate,GETDATE());
								
                SELECT DISTINCT o.CusNum,o.CusCom,o.[GrandTotal],o.[Overday]
                ,ISNULL(v.[CreditLimitAllow],0) [CreditLimitAllow],ISNULL(CONVERT(DECIMAL,v.[MaxMonthAllow]),0) [MaxMonthAllow]
                ,x.[Average]
				,o.InvNumber,o.ShipDate,o.DeltoId,o.Delto,o.SupNum,i.SupName,o.Period,o.Amount,o.Dis,o.Vat,o.Division
				--,s.[SalesmanCode],s.[SalesmanName],s.[SalesmanNumber]
                ,ISNULL(s.[SalesmanCode],N'''') [SalesmanCode],ISNULL(s.[SalesmanName],N'''') [SalesmanName],ISNULL(s.[SalesmanNumber],N'''') [SalesmanNumber]
                FROM #oFinalPayment o
				INNER JOIN #oAverage x ON (o.CusNum = x.CusNum) 
                INNER JOIN [Stock].[dbo].[TPRCustomer] v ON (o.CusNum = v.CusNum)
                INNER JOIN [{0}].[dbo].[TblSuppliers] i ON (o.SupNum = i.SupId)
				LEFT OUTER JOIN #oDailyVisit s ON (o.SupNum = s.SupNum) AND (s.DeltoId = o.DeltoId)
				WHERE ((o.[GrandTotal] >= ISNULL(v.[CreditLimitAllow],0)) OR (ISNULL(o.[Overday],0) >= ISNULL(CONVERT(DECIMAL,v.[MaxMonthAllow]),0)))
                ORDER BY o.[Overday] DESC,o.InvNumber,o.[GrandTotal] DESC,o.CusCom,ISNULL(s.[SalesmanName],N'''');
				
                DROP TABLE #oDutchmill;
                DROP TABLE #oFinalPayment;
				DROP TABLE #oPayment;
                DROP TABLE #oAverage;
				DROP TABLE #oDailyVisit;
				DROP TABLE #oGrandTotal;
                ';
                EXEC (@oQuery);            
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, RequiredDate)
        Dim oPaymentlists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        Dim vreport As DeliveryTakeOrder.xOverCreditNOverTerm
        Dim vadapter As OleDbDataAdapter
        Dim vds As DataSet
        Dim vtool As ReportPrintTool
        Application.DoEvents()
        If Not (oPaymentlists Is Nothing) Then
            If oPaymentlists.Rows.Count > 0 Then
                'Dim vfrm As New FrmOverCreditAmountOrCreditTerm With {.RequiredDate = RequiredDate}
                'vfrm.DgvShow.DataSource = oPaymentlists
                'vfrm.DgvShow.Refresh()
                'vfrm.DgvShow.Visible = True
                'vfrm.ShowDialog()

                vreport = New DeliveryTakeOrder.xOverCreditNOverTerm
                vadapter = New OleDbDataAdapter
                vds = New DataSet
                vtool = New ReportPrintTool(vreport)
                vreport.Parameters("companyname").Value = String.Format("{0}{1}{2}", Initialized.R_CompanyKhmerName, vbCrLf, Initialized.R_CompanyName)
                vreport.Parameters("companyaddress").Value = String.Format("{0}{1}{2}{1}Tel:{3}", Initialized.R_CompanyKhmAddress.Replace(vbCrLf, "").Trim(), vbCrLf, Initialized.R_CompanyAddress.Replace(vbCrLf, "").Trim(), Initialized.R_CompanyTelephone)
                vreport.DataSource = oPaymentlists
                vreport.DataAdapter = vadapter
                vreport.DataMember = "OverCreditOverTerm"
                vreport.RequestParameters = False
                vtool.AutoShowParametersPanel = False
                vtool.PrinterSettings.Copies = 1
                vtool.ShowRibbonPreviewDialog()
            End If
        End If

        query = <SQL><![CDATA[
                DECLARE @DateRequired AS DATE = N'{1:yyyy-MM-dd}';
                WITH o AS (
	                SELECT v.ProID,v.ProNumY,v.ProNumYP,v.ProNumYC,v.ProName,v.ProPacksize,v.ProQtyPCase,v.ProQtyPPack,v.ProCat
	                FROM [Stock].[dbo].[TPRProducts] v
	                UNION ALL
                    SELECT v.ProID,v.ProNumY,v.ProNumYP,v.ProNumYC,v.ProName,v.ProPacksize,v.ProQtyPCase,v.ProQtyPPack,v.ProCat
	                FROM [Stock].[dbo].[TPRProductsDeactivated] v
	                UNION ALL
                    SELECT v.ProID,o.OldProNumy AS ProNumY,v.ProNumYP,v.ProNumYC,v.ProName,v.ProPacksize,v.ProQtyPCase,v.ProQtyPPack,v.ProCat
	                FROM [Stock].[dbo].[TPRProducts] v 
	                INNER JOIN [Stock].[dbo].[TPRProductsOldCode] o ON o.ProId = v.ProID
	                UNION ALL
                    SELECT v.ProID,o.OldProNumy AS ProNumY,v.ProNumYP,v.ProNumYC,v.ProName,v.ProPacksize,v.ProQtyPCase,v.ProQtyPPack,v.ProCat
	                FROM [Stock].[dbo].[TPRProductsDeactivated] v 
	                INNER JOIN [Stock].[dbo].[TPRProductsOldCode] o ON o.ProId = v.ProID
                )
                SELECT o.*
                INTO #vList
                FROM o;

                UPDATE v
                SET v.[ProName] = o.ProName
                ,v.[Size] = o.ProPacksize
                ,v.[QtyPCase] = CONVERT(INT,o.ProQtyPCase)
                ,v.[QtyPPack] = CONVERT(INT,o.ProQtyPPack)
                ,v.[Category] = o.ProCat
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] v
                INNER JOIN #vList o ON o.ProNumY = v.UnitNumber;

                UPDATE v
                SET v.ProName = o.ProName
                ,v.Size = o.ProPacksize
                ,v.QtyPCase = CONVERT(INT,o.ProQtyPCase)
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS v
                INNER JOIN #vList o ON o.ProNumY = v.Barcode;

                UPDATE v
                SET v.[ProName] = o.ProName
                ,v.[Size] = o.ProPacksize
                ,v.[QtyPerCase] = CONVERT(INT,o.ProQtyPCase)
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] v
                INNER JOIN #vList o ON o.ProNumY = v.Barcode;

                DROP TABLE #vList;

                WITH v as (
                    SELECT [Barcode],[ProName],[Category] AS [Remark],[Size],[QtyPCase],ISNULL([DelTo],'') AS [CusName],SUM(ISNULL([TotalPcsOrder],0)) AS [TotalPcsOrder],[DateRequired],[DelToId],[PromotionMachanic]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE DATEDIFF(DAY,[DateRequired],@DateRequired) = 0
                    GROUP BY [Barcode],[ProName],[Category],[Size],[QtyPCase],ISNULL([DelTo],''),[DateRequired],[DelToId],[PromotionMachanic]
                    UNION ALL
                    SELECT x.[Barcode],x.[ProName],x.[Category] AS [Remark],x.[Size],x.[QtyPCase],ISNULL(x.[DelTo],'') AS [CusName],NULL AS [TotalPcsOrder],[DateRequired],x.[DelToId],x.[PromotionMachanic]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] as x
                    WHERE DATEDIFF(DAY,x.[DateRequired],@DateRequired) = 0
                ),

				w AS (
                SELECT v.[Barcode],v.[ProName],v.[Remark],v.[Size],v.[QtyPCase],v.[CusName],ISNULL(SUM(v.[TotalPcsOrder]),0) AS [TotalPcsOrder]
                ,CASE WHEN ((DATENAME(WEEKDAY,v.[DateRequired]) = N'Monday' AND x.[Monday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Tuesday' AND x.[Tuesday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Wednesday' AND x.[Wednesday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Thursday' AND x.[Thursday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Friday' AND x.[Friday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Saturday' AND x.[Saturday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Sunday' AND x.[Sunday] = 1 AND v.[DelToId] = x.[DelToId]))
                THEN 1 ELSE 0 END AS [Missing],v.[DelToId],
                REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(v.[PromotionMachanic],N'1,',N''),N'1 ,',N''),N'1.',N''),N'1 .',N''),N'2,',N''),N'2 ,',N''),N'2.',N''),N'2 .',N''),N'3,',N''),N'3 ,',N''),N'3.',N''),N'3 .',N''),N'4,',N''),N'4 ,',N''),N'4.',N''),N'4 .',N''),N'5,',N''),N'5 ,',N''),N'5.',N''),N'5 .',N''),N'6,',N''),N'6 ,',N''),N'6.',N''),N'6 .',N''),N'7,',N''),N'7 ,',N''),N'7.',N''),N'7 .',N''),N'8,',N''),N'8 ,',N''),N'8.',N''),N'8 .',N''),N'9,',N''),N'9 ,',N''),N'9.',N''),N'9 .',N''),N'0,',N''),N'0 ,',N''),N'0.',N''),N'0 .',N'') [PromotionMachanic]
                FROM v
                LEFT OUTER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting] AS x ON v.[DelToId] = x.[DelToId]
                GROUP BY v.[Barcode],v.[ProName],v.[Remark],v.[Size],v.[QtyPCase],v.[CusName]
                ,CASE WHEN ((DATENAME(WEEKDAY,v.[DateRequired]) = N'Monday' AND x.[Monday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Tuesday' AND x.[Tuesday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Wednesday' AND x.[Wednesday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Thursday' AND x.[Thursday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Friday' AND x.[Friday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Saturday' AND x.[Saturday] = 1 AND v.[DelToId] = x.[DelToId])
                OR (DATENAME(WEEKDAY,v.[DateRequired]) = N'Sunday' AND x.[Sunday] = 1 AND v.[DelToId] = x.[DelToId]))
                THEN 1 ELSE 0 END,v.[DelToId],
                REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(v.[PromotionMachanic],N'1,',N''),N'1 ,',N''),N'1.',N''),N'1 .',N''),N'2,',N''),N'2 ,',N''),N'2.',N''),N'2 .',N''),N'3,',N''),N'3 ,',N''),N'3.',N''),N'3 .',N''),N'4,',N''),N'4 ,',N''),N'4.',N''),N'4 .',N''),N'5,',N''),N'5 ,',N''),N'5.',N''),N'5 .',N''),N'6,',N''),N'6 ,',N''),N'6.',N''),N'6 .',N''),N'7,',N''),N'7 ,',N''),N'7.',N''),N'7 .',N''),N'8,',N''),N'8 ,',N''),N'8.',N''),N'8 .',N''),N'9,',N''),N'9 ,',N''),N'9.',N''),N'9 .',N''),N'0,',N''),N'0 ,',N''),N'0.',N''),N'0 .',N''))

				SELECT w.[Barcode],w.[ProName],w.[Remark],REPLACE(w.[Size],' ','') AS [Size],w.[QtyPCase],w.[CusName],case when isnull(w.[TotalPcsOrder],0) = 0 then null else w.[TotalPcsOrder] end as [TotalPcsOrder],
                CASE WHEN (w.[Missing] = 1 AND ISNULL(w.[TotalPcsOrder],0) <> 0) OR ISNULL(w.[DelToId],0) = 0 THEN 1 ELSE 0 END AS [Missing],
                ISNULL(v.[PiecesPerTray],1) AS [PiecesPerTray],w.[DeltoId],w.[PromotionMachanic]
				INTO #vLists
				FROM w
                LEFT OUTER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS v ON v.Barcode = w.Barcode
                GROUP BY w.[Barcode],w.[ProName],w.[Remark],REPLACE(w.[Size],' ',''),w.[QtyPCase],w.[CusName],case when isnull(w.[TotalPcsOrder],0) = 0 then null else w.[TotalPcsOrder] end,
                CASE WHEN (w.[Missing] = 1 AND ISNULL(w.[TotalPcsOrder],0) <> 0) OR ISNULL(w.[DelToId],0) = 0 THEN 1 ELSE 0 END,
                ISNULL(v.[PiecesPerTray],1),w.[DeltoId],w.[PromotionMachanic]
                ORDER BY w.[Remark],w.[ProName];
				SELECT v.Barcode,v.ProName,v.Remark,v.Size,v.QtyPCase,v.PiecesPerTray,SUM(v.TotalPcsOrder) AS [TotalOrder],((CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) - SUM(v.TotalPcsOrder)) AS [TotalExtraLeft],(CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) AS [TotalOrderToThailand],ROUND(((CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) / ISNULL(v.PiecesPerTray,1)),2) AS [TotalTray],v.[PromotionMachanic]
				INTO #v
				FROM #vLists AS v
				GROUP BY v.Barcode,v.ProName,v.Remark,v.Size,v.QtyPCase,v.PiecesPerTray,v.[PromotionMachanic]
				ORDER BY v.Size,v.ProName;

				SELECT v.*
				FROM #v AS v
				ORDER BY v.Remark,v.Size,v.Barcode,v.ProName;
				DROP TABLE #vLists;
				DROP TABLE #v;
            ]]></SQL>
        query = String.Format(query, DatabaseName, RequiredDate)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        'Dim Report As New ReportViewer
        'Dim Path As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\PODutchmill" & String.Format("{0:yyyyMMddhhmmss}", Now()) & ".xls" 'Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\PODutchmill" & String.Format("{0:yyyyMMddhhmmss}", Now()) & ".xls"
        'Dim picList As String() = Directory.GetFiles(My.Computer.FileSystem.SpecialDirectories.Temp, "*.xls") 'Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "*.xls")
        'For Each f As String In picList
        '    Try
        '        File.Delete(f)
        '    Catch ex As Exception

        '    End Try
        'Next
        Dim oTodate As Date = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vAdapter1 As New OleDbDataAdapter
        Dim vReport1 As New XtraPODutchmills
        Dim vTool1 As ReportPrintTool = New ReportPrintTool(vReport1)
        vReport1.Parameters("companyname").Value = String.Format("{0}{1}{2}", Initialized.R_CompanyKhmerName, vbCrLf, Initialized.R_CompanyName)
        vReport1.Parameters("companyaddress").Value = String.Format("{0}{1}{2}{1}Tel:{3}", Initialized.R_CompanyKhmAddress.Replace(vbCrLf, "").Trim(), vbCrLf, Initialized.R_CompanyAddress.Replace(vbCrLf, "").Trim(), Initialized.R_CompanyTelephone)
        vReport1.Parameters("planningorder").Value = String.Format("DUTCHMILL ( {0:dddd} )", RequiredDate).ToUpper()
        vReport1.Parameters("planningdate").Value = oTodate
        vReport1.Parameters("shipmentdate").Value = RequiredDate
        vReport1.DataSource = lists
        vReport1.DataAdapter = vAdapter1
        vReport1.DataMember = "XrPODutchmill"
        vReport1.RequestParameters = False
        vTool1.AutoShowParametersPanel = False
        vTool1.PrinterSettings.Copies = 1
        vTool1.ShowRibbonPreviewDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub IsFileOpen(ByVal file As FileInfo)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception

            If TypeOf ex Is IOException AndAlso IsFileLocked(ex) Then
                ' do something here, either close the file if you have a handle, show a msgbox, retry  or as a last resort terminate the process - which could cause corruption and lose data
            End If
        End Try
    End Sub

    Private Shared Function IsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

    Private Sub FrmProcessTakeOrder_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private Sub DgvShow_MouseDown(sender As Object, e As MouseEventArgs) Handles DgvShow.MouseDown
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Right Then Me.Popmenu.Show(DgvShow, New System.Drawing.Point(e.X, e.Y))
    End Sub

    Private Sub MnuChangeCustomer_Click(sender As Object, e As EventArgs) Handles MnuChangeCustomer.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            Dim vDeltoId As Decimal = CDec(IIf(IsDBNull(.Cells("DelToId").Value) = True, 0, .Cells("DelToId").Value))
            Dim vCusNum As String = Trim(IIf(IsDBNull(.Cells("CusNum").Value) = True, "", .Cells("CusNum").Value))
            Dim vCusName As String = Trim(IIf(IsDBNull(.Cells("CusName").Value) = True, "", .Cells("CusName").Value))
            Dim Frm As New FrmProcessTakeOrderCustomer With {.vTakeOrder = vTakeOrder, .vCusNum = vCusNum, .vCusName = vCusName, .vDeltoId = vDeltoId}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuChangeDelto_Click(sender As Object, e As EventArgs) Handles MnuChangeDelto.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            Dim vCusNum As String = Trim(IIf(IsDBNull(.Cells("CusNum").Value) = True, "", .Cells("CusNum").Value))
            Dim vDeltoId As String = Trim(IIf(IsDBNull(.Cells("DelToId").Value) = True, "", .Cells("DelToId").Value))
            Dim vDeltoName As String = Trim(IIf(IsDBNull(.Cells("DelTo").Value) = True, "", .Cells("DelTo").Value))
            Dim Frm As New FrmProcessTakeOrderDelto With {.vTakeOrder = vTakeOrder, .vDeltoId = vDeltoId, .vDeltoName = vDeltoName, .vCusNum = vCusNum}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuChangeQtyOrder_Click(sender As Object, e As EventArgs) Handles MnuChangeQtyOrder.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            Dim vId As Decimal = CDec(IIf(IsDBNull(.Cells("Id").Value) = True, 0, .Cells("Id").Value))
            Dim vBarcode As String = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
            Dim vProName As String = Trim(IIf(IsDBNull(.Cells("ProName").Value) = True, "", .Cells("ProName").Value))
            Dim vSize As String = Trim(IIf(IsDBNull(.Cells("Size").Value) = True, "", .Cells("Size").Value))
            Dim vQtyPerCase As Integer = CInt(IIf(IsDBNull(.Cells("QtyPCase").Value) = True, 1, .Cells("QtyPCase").Value))
            Dim vPcsOrder As Decimal = CDec(IIf(IsDBNull(.Cells("PcsOrder").Value) = True, 0, .Cells("PcsOrder").Value))
            Dim vPackOrder As Decimal = CDec(IIf(IsDBNull(.Cells("PackOrder").Value) = True, 0, .Cells("PackOrder").Value))
            Dim vCTNOrder As Decimal = CDec(IIf(IsDBNull(.Cells("CTNOrder").Value) = True, 0, .Cells("CTNOrder").Value))
            Dim Frm As New FrmProcessTakeOrderQtyOrder With {.vTakeOrder = vTakeOrder, .vBarcode = vBarcode, .vProName = vProName, .vSize = vSize, .vQtyPerCase = vQtyPerCase, .vPcsOrder = vPcsOrder, .vPackOrder = vPackOrder, .vCTNOrder = vCTNOrder, .vId = vId}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuDeleteTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuDeleteTakeOrder.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vId As Decimal = CDec(IIf(IsDBNull(.Cells("Id").Value) = True, 0, .Cells("Id").Value))
            Dim vBarcode As String = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
            If MessageBox.Show("Are you sure, you want to delete the Barcode <" & vBarcode & ">?(Yes/No)", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                query = _
                <SQL>
                    <![CDATA[
                        DECLARE @vId AS DECIMAL(18,0) = {1};
                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_Deleted]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeletedDate])
                        SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                        WHERE [Id] = @vId;

                        DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                        WHERE [Id] = @vId;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vId)
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                MessageBox.Show("Deleting have been completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TimerTakeOrderLoading.Enabled = True
                TimerDisplayLoading.Enabled = True
            Catch ex As SqlException
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End With
    End Sub

    Private Sub DeltoLoading_Tick(sender As Object, e As EventArgs) Handles DeltoLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DeltoLoading.Enabled = False
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Dim vRequiredDate As Date = Todate
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then
            vRequiredDate = Todate
        Else
            If CmbRequiredDate.Text.Trim().Equals("") = True Then
                vRequiredDate = Todate
            Else
                vRequiredDate = CmbRequiredDate.SelectedValue
            End If
        End If

        Dim CusNum As String = ""
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then
            CusNum = ""
        Else
            If CmbCustomer.Text.Trim().Equals("") = True Then
                CusNum = ""
            Else
                CusNum = CmbCustomer.SelectedValue
            End If
        End If

        query = <SQL>
                    <![CDATA[
                        UPDATE v
                        SET v.[DelTo] = o.[DelTo]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                        INNER JOIN [Stock].[dbo].[TPRDelto] AS o ON o.DefId = v.[DeltoId];

                        UPDATE v
                        SET v.[DelTo] = o.[DelTo]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] AS v
                        INNER JOIN [Stock].[dbo].[TPRDelto] AS o ON o.DefId = v.[DeltoId];

                        DECLARE @vRequiredDate AS DATE = N'{1:yyyy-MM-dd}';
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{2}';
                        SELECT [DelToId],[DelTo]
                        FROM (
                            SELECT 1 AS [Index], [DelToId],[DelTo]
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                            WHERE (CONVERT(DATE,[DateRequired]) = @vRequiredDate) AND ([CusNum] = @vCusNum OR N'CUS00000' = @vCusNum)
                            GROUP BY [DelToId],[DelTo]
                        ) LISTS
                        ORDER BY [Index],[DelTo];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vRequiredDate, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, lists, "DelTo", "DelToId")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub RequiredDateLoading_Tick(sender As Object, e As EventArgs) Handles RequiredDateLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.RequiredDateLoading.Enabled = False
        Dim CusNum As String = ""
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then
            CusNum = ""
        Else
            If CmbCustomer.Text.Trim().Equals("") = True Then
                CusNum = ""
            Else
                CusNum = CmbCustomer.SelectedValue
            End If
        End If
        Dim vDeltoId As Decimal = 0
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then
            vDeltoId = 0
        Else
            If CmbDelto.Text.Trim().Equals("") = True Then
                vDeltoId = 0
            Else
                vDeltoId = CmbDelto.SelectedValue
            End If
        End If
        query = _
        <SQL>
            <![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                SELECT [DateRequired]
                FROM (
                    SELECT [DateRequired]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    --WHERE ([CusNum] = @vCusNum OR N'CUS00000' = @vCusNum) AND ([DelToId] = @vDeltoId OR 0 = @vDeltoId)
                    GROUP BY [DateRequired]
                ) LISTS
                ORDER BY [DateRequired];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum, vDeltoId)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbRequiredDate, lists, "DateRequired", "DateRequired")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbDelto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDelto.SelectedIndexChanged
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then Exit Sub
        If CmbDelto.Text.Trim().Equals("") = True Then Exit Sub
        'RequiredDateLoading.Enabled = True
        'TimerTakeOrderLoading.Enabled = True
        TimerDisplayLoading.Enabled = True
    End Sub

    Private Sub CmbRequiredDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbRequiredDate.SelectedIndexChanged
        If TypeOf CmbRequiredDate.SelectedValue Is DataRowView Or CmbRequiredDate.SelectedValue Is Nothing Then Exit Sub
        If CmbRequiredDate.Text.Trim().Equals("") = True Then Exit Sub
        TimerCustomerLoading.Enabled = True
        TimerDisplayLoading.Enabled = True
    End Sub

    Private Sub DgvShow_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvShow.CellContentClick
        Dim vSelected As Integer = 0
        Dim vCusVAT As String = ""
        Dim vCheckVAT As Boolean = False
        Dim vSpecial As Boolean = False
        If e.ColumnIndex = 0 Then
            DgvShow.Rows(e.RowIndex).Cells("Tick").Value = Not DgvShow.Rows(e.RowIndex).Cells("Tick").Value
            query = _
            <SQL>
                <![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                    SELECT [Id],[CusNum],[CusName],[CreatedDate]
                    FROM [{0}].[dbo].[TblCustomerSetting_SpecialInvoices]
                    WHERE [CusNum] = @CusNum;
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, DgvShow.Rows(e.RowIndex).Cells("CusNum").Value)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then vSpecial = True
            End If
            For Each row As DataGridViewRow In DgvShow.Rows
                If CBool(row.Cells("Tick").Value) = True Then
                    If vSpecial = True Then GoTo Err_Skip_SpecialInvoice
                    If vCheckVAT = False Then
                        vCusVAT = ""
                        query = _
                        <SQL>
                            <![CDATA[
                                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                                SELECT v.CusVat
                                FROM Stock.dbo.TPRCustomer AS v
                                WHERE v.CusNum = @vCusNum;
                            ]]>
                        </SQL>
                        query = String.Format(query, DatabaseName, row.Cells("CusNum").Value)
                        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                        If Not (lists Is Nothing) Then
                            If lists.Rows.Count > 0 Then
                                vCusVAT = Trim(IIf(IsDBNull(lists.Rows(0).Item("CusVat")) = True, "", lists.Rows(0).Item("CusVat")))
                            End If
                        End If
                        vCheckVAT = True
                    End If
                    If vCusVAT.Trim.Equals("") = True Or vCusVAT.Trim.Equals("0") = True Then
                        If vSelected >= 21 Then
                            MessageBox.Show("The Invoice allow 21 rows only!", "21 Rows", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            DgvShow.Rows(e.RowIndex).Cells("Tick").Value = False
                            Exit For
                        End If
                    Else
                        If vSelected >= 12 Then
                            MessageBox.Show("The Invoice allow 12 rows only!", "12 Rows", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            DgvShow.Rows(e.RowIndex).Cells("Tick").Value = False
                            Exit For
                        End If
                    End If
Err_Skip_SpecialInvoice:
                    vSelected += 1
                End If
            Next
        End If
        LblSeletedRow.Text = String.Format("||  Selected Row : {0}", vSelected)
    End Sub

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            e.IsInputKey = True
            MnuDeleteTakeOrder_Click(MnuDeleteTakeOrder, New System.EventArgs())
        End If
    End Sub

    Private Sub BtnPreviewNEditTakeOrder_Click(sender As Object, e As EventArgs) Handles BtnPreviewNEditTakeOrder.Click
        Dim vF1 As New FrmPODutchmillDate_
        vF1.ChkLock.Visible = False
        vF1.BtnExportToExcel.Text = "&Preview"
        vF1.BtnExportToExcel.Image = My.Resources.Search16
        If vF1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim iPlanningOrder As String = vF1.iPlanningOrder
        Dim vRequiredDate As Date = vF1.iRequiredDate
        Dim vF2 As New FrmProcessTakeOrderPreviewNEdit With {.WindowState = FormWindowState.Maximized, .vRequiredDate = vRequiredDate, .iPlanningOrder = iPlanningOrder}
        vF2.ShowDialog(MDI)
    End Sub
End Class