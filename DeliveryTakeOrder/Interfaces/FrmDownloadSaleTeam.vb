Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDownloadSaleTeam
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

    Private Sub FrmDownloadSaleTeam_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmDownloadSaleTeam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        TimerCustomerLoading.Enabled = True
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TimerCustomerLoading_Tick(sender As Object, e As EventArgs) Handles TimerCustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCustomerLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                SELECT [CusNum],[CusName],[Customer]
                FROM (
                    --SELECT 0 AS [Index], N'CUS00000' AS [CusNum],N'All Customers' AS [CusName],N'All Customers' AS [Customer]
                    --UNION ALL
                    SELECT 1 AS [Index], [CusNum],[CusName],ISNULL([CusNum],'') + SPACE(3) + ISNULL([CusName],'') AS [Customer]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    GROUP BY [CusNum],[CusName],ISNULL([CusNum],'') + SPACE(3) + ISNULL([CusName],'')
                ) LISTS
                ORDER BY [Index],[CusName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
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
        If CusNum.Equals("") = False Then
            query = _
            <SQL>
                <![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                    SELECT [TakeOrderNumber]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    WHERE ([CusNum] = @CusNum OR N'CUS00000' = @CusNum)
                    GROUP BY [TakeOrderNumber]
                    ORDER BY [TakeOrderNumber];
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, CusNum)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            DataSources(CmbTakeOrderNo, lists, "TakeOrderNumber", "TakeOrderNumber")
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCustomer.SelectedIndexChanged
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then Exit Sub
        If CmbCustomer.Text.Trim().Equals("") = True Then Exit Sub
        Initialized.R_AllUnpaid = True
        If CmbCustomer.SelectedValue.Equals("CUS00000") = True Then
            Dim Frm As New FrmPODutchmillSelected
            If Frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        End If
        TimerTakeOrderLoading.Enabled = True
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
        query = _
        <SQL>
            <![CDATA[
                DECLARE @TakeOrderNo AS DECIMAL(18,0) = {1};
                DECLARE @IsAllProcess AS BIT = {2};
                DECLARE @DateTo AS DATE = N'{3:yyyy-MM-dd}';
                DECLARE @DateFrom AS DATE = N'{4:yyyy-MM-dd}';
                
                IF (@IsAllProcess = 0)
                BEGIN
                    SELECT v.[Id],v.[TakeOrderNumber],v.[PONumber],v.[CusNum],v.[CusName],v.[DelToId],v.[DelTo],v.[DateOrd],v.[DateRequired],v.[UnitNumber],v.[Barcode],v.[ProName],v.[Size],v.[QtyPCase],v.[QtyPPack],v.[Category],v.[PcsFree],v.[PcsOrder],v.[PackOrder],v.[CTNOrder],v.[TotalPcsOrder],v.[LogInName],v.[PromotionMachanic],v.[ItemDiscount],v.[Remark],v.[Saleman],v.[CreatedDate]
                    FROM (
	                    SELECT [Id],[CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate]
	                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
	                    WHERE ([TakeOrderNumber] = @TakeOrderNo OR 0 = @TakeOrderNo)
                    ) v
                    ORDER BY [CusName],[TakeOrderNumber];
                END
                ELSE
                BEGIN
                    SELECT v.[Id],v.[TakeOrderNumber],v.[PONumber],v.[CusNum],v.[CusName],v.[DelToId],v.[DelTo],v.[DateOrd],v.[DateRequired],v.[UnitNumber],v.[Barcode],v.[ProName],v.[Size],v.[QtyPCase],v.[QtyPPack],v.[Category],v.[PcsFree],v.[PcsOrder],v.[PackOrder],v.[CTNOrder],v.[TotalPcsOrder],v.[LogInName],v.[PromotionMachanic],v.[ItemDiscount],v.[Remark],v.[Saleman],v.[CreatedDate]
                    FROM (
	                    SELECT [Id],[CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate]
	                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
	                    WHERE ([TakeOrderNumber] = @TakeOrderNo OR 0 = @TakeOrderNo)
	                    UNION ALL
	                    SELECT [Id],[CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate]
	                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam_Processed]
	                    WHERE ([TakeOrderNumber] = @TakeOrderNo OR 0 = @TakeOrderNo) AND (CONVERT(DATE,[DateOrd]) BETWEEN @DateTo AND @DateFrom)
                    ) v
                    ORDER BY [CusName],[TakeOrderNumber];
                END
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, TakeOrderNo, IIf(Initialized.R_AllUnpaid = True, 0, 1), Initialized.R_DateFrom, Initialized.R_DateTo)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnProcessTakeOrder_Click(sender As Object, e As EventArgs) Handles BtnDownloadTakeOrder.Click
        If CmbTakeOrderNo.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any Take Order Number first.", "Select Take Order Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbTakeOrderNo.Focus()
            Exit Sub
        ElseIf DgvShow.Rows.Count <= 0 Then
            MessageBox.Show("No record to process finish.", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim CusNum As String = Trim(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value) = True, "", DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value))
        If MessageBox.Show("Are you sure, you already to process this take order?(Yes/No)", "Confirm Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Initialized.R_MessageAlert = ""
        Initialized.R_DocumentNumber = ""
        Initialized.R_LineCode = ""
        Initialized.R_DeptCode = ""

        If FrmDeliveryTakeOrderMessage.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FrmDeliveryTakeOrderInfoAeon.ShowDialog()

        'Key
        Dim Key As Long = 0
        query = _
        <SQL>
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
            query = _
            <SQL>
                <![CDATA[
                    DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                    DECLARE @RelatedKey AS NVARCHAR(10) = N'{2}';

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    WHERE [TakeOrderNumber] = @TakeOrderNumber;

                    INSERT INTO [dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam_Processed]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[ProcessedDate],[Downloaded])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[ProcessedDate],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    WHERE [TakeOrderNumber] = @TakeOrderNumber;

                    DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    WHERE [TakeOrderNumber] = @TakeOrderNumber;
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, CmbTakeOrderNo.SelectedValue, RelatedKey)
            RCom.CommandText = query
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()
            MessageBox.Show("Downloading have been finished.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    End Sub

    Private Sub BtnExportToExcel_Click(sender As Object, e As EventArgs) Handles BtnExportToExcel.Click
        Dim Frm As New FrmPODutchmillDate
        If Frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim RequiredDate As Date = Frm.RequiredDate
        query = _
        <SQL>
            <![CDATA[
                DECLARE @DateRequired AS DATE = N'{1:yyyy-MM-dd}';
                WITH v as (
                    SELECT [Barcode],[ProName],[Category] AS [Remark],[Size],[QtyPCase],CASE WHEN LTRIM(RTRIM(ISNULL([CusName],''))) LIKE 'YN %' THEN 'Total Sale' ELSE ISNULL([DelTo],'') END AS [CusName],SUM(ISNULL([TotalPcsOrder],0)) AS [TotalPcsOrder],[DateRequired],[DelToId]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                    WHERE DATEDIFF(DAY,[DateRequired],@DateRequired) = 0
                    GROUP BY [Barcode],[ProName],[Category],[Size],[QtyPCase],CASE WHEN LTRIM(RTRIM(ISNULL([CusName],''))) LIKE 'YN %' THEN 'Total Sale' ELSE ISNULL([DelTo],'') END,[DateRequired],[DelToId]
                    UNION ALL
                    SELECT x.[Barcode],x.[ProName],x.[Category] AS [Remark],x.[Size],x.[QtyPCase],ISNULL(v.[DelTo],'') AS [CusName],NULL AS [TotalPcsOrder],NULL AS [DateRequired],v.[DelToId]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam] as x,[{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting] as v
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
                THEN 1 ELSE 0 END AS [Missing],x.[DelToId]
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
                THEN 1 ELSE 0 END,x.[DelToId])

				SELECT w.[Barcode],w.[ProName],w.[Remark],w.[Size],w.[QtyPCase],w.[CusName],case when isnull(w.[TotalPcsOrder],0) = 0 then null else w.[TotalPcsOrder] end as [TotalPcsOrder],
                CASE WHEN (w.[Missing] = 1 AND ISNULL(w.[TotalPcsOrder],0) <> 0) OR ISNULL(w.[DelToId],0) = 0 THEN 1 ELSE 0 END AS [Missing],
                ISNULL(v.[PiecesPerTray],1) AS [PiecesPerTray]
				FROM w
                LEFT OUTER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS v ON v.Barcode = w.Barcode
                ORDER BY w.[Remark],w.[ProName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, RequiredDate)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        Dim Report As New ReportViewer
        Dim Path As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\PODutchmill" & String.Format("{0:yyyyMMddhhmmss}", Now()) & ".xls" 'Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\PODutchmill" & String.Format("{0:yyyyMMddhhmmss}", Now()) & ".xls"
        Dim picList As String() = Directory.GetFiles(My.Computer.FileSystem.SpecialDirectories.Temp, "*.xls") 'Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "*.xls")
        For Each f As String In picList
            File.Delete(f)
        Next

        Report.LocalReport.ReportEmbeddedResource = "DeliveryTakeOrder.PODutchmill.rdlc"
        Report.LocalReport.DataSources.Add(New ReportDataSource("PODutchmill", lists))
        Export.Save(Report, Path, ConvertReport.Type_Converter.Excel, True)
    End Sub

    Private Sub FrmDownloadSaleTeam_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
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
            Dim vCusNum As String = Trim(IIf(IsDBNull(.Cells("CusNum").Value) = True, "", .Cells("CusNum").Value))
            Dim vCusName As String = Trim(IIf(IsDBNull(.Cells("CusName").Value) = True, "", .Cells("CusName").Value))
            Dim Frm As New FrmProcessTakeOrderCustomer With {.vTakeOrder = vTakeOrder, .vCusNum = vCusNum, .vCusName = vCusName}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuChangeDelto_Click(sender As Object, e As EventArgs) Handles MnuChangeDelto.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            Dim vDeltoId As String = Trim(IIf(IsDBNull(.Cells("DelToId").Value) = True, "", .Cells("DelToId").Value))
            Dim vDeltoName As String = Trim(IIf(IsDBNull(.Cells("DelTo").Value) = True, "", .Cells("DelTo").Value))
            Dim Frm As New FrmProcessTakeOrderDelto With {.vTakeOrder = vTakeOrder, .vDeltoId = vDeltoId, .vDeltoName = vDeltoName}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuChangeQtyOrder_Click(sender As Object, e As EventArgs) Handles MnuChangeQtyOrder.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            Dim vBarcode As String = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
            Dim vProName As String = Trim(IIf(IsDBNull(.Cells("ProName").Value) = True, "", .Cells("ProName").Value))
            Dim vSize As String = Trim(IIf(IsDBNull(.Cells("Size").Value) = True, "", .Cells("Size").Value))
            Dim vQtyPerCase As Integer = CInt(IIf(IsDBNull(.Cells("QtyPCase").Value) = True, 1, .Cells("QtyPCase").Value))
            Dim vPcsOrder As Decimal = CDec(IIf(IsDBNull(.Cells("PcsOrder").Value) = True, 0, .Cells("PcsOrder").Value))
            Dim vPackOrder As Decimal = CDec(IIf(IsDBNull(.Cells("PackOrder").Value) = True, 0, .Cells("PackOrder").Value))
            Dim vCTNOrder As Decimal = CDec(IIf(IsDBNull(.Cells("CTNOrder").Value) = True, 0, .Cells("CTNOrder").Value))
            Dim Frm As New FrmProcessTakeOrderQtyOrder With {.vTakeOrder = vTakeOrder, .vBarcode = vBarcode, .vProName = vProName, .vSize = vSize, .vQtyPerCase = vQtyPerCase, .vPcsOrder = vPcsOrder, .vPackOrder = vPackOrder, .vCTNOrder = vCTNOrder}
            If Frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            TimerDisplayLoading.Enabled = True
        End With
    End Sub

    Private Sub MnuDeleteTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuDeleteTakeOrder.Click
        Me.Popmenu.Close()
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vTakeOrder As Decimal = CDec(IIf(IsDBNull(.Cells("TakeOrderNumber").Value) = True, 0, .Cells("TakeOrderNumber").Value))
            If MessageBox.Show("Are you sure, you want to delete the take order <" & vTakeOrder & ">?(Yes/No)", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
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
                        DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam_Deleted]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeletedDate])
                        SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                        WHERE [TakeOrderNumber] = @TakeOrderNumber;

                        DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_SaleTeam]
                        WHERE [TakeOrderNumber] = @TakeOrderNumber;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vTakeOrder)
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
End Class