Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmDutchmillTakeOrder
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private MyData As New DatabaseFramework_MySQL
    Private Todate As Date
    Private Printer As New PrintToPrinter
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Private Report As LocalReport
    Private RParameter As ReportParameter
    Private DatabaseName As String
    Private query As String
    Private lists As DataTable
    Private vHostName As String = System.Net.Dns.GetHostName()
    Private vIPAddress As String = System.Net.Dns.GetHostByName(vHostName).AddressList(0).ToString()
    Private vCheckBox As CheckBox
    Public Property vDepartment As String

    Private Sub DrawCheckBoxInDataGridView()
        Dim rect As Rectangle = DgvShow.GetCellDisplayRectangle(0, -1, True)
        rect.Y = 3
        rect.X = rect.Location.X + (rect.Width / 10)
        vCheckBox = New CheckBox()
        With vCheckBox
            .BackColor = Color.Transparent
            .Visible = False
        End With
        vCheckBox.Name = "Checker"
        vCheckBox.Size = New Size(18, 18)
        vCheckBox.Location = rect.Location
        AddHandler vCheckBox.CheckedChanged, AddressOf vCheckBox_CheckedChanged
        DgvShow.Controls.Add(vCheckBox)
    End Sub

    Private vRefresh As Boolean
    Private Sub vCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If vRefresh = True Then
            vRefresh = False
            Exit Sub
        End If
        Dim headerBox As CheckBox = DirectCast(DgvShow.Controls.Find("Checker", True)(0), CheckBox)
        Dim vDefaulValue As Boolean = False
        For Each row As DataGridViewRow In DgvShow.Rows
            vDefaulValue = row.Cells("ManualRenew").Value
            'AddHandler DgvShow.CellContentClick, AddressOf DgvShow_CellContentClick
            DgvShow_CellContentClick(DgvShow, New System.Windows.Forms.DataGridViewCellEventArgs(0, row.Index))
            If vDefaulValue = False And CBool(row.Cells("ManualRenew").Value) = False Then
                vRefresh = True
                vCheckBox.Checked = False
                DgvShow.CurrentCell = DgvShow.Rows(row.Index).Cells("CusName")
                DgvShow.Rows(row.Index).Selected = True
                DgvShow.RowsDefaultCellStyle.SelectionBackColor = Color.FromName("Highlight")
                DgvShow.FirstDisplayedScrollingRowIndex = row.Index
                Exit For
            End If
            row.Cells("ManualRenew").Value = headerBox.Checked
            vRefresh = False
        Next
    End Sub

    Private Sub LoadingInitialized()
        Initialized.LoadingInitialized(Data, App)
        DatabaseName = String.Format("{0}{1}", Data.PrefixDatabase, Data.DatabaseName)
        Me.Text = String.Format("Dutchmill Planning Order ( {0} )", vDepartment.Trim())
        Me.LblTeam.Text = vDepartment.Trim().ToUpper()
        vDefaultIndex = 0
        vFilterNotRenew = 0
    End Sub

    Private Sub DataSources(ByVal ComboBoxName As ComboBox, ByVal DTable As DataTable, ByVal DisplayMember As String, ByVal ValueMember As String)
        ComboBoxName.DataSource = DTable
        ComboBoxName.DisplayMember = DisplayMember
        ComboBoxName.ValueMember = ValueMember
        ComboBoxName.SelectedIndex = -1
    End Sub

    Private Sub FrmDutchmillTakeOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmDutchmillTakeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        PlanningOrderLoading.Enabled = True
        BillToLoading.Enabled = True
        DeltoLoading.Enabled = True
        DrawCheckBoxInDataGridView()
    End Sub

    Private Sub FrmDutchmillTakeOrder_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private Sub BillToLoading_Tick(sender As Object, e As EventArgs) Handles BillToLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        BillToLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                SELECT [CusNum],[CusName]
                FROM [Stock].[dbo].[TPRCustomer]
                WHERE [Status] = N'Activate'
                GROUP BY [CusNum],[CusName]
                ORDER BY [CusName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbBillTo, lists, "CusName", "CusNum")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DeltoLoading_Tick(sender As Object, e As EventArgs) Handles DeltoLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DeltoLoading.Enabled = False
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
                        
                        SELECT [DefId] [Id],[DelTo]
                        FROM [Stock].[dbo].[TPRDelto]
                        GROUP BY [DefId],[DelTo]
                        ORDER BY [DelTo];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, lists, "DelTo", "Id")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ItemLoading_Tick(sender As Object, e As EventArgs) Handles ItemLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        ItemLoading.Enabled = False
        Dim vCusNum As String = ""
        If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
            vCusNum = ""
        Else
            If CmbBillTo.Text.Trim() = "" Then
                vCusNum = ""
            Else
                vCusNum = CmbBillTo.SelectedValue
            End If
        End If
        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT [Barcode],[ProName],[Display]
                FROM (
	                SELECT ISNULL([ProNumY],'') AS [Barcode],RTRIM(LTRIM(ISNULL([ProName],N''))) AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
                    WHERE ISNULL([ProNumY],N'') <> N'' 
                    AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                    AND ISNULL([ProNumY],N'') IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE ([CusNum] = @CusNum))
                    GROUP BY ISNULL([ProNumY],''),RTRIM(LTRIM(ISNULL([ProName],N''))),ISNULL([ProNumY],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYP],'') AS [Barcode],RTRIM(LTRIM(ISNULL([ProName],N''))) AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE ISNULL([ProNumYP],N'') <> N'' 
                    AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                    AND ISNULL([ProNumYP],N'') IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE ([CusNum] = @CusNum))
                    GROUP BY ISNULL([ProNumYP],''),RTRIM(LTRIM(ISNULL([ProName],N''))),ISNULL([ProNumYP],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYC],'') AS [Barcode],RTRIM(LTRIM(ISNULL([ProName],N''))) AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE ISNULL([ProNumYC],N'') <> N'' 
                    AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                    AND ISNULL([ProNumYC],N'') IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE ([CusNum] = @CusNum))
                    GROUP BY ISNULL([ProNumYC],''),RTRIM(LTRIM(ISNULL([ProName],N''))),ISNULL([ProNumYC],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],RTRIM(LTRIM(ISNULL([ProName],N''))) AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                    WHERE ISNULL([OldProNumy],N'') <> N'' 
                    AND LEFT(A.[Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                    AND ISNULL(B.[OldProNumy],N'') IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE ([CusNum] = @CusNum))
                    GROUP BY ISNULL([B].[OldProNumy],''),RTRIM(LTRIM(ISNULL([ProName],N''))),ISNULL([B].[OldProNumy],'') + SPACE(3) + RTRIM(LTRIM(ISNULL([ProName],N''))) + SPACE(3) + ISNULL([ProPacksize],'')                    
                ) Lists
                GROUP BY [Barcode],[ProName],[Display]
                ORDER BY [ProName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vCusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbProducts, lists, "Display", "Barcode")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbBillTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBillTo.SelectedIndexChanged
        If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then Exit Sub
        If CmbBillTo.Text.Trim().Equals("") = True Then Exit Sub
        Dim CusNum As String = ""
        If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
            CusNum = ""
        Else
            If CmbBillTo.Text.Trim() = "" Then
                CusNum = ""
            Else
                CusNum = CmbBillTo.SelectedValue
            End If
        End If
        REM Alert Customer Bad Payment
        PCustomerRemark.Visible = False
        Panel3.Enabled = True
        query = <SQL>
                    <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        SELECT [Id],[CusNum],[CusName],[Remark],[AlertDate],[BlockDate],[CreatedDate]
                        FROM [{0}].[dbo].[TblCustomerRemarkSetting]
                        WHERE ([CusNum] = @vCusNum) 
                        AND (([Status] = N'Both') OR ([Status] = N'Dutchmill'))
                        AND (CONVERT(DATE,[AlertDate]) <= CONVERT(DATE,GETDATE()));                
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        Dim oRemarkList As DataTable = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (oRemarkList Is Nothing) Then
            If oRemarkList.Rows.Count > 0 Then
                Dim vBlockDate As Date = CDate(IIf(DBNull.Value.Equals(oRemarkList.Rows(0).Item("BlockDate")) = True, Todate, oRemarkList.Rows(0).Item("BlockDate")))
                Dim vCustomerRemark As String = ""
                vCustomerRemark = Trim(IIf(IsDBNull(oRemarkList.Rows(0).Item("Remark")) = True, "", oRemarkList.Rows(0).Item("Remark")))
                vCustomerRemark &= IIf(IsDBNull(oRemarkList.Rows(0).Item("BlockDate")) = True, "", String.Format("{1}អតិថិជននេះនឹងប្លុកនៅថ្ងៃទី : {0:dd-MMM-yyyy}", CDate(oRemarkList.Rows(0).Item("BlockDate")), vbCrLf))
                TxtCustomerRemark.Text = String.Format("*សូមចំណាំ៖{1}{0}", vCustomerRemark, vbCrLf)
                PCustomerRemark.Visible = True
                Dim vExpiry As Decimal = DateDiff(DateInterval.Day, Todate.Date, vBlockDate.Date)
                If vExpiry <= 0 Then
                    Panel3.Enabled = False
                Else
                    Application.DoEvents()
                    Threading.Thread.Sleep(5000)
                    PCustomerRemark.Visible = False
                    TxtCustomerRemark.Text = ""
                End If
            End If
        End If
        ItemLoading.Enabled = True
        DisplayLoading.Enabled = True
    End Sub

    Private Sub PlanningOrderLoading_Tick(sender As Object, e As EventArgs) Handles PlanningOrderLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.PlanningOrderLoading.Enabled = False
        Me.query = <SQL>
                       <![CDATA[
                        WITH v AS (
	                        SELECT [PlanningOrder]
	                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
	                        GROUP BY [PlanningOrder]
                        )
                        SELECT v.[PlanningOrder]
                        FROM v
                        GROUP BY v.[PlanningOrder]
                        ORDER BY v.[PlanningOrder];           
                    ]]>
                   </SQL>
        query = String.Format(query, DatabaseName)
        Dim oLists As DataTable = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbPlanningOrder, oLists, "PlanningOrder", "PlanningOrder")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbPlanningOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPlanningOrder.SelectedIndexChanged
        If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then Exit Sub
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then Exit Sub
        If Not (vDisplayList Is Nothing) Then vDisplayList = Nothing
        vDisplayList = New DataTable
        With vDisplayList.Columns
            .Add("Renew", GetType(Boolean))
            .Add("NotAccept", GetType(Boolean))
            .Add("ChangeQty", GetType(Boolean))
            .Add("Id", GetType(Decimal))
            .Add("CusNum", GetType(String))
            .Add("CusName", GetType(String))
            .Add("DeltoId", GetType(Decimal))
            .Add("Delto", GetType(String))
            .Add("UnitNumber", GetType(String))
            .Add("Barcode", GetType(String))
            .Add("ProName", GetType(String))
            .Add("Size", GetType(String))
            .Add("QtyPerCase", GetType(Integer))
            .Add("PcsOrder", GetType(Decimal))
            .Add("CTNOrder", GetType(Decimal))
            .Add("TotalPcsOrder", GetType(Decimal))
            .Add("SupNum", GetType(String))
            .Add("SupName", GetType(String))
            .Add("Department", GetType(String))
            .Add("PlanningOrder", GetType(String))
            .Add("MachineName", GetType(String))
            .Add("IPAddress", GetType(String))
            .Add("CreatedDate", GetType(DateTime))
            .Add("VerifyDate", GetType(DateTime))
            .Add("RequiredDate", GetType(DateTime))
        End With
        DgvShow.DataSource = vDisplayList
        DgvShow.Refresh()
        'CmbBillTo.SelectedIndex = -1
        'CmbDelto.SelectedIndex = -1
        DisplayLoading.Enabled = True
    End Sub

    Private vDefaultIndex As Decimal
    Private vFilterNotRenew As Boolean
    Private vDisplayList As DataTable
    Private Sub DisplayLoading_Tick(sender As Object, e As EventArgs) Handles DisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DisplayLoading.Enabled = False
        If vDisplayList Is Nothing Then
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Dim vPlanning As String = ""
        If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
            vPlanning = ""
        Else
            If CmbPlanningOrder.Text.Trim() = "" Then
                vPlanning = ""
            Else
                vPlanning = CmbPlanningOrder.SelectedValue
            End If
        End If
        Dim vCusNum As String = ""
        If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
            vCusNum = ""
        Else
            If CmbBillTo.Text.Trim() = "" Then
                vCusNum = ""
            Else
                vCusNum = CmbBillTo.SelectedValue
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
        Dim vBarcode As String = CmbProducts.Text.Trim()
        If vBarcode.Length > 13 Then vBarcode = Trim(Mid(vBarcode, 1, 15))
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then
            vBarcode = vBarcode
        Else
            If CmbProducts.Text.Trim() = "" Then
                vBarcode = vBarcode
            Else
                vBarcode = CmbProducts.SelectedValue
            End If
        End If
        query = <SQL>
                    <![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                DECLARE @vBarcode AS NVARCHAR(MAX) = N'{5}';
                DECLARE @vFilterNotRenew AS BIT = {6};

                IF (@vFilterNotRenew = 0)
                BEGIN
                    SELECT [Renew],[NotAccept],[ChangeQty],[Id],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[RequiredDate]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([Department] = @vDepartment)
                    AND ([CusNum] = @vCusNum OR N'' = @vCusNum) 
                    AND ([DeltoId] = @vDeltoId OR 0 = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    --AND ([PlanningOrder] = @vPlanningOrder OR N'' = @vPlanningOrder)
                    --AND ([Barcode] LIKE @vBarcode + '%' OR N'' = @vBarcode)
                    ORDER BY [CusName],[Delto],[Size],[ProName];
                END
                ELSE
                BEGIN
                    SELECT [Renew],[NotAccept],[ChangeQty],[Id],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[RequiredDate]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([Department] = @vDepartment)
                    AND ([CusNum] = @vCusNum OR N'' = @vCusNum) 
                    AND ([DeltoId] = @vDeltoId OR 0 = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    --AND ([PlanningOrder] = @vPlanningOrder OR N'' = @vPlanningOrder)
                    --AND ([Barcode] LIKE @vBarcode + '%' OR N'' = @vBarcode)
                    ORDER BY [NotAccept],[Renew],[ChangeQty],[CusName],[Delto],[Size],[ProName];
                END
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vBarcode, IIf(vFilterNotRenew = True, 1, 0))
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        vDisplayList.Rows.Clear()
        Dim vO As DataRow = Nothing
        For Each vR As DataRow In lists.Rows
            vO = vDisplayList.NewRow
            vO("Renew") = CBool(IIf(IsDBNull(vR("Renew")) = True, 0, vR("Renew")))
            vO("NotAccept") = CBool(IIf(IsDBNull(vR("NotAccept")) = True, 0, vR("NotAccept")))
            vO("ChangeQty") = CBool(IIf(IsDBNull(vR("ChangeQty")) = True, 0, vR("ChangeQty")))
            vO("Id") = CDec(IIf(IsDBNull(vR("Id")) = True, 0, vR("Id")))
            vO("CusNum") = Trim(IIf(IsDBNull(vR("CusNum")) = True, "", vR("CusNum")))
            vO("CusName") = Trim(IIf(IsDBNull(vR("CusName")) = True, "", vR("CusName")))
            vO("DeltoId") = CDec(IIf(IsDBNull(vR("DeltoId")) = True, 0, vR("DeltoId")))
            vO("Delto") = Trim(IIf(IsDBNull(vR("Delto")) = True, "", vR("Delto")))
            vO("UnitNumber") = Trim(IIf(IsDBNull(vR("UnitNumber")) = True, "", vR("UnitNumber")))
            vO("Barcode") = Trim(IIf(IsDBNull(vR("Barcode")) = True, "", vR("Barcode")))
            vO("ProName") = Trim(IIf(IsDBNull(vR("ProName")) = True, "", vR("ProName")))
            vO("Size") = Trim(IIf(IsDBNull(vR("Size")) = True, "", vR("Size")))
            vO("QtyPerCase") = CInt(IIf(IsDBNull(vR("QtyPerCase")) = True, 1, vR("QtyPerCase")))
            vO("PcsOrder") = CDec(IIf(IsDBNull(vR("PcsOrder")) = True, 0, vR("PcsOrder")))
            vO("CTNOrder") = CDec(IIf(IsDBNull(vR("CTNOrder")) = True, 0, vR("CTNOrder")))
            vO("TotalPcsOrder") = CDec(IIf(IsDBNull(vR("TotalPcsOrder")) = True, 0, vR("TotalPcsOrder")))
            vO("SupNum") = Trim(IIf(IsDBNull(vR("SupNum")) = True, "", vR("SupNum")))
            vO("SupName") = Trim(IIf(IsDBNull(vR("SupName")) = True, "", vR("SupName")))
            vO("Department") = Trim(IIf(IsDBNull(vR("Department")) = True, "", vR("Department")))
            vO("PlanningOrder") = Trim(IIf(IsDBNull(vR("PlanningOrder")) = True, "", vR("PlanningOrder")))
            vO("MachineName") = Trim(IIf(IsDBNull(vR("MachineName")) = True, "", vR("MachineName")))
            vO("IPAddress") = Trim(IIf(IsDBNull(vR("IPAddress")) = True, "", vR("IPAddress")))
            vO("CreatedDate") = CDate(IIf(IsDBNull(vR("CreatedDate")) = True, Todate, vR("CreatedDate")))
            vO("VerifyDate") = CDate(IIf(IsDBNull(vR("VerifyDate")) = True, Todate, vR("VerifyDate")))
            vO("RequiredDate") = IIf(IsDBNull(vR("RequiredDate")) = True, DBNull.Value, vR("RequiredDate"))
            vDisplayList.Rows.Add(vO)
        Next
        For Each vRow As DataGridViewRow In DgvShow.Rows
            vRow.Cells("ManualRenew").Value = CBool(vRow.Cells("Renew").Value)
            vRow.Cells("ManualNotAccept").Value = CBool(vRow.Cells("NotAccept").Value)
            vRow.Cells("ManualChangeQty").Value = CBool(vRow.Cells("ChangeQty").Value)
        Next
        If vDefaultIndex > 0 And DgvShow.RowCount > 0 Then
            DgvShow.CurrentCell = DgvShow.Rows(vDefaultIndex).Cells("Barcode")
            DgvShow.Rows(vDefaultIndex).Selected = True
            DgvShow.RowsDefaultCellStyle.SelectionBackColor = Color.FromName("Highlight")
            DgvShow.FirstDisplayedScrollingRowIndex = vDefaultIndex
        End If
        LblCountRow.Text = String.Format("Count Row : {0:N0}", DgvShow.RowCount)
        Dim vTotalCus As Decimal = 0
        Dim vTotalDelto As Decimal = 0
        Dim vTotalItems As Decimal = 0
        query = _
        <SQL>
            <![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                DECLARE @vBarcode AS NVARCHAR(MAX) = N'{5}';
                
                SELECT COUNT(DISTINCT [CusNum]) AS [CusNum],COUNT(DISTINCT [DeltoId]) AS [DeltoId],COUNT(DISTINCT [UnitNumber]) AS [UnitNumber]
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                WHERE ([Department] = @vDepartment)
                AND ([PlanningOrder] = @vPlanningOrder OR N'' = @vPlanningOrder);
                
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vBarcode, IIf(vFilterNotRenew = True, 1, 0))
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vTotalCus = CDec(IIf(IsDBNull(lists.Rows(0).Item("CusNum")) = True, 0, lists.Rows(0).Item("CusNum")))
                vTotalDelto = CDec(IIf(IsDBNull(lists.Rows(0).Item("DeltoId")) = True, 0, lists.Rows(0).Item("DeltoId")))
                vTotalItems = CDec(IIf(IsDBNull(lists.Rows(0).Item("UnitNumber")) = True, 0, lists.Rows(0).Item("UnitNumber")))
            End If
        End If
        LblTotalCustomer.Text = String.Format("♦ Total Customer = {0}", vTotalCus)
        LblTotalDelto.Text = String.Format("♦ Total Delto = {0}", vTotalDelto)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbDelto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDelto.SelectedIndexChanged
        TxtZone.Text = "" : TxtKhmerName.Text = ""
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then Exit Sub
        If CmbDelto.Text.Trim().Equals("") = True Then Exit Sub
        DisplayLoading.Enabled = True
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
        query = <SQL>
                    <![CDATA[
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {1};
                        SELECT [DelTo],[Zone],[KhmerUnicode]
                        FROM [Stock].[dbo].[TPRDelto]
                        WHERE [DefId] = @vDeltoId;
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vDeltoId)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtZone.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("Zone")) = True, 0, lists.Rows(0).Item("Zone")))
                TxtKhmerName.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("KhmerUnicode")) = True, 0, lists.Rows(0).Item("KhmerUnicode")))
                CmbProducts.Focus()
            End If
        End If
    End Sub

    Private Sub CmbDelto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbDelto.KeyPress
        'App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub CmbDelto_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles CmbDelto.PreviewKeyDown
        If e.KeyCode = Keys.Return Then
            Dim vDeltoId As Decimal = 0
            If IsNumeric(IIf(CmbDelto.Text.Trim().Equals("") = True, 0, CmbDelto.Text.Trim())) = True Then
                vDeltoId = CDec(IIf(CmbDelto.Text.Trim().Equals("") = True, 0, CmbDelto.Text.Trim()))
                CmbDelto.SelectedValue = vDeltoId
                If CmbDelto.Text.Trim().Equals("") = True Then CmbDelto.Text = vDeltoId
            End If
        End If
    End Sub

    Private Sub TxtPcsOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPcsOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 6)
    End Sub

    Private Sub TxtCTNOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCTNOrder.KeyPress
        Dim vQtyPCase As Decimal = CDec(IIf(TxtQtyPerCase.Text.Trim() = "", 1, TxtQtyPerCase.Text.Trim()))
        If (vQtyPCase Mod 2) = 0 Then
            Dim vStr() As String = TxtCTNOrder.Text.Trim().Split(".")
            If UBound(vStr) > 0 Then
                If vStr(1).Trim().Length >= 2 Then e.Handled = True
            End If
            App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Float, , 6)
        Else
            App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 6)
        End If
    End Sub

    Private Sub CmbProducts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbProducts.KeyPress
        If e.KeyChar = ChrW(Keys.Back) Then Exit Sub
        If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) Or (e.KeyChar >= "a" And e.KeyChar <= "z") Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
            If CmbProducts.Text.Trim() <> "" And IsNumeric(CmbProducts.Text.Trim()) = False Then e.Handled = True
            e.KeyChar = UCase(e.KeyChar)
        Else
            App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 15)
        End If
    End Sub

    Private Sub CmbProducts_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles CmbProducts.PreviewKeyDown
        If e.KeyCode <> Keys.Return And e.KeyCode <> Keys.Up And e.KeyCode <> Keys.Down Then CmbProducts.SelectedText = ""
        If e.Control = True And e.KeyCode = Keys.V Then
            With CmbProducts
                .Text = Clipboard.GetText
                .SelectionStart = .Text.Length
                .Focus()
            End With
        End If
        If e.KeyCode = Keys.Return Then
            If CmbProducts.Text.Trim() <> "" Then ProductSearch(CmbProducts)
        End If
    End Sub

    Private Sub ProductSearch(ByVal ComboName As ComboBox)
        Dim RBarcode As String = DirectCast(ComboName, ComboBox).Text.Trim()
        If RBarcode.Length > 13 Then RBarcode = Trim(Mid(RBarcode, 1, 15))
        If IsNumeric(RBarcode.Trim()) = True Then RBarcode = String.Format("{0:0000000000000}", Val(RBarcode))
        Dim TableProducts As DataTable = DirectCast(DirectCast(ComboName, ComboBox).DataSource, DataTable)
        Dim vIsExisted As Boolean = False
        For i As Integer = 0 To TableProducts.Rows.Count - 1
            Dim displayItem As String = TableProducts.Rows(i)(ComboName.DisplayMember).ToString()
            Dim valueItem As String = TableProducts.Rows(i)(ComboName.ValueMember).ToString()
            valueItem = Trim(Mid(valueItem, 1, RBarcode.Length))
            If valueItem.Equals(RBarcode) = True Then
                DirectCast(ComboName, ComboBox).SelectedIndex = i
                vIsExisted = True
                Exit For
            End If
        Next
        If vIsExisted = True Then
            TxtCTNOrder.Focus()
        Else
            CmbProducts.Focus()
        End If
    End Sub

    Private Sub CmbProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbProducts.SelectedIndexChanged
        BtnAdd.Enabled = True : TxtQtyPerCase.Text = "" : TxtPcsOrder.Text = "" : TxtCTNOrder.Text = "" : TxtTotalPcsOrder.Text = ""
        If CmbProducts.Text.Trim() = "" Then Exit Sub
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then Exit Sub
        Dim vBarcode As String = ""
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then
            vBarcode = ""
        Else
            If CmbProducts.Text.Trim() = "" Then
                vBarcode = ""
            Else
                vBarcode = CmbProducts.SelectedValue
            End If
        End If
        query = _
        <SQL>
            <![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = N'{1}';
                SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],[SupNum],[SupName]
                FROM (
	                SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                UNION ALL
	                SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                WHERE B.[OldProNumy] = @Barcode
                    UNION ALL
                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                UNION ALL
	                SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                WHERE B.[OldProNumy] = @Barcode
                ) LISTS;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vBarcode)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtQtyPerCase.Text = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPCase")) = True, 1, lists.Rows(0).Item("ProQtyPCase")))
            Else
                GoTo Check_Item
            End If
        Else
Check_Item:
            BtnAdd.Enabled = False
            MessageBox.Show("The Item is wrong. Please check item again...", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Private Sub TxtPcsOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtPcsOrder.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            TxtCTNOrder.Focus()
        ElseIf e.KeyCode = Keys.Return Then
            BtnAdd_Click(BtnAdd, New System.EventArgs())
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select the Planning Order first.", "Select Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPlanningOrder.Focus()
            Exit Sub
        ElseIf CmbBillTo.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select the Bill To.", "Select Bill To", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbBillTo.Focus()
            Exit Sub
        ElseIf CmbDelto.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select the Delto.", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbDelto.Focus()
            Exit Sub
        ElseIf CDec(IIf(TxtTotalPcsOrder.Text.Trim().Equals("") = True, 0, TxtTotalPcsOrder.Text.Trim())) = 0 Then
            MessageBox.Show("Please enter the Quantity Order.", "Enter Quantity Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtCTNOrder.Focus()
            Exit Sub
        Else
            Dim vPlanning As String = ""
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                vPlanning = ""
            Else
                If CmbPlanningOrder.Text.Trim() = "" Then
                    vPlanning = ""
                Else
                    vPlanning = CmbPlanningOrder.SelectedValue
                End If
            End If
            Dim vCusNum As String = ""
            If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
                vCusNum = ""
            Else
                If CmbBillTo.Text.Trim() = "" Then
                    vCusNum = ""
                Else
                    vCusNum = CmbBillTo.SelectedValue
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
            Dim vBarcode As String = ""
            If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then
                vBarcode = ""
            Else
                If CmbProducts.Text.Trim() = "" Then
                    vBarcode = ""
                Else
                    vBarcode = CmbProducts.SelectedValue
                End If
            End If
            If vBarcode.Trim().Equals("") = True Then
                MessageBox.Show("Please check the Barcode again.", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmbProducts.Focus()
                Exit Sub
            End If
            Dim vPcsOrder As Decimal = CDec(IIf(TxtPcsOrder.Text.Trim().Equals("") = True, 0, TxtPcsOrder.Text.Trim()))
            Dim vCTNOrder As Decimal = CDec(IIf(TxtCTNOrder.Text.Trim().Equals("") = True, 0, TxtCTNOrder.Text.Trim()))
            query = _
            <SQL>
                <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                    DECLARE @vBarcode AS NVARCHAR(MAX) = N'{5}';
                    DECLARE @vPcsOrder AS DECIMAL(18,0) = {6};
                    DECLARE @vCTNOrder AS DECIMAL(18,0) = {7};
                    SELECT *
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([CusNum] = @vCusNum) 
                    --AND ([Department] = @vDepartment) 
                    AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    AND ([Barcode] = @vBarcode)
                    --AND ([PcsOrder] = @vPcsOrder)
                    --AND ([CTNOrder] = @vCTNOrder)
                    ORDER BY [CusName],[Size],[ProName];
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vBarcode, vPcsOrder, vCTNOrder)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    If MessageBox.Show("Please check the Barcode again!" & vbCrLf & "The Barcode is existed already." & vbCrLf & "Do you want to go ahead?(Yes/No)", "Duplicated Barcode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        CmbProducts.Focus()
                        Exit Sub
                    End If
                End If
            End If

            query = <SQL>
                        <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vCusName AS NVARCHAR(100) = N'';
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                        DECLARE @vDelto AS NVARCHAR(100) = N'';
                        DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                        DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                        DECLARE @vBarcode AS NVARCHAR(MAX) = N'{5}';
                        DECLARE @vMachineName AS NVARCHAR(100) = N'{6}';
                        DECLARE @vIPAddress AS NVARCHAR(25) = N'{7}';
                        DECLARE @vPcsOrder AS DECIMAL(18,0) = {8};
                        DECLARE @vCTNOrder AS DECIMAL(18,0) = {9};
                        DECLARE @vTotalPcsOrder AS DECIMAL(18,0) = 0;
                        DECLARE @vUnitNumber AS NVARCHAR(MAX) = N'';
                        DECLARE @vProName AS NVARCHAR(100) = N'';
                        DECLARE @vSize AS NVARCHAR(10) = N'';
                        DECLARE @vQtyPerCase AS INT = 1;
                        DECLARE @vSupNum AS NVARCHAR(8) = N'';
                        DECLARE @vSupName AS NVARCHAR(100) = N'';
                        DECLARE @vRenew AS BIT = 0;
                        DECLARE @vNotAccept AS BIT = 0;
                        DECLARE @vChangeQty AS BIT = 0;

                        SELECT @vCusName = v.CusName FROM [Stock].[dbo].[TPRCustomer] AS v WHERE v.CusNum = @vCusNum;
                        SELECT @vDelto = v.DelTo FROM [Stock].[dbo].[TPRDelto] AS v WHERE v.DefId = @vDeltoId;
                        WITH vItems AS (
                        SELECT [ProNumY] AS [UnitNumber],[ProName],[ProPacksize] AS [Size],[ProQtyPCase] AS [QtyPerCase],RTRIM(LTRIM(LEFT([Sup1],8))) AS [SupNum],RTRIM(LTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
                        FROM [Stock].[dbo].[TPRProducts]
                        WHERE (ISNULL([ProNumY],N'') = @vBarcode OR ISNULL([ProNumYP],N'') = @vBarcode OR ISNULL([ProNumYC],N'') = @vBarcode)
                        UNION ALL
                        SELECT [ProNumY] AS [UnitNumber],[ProName],[ProPacksize] AS [Size],[ProQtyPCase] AS [QtyPerCase],RTRIM(LTRIM(LEFT([Sup1],8))) AS [SupNum],RTRIM(LTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
                        FROM [Stock].[dbo].[TPRProductsDeactivated]
                        WHERE (ISNULL([ProNumY],N'') = @vBarcode OR ISNULL([ProNumYP],N'') = @vBarcode OR ISNULL([ProNumYC],N'') = @vBarcode)
                        UNION ALL
                        SELECT x.[OldProNumy] AS [UnitNumber],v.[ProName],v.[ProPacksize] AS [Size],v.[ProQtyPCase] AS [QtyPerCase],RTRIM(LTRIM(LEFT(v.[Sup1],8))) AS [SupNum],RTRIM(LTRIM(SUBSTRING(v.[Sup1],9,LEN(v.[Sup1])))) AS [SupName]
                        FROM [Stock].[dbo].[TPRProducts] AS v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS x ON x.ProId = v.ProID
                        WHERE x.[OldProNumy] = @vBarcode
                        UNION ALL
                        SELECT x.[OldProNumy] AS [UnitNumber],v.[ProName],v.[ProPacksize] AS [Size],v.[ProQtyPCase] AS [QtyPerCase],RTRIM(LTRIM(LEFT(v.[Sup1],8))) AS [SupNum],RTRIM(LTRIM(SUBSTRING(v.[Sup1],9,LEN(v.[Sup1])))) AS [SupName]
                        FROM [Stock].[dbo].[TPRProductsDeactivated] AS v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS x ON x.ProId = v.ProID
                        WHERE x.[OldProNumy] = @vBarcode)
                        SELECT @vUnitNumber = vItems.UnitNumber,
                        @vProName = vItems.ProName,
                        @vSize = vItems.Size,
                        @vQtyPerCase = vItems.QtyPerCase,
                        @vSupNum = vItems.SupNum,
                        @vSupName = vItems.SupName
                        FROM vItems
                        GROUP BY vItems.UnitNumber,vItems.ProName,vItems.Size,vItems.QtyPerCase,vItems.SupNum,vItems.SupName;
                        IF (@vQtyPerCase IS NULL) SET @vQtyPerCase = 1;
                        SET @vTotalPcsOrder = (@vPcsOrder) + (@vCTNOrder * @vQtyPerCase);
                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]([CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate])
                        VALUES(@vCusNum,@vCusName,@vDeltoId,@vDelto,@vUnitNumber,@vBarcode,@vProName,@vSize,@vQtyPerCase,@vPcsOrder,@vCTNOrder,@vTotalPcsOrder,@vSupNum,@vSupName,@vRenew,@vNotAccept,@vChangeQty,@vDepartment,@vPlanningOrder,@vMachineName,@vIPAddress,GETDATE(),GETDATE());
                    ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vBarcode, Environment.MachineName, vIPAddress, vPcsOrder, vCTNOrder)
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                vDefaultIndex = IIf(vDisplayList.Rows.Count <= 0, -1, vDisplayList.Rows.Count)
                DisplayLoading.Enabled = True
                CmbProducts.SelectedIndex = -1
                CmbProducts.Focus()
            Catch ex As SqlException
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    Private Sub TxtCTNOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtCTNOrder.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            BtnAdd.Focus()
        ElseIf e.KeyCode = Keys.Return Then
            BtnAdd_Click(BtnAdd, New System.EventArgs())
        End If
    End Sub

    Private Sub TxtPcsOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtPcsOrder.TextChanged
        vTotalPcsOrder()
    End Sub

    Private Sub TxtCTNOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtCTNOrder.TextChanged
        vTotalPcsOrder()
    End Sub

    Private Sub vTotalPcsOrder()
        Dim vQtyPCase As Integer = CInt(IIf(TxtQtyPerCase.Text.Trim().Equals("") = True, 1, TxtQtyPerCase.Text.Trim()))
        Dim vPcsOrder As Decimal = CDec(IIf(TxtPcsOrder.Text.Trim().Equals("") = True, 0, TxtPcsOrder.Text.Trim()))
        Dim vCTNOrder As Decimal = CDec(IIf(TxtCTNOrder.Text.Trim().Equals("") = True, 0, TxtCTNOrder.Text.Trim()))
        Dim vTotalPcsOrder As Decimal = vPcsOrder + (vCTNOrder * vQtyPCase)
        TxtTotalPcsOrder.Text = String.Format("{0:N0}", vTotalPcsOrder)
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub DgvShow_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvShow.CellContentClick
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.ColumnIndex <> 0 And e.ColumnIndex <> 1 And e.ColumnIndex <> 2 Then Exit Sub
        If e.RowIndex = -1 Then Exit Sub
        Dim vReNew As Integer = 0
        Dim vNotAccept As Integer = 0
        Dim vChangeQty As Integer = 0
        Dim vId As Decimal = 0
        Dim vBarcode As String = ""
        Dim vProName As String = ""
        Dim vSize As String = ""
        Dim vQtyPerCase As Integer = 1
        Dim vPcsOrder As Decimal = 0
        Dim vCTNOrder As Decimal = 0
        Dim vCusNum As String = ""
        Dim vQuery As String = ""
        With DgvShow.Rows(e.RowIndex)
            vCusNum = Trim(IIf(IsDBNull(.Cells("CusNum").Value) = True, "", .Cells("CusNum").Value))
            vId = CDec(IIf(IsDBNull(.Cells("Id").Value) = True, 0, .Cells("Id").Value))
            vBarcode = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
            vProName = Trim(IIf(IsDBNull(.Cells("ProName").Value) = True, "", .Cells("ProName").Value))
            vSize = Trim(IIf(IsDBNull(.Cells("Size").Value) = True, "", .Cells("Size").Value))
            vQtyPerCase = CInt(IIf(IsDBNull(.Cells("QtyPerCase").Value) = True, 1, .Cells("QtyPerCase").Value))
            vPcsOrder = CDec(IIf(IsDBNull(.Cells("PcsOrder").Value) = True, 0, .Cells("PcsOrder").Value))
            vCTNOrder = CDec(IIf(IsDBNull(.Cells("CTNOrder").Value) = True, 0, .Cells("CTNOrder").Value))
            If DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualRenew") = True Then
                If CBool(.Cells("ManualNotAccept").Value) = True Then
                    MessageBox.Show("The Item is not accept." & vbCrLf & "So, Cannot renew this item.", "Not Accept", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If CBool(.Cells("ManualRenew").Value) = True Then
                    vReNew = 0
                Else
                    vReNew = 1
                End If
                vQuery = <SQL>
                             <![CDATA[
                                DECLARE @vId AS DECIMAL(18,0) = {1};
                                DECLARE @vReNew AS BIT = {2};
                                UPDATE [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                                SET [Renew] = @vReNew,[NotAccept] = 0,[ChangeQty] = 0,[VerifyDate] = GETDATE(),[RequiredDate] = NULL
                                WHERE [Id] = @vId;
                            ]]>
                         </SQL>
                vQuery = String.Format(vQuery, DatabaseName, vId, vReNew)
            ElseIf DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualNotAccept") = True Then
                If CBool(.Cells("ManualNotAccept").Value) = True Then
                    vNotAccept = 0
                Else
                    vNotAccept = 1
                End If
                vQuery = <SQL>
                             <![CDATA[
                                DECLARE @vId AS DECIMAL(18,0) = {1};
                                DECLARE @vNotAccept AS BIT = {2};
                                UPDATE [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                                SET [NotAccept] = @vNotAccept,[Renew] = 0,[ChangeQty] = 0,[VerifyDate] = GETDATE()
                                WHERE [Id] = @vId;
                            ]]>
                         </SQL>
                vQuery = String.Format(vQuery, DatabaseName, vId, vNotAccept)
            ElseIf DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualChangeQty") = True Then
                If CBool(.Cells("ManualChangeQty").Value) = True Then
                    vChangeQty = 0
                Else
                    Dim vFrm As New FrmDutchmillTakeOrderQty With {.vId = vId, .vBarcode = vBarcode, .vProName = vProName, .vSize = vSize, .vQtyPerCase = vQtyPerCase, .vPcsOrder = vPcsOrder, .vCTNOrder = vCTNOrder}
                    If vFrm.ShowDialog(MDI) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    vChangeQty = 1
                End If
                vQuery = <SQL>
                             <![CDATA[
                                DECLARE @vId AS DECIMAL(18,0) = {1};
                                DECLARE @vChangeQty AS BIT = {2};
                                UPDATE [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                                SET [Renew] = 0,[NotAccept] = 0,[ChangeQty] = @vChangeQty,[VerifyDate] = GETDATE(),[RequiredDate] = NULL
                                WHERE [Id] = @vId;
                            ]]>
                         </SQL>
                vQuery = String.Format(vQuery, DatabaseName, vId, vChangeQty)
            End If
            If DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualRenew") = True Then
                vDisplayList.Rows(e.RowIndex).Item("Renew") = Not vDisplayList.Rows(e.RowIndex).Item("Renew")
                vDisplayList.Rows(e.RowIndex).Item("NotAccept") = False
                vDisplayList.Rows(e.RowIndex).Item("ChangeQty") = False
            ElseIf DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualNotAccept") = True Then
                vDisplayList.Rows(e.RowIndex).Item("Renew") = False
                vDisplayList.Rows(e.RowIndex).Item("NotAccept") = Not vDisplayList.Rows(e.RowIndex).Item("NotAccept")
                vDisplayList.Rows(e.RowIndex).Item("ChangeQty") = False
            ElseIf DgvShow.Columns(e.ColumnIndex).Name.Equals("ManualChangeQty") = True Then
                vDisplayList.Rows(e.RowIndex).Item("Renew") = False
                vDisplayList.Rows(e.RowIndex).Item("NotAccept") = False
                vDisplayList.Rows(e.RowIndex).Item("ChangeQty") = Not vDisplayList.Rows(e.RowIndex).Item("ChangeQty")
            End If
        End With

        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RTran = RCon.BeginTransaction()
        Try
            RCom = New SqlCommand
            RCom.Transaction = RTran
            RCom.Connection = RCon
            RCom.CommandType = CommandType.Text
            RCom.CommandText = vQuery
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()
            If e.ColumnIndex = 2 Then
                vDefaultIndex = e.RowIndex
                DisplayLoading.Enabled = True
            Else
                For Each vRow As DataGridViewRow In DgvShow.Rows
                    vRow.Cells("ManualRenew").Value = CBool(vRow.Cells("Renew").Value)
                    vRow.Cells("ManualNotAccept").Value = CBool(vRow.Cells("NotAccept").Value)
                    vRow.Cells("ManualChangeQty").Value = CBool(vRow.Cells("ChangeQty").Value)
                    If CBool(vRow.Cells("ManualRenew").Value) = False And CBool(vRow.Cells("ManualChangeQty").Value) = False Then
                        If CBool(vRow.Cells("ManualNotAccept").Value) = True Then
                            vRow.DefaultCellStyle.ForeColor = Color.Gray
                        Else
                            vRow.DefaultCellStyle.ForeColor = Color.Red
                        End If
                    Else
                        If CBool(vRow.Cells("ManualNotAccept").Value) = True Then
                            vRow.DefaultCellStyle.ForeColor = Color.Gray
                        Else
                            vRow.DefaultCellStyle.ForeColor = Color.Black
                        End If
                    End If
                Next
            End If
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

    Private Function CheckCreditAmountCustomer(CusNum As String, Optional ByVal Amount As Double = 0) As Boolean
        Dim IsAllow As Boolean = False
        Dim InvNumber As Long = 0
        Dim ShipDate As Date = Nothing
        Dim GrandTotal As Double = 0
        Dim CreditAmount As Double = 0
        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
                DECLARE @query AS NVARCHAR(MAX) = '';
                DECLARE @CreditLists AS TABLE
                (
	                [InvNumber] [DECIMAL](18,0) NULL,
	                [PONumber] [NVARCHAR](25) NULL,
	                [CusNum] [NVARCHAR](8) NULL,
	                [CusName] [NVARCHAR](100) NULL,
	                [ShipDate] [DATE] NULL,
	                [DelTo] [NVARCHAR](100) NULL,
	                [GrandTotal] [MONEY] NULL
                );
                DECLARE @StatementName AS NVARCHAR(MAX) = N'';
                DECLARE @TableName AS NVARCHAR(MAX) = N'';
                DECLARE @Division AS NVARCHAR(MAX) = N'';
                DECLARE @Condition AS NVARCHAR(MAX) = N'';
                DECLARE CUR_DIV CURSOR
                FOR SELECT [TableName],[Division] FROM [Stock].[dbo].[AAllDivisionPayment] WHERE [Division] NOT IN ('Division8', 'Division8 VAT', 'Division9','Division9 VAT', 'Division11', 'Division11 VAT') ORDER BY [Division];
                OPEN CUR_DIV
                FETCH NEXT FROM CUR_DIV INTO @TableName,@Division;
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                SET @query = 'SELECT [InvNumber],[PONumber],[CusNum],[CusCom],[ShipDate],[DelTo],[GrandTotal] ' + @NewLineChar;
	                SET @query = @query + 'FROM [Stock].[dbo].[' + @TableName + '] ' + @NewLineChar;
	                SET @query = @query + 'WHERE ISNULL([PAID],''0'') = ''0'' AND [CusNum] = '''+ @CusNum + ''' AND [InvNumber] NOT IN (' + @NewLineChar;
	                SET @Condition = '';
	                DECLARE CUR_STA CURSOR
	                FOR SELECT [TableName] FROM [Stock].[dbo].[AAllDivisionStatement] WHERE [Division] = @Division ORDER BY [TableName];
	                OPEN CUR_STA
	                FETCH NEXT FROM CUR_STA INTO @StatementName;
	                WHILE @@FETCH_STATUS = 0
	                BEGIN
		                SET @Condition = @Condition + 'SELECT [InvNumber] ' + @NewLineChar;
		                SET @Condition = @Condition + 'FROM [Stock].[dbo].[' + @StatementName + '] ' + @NewLineChar;
		                SET @Condition = @Condition + 'WHERE [CusNum] = ''' + @CusNum + ''' ' + @NewLineChar;
		                SET @Condition = @Condition + 'UNION ALL ' + @NewLineChar;
		                FETCH NEXT FROM CUR_STA INTO @StatementName;
	                END
	                CLOSE CUR_STA;
	                DEALLOCATE CUR_STA;
	                IF (RTRIM(LTRIM(@Condition)) = '')
	                BEGIN
		                SET @Condition = '0';
	                END
	                ELSE
	                BEGIN
		                SET @Condition = @Condition + 'SELECT [InvNumber] ' + @NewLineChar;
                        SET @Condition = @Condition + 'FROM [Stock].[dbo].[TPRDeliveryPaymentViewCreditException] ' + @NewLineChar;
                        SET @Condition = @Condition + 'WHERE [CusNum] = ''' + @CusNum + ''' ' + @NewLineChar;
	                END
	                SET @query = @query + @Condition + ' ' + @NewLineChar;
	                SET @query = @query + '); ' + @NewLineChar;
	                INSERT INTO @CreditLists ([InvNumber],[PONumber],[CusNum],[CusName],[ShipDate],[DelTo],[GrandTotal])
	                EXEC(@query);
	                FETCH NEXT FROM CUR_DIV INTO @TableName,@Division;
                END
                CLOSE CUR_DIV;
                DEALLOCATE CUR_DIV;

                SELECT [InvNumber],[PONumber],[CusNum],[CusName],[ShipDate],[DelTo],[GrandTotal]
                FROM @CreditLists
                ORDER BY [ShipDate] ASC;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        Dim CreditAmountList As DataTable = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (CreditAmountList Is Nothing) Then
            If CreditAmountList.Rows.Count > 0 Then
                InvNumber = CLng(IIf(IsDBNull(CreditAmountList.Rows(0).Item("InvNumber")) = True, 0, CreditAmountList.Rows(0).Item("InvNumber")))
                ShipDate = CDate(IIf(IsDBNull(CreditAmountList.Rows(0).Item("ShipDate")) = True, Todate, CreditAmountList.Rows(0).Item("ShipDate")))
                GrandTotal = CDbl(IIf(IsDBNull(CreditAmountList.Rows(0).Item("GrandTotal")) = True, 0, CreditAmountList.Rows(0).Item("GrandTotal")))
            End If
        End If

        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
                DECLARE @query AS NVARCHAR(MAX) = '';
                DECLARE @CreditLists AS TABLE
                (
	                [InvNumber] [DECIMAL](18,0) NULL,
	                [PONumber] [NVARCHAR](25) NULL,
	                [CusNum] [NVARCHAR](8) NULL,
	                [CusName] [NVARCHAR](100) NULL,
	                [ShipDate] [DATE] NULL,
	                [DelTo] [NVARCHAR](100) NULL,
	                [GrandTotal] [MONEY] NULL
                );
                DECLARE @StatementName AS NVARCHAR(MAX) = N'';
                DECLARE @TableName AS NVARCHAR(MAX) = N'';
                DECLARE @Division AS NVARCHAR(MAX) = N'';
                DECLARE @Condition AS NVARCHAR(MAX) = N'';
                DECLARE CUR_DIV CURSOR
                FOR SELECT [TableName],[Division] FROM [Stock].[dbo].[AAllDivisionPayment] WHERE [Division] NOT IN ('Division8', 'Division8 VAT', 'Division9','Division9 VAT', 'Division11', 'Division11 VAT') ORDER BY [Division];
                OPEN CUR_DIV
                FETCH NEXT FROM CUR_DIV INTO @TableName,@Division;
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                SET @query = 'SELECT [InvNumber],[PONumber],[CusNum],[CusCom],[ShipDate],[DelTo],[GrandTotal] ' + @NewLineChar;
	                SET @query = @query + 'FROM [Stock].[dbo].[' + @TableName + '] ' + @NewLineChar;
	                SET @query = @query + 'WHERE ISNULL([PAID],''0'') = ''0'' AND [CusNum] = '''+ @CusNum + ''' AND [InvNumber] NOT IN (' + @NewLineChar;
	                SET @Condition = '';
	                DECLARE CUR_STA CURSOR
	                FOR SELECT [TableName] FROM [Stock].[dbo].[AAllDivisionStatement] WHERE [Division] = @Division ORDER BY [TableName];
	                OPEN CUR_STA
	                FETCH NEXT FROM CUR_STA INTO @StatementName;
	                WHILE @@FETCH_STATUS = 0
	                BEGIN
		                SET @Condition = @Condition + 'SELECT [InvNumber] ' + @NewLineChar;
		                SET @Condition = @Condition + 'FROM [Stock].[dbo].[' + @StatementName + '] ' + @NewLineChar;
		                SET @Condition = @Condition + 'WHERE [CusNum] = ''' + @CusNum + ''' ' + @NewLineChar;
		                SET @Condition = @Condition + 'UNION ALL ' + @NewLineChar;
		                FETCH NEXT FROM CUR_STA INTO @StatementName;
	                END
	                CLOSE CUR_STA;
	                DEALLOCATE CUR_STA;
	                IF (RTRIM(LTRIM(@Condition)) = '')
	                BEGIN
		                SET @Condition = '0';
	                END
	                ELSE
	                BEGIN
		                SET @Condition = @Condition + 'SELECT 0 AS [InvNumber] ' + @NewLineChar;
	                END
	                SET @query = @query + @Condition + ' ' + @NewLineChar;
	                SET @query = @query + '); ' + @NewLineChar;
	                INSERT INTO @CreditLists ([InvNumber],[PONumber],[CusNum],[CusName],[ShipDate],[DelTo],[GrandTotal])
	                EXEC(@query);
	                FETCH NEXT FROM CUR_DIV INTO @TableName,@Division;
                END
                CLOSE CUR_DIV;
                DEALLOCATE CUR_DIV;

                SELECT SUM([GrandTotal]) AS [CreditAmountOwed]
                FROM @CreditLists;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                For Each r As DataRow In lists.Rows
                    CreditAmount += CDbl(IIf(IsDBNull(r.Item("CreditAmountOwed")) = True, 0, r.Item("CreditAmountOwed")))
                Next
            End If
        End If

        'Check Credit Amount Allow
        Dim AllowInv As Integer = 0
        Dim CreditAllow As Double = 0
        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT [Id],[CusNum],[CusName],[Allow],[MaxCredit],[CreatedDate] 
                FROM [{0}].[dbo].[TblCustomerAllowCredits] 
                WHERE DATEDIFF(DAY,GETDATE(),ISNULL([Expiry],GETDATE())) > 0 AND [CusNum] = @CusNum;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                AllowInv = CInt(IIf(IsDBNull(lists.Rows(0).Item("Allow")) = True, 0, lists.Rows(0).Item("Allow")))
                CreditAllow = CDbl(IIf(IsDBNull(lists.Rows(0).Item("MaxCredit")) = True, 0, lists.Rows(0).Item("MaxCredit")))
            End If
        End If
        If (AllowInv > 0 And CreditAllow >= (CreditAmount + Amount)) Then
            IsAllow = True
            GoTo CreditAllows
        End If
        If Not (CreditAmountList Is Nothing) Then
            If CreditAmountList.Rows.Count <= 0 Then
                IsAllow = True
                GoTo CreditAllows
            End If
        Else
            IsAllow = True
            GoTo CreditAllows
        End If
        If Amount <> 0 Then GoTo Err_CheckCreditAmount
        Dim Days As Integer = DateDiff(DateInterval.Day, ShipDate, Todate.Date)
        If (Days >= MaxMonthAllow) Then
            With FrmAlertCreditAmount
                .Text = "Reminder(Need manager to access)"
                .LblMsg.Text = "Due Date " & MaxMonthAllow & " days. Latest day " & Days & " due invoice. Must notify customer. Latest Invoice Number = " & InvNumber & ", Ship Date = " & String.Format("{0:dd-MMM-yyyy}", ShipDate) & ",Amount $" & String.Format("{0:N2}", GrandTotal) & "." & vbCrLf & "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & ". This customer owes $" & CreditAmount & ". Not allow issue another invoice."
                .PicAlert.Visible = True
                .BtnOK.Visible = True
                .BtnYes.Visible = False
                .BtnNo.Visible = False
                .DgvShow.DataSource = CreditAmountList
                .DgvShow.Refresh()
                .ShowDialog()
            End With
            IsAllow = False
        ElseIf (Days >= Terms) Then
            With FrmAlertCreditAmount
                .Text = "Reminder(Need account manager or manager to access)"
                .LblMsg.Text = "Terms Allow " & Terms & " days.  Maximum " & MaxMonthAllow & " days is allow due on latest invoice. Should notify customer. Latest Invoice Number = " & InvNumber & ", Ship Date = " & String.Format("{0:dd-MMM-yyyy}", ShipDate) & ",Amount $" & String.Format("{0:N2}", GrandTotal) & "." & vbCrLf & "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & ". This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Not allow issue another invoice."
                .PicAlert.Visible = False
                .BtnOK.Visible = False
                .BtnYes.Visible = True
                .BtnNo.Visible = True
                .DgvShow.DataSource = CreditAmountList
                .DgvShow.Refresh()
                If .ShowDialog() = Windows.Forms.DialogResult.Yes Then
                    If CreditAmount >= CreditLimitAllow Then
                        .Text = "Credit Limit (Need Manager to access)"
                        .LblMsg.Text = "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & ". This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Not allow issue another invoice."
                        .PicAlert.Visible = True
                        .BtnOK.Visible = True
                        .BtnYes.Visible = False
                        .BtnNo.Visible = False
                        .DgvShow.DataSource = CreditAmountList
                        .DgvShow.Refresh()
                        .ShowDialog()

                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            IsAllow = True
                        Else
                            IsAllow = False
                        End If
                    Else
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            IsAllow = True
                        Else
                            IsAllow = False
                        End If
                    End If
                Else
                    IsAllow = False
                End If
            End With
        Else
Err_CheckCreditAmount:
            If CreditAmount >= CreditLimitAllow Then
                With FrmAlertCreditAmount
                    .Text = "Credit Limit (Need manager to access)"
                    .LblMsg.Text = "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & "." & vbCrLf & "This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Not allow issue another invoice."
                    .PicAlert.Visible = True
                    .BtnOK.Visible = True
                    .BtnYes.Visible = False
                    .BtnNo.Visible = False
                    .DgvShow.DataSource = CreditAmountList
                    .DgvShow.Refresh()
                    .ShowDialog()
                End With
                IsAllow = False
            ElseIf CreditAmount >= CreditLimit And Amount = 0 Then
                With FrmAlertCreditAmount
                    .Text = "Credit Limit (Need account manager to access)"
                    .LblMsg.Text = "Credit limit for this customer = $" & String.Format("{0:N2}", CreditLimit) & "." & vbCrLf & "This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Would you like to issue another invoice."
                    .PicAlert.Visible = False
                    .BtnOK.Visible = False
                    .BtnYes.Visible = True
                    .BtnNo.Visible = True
                    .DgvShow.DataSource = CreditAmountList
                    .DgvShow.Refresh()
                    If .ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            IsAllow = True
                        Else
                            IsAllow = False
                        End If
                    Else
                        IsAllow = False
                    End If
                End With
            Else
                IsAllow = True
            End If
        End If
CreditAllows:
        Return IsAllow
    End Function

    Private Function CheckInfoCustomer(CusNum As String) As Boolean
        Dim IsExisted As Boolean = True
        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT [CusID],[CusNum],[CusName],[CusVat],[Terms],[Discount],[InvoiceDiscount],[CreditLimit],[CreditLimitAllow],[MaxMonthAllow],[ServiceRebate]
                FROM [Stock].[dbo].[TPRCustomer]
                WHERE [CusNum] = @CusNum;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                CusItemDis = CSng(IIf(IsDBNull(lists.Rows(0).Item("Discount")) = True, 0, lists.Rows(0).Item("Discount")))
                CusInvoiceDis = CSng(IIf(IsDBNull(lists.Rows(0).Item("InvoiceDiscount")) = True, 0, lists.Rows(0).Item("InvoiceDiscount")))
                CusVAT = Trim(IIf(IsDBNull(lists.Rows(0).Item("CusVat")) = True, "", lists.Rows(0).Item("CusVat")))
                CusServiceRebate = CSng(IIf(IsDBNull(lists.Rows(0).Item("ServiceRebate")) = True, 0, lists.Rows(0).Item("ServiceRebate")))
                Terms = CInt(IIf(IsDBNull(lists.Rows(0).Item("Terms")) = True, 0, lists.Rows(0).Item("Terms")))
                CreditLimit = CDbl(IIf(IsDBNull(lists.Rows(0).Item("CreditLimit")) = True, 0, lists.Rows(0).Item("CreditLimit")))
                MaxMonthAllow = CInt(IIf(IsDBNull(lists.Rows(0).Item("MaxMonthAllow")) = True, 0, lists.Rows(0).Item("MaxMonthAllow")))
                CreditLimitAllow = CDbl(IIf(IsDBNull(lists.Rows(0).Item("CreditLimitAllow")) = True, 0, lists.Rows(0).Item("CreditLimitAllow")))
                IsExisted = True
            Else
                IsExisted = False
            End If
        Else
            IsExisted = False
        End If
        Return IsExisted
    End Function

    Private CusItemDis As Single
    Private CusInvoiceDis As Single
    Private CusVAT As String
    Private CusServiceRebate As Single
    Private Terms As Integer
    Private CreditLimit As Double
    Private MaxMonthAllow As Integer
    Private CreditLimitAllow As Double
    Private Function CheckBankGaranteeCustomer(CusNum As String) As Boolean
        Dim IsExpiry As Boolean = False
        Dim DateExpiry As Date
        Dim DateAlert As Date
        Dim CurDate As Date
        Dim IsAlertForm As Boolean = False
        Dim Msg As String = ""
        Dim Title As String = ""
        query = _
        <SQL>
            <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT [CreditLimit],[Expiry],[AlertDate],GETDATE() AS [CurDate]
                FROM [Stock].[dbo].[TPRCustomerBankGarantee]
                WHERE [CusId] = @CusNum
                ORDER BY [Expiry];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                For Each r As DataRow In lists.Rows
                    DateExpiry = CDate(IIf(IsDBNull(r.Item("Expiry")) = True, Todate, r.Item("Expiry")))
                    DateAlert = CDate(IIf(IsDBNull(r.Item("AlertDate")) = True, Todate, r.Item("AlertDate")))
                    CurDate = CDate(IIf(IsDBNull(r.Item("CurDate")) = True, Todate, r.Item("CurDate")))
                    If DateDiff(DateInterval.Day, CurDate, DateExpiry) <= 0 Then
                        Msg = "The customer bank garantee expiry reaches." & vbCrLf & "Not allow to continue!"
                        Title = "Expiry Reaches"
                        IsExpiry = True
                        IsAlertForm = True
                    ElseIf DateDiff(DateInterval.Day, CurDate, DateAlert) <= 0 Then
                        Msg = DateDiff(DateInterval.Day, CurDate, DateExpiry) & " day(s) left for customer bank garantee expiry." & vbCrLf & "Please inform the administrator."
                        Title = "Near Expiry"
                        IsAlertForm = True
                    End If
                Next
            End If
        End If
        If IsAlertForm = True Then
            With FrmAlertBankGarantee
                .Text = Title
                .LblMsg.Text = Msg
                .DgvShow.DataSource = lists
                .DgvShow.Refresh()
                .ShowDialog()
            End With
        End If
        Return IsExpiry
    End Function

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            If MessageBox.Show("Are you sure, you want to delete all item selected?(Yes/No)", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
            Dim vId As String = ""
            For Each r As DataGridViewRow In DgvShow.Rows
                If r.Selected = True Then vId &= String.Format("{0},", CDec(IIf(IsDBNull(r.Cells("Id").Value) = True, 0, r.Cells("Id").Value)))
            Next
            If vId.Trim().Equals("") = True Then
                vId = "0"
            Else
                vId = vId.Trim()
                vId = Trim(Mid(vId, 1, vId.Length - 1))
            End If
            vDefaultIndex = 0
            query = _
            <SQL>
                <![CDATA[
                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder_Deleting]([CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[DeletedDate])
                    SELECT [CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE [Id] IN ({1});

                    DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE [Id] IN ({1});
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, vId)
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                DisplayLoading.Enabled = True
            Catch ex As SqlException
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles BtnFinish.Click
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any planning order!", "Select Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPlanningOrder.Focus()
            Exit Sub
            'ElseIf CmbBillTo.Text.Trim().Equals("") = True Then
            '    MessageBox.Show("Please select any bill to!", "Select Bill To", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbBillTo.Focus()
            '    Exit Sub
            'ElseIf CmbDelto.Text.Trim().Equals("") = True Then
            '    MessageBox.Show("Please select any delto!", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbDelto.Focus()
            '    Exit Sub
        Else
            Dim vPlanning As String = ""
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                vPlanning = ""
            Else
                If CmbPlanningOrder.Text.Trim() = "" Then
                    vPlanning = ""
                Else
                    vPlanning = CmbPlanningOrder.SelectedValue
                End If
            End If
            query = <SQL>
                        <![CDATA[
                    DECLARE @vPlanning AS NVARCHAR(50) = N'{1}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{2}';
                    SELECT [Id],[CusNum],[CusName],[Remark],[AlertDate],[BlockDate],[CreatedDate],
                    CASE WHEN DATEDIFF(DAY,CONVERT(DATE,GETDATE()),CONVERT(DATE,[BlockDate])) <=0 THEN N'Block' ELSE N'' END [Status]
                    FROM [{0}].[dbo].[TblCustomerRemarkSetting]
                    WHERE ([CusNum] IN (SELECT [CusNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] WHERE ([Department] = @vDepartment) AND ([PlanningOrder] = @vPlanning) AND ([NotAccept] = 0) GROUP BY [CusNum])) 
                    AND (([Status] = N'Both') OR ([Status] = N'Dutchmill'))
                    AND (CONVERT(DATE,[AlertDate]) <= CONVERT(DATE,GETDATE())); 
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vPlanning, vDepartment)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    Dim vF As New FrmAlertBadPayment
                    vF.DgvShow.DataSource = lists
                    vF.DgvShow.Refresh()
                    Dim vResult As Object = vF.ShowDialog()
                    If vResult = DialogResult.Cancel Then
                        Exit Sub
                    ElseIf vResult = DialogResult.No Then
                        query = <SQL>
                                    <![CDATA[
                            DECLARE @vPlanning AS NVARCHAR(50) = N'{1}';
                            DECLARE @vDepartment AS NVARCHAR(50) = N'{2}';
                            WITH v AS (
                                SELECT [Id],[CusNum],[CusName],[Remark],[AlertDate],[BlockDate],[CreatedDate],
                                CASE WHEN DATEDIFF(DAY,CONVERT(DATE,GETDATE()),CONVERT(DATE,[BlockDate])) <=0 THEN N'Block' ELSE N'' END [Status]
                                FROM [{0}].[dbo].[TblCustomerRemarkSetting]
                                WHERE ([CusNum] IN (SELECT [CusNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] WHERE ([Department] = @vDepartment) AND ([PlanningOrder] = @vPlanning) AND ([NotAccept] = 0) GROUP BY [CusNum])) 
                                AND (([Status] = N'Both') OR ([Status] = N'Dutchmill'))
                                AND (CONVERT(DATE,[AlertDate]) <= CONVERT(DATE,GETDATE()))
                            )
                            UPDATE o
                            SET o.[NotAccept] = 1, o.[Renew] = 0, o.[ChangeQty] = 0
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] o
                            WHERE (o.[Department] = @vDepartment) AND (o.[PlanningOrder] = @vPlanning) AND (o.[NotAccept] = 0)
                            AND o.[CusNum] IN (SELECT v.[CusNum] FROM v);
                        ]]>
                                </SQL>
                        query = String.Format(query, DatabaseName, vPlanning, vDepartment)
                        Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                    ElseIf vResult = DialogResult.Yes Then
                        query = <SQL>
                                    <![CDATA[
                            DECLARE @vPlanning AS NVARCHAR(50) = N'{1}';
                            DECLARE @vDepartment AS NVARCHAR(50) = N'{2}';
                            WITH v AS (
                                SELECT [Id],[CusNum],[CusName],[Remark],[AlertDate],[BlockDate],[CreatedDate],
                                CASE WHEN DATEDIFF(DAY,CONVERT(DATE,GETDATE()),CONVERT(DATE,[BlockDate])) <=0 THEN N'Block' ELSE N'' END [Status]
                                FROM [{0}].[dbo].[TblCustomerRemarkSetting]
                                WHERE ([CusNum] IN (SELECT [CusNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] WHERE ([Department] = @vDepartment) AND ([PlanningOrder] = @vPlanning) AND ([NotAccept] = 0) GROUP BY [CusNum])) 
                                AND (([Status] = N'Both') OR ([Status] = N'Dutchmill'))
                                AND (CONVERT(DATE,[AlertDate]) <= CONVERT(DATE,GETDATE()))
                            )
                            UPDATE o
                            SET o.[NotAccept] = 1, o.[Renew] = 0, o.[ChangeQty] = 0
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] o
                            WHERE (o.[Department] = @vDepartment) AND (o.[PlanningOrder] = @vPlanning) AND (o.[NotAccept] = 0)
                            AND o.[CusNum] IN (SELECT v.[CusNum] FROM v WHERE (v.[Status] = N'Block'));
                        ]]>
                                </SQL>
                        query = String.Format(query, DatabaseName, vPlanning, vDepartment)
                        Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                    End If
                End If
            End If

            CmbPlanningOrder_SelectedIndexChanged(CmbPlanningOrder, e)
            vFilterNotRenew = 0
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                vPlanning = ""
            Else
                If CmbPlanningOrder.Text.Trim() = "" Then
                    vPlanning = ""
                Else
                    vPlanning = CmbPlanningOrder.SelectedValue
                End If
            End If
            Dim vCusNum As String = ""
            If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
                vCusNum = ""
            Else
                If CmbBillTo.Text.Trim() = "" Then
                    vCusNum = ""
                Else
                    vCusNum = CmbBillTo.SelectedValue
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
            Dim vMessage As String = ""
            query = <SQL>
                        <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';

                    SELECT [Renew],[NotAccept],[ChangeQty],[Id],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[RequiredDate]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    AND (ISNULL([Renew],0) = 0)
                    AND (ISNULL([NotAccept],0) = 0)
                    AND (ISNULL([ChangeQty],0) = 0)
                    ORDER BY [CusName],[Size],[ProName];
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                For Each r As DataRow In lists.Rows
                    vMessage &= "► " & Trim(IIf(IsDBNull(r("Barcode")) = True, "", r("Barcode"))) & " = " & CDec(IIf(IsDBNull(r("TotalPcsOrder")) = True, 0, r("TotalPcsOrder"))) & " Pcs" & vbCrLf
                Next
            End If
            If vMessage.Trim.Equals("") = False Then
                vMessage = "Please renew the items:" & vbCrLf & vMessage
                MessageBox.Show(vMessage, "Need Renew Items", MessageBoxButtons.OK, MessageBoxIcon.Information)
                vFilterNotRenew = 1
                DisplayLoading.Enabled = True
                Exit Sub
            End If

            If MessageBox.Show("Are you sure, you already to renew the items?(Yes/No)", "Confirm Renew", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
            Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
            Dim vTakeOrderNumber As Decimal = 0
            Dim vDateOrder As Date = Today.Date
            Dim vRequiredDate As Date = Today.Date
            Dim vPONumber As String = ""
            Dim vFrm As New FrmDutchmillTakeOrderPONumber With {.vDateOrder = vDateOrder, .vRequiredDate = vRequiredDate, .vPONumber = vPONumber}
            If vFrm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            vDateOrder = vFrm.vDateOrder
            vRequiredDate = vFrm.vRequiredDate
            vPONumber = vFrm.vPONumber

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
            vTakeOrderNumber = vInvNo

            query = <SQL>
                        <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                    DECLARE @vTakeOrderNumber AS DECIMAL(18,0) = {5};
                    DECLARE @vDateOrder AS DATE = N'{6:yyyy-MM-dd}';
                    DECLARE @vRequiredDate AS DATE = N'{7:yyyy-MM-dd}';
                    DECLARE @vPONumber AS NVARCHAR(50) = N'{8}';

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderFinish]([TakeOrderNumber],[DateOrd],[DateRequired],[PONumber],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[FinishDate])
                    SELECT @vTakeOrderNumber,@vDateOrder,@vRequiredDate,@vPONumber,[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ((ISNULL([Renew],0) = 1) OR (ISNULL([ChangeQty],0) = 1))
                    AND (ISNULL([NotAccept],0) = 0)
                    AND ([PlanningOrder] = @vPlanningOrder);

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate])
                    SELECT [CusNum],[CusName],[DeltoId],[Delto],@vDateOrder,@vRequiredDate,[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],NULL,NULL,0,[PcsOrder],0,[CTNOrder],[TotalPcsOrder],@vPONumber,[MachineName],@vTakeOrderNumber,[PlanningOrder],0,[Department],[IPAddress],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ((ISNULL([Renew],0) = 1) OR (ISNULL([ChangeQty],0) = 1))
                    AND (ISNULL([NotAccept],0) = 0)
                    AND ([PlanningOrder] = @vPlanningOrder);

                    UPDATE v
                    SET v.Renew = 0, v.ChangeQty = 0, v.RequiredDate = @vRequiredDate
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] AS v
                    WHERE (v.[Department] = @vDepartment)
                    --AND (v.[CusNum] = @vCusNum)
                    --AND (v.[DeltoId] = @vDeltoId)
                    AND ((ISNULL([Renew],0) = 1) OR (ISNULL([ChangeQty],0) = 1))
                    AND (ISNULL([NotAccept],0) = 0)
                    AND (v.[PlanningOrder] = @vPlanningOrder);
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vTakeOrderNumber, vDateOrder, vRequiredDate, vPONumber)
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RCom.CommandText = "UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 0,[PrintInvNo] = " & vInvNo & "; "
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                MessageBox.Show("Processing Take Order have been completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                vFilterNotRenew = 0
                vDefaultIndex = 0
                DisplayLoading.Enabled = True
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
        End If
    End Sub

    Private Sub CmbProducts_TextChanged(sender As Object, e As EventArgs) Handles CmbProducts.TextChanged
        DisplayLoading.Enabled = True
    End Sub

    Private Sub BtnRetrieveTakeOrder_Click(sender As Object, e As EventArgs) Handles BtnRetrieveTakeOrder.Click
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any planning order!", "Select Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPlanningOrder.Focus()
            Exit Sub
            'ElseIf CmbBillTo.Text.Trim().Equals("") = True Then
            '    MessageBox.Show("Please select any bill to!", "Select Bill To", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbBillTo.Focus()
            '    Exit Sub
            'ElseIf CmbDelto.Text.Trim().Equals("") = True Then
            '    MessageBox.Show("Please select any delto!", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbDelto.Focus()
            '    Exit Sub
        Else
            Dim vPlanning As String = ""
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                vPlanning = ""
            Else
                If CmbPlanningOrder.Text.Trim() = "" Then
                    vPlanning = ""
                Else
                    vPlanning = CmbPlanningOrder.SelectedValue
                End If
            End If
            Dim vCusNum As String = ""
            If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
                vCusNum = ""
            Else
                If CmbBillTo.Text.Trim() = "" Then
                    vCusNum = ""
                Else
                    vCusNum = CmbBillTo.SelectedValue
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
            query = <SQL>
                        <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                    
                    WITH vTakeOrder AS (
                    SELECT [TakeOrderNumber]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderFinish]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder))
                    SELECT [PONumber] + ' ( ' + CONVERT(NVARCHAR,[DateRequired]) + ' ) ' AS [PONumber],[DateRequired]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] 
                    WHERE [TakeOrderNumber] IN (SELECT vTakeOrder.TakeOrderNumber FROM vTakeOrder)
                    GROUP BY [PONumber] + ' ( ' + CONVERT(NVARCHAR,[DateRequired]) + ' ) ', [DateRequired]
                    ORDER BY [DateRequired] DESC;
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
            Dim vDateRequired As Date = Todate
            Dim vFrm As New FrmDutchmillTakeOrderRetrieve With {.vDateRequired = vDateRequired, .vList = lists, .vCusNum = vCusNum, .vDeltoId = vDeltoId, .vPlanning = vPlanning, .vDepartment = vDepartment}
            If vFrm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            vDateRequired = vFrm.vDateRequired
            query = <SQL>
                        <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';
                    DECLARE @vDateRequired AS DATE = N'{5:yyyy-MM-dd}';

                    UPDATE v
                    SET v.[PcsOrder] = x.PcsOrder
                    ,v.[CTNOrder] = x.CTNOrder
                    ,v.[TotalPcsOrder] = x.TotalPcsOrder
                    ,v.[Renew] = x.Renew
                    ,v.[NotAccept] = x.NotAccept
                    ,v.[ChangeQty] = x.ChangeQty
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] AS v
                    INNER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderFinish] AS x ON x.Barcode = v.Barcode AND x.CusNum = v.CusNum AND x.DeltoId = v.DeltoId AND x.Department = v.Department AND x.PlanningOrder = v.PlanningOrder
                    WHERE (x.[Department] = @vDepartment)
                    --AND (x.[CusNum] = @vCusNum) 
                    --AND (x.[DeltoId] = @vDeltoId)
                    AND (x.[PlanningOrder] = @vPlanningOrder)
                    AND (DATEDIFF(DAY,x.[DateRequired],@vDateRequired) = 0);
                    
                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderRetrieve]([TakeOrderNumber],[DateOrd],[DateRequired],[PONumber],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[FinishDate],[RetrievedDate])
                    SELECT [TakeOrderNumber],[DateOrd],[DateRequired],[PONumber],[CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[FinishDate],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderFinish]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0);

                    DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrderFinish]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0);

                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_Deleted]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeletedDate])
                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],GETDATE()
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum)
                    --AND ([DelToId] = @vDeltoId) 
                    AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0);

                    DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DelToId] = @vDeltoId) 
                    AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0);
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vDateRequired)
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                MessageBox.Show("Retrieving Take Order have been completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                vFilterNotRenew = 0
                vDefaultIndex = 0
                DisplayLoading.Enabled = True
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
        End If
    End Sub

    Private Sub DgvShow_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DgvShow.RowPostPaint
        With DgvShow.Rows(e.RowIndex)
            If CBool(.Cells("ManualRenew").Value) = False And CBool(.Cells("ManualChangeQty").Value) = False Then
                If CBool(.Cells("ManualNotAccept").Value) = True Then
                    .DefaultCellStyle.ForeColor = Color.Gray
                Else
                    .DefaultCellStyle.ForeColor = Color.Red
                End If
            Else
                If CBool(.Cells("ManualNotAccept").Value) = True Then
                    .DefaultCellStyle.ForeColor = Color.Gray
                Else
                    .DefaultCellStyle.ForeColor = Color.Black
                End If
            End If
            If (e.RowIndex Mod 2) = 0 Then .DefaultCellStyle.BackColor = Color.LightGray
        End With
    End Sub

    Private Sub DgvShow_Sorted(sender As Object, e As EventArgs) Handles DgvShow.Sorted
        For Each vRow As DataGridViewRow In DgvShow.Rows
            vRow.Cells("ManualRenew").Value = CBool(vRow.Cells("Renew").Value)
            vRow.Cells("ManualNotAccept").Value = CBool(vRow.Cells("NotAccept").Value)
            vRow.Cells("ManualChangeQty").Value = CBool(vRow.Cells("ChangeQty").Value)
        Next
    End Sub

    Private Sub MnuChangeCustomer_Click(sender As Object, e As EventArgs) Handles MnuChangeCustomer.Click
        Me.Popmain.Close()
        If DgvShow.Rows.Count <= 0 Then Exit Sub
        Dim vPlanning As String = ""
        If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
            vPlanning = ""
        Else
            If CmbPlanningOrder.Text.Trim() = "" Then
                vPlanning = ""
            Else
                vPlanning = CmbPlanningOrder.SelectedValue
            End If
        End If
        vDefaultIndex = DgvShow.CurrentRow.Index
        Dim vCusNum As String = Trim(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value) = True, "", DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusNum").Value))
        Dim vCusName As String = Trim(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusName").Value) = True, "", DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("CusName").Value))
        Dim vDeltoId As Decimal = CDec(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("DeltoId").Value) = True, 0, DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("DeltoId").Value))
        Dim vFrm As New FrmDutchmillTakeOrderCustomer With {.vCusNum = vCusNum, .vCusName = vCusName, .vDepartment = vDepartment, .vPlanning = vPlanning, .vDeltoId = vDeltoId}
        If vFrm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        CmbBillTo.SelectedIndex = -1
        CmbDelto.SelectedIndex = -1
        DisplayLoading.Enabled = True
    End Sub

    Private Sub DgvShow_MouseDown(sender As Object, e As MouseEventArgs) Handles DgvShow.MouseDown
        Me.Popmain.Close()
        If DgvShow.Rows.Count <= 0 Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Right Then Me.Popmain.Show(DgvShow, New System.Drawing.Point(x:=e.X, y:=e.Y))
    End Sub

    Private Sub DgvShow_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvShow.CellMouseMove
        'If vCheckBox Is Nothing Then Exit Sub
        'Dim vMousePosition As Point = DgvShow.PointToClient(Control.MousePosition)
        'Dim vInfo As DataGridView.HitTestInfo
        'vInfo = DgvShow.HitTest(vMousePosition.X, vMousePosition.Y)
        'If vInfo.RowY = 1 And vInfo.ColumnX = 26 Then
        '    vCheckBox.Visible = True
        'Else
        '    If vCheckBox.Checked = False Then vCheckBox.Visible = False
        'End If
    End Sub

    Private Sub BtnViewCredit_Click(sender As Object, e As EventArgs) Handles BtnViewCredit.Click
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any planning order!", "Select Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPlanningOrder.Focus()
            Exit Sub
        End If
        Dim vPlanning As String = ""
        If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
            vPlanning = ""
        Else
            If CmbPlanningOrder.Text.Trim() = "" Then
                vPlanning = ""
            Else
                vPlanning = CmbPlanningOrder.SelectedValue
            End If
        End If
        Dim vFrm As New FrmDutchmillTakeOrderViewSummaryCredit With {.vPlanning = vPlanning, .vDepartment = vDepartment}
        If vFrm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then Exit Sub
    End Sub

    Private Sub picplanningorder_Click(sender As Object, e As EventArgs) Handles picplanningorder.Click
        Dim oFrm As New FrmPlanningOrder With {.WindowState = FormWindowState.Normal, .vDepartment = vDepartment}
        oFrm.ShowDialog()
        Me.CmbPlanningOrder.SelectedIndex = -1
        Me.PlanningOrderLoading.Enabled = True
    End Sub
End Class