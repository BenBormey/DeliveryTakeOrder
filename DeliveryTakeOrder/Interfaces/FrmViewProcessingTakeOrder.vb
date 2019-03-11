Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Data.OleDb
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI

Public Class FrmViewProcessingTakeOrder
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private DatabaseName As String
    Private Todate As Date
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Private vquery As String
    Private vlist As DataTable

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

    Private Sub FrmFixedAssetPayment_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        LoadingInitialized()
    End Sub

    Private Sub FrmFixedAssetPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        oBarcode_Search = ""
        Me.CustomerLoading.Enabled = True
    End Sub

    Private Sub FrmFixedAssetPayment_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private oDateFrom As Date
    Private oDateTo As Date
    Private oAllUnpaid As Boolean
    Private Sub CmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCustomer.SelectedIndexChanged
        If TypeOf CmbCustomer.SelectedValue Is DataRowView Or CmbCustomer.SelectedValue Is Nothing Then Exit Sub
        If CmbCustomer.Text.Trim().Equals("") = True Then Exit Sub
        Dim vCusNum As String = CmbCustomer.SelectedValue
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Initialized.R_AllUnpaid = True
        Initialized.R_DateFrom = Todate
        Initialized.R_DateTo = Todate
        oDateFrom = Todate
        oDateTo = Todate
        oAllUnpaid = True
        vquery = <SQL>
                     <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        WITH v AS (
	                        SELECT MIN([DateRequired]) [InvDate]
	                        FROM [Stock].[dbo].[TPRDeliveryTakeOrder]
	                        WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
                            UNION ALL
	                        SELECT MIN([DateRequired]) [InvDate]
	                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaitingPicking]
	                        WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
	                        UNION ALL
	                        SELECT MIN([DateRequired]) [InvDate]
	                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaiting]
	                        WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
	                        UNION ALL
	                        SELECT MIN([DateRequired]) [InvDate]
	                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrint]
	                        WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
                            UNION ALL
	                        SELECT MIN([DateRequired]) [InvDate]
	                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintFinish]
	                        WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
                        )
                        SELECT MIN([InvDate]) [InvDate]
                        FROM v;
                    ]]>
                 </SQL>
        vquery = String.Format(vquery, DatabaseName, vCusNum)
        vlist = Data.Selects(vquery, Initialized.GetConnectionType(Data, App))
        Initialized.R_AllUnpaid = True
        Initialized.R_IsCancel = True
        Dim oFr As New FrmSelectedPayment
        oFr.DTable = vlist
        oFr.ShowDialog()
        If Initialized.R_IsCancel = True Then Exit Sub
        oDateFrom = Initialized.R_DateFrom
        oDateTo = Initialized.R_DateTo
        oAllUnpaid = Initialized.R_AllUnpaid
        Me.DeltoLoading.Enabled = True
        Me.displayloading.Enabled = True
    End Sub

    Private Sub CmbInvoiceNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDate.SelectedIndexChanged
        If TypeOf CmbDate.SelectedValue Is DataRowView Or CmbDate.SelectedValue Is Nothing Then Exit Sub
        Me.displayloading.Enabled = True
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnExportToExcel_Click(sender As Object, e As EventArgs) Handles BtnExportToExcel.Click
        If DgvShow.RowCount > 0 Then
            Dim RExcel As Excel.Application
            Dim RBook As Excel.Workbook
            Dim RSheet As Excel.Worksheet
            RExcel = CreateObject("Excel.Application")
            RBook = RExcel.Workbooks.Add(Type.Missing)
            RSheet = RBook.Worksheets(1)
            RSheet.Range("A:Z").Font.Name = "Arial"
            RSheet.Range("A:Z").Font.Size = 8
            RSheet.Range("A1").Value = "Procesing Take Order Report"
            RSheet.Range("A2").Value = "Export Date : " & Format(Now(), "dd-MMM-yy")
            RSheet.Range("A4").Value = "Nº"
            RSheet.Range("B4").Value = "Customer No."
            RSheet.Range("C4").Value = "Customer Name"
            RSheet.Range("D4").Value = "Delto"
            RSheet.Range("E4").Value = "Order Date"
            RSheet.Range("F4").Value = "Required Date"
            RSheet.Range("G4").Value = "Delivery Date"
            RSheet.Range("H4").Value = "Barcode"
            RSheet.Range("I4").Value = "Description"
            RSheet.Range("J4").Value = "Size"
            RSheet.Range("K4").Value = "Q/C"
            RSheet.Range("L4").Value = "PCS Free"
            RSheet.Range("M4").Value = "PCS Ord."
            RSheet.Range("N4").Value = "CTN Ord."
            RSheet.Range("O4").Value = "Total PCs Ord."
            RSheet.Range("P4").Value = "P.O Number"
            RSheet.Range("Q4").Value = "T.O Number"
            RSheet.Range("R4").Value = "Picking Number"
            RSheet.Range("S4").Value = "Status"
            RSheet.Range("G:G").NumberFormat = "@"
            RSheet.Range("C:C").NumberFormat = "dd-MMM-yy"
            'RSheet.Range("O:O, P:P").NumberFormat = "_(* #,##0.00_);_(* (#,##0.00);_(* ""-""??_);_(@_)"
            RSheet.Range("A4:T4").WrapText = True
            RSheet.Range("A4:T4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            RSheet.Range("A4:T4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            Dim QsRow As Long = 5
            For Each QsDataRow As DataGridViewRow In DgvShow.Rows
                RSheet.Range("A" & QsRow).Value = (QsRow - 4)
                RSheet.Range("B" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CusNum").Value) = True, "", QsDataRow.Cells("CusNum").Value)
                RSheet.Range("C" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CusName").Value) = True, "", QsDataRow.Cells("CusName").Value)
                RSheet.Range("D" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DelTo").Value) = True, "", QsDataRow.Cells("DelTo").Value)
                RSheet.Range("E" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DateOrd").Value) = True, "", QsDataRow.Cells("DateOrd").Value)
                RSheet.Range("F" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DateRequired").Value) = True, "", QsDataRow.Cells("DateRequired").Value)
                RSheet.Range("G" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DeliveryDate").Value) = True, "", QsDataRow.Cells("DeliveryDate").Value)
                RSheet.Range("H" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ProNumy").Value) = True, "", QsDataRow.Cells("ProNumy").Value)
                RSheet.Range("I" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ProName").Value) = True, "", QsDataRow.Cells("ProName").Value)
                RSheet.Range("J" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ProPackSize").Value) = True, "", QsDataRow.Cells("ProPackSize").Value)
                RSheet.Range("K" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ProQtyPCase").Value) = True, "", QsDataRow.Cells("ProQtyPCase").Value)
                RSheet.Range("L" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PcsFree").Value) = True, "", QsDataRow.Cells("PcsFree").Value)
                RSheet.Range("M" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PcsOrder").Value) = True, "", QsDataRow.Cells("PcsOrder").Value)
                RSheet.Range("N" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CTNOrder").Value) = True, "", QsDataRow.Cells("CTNOrder").Value)
                RSheet.Range("O" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("TotalPcsOrder").Value) = True, "", QsDataRow.Cells("TotalPcsOrder").Value)
                RSheet.Range("P" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PONumber").Value) = True, "", QsDataRow.Cells("PONumber").Value)
                RSheet.Range("Q" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("TakeOrderInvoiceNumber").Value) = True, "", QsDataRow.Cells("TakeOrderInvoiceNumber").Value)
                RSheet.Range("R" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PrintInvoiceNumber").Value) = True, "", QsDataRow.Cells("PrintInvoiceNumber").Value)
                RSheet.Range("S" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Status").Value) = True, "", QsDataRow.Cells("Status").Value)
                QsRow = QsRow + 1
            Next
            RSheet.Columns.AutoFit()
            RSheet.Range("A:A").ColumnWidth = 4
            RExcel.Visible = True
            App.ReleaseObject(RSheet)
            App.ReleaseObject(RBook)
            App.ReleaseObject(RExcel)
        End If
    End Sub

    Private Sub CustomerLoading_Tick(sender As Object, e As EventArgs) Handles CustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.CustomerLoading.Enabled = False
        vquery = <SQL>
                     <![CDATA[
                        WITH v AS (
	                        SELECT 0 [Index],N'CUS00000' [CusNum],N'All Customers' [CusName]
	                        UNION ALL 
	                        SELECT 1 [Index],[CusNum],[CusName]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
	                        GROUP BY [CusNum],[CusName]
                            UNION ALL 
	                        SELECT 1 [Index],[CusNum],[CusName]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
	                        GROUP BY [CusNum],[CusName]
                            UNION ALL 
	                        SELECT 1 [Index],[CusNum],[CusName]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
	                        GROUP BY [CusNum],[CusName]
                        )
                        SELECT v.[Index],v.CusNum,v.CusName
                        FROM v
                        GROUP BY v.[Index],v.CusNum,v.CusName
                        ORDER BY v.[Index],v.CusName;
                    ]]>
                 </SQL>
        vquery = String.Format(vquery, DatabaseName)
        vlist = Data.Selects(vquery, Initialized.GetConnectionType(Data, App))
        DataSources(CmbCustomer, vlist, "CusName", "CusNum")
        If CmbCustomer.Items.Count > 0 Then CmbCustomer.SelectedIndex = 0
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DateLoading_Tick(sender As Object, e As EventArgs) Handles DateLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.DateLoading.Enabled = False
        Dim vCusNum As String = ""
        If (CmbCustomer.Text.Trim().Equals("") = True) Or (TypeOf CmbCustomer.SelectedValue Is DataRowView) Or (CmbCustomer.SelectedValue Is Nothing) Then
            vCusNum = ""
        Else
            vCusNum = CmbCustomer.SelectedValue
        End If
        Dim vdeltoid As Decimal = 0
        If (CmbDelto.Text.Trim().Equals("") = True) Or (TypeOf CmbDelto.SelectedValue Is DataRowView) Or (CmbDelto.SelectedValue Is Nothing) Then
            vdeltoid = 0
        Else
            vdeltoid = CmbDelto.SelectedValue
        End If
        vquery = <SQL>
                     <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vdeltoid AS DECIMAL(18,0) = {2};
                        WITH v AS (
	                        SELECT CONVERT(DATE,[daterequired]) [Date]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
                            WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND ([deltoid] = @vdeltoid)
	                        GROUP BY CONVERT(DATE,[daterequired])
                            UNION ALL 
	                        SELECT CONVERT(DATE,[daterequired]) [Date]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
                            WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND ([deltoid] = @vdeltoid)
	                        GROUP BY CONVERT(DATE,[daterequired])
                            UNION ALL 
	                        SELECT CONVERT(DATE,[daterequired]) [Date]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
                            WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND ([deltoid] = @vdeltoid)
	                        GROUP BY CONVERT(DATE,[daterequired])
                        )
                        SELECT v.[Date]
                        FROM v
                        GROUP BY v.[Date]
                        ORDER BY v.[Date];
                    ]]>
                 </SQL>
        vquery = String.Format(vquery, DatabaseName, vCusNum, vdeltoid)
        vlist = Data.Selects(vquery, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDate, vlist, "Date", "Date")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub displayloading_Tick(sender As Object, e As EventArgs) Handles displayloading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.displayloading.Enabled = False
        Dim vCusNum As String = ""
        If (CmbCustomer.Text.Trim().Equals("") = True) Or (TypeOf CmbCustomer.SelectedValue Is DataRowView) Or (CmbCustomer.SelectedValue Is Nothing) Then
            vCusNum = ""
        Else
            vCusNum = CmbCustomer.SelectedValue
        End If
        Dim vdeltoid As Decimal = 0
        If (CmbDelto.Text.Trim().Equals("") = True) Or (TypeOf CmbDelto.SelectedValue Is DataRowView) Or (CmbDelto.SelectedValue Is Nothing) Then
            vdeltoid = 0
        Else
            vdeltoid = CmbDelto.SelectedValue
        End If
        Dim vDate As String = ""
        If (CmbDate.Text.Trim().Equals("") = True) Or (TypeOf CmbDate.SelectedValue Is DataRowView) Or (CmbDate.SelectedValue Is Nothing) Then
            vDate = ""
        Else
            vDate = CmbDate.SelectedValue
        End If
        vquery = <SQL>
                     <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vdeltoid AS DECIMAL(18,0) = {2};
                        DECLARE @vRequiredDate AS NVARCHAR(MAX) = N'{3}';
                        DECLARE @vDateFrom AS DATE = N'{4:yyyy-MM-dd}';
                        DECLARE @vDateTo AS DATE = N'{5:yyyy-MM-dd}';
                        DECLARE @vbarcode AS NVARCHAR(15) = N'{8}';
                        IF (@vRequiredDate = N'') SET @vRequiredDate = CONVERT(NVARCHAR,CONVERT(DATE,GETDATE()));
                        WITH v AS (
	                        SELECT [ID],[CusName],[CusNum],[DelTo],[dateorder] [DateOrd],CONVERT(DATE,[DateRequired]) [DateRequired],[DeliveryDate],[barcode] [ProNumy],[ProName],[size] [ProPackSize],[qtypercase] [ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[takeordernumber] [TakeOrderInvoiceNumber],null [PrintInvoiceNumber],[PromotionMachanic],[category] [ProCat],[ItemDiscount],[remark] [RemarkExpiry],NULL [RelatedKey],[Saleman],N'' [Status]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
	                        WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND (([deltoid] = @vdeltoid) OR (0 = @vdeltoid))
	                        {7} AND ((CONVERT(DATE,[DateRequired]) = CONVERT(DATE,@vRequiredDate)) OR (CONVERT(DATE,GETDATE()) = CONVERT(DATE,@vRequiredDate)))
	                        {6} AND (CONVERT(DATE,[DateRequired]) BETWEEN @vDateFrom AND @vDateTo)
                            {9} AND (([barcode] = @vbarcode) OR (N'' = @vbarcode))
	                        UNION ALL
                            SELECT [ID],[CusName],[CusNum],[DelTo],[dateorder] [DateOrd],CONVERT(DATE,[DateRequired]) [DateRequired],[DeliveryDate],[barcode] [ProNumy],[ProName],[size] [ProPackSize],[qtypercase] [ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[takeordernumber] [TakeOrderInvoiceNumber],[pickingnumber] [PrintInvoiceNumber],[PromotionMachanic],[category] [ProCat],[ItemDiscount],[remark] [RemarkExpiry],NULL [RelatedKey],[Saleman],N'Picking T.0 @ ' + CONVERT(NVARCHAR,CONVERT(DATE,[pickingdate])) [Status]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
	                        WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND (([deltoid] = @vdeltoid) OR (0 = @vdeltoid))
	                        {7} AND ((CONVERT(DATE,[DateRequired]) = CONVERT(DATE,@vRequiredDate)) OR (CONVERT(DATE,GETDATE()) = CONVERT(DATE,@vRequiredDate)))
	                        {6} AND (CONVERT(DATE,[DateRequired]) BETWEEN @vDateFrom AND @vDateTo)
                            {9} AND (([barcode] = @vbarcode) OR (N'' = @vbarcode))
	                        UNION ALL
                            SELECT [ID],[CusName],[CusNum],[DelTo],[dateorder] [DateOrd],CONVERT(DATE,[DateRequired]) [DateRequired],[DeliveryDate],[barcode] [ProNumy],[ProName],[size] [ProPackSize],[qtypercase] [ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[takeordernumber] [TakeOrderInvoiceNumber],[pickingnumber] [PrintInvoiceNumber],[PromotionMachanic],[category] [ProCat],[ItemDiscount],[remark] [RemarkExpiry],NULL [RelatedKey],[Saleman],N'Invoicing @ ' + CONVERT(NVARCHAR,CONVERT(DATE,[invoicingdate])) [Status]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
	                        WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND (([deltoid] = @vdeltoid) OR (0 = @vdeltoid))
	                        {7} AND ((CONVERT(DATE,[DateRequired]) = CONVERT(DATE,@vRequiredDate)) OR (CONVERT(DATE,GETDATE()) = CONVERT(DATE,@vRequiredDate)))
	                        {6} AND (CONVERT(DATE,[DateRequired]) BETWEEN @vDateFrom AND @vDateTo)
                            {9} AND (([barcode] = @vbarcode) OR (N'' = @vbarcode))
	                        {6}UNION ALL
                            {6}SELECT [ID],[CusName],[CusNum],[DelTo],[dateorder] [DateOrd],CONVERT(DATE,[DateRequired]) [DateRequired],[DeliveryDate],[barcode] [ProNumy],[ProName],[size] [ProPackSize],[qtypercase] [ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[takeordernumber] [TakeOrderInvoiceNumber],[pickingnumber] [PrintInvoiceNumber],[PromotionMachanic],[category] [ProCat],[ItemDiscount],[remark] [RemarkExpiry],NULL [RelatedKey],[Saleman],N'Finish @ ' + CONVERT(NVARCHAR,CONVERT(DATE,[finishdate])) [Status]
	                        {6}FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.finish]
	                        {6}WHERE (([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)) AND (([deltoid] = @vdeltoid) OR (0 = @vdeltoid))
	                        {7}{6} AND ((CONVERT(DATE,[DateRequired]) = CONVERT(DATE,@vRequiredDate)) OR (CONVERT(DATE,GETDATE()) = CONVERT(DATE,@vRequiredDate)))
	                        {6} AND (CONVERT(DATE,[DateRequired]) BETWEEN @vDateFrom AND @vDateTo)
                            {9} AND (([barcode] = @vbarcode) OR (N'' = @vbarcode))
                        )
                        SELECT v.[ID],v.[CusName],v.[CusNum],v.[DelTo],v.[DateOrd],v.[DateRequired],v.[DeliveryDate],v.[ProNumy],v.[ProName],v.[ProPackSize],v.[ProQtyPCase],v.[PcsFree],v.[PcsOrder],v.[CTNOrder],v.[TotalPcsOrder],v.[PONumber],v.[LogInName],v.[TakeOrderInvoiceNumber],v.[PrintInvoiceNumber],v.[PromotionMachanic],v.[ProCat],v.[ItemDiscount],v.[RemarkExpiry],v.[RelatedKey],v.[Saleman],v.[Status]
                        FROM v
                        ORDER BY v.[DateRequired],v.[CusName],v.[ProName];
                    ]]>
                 </SQL>
        vquery = String.Format(vquery, DatabaseName, vCusNum, vdeltoid, vDate, oDateFrom, oDateTo, IIf(oAllUnpaid = True, "--", ""), IIf(oAllUnpaid = True, "", "--"), oBarcode_Search, IIf(oBarcode_Search.Trim().Equals("") = True, "--", ""))
        vlist = Data.Selects(vquery, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = vlist
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0:N0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbDelto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDelto.SelectedIndexChanged
        Me.DateLoading.Enabled = True
    End Sub

    Private Sub DeltoLoading_Tick(sender As Object, e As EventArgs) Handles DeltoLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.DeltoLoading.Enabled = False
        Dim vCusNum As String = ""
        If (CmbCustomer.Text.Trim().Equals("") = True) Or (TypeOf CmbCustomer.SelectedValue Is DataRowView) Or (CmbCustomer.SelectedValue Is Nothing) Then
            vCusNum = ""
        Else
            vCusNum = CmbCustomer.SelectedValue
        End If
        vquery = <SQL>
                     <![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        WITH v AS (
	                        SELECT [deltoid],[DelTo]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
                            WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
	                        GROUP BY [deltoid],[DelTo]
                            UNION ALL 
	                        SELECT [deltoid],[DelTo]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
                            WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
	                        GROUP BY [deltoid],[DelTo]
                            UNION ALL 
	                        SELECT [deltoid],[DelTo]
	                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
                            WHERE ([CusNum] = @vCusNum) OR (N'CUS00000' = @vCusNum)
	                        GROUP BY [deltoid],[DelTo]
                        )
                        SELECT v.[deltoid],v.[DelTo]
                        FROM v
                        GROUP BY v.[deltoid],v.[DelTo]
                        ORDER BY v.[DelTo];
                    ]]>
                 </SQL>
        vquery = String.Format(vquery, DatabaseName, vCusNum)
        vlist = Data.Selects(vquery, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, vlist, "DelTo", "deltoid")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtbarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbarcode.KeyPress
        If e.KeyChar = ChrW(Keys.Back) Then Exit Sub
        If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) Or (e.KeyChar >= "a" And e.KeyChar <= "z") Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
            If txtbarcode.Text.Trim() <> "" And IsNumeric(txtbarcode.Text.Trim()) = False Then e.Handled = True
            e.KeyChar = UCase(e.KeyChar)
        Else
            App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 15)
        End If
    End Sub

    Private Sub txtbarcode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtbarcode.PreviewKeyDown
        If e.KeyCode = Keys.Return Then BtnSearch_Click(BtnSearch, New System.EventArgs())
    End Sub

    Private oBarcode_Search As String
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        oBarcode_Search = ""
        If txtbarcode.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please enter the barcode which you want to search...", "Enter Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtbarcode.Focus()
            Exit Sub
        End If
        oBarcode_Search = txtbarcode.Text.Trim()
        CmbCustomer_SelectedIndexChanged(CmbCustomer, e)
    End Sub

    Private Sub txtbarcode_TextChanged(sender As Object, e As EventArgs) Handles txtbarcode.TextChanged
        oBarcode_Search = ""
    End Sub
End Class