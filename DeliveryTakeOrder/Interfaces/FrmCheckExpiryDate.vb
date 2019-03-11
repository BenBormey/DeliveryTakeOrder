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

Public Class FrmCheckExpiryDate
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
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
    Public Property vBarcode As String
    Public Property vCusNum As String
    Public Property vCusname As String

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

    Private Sub DgvShow_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DgvShow.RowPrePaint
        Dim DateExpiry As Date
        Dim DateAlert As Date
        Dim CurDate As Date
        With DgvShow.Rows(e.RowIndex)
            DateExpiry = CDate(IIf(IsDBNull(.Cells("Expiry").Value) = True, Todate, .Cells("Expiry").Value))
            CurDate = CDate(IIf(IsDBNull(.Cells("CurDate").Value) = True, Todate, .Cells("CurDate").Value))
            If DateDiff(DateInterval.Day, CurDate, DateExpiry) <= 0 Then
                .DefaultCellStyle.ForeColor = Color.Red
            Else
                .DefaultCellStyle.ForeColor = Color.Black
            End If
        End With
    End Sub

    Private Sub FrmCheckExpiryDate_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Me.Dispose()
    End Sub

    Private Sub FrmCheckExpiryDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        Me.Loading.Enabled = True
    End Sub

    Private Sub Loading_Tick(sender As Object, e As EventArgs) Handles Loading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.Loading.Enabled = False
        query = <SQL><![CDATA[
                    DECLARE @vBarcode AS NVARCHAR(15) = N'{1}';
                    SELECT i.[Expiry],SUM(i.[QtyOnHand]) [Stock],o.[Status],GETDATE() [CurDate]
                    FROM [Stock].[dbo].[TPRWarehouseStockIn] i
					LEFT OUTER JOIN (
					SELECT [id],[locationname],[location],[level],N'Not For Sell' [Status]
					FROM [DBWarehouses].[dbo].[.tbllocation.not.for.sell]
					) o ON (o.locationname = i.LocName) AND (o.location = i.Location) AND (o.level = i.LocationLevel)                  
                    WHERE ([ProNumy] = @vBarcode)
                    GROUP BY [Expiry],o.[Status]
                    ORDER BY [Expiry];
                ]]></SQL>
        query = String.Format(query, DatabaseName, vBarcode)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Dim vreport As DeliveryTakeOrder.sExpiryDate
        Dim vadapter As OleDbDataAdapter
        Dim vds As DataSet
        Dim vtool As ReportPrintTool
        query = <SQL>
                    <![CDATA[                
                        DECLARE @oBarcode AS NVARCHAR(15) = N'{1}';
                        WITH s AS (
	                        SELECT i.[ProID],i.[ProNumY],i.[ProNumYP],i.[ProNumYC],i.[Sup1],i.[ProName],i.[KhmerNameUnicode],i.[ProPacksize],i.[ProQtyPCase],i.[ProQtyPPack],i.[ProTotQty],N'' [Status]
	                        FROM [Stock].[dbo].[TPRProducts] i
	                        WHERE (ISNULL(i.[ProNumY],N'') = @oBarcode) OR (ISNULL(i.[ProNumYP],N'') = @oBarcode) OR (ISNULL(i.[ProNumYC],N'') = @oBarcode)
	                        UNION ALL
	                        SELECT i.[ProID],i.[ProNumY],i.[ProNumYP],i.[ProNumYC],i.[Sup1],i.[ProName],i.[KhmerNameUnicode],i.[ProPacksize],i.[ProQtyPCase],i.[ProQtyPPack],i.[ProTotQty],N'DC' [Status]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated] i
	                        WHERE (ISNULL(i.[ProNumY],N'') = @oBarcode) OR (ISNULL(i.[ProNumYP],N'') = @oBarcode) OR (ISNULL(i.[ProNumYC],N'') = @oBarcode)
	                        UNION ALL
	                        SELECT i.[ProID],v.[OldProNumy] [ProNumY],i.[ProNumYP],i.[ProNumYC],i.[Sup1],i.[ProName],i.[KhmerNameUnicode],i.[ProPacksize],i.[ProQtyPCase],i.[ProQtyPPack],v.[Stock] [ProTotQty],N'Old Code' [Status]
	                        FROM [Stock].[dbo].[TPRProducts] i 
	                        INNER JOIN [Stock].[dbo].[TPRProductsOldCode] v ON v.ProId = i.ProID
	                        WHERE (ISNULL(i.[ProNumY],N'') = @oBarcode) OR (ISNULL(i.[ProNumYP],N'') = @oBarcode) OR (ISNULL(i.[ProNumYC],N'') = @oBarcode)
	                        UNION ALL
	                        SELECT i.[ProID],v.[OldProNumy] [ProNumY],i.[ProNumYP],i.[ProNumYC],i.[Sup1],i.[ProName],i.[KhmerNameUnicode],i.[ProPacksize],i.[ProQtyPCase],i.[ProQtyPPack],v.[Stock] [ProTotQty],N'Old Code' [Status]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated] i 
	                        INNER JOIN [Stock].[dbo].[TPRProductsOldCode] v ON v.ProId = i.ProID
	                        WHERE (ISNULL(i.[ProNumY],N'') = @oBarcode) OR (ISNULL(i.[ProNumYP],N'') = @oBarcode) OR (ISNULL(i.[ProNumYC],N'') = @oBarcode)
                        )
                        SELECT s.ProNumY [UnitNumber],
                        s.ProNumYP [PackNumber],
                        s.ProNumYC [CaseNumber],
                        s.ProName [ProName],
                        s.KhmerNameUnicode [KhmerName],
                        s.ProPacksize [Size],
                        s.ProQtyPCase [QtyPerCase],
                        s.ProQtyPPack [QtyPerPack],
                        s.ProTotQty [StockIn],
                        LEFT(s.Sup1,8) [SupNum],
                        RTRIM(LTRIM(SUBSTRING(s.Sup1,9,LEN(s.Sup1)))) [SupName],
						o.[locationname],
						o.[location],
						o.[level],
                        w.QtyOnHand [QtyOnHand],
                        w.Expiry [ExpiryDate],
                        o.Status [Status]
						INTO #oExpiryList
                        FROM s
                        LEFT OUTER JOIN [Stock].[dbo].[TPRWarehouseStockIn] w ON (w.ProNumy = s.ProNumY)
                        LEFT OUTER JOIN (
					    SELECT [id],[locationname],[location],[level],N'Not For Sell' [Status]
					    FROM [DBWarehouses].[dbo].[.tbllocation.not.for.sell]
					    ) o ON (o.locationname = w.LocName) AND (o.location = w.Location) AND (o.level = w.LocationLevel) 
                        ORDER BY s.ProNumY,w.Expiry,CONVERT(DECIMAL,w.LocationLevel) DESC,w.Location,w.LocName;

						SELECT s.[UnitNumber],
                        s.[PackNumber],
                        s.[CaseNumber],
                        s.[ProName],
                        s.[KhmerName],
                        s.[Size],
                        s.[QtyPerCase],
                        s.[QtyPerPack],
                        s.[StockIn],
                        s.[SupNum],
                        s.[SupName],
						s.[locationname],
						s.[location],
						s.[level],
                        SUM(s.[QtyOnHand]) [QtyOnHand],
                        CONVERT(DATE,s.[ExpiryDate]) [ExpiryDate],
                        s.[Status]
						FROM #oExpiryList s
						GROUP BY s.[UnitNumber],
                        s.[PackNumber],
                        s.[CaseNumber],
                        s.[ProName],
                        s.[KhmerName],
                        s.[Size],
                        s.[QtyPerCase],
                        s.[QtyPerPack],
                        s.[StockIn],
                        s.[SupNum],
                        s.[SupName],
						s.[locationname],
						s.[location],
						s.[level],
                        CONVERT(DATE,s.[ExpiryDate]),
                        s.[Status]
						ORDER BY s.[UnitNumber],CONVERT(DATE,s.[ExpiryDate]);

						DROP TABLE #oExpiryList;
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vBarcode)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))

        vreport = New DeliveryTakeOrder.sExpiryDate
        vadapter = New OleDbDataAdapter
        vds = New DataSet
        vtool = New ReportPrintTool(vreport)
        vreport.Parameters("companyname").Value = String.Format("{0}{1}{2}", Initialized.R_CompanyKhmerName, vbCrLf, Initialized.R_CompanyName)
        vreport.Parameters("companyaddress").Value = String.Format("{0}{1}{2}{1}Tel:{3}", Initialized.R_CompanyKhmAddress.Replace(vbCrLf, "").Trim(), vbCrLf, Initialized.R_CompanyAddress.Replace(vbCrLf, "").Trim(), Initialized.R_CompanyTelephone)
        vreport.Parameters("currentdate").Value = Todate
        vreport.Parameters("cusnum").Value = vCusNum.Trim()
        vreport.Parameters("cusname").Value = vCusname.Trim()
        vreport.DataSource = lists
        vreport.DataAdapter = vadapter
        vreport.DataMember = "dtExpiryDate"
        vreport.RequestParameters = False
        vtool.AutoShowParametersPanel = False
        vtool.PrinterSettings.Copies = 1
        vtool.ShowRibbonPreviewDialog()
    End Sub
End Class