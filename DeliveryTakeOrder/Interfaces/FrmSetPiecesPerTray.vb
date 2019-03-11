Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmSetPiecesPerTray
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

    Private Sub FrmSetPiecesPerTray_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmSetPiecesPerTray_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        ItemLoading.Enabled = True
        DisplayLoading.Enabled = True
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub DisplayLoading_Tick(sender As Object, e As EventArgs) Handles DisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DisplayLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                SELECT [Id],[Barcode],[ProName],[Size],[QtyPCase],[PiecesPerTray],[CreatedDate]
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting]
                ORDER BY [ProName],[Size];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FrmSetPiecesPerTray_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private Sub ItemLoading_Tick(sender As Object, e As EventArgs) Handles ItemLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        ItemLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[                
                SELECT [Barcode],[ProName],[Display]
                FROM (
	                SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
                    WHERE ISNULL([ProNumY],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumY],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE ISNULL([ProNumYP],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumYP],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE ISNULL([ProNumYC],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumYC],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL
                    SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProductsDeactivated]
                    WHERE ISNULL([ProNumY],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumY],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                WHERE ISNULL([ProNumYP],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumYP],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                WHERE ISNULL([ProNumYC],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([ProNumYC],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                UNION ALL 
	                SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                    WHERE ISNULL([OldProNumy],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([OldProNumy],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                    UNION ALL 
	                SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                    WHERE ISNULL([OldProNumy],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    AND ISNULL([OldProNumy],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting])
                    GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                ) Lists
                GROUP BY [Barcode],[ProName],[Display]
                ORDER BY [ProName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbProducts, lists, "Display", "Barcode")
        Me.Cursor = Cursors.Default
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
        For i As Integer = 0 To TableProducts.Rows.Count - 1
            Dim displayItem As String = TableProducts.Rows(i)(ComboName.DisplayMember).ToString()
            Dim valueItem As String = TableProducts.Rows(i)(ComboName.ValueMember).ToString()
            valueItem = Trim(Mid(valueItem, 1, RBarcode.Length))
            If valueItem.Equals(RBarcode) = True Then
                DirectCast(ComboName, ComboBox).SelectedIndex = i
                Exit For
            End If
        Next
        TxtPiecesPerTray.Focus()
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

    Private Sub TxtPiecesPerTray_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPiecesPerTray.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtPiecesPerTray_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtPiecesPerTray.PreviewKeyDown
        If e.KeyCode = Keys.Return Then
            e.IsInputKey = True
            If BtnAdd.Visible = True Then
                BtnAdd_Click(BtnAdd, New System.EventArgs())
            Else
                BtnAdd_Click(BtnUpdate, New System.EventArgs())
            End If
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click, BtnUpdate.Click
        If CmbProducts.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any product!", "Select Product", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbProducts.Focus()
            Exit Sub
        ElseIf CDec(IIf(TxtPiecesPerTray.Text.Trim().Equals("") = True, 0, TxtPiecesPerTray.Text.Trim())) = 0 Then
            MessageBox.Show("Please enter the pieces per tray!", "Enter Pieces Per Tray", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtPiecesPerTray.Focus()
            Exit Sub
        Else
            Dim vBarcode As String
            Dim vPiecePerTray As Decimal = CDec(IIf(TxtPiecesPerTray.Text.Trim().Equals("") = True, 0, TxtPiecesPerTray.Text.Trim()))
            If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then
                vBarcode = ""
            Else
                If CmbProducts.Text.Trim() = "" Then
                    vBarcode = ""
                Else
                    vBarcode = CmbProducts.SelectedValue
                End If
            End If
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand()
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                If sender Is BtnAdd Then
                    query = _
                    <SQL>
                        <![CDATA[
                            DECLARE @vBarcode AS NVARCHAR(MAX) = N'{1}';
                            DECLARE @vPiecesPerTray AS DECIMAL(18,0) = {2};
                            WITH vItem AS (
	                            SELECT [ProNumY] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumY],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYP] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumYP],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYC] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumYC],'') = @vBarcode)
	                            UNION ALL
	                            SELECT B.[OldProNumy] AS [Barcode],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                            WHERE B.[OldProNumy] = @vBarcode
                                UNION ALL
                                SELECT [ProNumY] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumY],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYP] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumYP],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYC] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumYC],'') = @vBarcode)
	                            UNION ALL
	                            SELECT B.[OldProNumy] AS [Barcode],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                            WHERE B.[OldProNumy] = @vBarcode
                            )
                            INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting]([Barcode],[ProName],[Size],[QtyPCase],[PiecesPerTray],[CreatedDate])
                            SELECT vItem.Barcode,vItem.ProName,vItem.ProPacksize,vItem.ProQtyPCase,@vPiecesPerTray,GETDATE()
                            FROM vItem
                            WHERE vItem.Barcode = @vBarcode AND N'' <> @vBarcode;
                        ]]>
                    </SQL>
                    query = String.Format(query, DatabaseName, vBarcode, vPiecePerTray)
                Else
                    Dim vId As Decimal = CDec(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value) = True, 0, DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value))
                    query = _
                    <SQL>
                        <![CDATA[
                            DECLARE @vBarcode AS NVARCHAR(MAX) = N'{1}';
                            DECLARE @vPiecesPerTray AS DECIMAL(18,0) = {2};
                            DECLARE @vId AS DECIMAL = {3};
                            WITH vItem AS (
	                            SELECT [ProNumY] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumY],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYP] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumYP],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYC] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts]
	                            WHERE (ISNULL([ProNumYC],'') = @vBarcode)
	                            UNION ALL
	                            SELECT B.[OldProNumy] AS [Barcode],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                            WHERE B.[OldProNumy] = @vBarcode
                                UNION ALL
                                SELECT [ProNumY] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumY],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYP] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumYP],'') = @vBarcode)
	                            UNION ALL
	                            SELECT [ProNumYC] AS [Barcode],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated]
	                            WHERE (ISNULL([ProNumYC],'') = @vBarcode)
	                            UNION ALL
	                            SELECT B.[OldProNumy] AS [Barcode],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName]
	                            FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                            WHERE B.[OldProNumy] = @vBarcode
                            )
                            UPDATE v
                            SET [Barcode] = vItem.Barcode,
                            [ProName] = vItem.ProName,
                            [Size] = vItem.ProPacksize,
                            [QtyPCase] = vItem.ProQtyPCase,
                            [PiecesPerTray] = @vPiecesPerTray
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS v
                            INNER JOIN vItem ON vItem.Barcode = @vBarcode AND v.Id = @vId
                            WHERE (vItem.Barcode = @vBarcode AND N'' <> @vBarcode) AND v.Id = @vId;
                        ]]>
                    </SQL>
                    query = String.Format(query, DatabaseName, vBarcode, vPiecePerTray, vId)
                End If
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                MessageBox.Show("Setting Pieces Per Tray have been completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                DisplayLoading.Enabled = True
                BtnCancel_Click(BtnCancel, e)
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

    Private Sub CmbProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbProducts.SelectedIndexChanged
        TxtQtyPerCase.Text = ""
        If CmbProducts.Text.Trim() = "" Then Exit Sub
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then Exit Sub
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
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtQtyPerCase.Text = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPCase")) = True, 1, lists.Rows(0).Item("ProQtyPCase")))
            Else
                GoTo Check_Item
            End If
        Else
Check_Item:
            MessageBox.Show("The Item is wrong. Please check item again...", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbProducts.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        App.ClearController(TxtQtyPerCase, TxtPiecesPerTray)
        App.SetVisibleController(False, BtnUpdate, BtnCancel)
        App.SetVisibleController(True, BtnAdd)
        App.SetEnableController(True, BtnExportToExcel, DgvShow)
        CmbProducts.Focus()
    End Sub

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            If MessageBox.Show("Are you sure, you want to delete this?(Yes/No)", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
            Dim vId As Decimal = CDec(IIf(IsDBNull(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value) = True, 0, DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value))
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom = New SqlCommand()
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                query = _
                <SQL>
                    <![CDATA[
                        DECLARE @vId AS DECIMAL = {1};
                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting_Deleted]([Barcode],[ProName],[Size],[QtyPCase],[PiecesPerTray],[CreatedDate],[DeletedDate])
                        SELECT [Barcode],[ProName],[Size],[QtyPCase],[PiecesPerTray],[CreatedDate],GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting]
                        WHERE [Id] = @vId;

                        DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting]
                        WHERE [Id] = @vId;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vId)
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
            RSheet.Range("A1").Value = "Account Setting Report"
            RSheet.Range("A2").Value = "Export Date : " & Format(Now(), "dd-MMM-yy")
            RSheet.Range("A4").Value = "Nº"
            RSheet.Range("B4").Value = "Barcode"
            RSheet.Range("C4").Value = "Product Name"
            RSheet.Range("D4").Value = "Size"
            RSheet.Range("E4").Value = "Q/C"
            RSheet.Range("F4").Value = "Pieces Per Tray"
            RSheet.Range("G4").Value = "Created Date"
            RSheet.Range("B:B").NumberFormat = "@"
            RSheet.Range("G:G").NumberFormat = "dd-MMM-yy hh:mm:ss AM/PM"
            RSheet.Range("A4:O4").WrapText = True
            RSheet.Range("A4:O4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            RSheet.Range("A4:O4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            Dim QsRow As Long = 5
            For Each QsDataRow As DataGridViewRow In DgvShow.Rows
                RSheet.Range("A" & QsRow).Value = (QsRow - 4)
                RSheet.Range("B" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Barcode").Value) = True, "", QsDataRow.Cells("Barcode").Value)
                RSheet.Range("C" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ProName").Value) = True, "", QsDataRow.Cells("ProName").Value)
                RSheet.Range("D" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Size").Value) = True, "", QsDataRow.Cells("Size").Value)
                RSheet.Range("E" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("QtyPCase").Value) = True, "", QsDataRow.Cells("QtyPCase").Value)
                RSheet.Range("F" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PiecesPerTray").Value) = True, "", QsDataRow.Cells("PiecesPerTray").Value)
                RSheet.Range("G" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CreatedDate").Value) = True, "", QsDataRow.Cells("CreatedDate").Value)
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

    Private Sub DgvShow_DoubleClick(sender As Object, e As EventArgs) Handles DgvShow.DoubleClick
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim vBarcode As String = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
            Dim vPiecePerTray As Decimal = CDec(IIf(IsDBNull(.Cells("PiecesPerTray").Value) = True, 0, .Cells("PiecesPerTray").Value))
            query = _
            <SQL>
                <![CDATA[
                    DECLARE @vBarcode AS NVARCHAR(MAX) = N'{1}';
                    SELECT [Barcode],[ProName],[Display]
                    FROM (
	                    SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
                        WHERE ISNULL([ProNumY],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumY],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE ISNULL([ProNumYP],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumYP],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE ISNULL([ProNumYC],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumYC],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL
                        SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
                        WHERE ISNULL([ProNumY],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumY],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE ISNULL([ProNumYP],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumYP],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE ISNULL([ProNumYC],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([ProNumYC],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                        WHERE ISNULL([OldProNumy],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([OldProNumy],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                        UNION ALL 
	                    SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                        WHERE ISNULL([OldProNumy],'') <> '' AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                        AND ISNULL([OldProNumy],'') NOT IN (SELECT [Barcode] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] WHERE [Barcode] <> @vBarcode)
                        GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                    ) Lists
                    GROUP BY [Barcode],[ProName],[Display]
                    ORDER BY [ProName];
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, vBarcode)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            DataSources(CmbProducts, lists, "Display", "Barcode")
            CmbProducts.SelectedValue = vBarcode
            TxtPiecesPerTray.Text = String.Format("{0:N0}", vPiecePerTray)
            App.SetVisibleController(True, BtnUpdate, BtnCancel)
            App.SetVisibleController(False, BtnAdd)
            App.SetEnableController(False, BtnExportToExcel, DgvShow)
        End With
    End Sub
End Class