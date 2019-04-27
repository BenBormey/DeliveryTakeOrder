Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmProcessTakeOrderPreviewNEdit
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
    Public Property vRequiredDate As Date
    Public Property iPlanningOrder As String

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

    Private Sub FrmProcessTakeOrderPreviewNEdit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmProcessTakeOrderPreviewNEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LoadingInitialized()
        Me.DisplayLoading.Enabled = True
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub DgvShow_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvShow.CellEnter
        If e.ColumnIndex <= 8 Then
            DgvShow.Rows(e.RowIndex).ReadOnly = True
        Else
            DgvShow.Rows(e.RowIndex).ReadOnly = False
        End If
    End Sub

    Private Sub DgvShow_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DgvShow.RowPostPaint
        With DgvShow.Rows(e.RowIndex)
            If (e.RowIndex Mod 2) = 0 Then
                .DefaultCellStyle.BackColor = Color.FromName("ControlLight")
            Else
                .DefaultCellStyle.BackColor = Color.FromName("Control")
            End If
        End With
    End Sub

    Private Sub DisplayLoading_Tick(sender As Object, e As EventArgs) Handles DisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DisplayLoading.Enabled = False
        query = <SQL><![CDATA[
                DECLARE @DateRequired AS DATE = N'{1:yyyy-MM-dd}';
                DECLARE @oPlanningOrder AS NVARCHAR(100) = N'{2}';
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
                    SELECT [Barcode],[ProName],[Category] AS [Remark],[Size],[QtyPCase],ISNULL([DelTo],'') AS [CusName],SUM(ISNULL([TotalPcsOrder],0)) AS [TotalPcsOrder],[DateRequired],[DelToId]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    WHERE (ISNULL([PromotionMachanic],N'') = @oPlanningOrder) AND DATEDIFF(DAY,[DateRequired],@DateRequired) = 0
                    GROUP BY [Barcode],[ProName],[Category],[Size],[QtyPCase],ISNULL([DelTo],''),[DateRequired],[DelToId]
                    UNION ALL
                    SELECT x.[Barcode],x.[ProName],x.[Category] AS [Remark],x.[Size],x.[QtyPCase],ISNULL(x.[DelTo],'') AS [CusName],NULL AS [TotalPcsOrder],[DateRequired],x.[DelToId]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] as x
                    WHERE (ISNULL([PromotionMachanic],N'') = @oPlanningOrder) AND DATEDIFF(DAY,x.[DateRequired],@DateRequired) = 0
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
                THEN 1 ELSE 0 END AS [Missing],v.[DelToId]
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
                THEN 1 ELSE 0 END,v.[DelToId])

				SELECT w.[Barcode],w.[ProName],w.[Remark],REPLACE(w.[Size],' ','') AS [Size],w.[QtyPCase],w.[CusName],case when isnull(w.[TotalPcsOrder],0) = 0 then null else w.[TotalPcsOrder] end as [TotalPcsOrder],
                CASE WHEN (w.[Missing] = 1 AND ISNULL(w.[TotalPcsOrder],0) <> 0) OR ISNULL(w.[DelToId],0) = 0 THEN 1 ELSE 0 END AS [Missing],
                ISNULL(v.[PiecesPerTray],1) AS [PiecesPerTray],w.[DeltoId]
				INTO #vLists
				FROM w
                LEFT OUTER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS v ON v.Barcode = w.Barcode
                GROUP BY w.[Barcode],w.[ProName],w.[Remark],REPLACE(w.[Size],' ',''),w.[QtyPCase],w.[CusName],case when isnull(w.[TotalPcsOrder],0) = 0 then null else w.[TotalPcsOrder] end,
                CASE WHEN (w.[Missing] = 1 AND ISNULL(w.[TotalPcsOrder],0) <> 0) OR ISNULL(w.[DelToId],0) = 0 THEN 1 ELSE 0 END,
                ISNULL(v.[PiecesPerTray],1),w.[DeltoId]
                ORDER BY w.[Remark],w.[ProName];
				SELECT v.Barcode,v.ProName,v.Remark,v.Size,v.QtyPCase,v.PiecesPerTray,SUM(v.TotalPcsOrder) AS [Total Order],((CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) - SUM(v.TotalPcsOrder)) AS [Total Extra/Left],(CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) AS [Total Order To Thailand] --,ROUND(((CEILING(SUM(v.TotalPcsOrder)/ISNULL(v.PiecesPerTray,1))*ISNULL(v.PiecesPerTray,1)) / ISNULL(v.PiecesPerTray,1)),2) AS [Total Tray]
				INTO #v
				FROM #vLists AS v
				GROUP BY v.Barcode,v.ProName,v.Remark,v.Size,v.QtyPCase,v.PiecesPerTray
				ORDER BY v.Size,v.ProName;
				DECLARE @vValue1 AS NVARCHAR(MAX) = N'';
				DECLARE @vValue2 AS NVARCHAR(MAX) = N'';
				DECLARE vCur CURSOR
				FOR SELECT N'ALTER TABLE #v ADD [' + x.CusName + '] DECIMAL(18,0) NULL ;' AS [value1],N'UPDATE v SET v.[' + x.CusName + '] = x.TotalPcsOrder FROM #v AS v INNER JOIN #vLists AS x ON x.Barcode = v.Barcode WHERE x.CusName = N''' + x.CusName + ''';' AS [value2] FROM #vLists AS x GROUP BY x.CusName ORDER BY x.CusName;
				OPEN vCur;
				FETCH NEXT FROM vCur INTO @vValue1,@vValue2;
				WHILE @@FETCH_STATUS = 0
				BEGIN
					EXEC(@vValue1);
					EXEC(@vValue2);
					FETCH NEXT FROM vCur INTO @vValue1,@vValue2;
				END
                CLOSE vCur;
				DEALLOCATE vCur;
				SELECT v.*
				FROM #v AS v
				ORDER BY v.Remark,v.Size,v.Barcode,v.ProName;
				DROP TABLE #vLists;				
				DROP TABLE #v;
            ]]></SQL>
        query = String.Format(query, DatabaseName, vRequiredDate, iPlanningOrder)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        'DgvShow.Columns("ProName").HeaderText = "Description"
        'DgvShow.Columns("QtyPCase").HeaderText = "Q/C"
        'DgvShow.Columns("PiecesPerTray").HeaderText = "Pieces/Tray"
        LblCountRow.Text = String.Format("Count Row : {0:N0}", DgvShow.RowCount)
        FreezeBand(DgvShow.Columns(8))
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FrmProcessTakeOrderPreviewNEdit_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Me.Text = String.Format("Preview & Edit Take Order For {0:dd-MMM-yyyy}", vRequiredDate)
    End Sub

    Private Sub DgvShow_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DgvShow.CellPainting
        If e.RowIndex = -1 Then
            If Not (e.FormattedValue Is Nothing) Then
                e.PaintBackground(e.CellBounds, True)
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom)
                e.Graphics.RotateTransform(270)
                e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5)
                e.Graphics.ResetTransform()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub DgvShow_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DgvShow.CellBeginEdit

    End Sub

    Private Sub DgvShow_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvShow.CellEndEdit
        If DgvShow.RowCount <= 0 Then Exit Sub
        Dim vBarcode As String = DgvShow.Rows(e.RowIndex).Cells("Barcode").Value
        Dim vHeader As String = DgvShow.Columns(e.ColumnIndex).HeaderText
        Dim vPcsOrder As Decimal = CDec(IIf(IsDBNull(DgvShow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = True Or DgvShow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.Equals("") = True, 0, DgvShow.Rows(e.RowIndex).Cells(e.ColumnIndex).Value))
        Dim vPiecesPerTray As Decimal = CDec(IIf(IsDBNull(DgvShow.Rows(e.RowIndex).Cells("PiecesPerTray").Value) = True Or DgvShow.Rows(e.RowIndex).Cells("PiecesPerTray").Value.Equals("") = True, 0, DgvShow.Rows(e.RowIndex).Cells("PiecesPerTray").Value))
        Dim vTotalOrderToThailand As Decimal = (Math.Ceiling(vPcsOrder / vPiecesPerTray) * vPiecesPerTray)
        Dim vTotalExtraLeft As Decimal = (vTotalOrderToThailand - vPcsOrder)
        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RTran = RCon.BeginTransaction()
        Try
            RCom.Transaction = RTran
            RCom.Connection = RCon
            RCom.CommandType = CommandType.Text
            query = <SQL><![CDATA[
                    DECLARE @vDelTo AS NVARCHAR(100) = N'{1}';
                    DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';
                    DECLARE @vPcsOrder AS DECIMAL(18,0) = {3};
                    DECLARE @vDateRequired AS DATE = N'{4:yyyy-MM-dd}';
                    UPDATE [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                    SET [PcsFree] = 0
                    ,[PcsOrder] = @vPcsOrder
                    ,[PackOrder] = 0
                    ,[CTNOrder] = 0
                    ,[TotalPcsOrder] = @vPcsOrder
                    WHERE [Barcode] = @vBarcode AND [DelTo] = @vDelTo AND DATEDIFF(DAY,CONVERT(DATE,[DateRequired]),@vDateRequired) = 0;
                ]]></SQL>
            query = String.Format(query, DatabaseName, vHeader, vBarcode, vPcsOrder, vRequiredDate)
            RCom.CommandText = query
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()

            query = <SQL><![CDATA[
                    DECLARE @vDateRequired AS DATE = N'{1:yyyy-MM-dd}';
                    DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';                    
                    SELECT SUM(v.TotalPcsOrder) AS [Total Order],((CEILING(SUM(v.TotalPcsOrder)/ISNULL(x.PiecesPerTray,1))*ISNULL(x.PiecesPerTray,1)) - SUM(v.TotalPcsOrder)) AS [Total Extra/Left],(CEILING(SUM(v.TotalPcsOrder)/ISNULL(x.PiecesPerTray,1))*ISNULL(x.PiecesPerTray,1)) AS [Total Order To Thailand]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                    LEFT OUTER JOIN [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_TraySetting] AS x ON v.Barcode = x.Barcode
                    WHERE v.[Barcode] = @vBarcode AND DATEDIFF(DAY,CONVERT(DATE,[DateRequired]),@vDateRequired) = 0
                    GROUP BY ISNULL(x.PiecesPerTray,1);
                ]]></SQL>
            query = String.Format(query, DatabaseName, vRequiredDate, vBarcode)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    vPcsOrder = CDec(IIf(IsDBNull(lists.Rows(0).Item("Total Order")) = True, 0, lists.Rows(0).Item("Total Order")))
                    vTotalOrderToThailand = CDec(IIf(IsDBNull(lists.Rows(0).Item("Total Order To Thailand")) = True, 0, lists.Rows(0).Item("Total Order To Thailand")))
                    vTotalExtraLeft = CDec(IIf(IsDBNull(lists.Rows(0).Item("Total Extra/Left")) = True, 0, lists.Rows(0).Item("Total Extra/Left")))
                End If
            End If
            DgvShow.Rows(e.RowIndex).Cells("Total Order").Value = vPcsOrder
            DgvShow.Rows(e.RowIndex).Cells("Total Extra/Left").Value = vTotalExtraLeft
            DgvShow.Rows(e.RowIndex).Cells("Total Order To Thailand").Value = vTotalOrderToThailand
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

    Private Shared Sub FreezeBand(ByVal band As DataGridViewBand)

        band.Frozen = True
        Dim style As DataGridViewCellStyle = New DataGridViewCellStyle()
        style.BackColor = Color.WhiteSmoke
        band.DefaultCellStyle = style

    End Sub
End Class