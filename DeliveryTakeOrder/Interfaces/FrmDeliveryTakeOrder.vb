Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.Net.Mail

Public Class FrmDeliveryTakeOrder
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
    Private Const InvoiceName As String = ""
    Public Property ProgramName As String
    Public Property IsDutchmill As Boolean
    Dim db As RMDB

    Private Sub LoadingInitialized()
        db = AppSetting.DBMain

        Initialized.LoadingInitialized(Data, App)
        DatabaseName = String.Format("{0}{1}", Data.PrefixDatabase, Data.DatabaseName)
        If IsDutchmill = True Then
            RdbManually.Visible = False
            RdbOnlinePO.Visible = False
            LblDisplay.Text = "DUTCHMILL"
        Else
            RdbManually.Visible = True
            RdbOnlinePO.Visible = True
            LblDisplay.Text = ""
        End If
    End Sub

    Private Sub DataSources(ByVal ComboBoxName As ComboBox, ByVal DTable As DataTable, ByVal DisplayMember As String, ByVal ValueMember As String)
        ComboBoxName.DataSource = DTable
        ComboBoxName.DisplayMember = DisplayMember
        ComboBoxName.ValueMember = ValueMember
        ComboBoxName.SelectedIndex = -1
    End Sub

    Private Sub FrmDeliveryTakeOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmDeliveryTakeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        CreateDeliveryTakeOrderList()
        TimerLoading.Enabled = True
        TimerSalemanLoading.Enabled = True
    End Sub

    Private DeliveryTakeOrderList As DataTable
    Private vPromotionList As DataTable
    Private vFixedPriceList As DataTable
    Private Sub CreateDeliveryTakeOrderList()
        If Not (vPromotionList Is Nothing) Then vPromotionList = Nothing
        vPromotionList = New DataTable
        With vPromotionList.Columns
            .Add("PromotionIdLink", GetType(Decimal))
            .Add("QtyInvoicing", GetType(Integer))
        End With

        If Not (vFixedPriceList Is Nothing) Then vFixedPriceList = Nothing
        vFixedPriceList = New DataTable
        With vFixedPriceList.Columns
            .Add("FixedIdLink", GetType(Decimal))
            .Add("Barcode", GetType(String))
            .Add("QtyInvoicing", GetType(Integer))
        End With

        If Not (DeliveryTakeOrderList Is Nothing) Then DeliveryTakeOrderList = Nothing
        DeliveryTakeOrderList = New DataTable
        With DeliveryTakeOrderList.Columns
            .Add("Barcode", GetType(String))
            .Add("ProNumy", GetType(String))
            .Add("ProNumyP", GetType(String))
            .Add("ProNumyC", GetType(String))
            .Add("ProName", GetType(String))
            .Add("ProPackSize", GetType(String))
            .Add("ProQtyPCase", GetType(Integer))
            .Add("ProCat", GetType(String))
            .Add("SupNum", GetType(String))
            .Add("SupName", GetType(String))
            .Add("PcsFree", GetType(Integer))
            .Add("PcsOrder", GetType(Integer))
            .Add("PackOrder", GetType(Integer))
            .Add("CTNOrder", GetType(Single))
            .Add("TotalPcsOrder", GetType(Decimal))
            .Add("ItemDiscount", GetType(Single))
            .Add("PromotionMachanic", GetType(String))
            .Add("Remark", GetType(String))
            .Add("TotalAmount", GetType(Double))
        End With
        DgvShow.DataSource = DeliveryTakeOrderList
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
    End Sub

    Private Sub FrmDeliveryTakeOrder_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private Sub DTPRequiredDate_MouseDown(sender As Object, e As MouseEventArgs) Handles DTPRequiredDate.MouseDown
        TxtRequiredDate.Text = String.Format("{0:dd-MMM-yyyy}", DTPRequiredDate.Value)
    End Sub

    Private Sub DTPRequiredDate_ValueChanged(sender As Object, e As EventArgs) Handles DTPRequiredDate.ValueChanged
        If DTPRequiredDate.Value.Date < Todate.Date Then
            MessageBox.Show("Please check Required Date again!", "Invalid Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DTPRequiredDate.Value = Todate
            Exit Sub
        End If
        TxtRequiredDate.Text = String.Format("{0:dd-MMM-yyyy}", DTPRequiredDate.Value)
    End Sub

    Private Sub PicRefreshPO_Click(sender As Object, e As EventArgs) Handles PicRefreshPO.Click
        lists = Data.ExecuteStoredProc("Stock.dbo.AutoPONumber", Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtPONo.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("Auto")) = True, "", lists.Rows(0).Item("Auto")))
            End If
        End If
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TimerLoading_Tick(sender As Object, e As EventArgs) Handles TimerLoading.Tick
        TimerLoading.Enabled = False
        App.ClearController(TxtEmpName, TxtOrderDate, TxtInvoiceNo, TxtPONo)
        TxtEmpName.Text = Environment.MachineName
        TxtOrderDate.Text = String.Format("{0:dd-MMM-yyyy}", Todate)
        DTPDeliverDate.Value = Todate

        Dim InvNo As Long = 0
        query = _
        <SQL>
            <![CDATA[
                SELECT [PrintInvNo], [IsBusy]
                FROM [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, 0)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                InvNo = CLng(IIf(IsDBNull(lists.Rows(0).Item("PrintInvNo")) = True, 0, lists.Rows(0).Item("PrintInvNo")))
            End If
        End If
        InvNo += 1
        TxtInvoiceNo.Text = InvNo
        RdbManually.Checked = True
    End Sub

    Private Sub TimerCustomerLoading_Tick(sender As Object, e As EventArgs) Handles TimerCustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCustomerLoading.Enabled = False
        IsKeyPress = True
        If RdbManually.Checked = True Then
            If ProgramName.Equals("Sale_Team") = True Then
                query = <SQL>
                            <![CDATA[
                        SELECT [CusNum],[CusName]
                        FROM [Stock].[dbo].[TPRCustomer]
                        WHERE [Status] = 'Activate' AND [CusName] LIKE 'YN %'
                        {1}AND [CusNum] IN (SELECT [CusNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetBillTo] GROUP BY [CusNum])
                        ORDER BY [CusName];
                    ]]>
                        </SQL>
            Else
                query = <SQL>
                            <![CDATA[
                        SELECT [CusNum],[CusName]
                        FROM [Stock].[dbo].[TPRCustomer]
                        WHERE [Status] = 'Activate'
                        {1}AND [CusNum] IN (SELECT [CusNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetBillTo] GROUP BY [CusNum])
                        ORDER BY [CusName];
                    ]]>
                        </SQL>
            End If
        Else
            query = <SQL>
                        <![CDATA[
                    SELECT * 
                    FROM `untapp`.`tprdeliverytakeorder` 
                    WHERE `Status`='Process';
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, IIf(Initialized.vIsNestleOnly = True, "", "--"))
            lists = MyData.Selects(query)
            If Not (lists Is Nothing) Then
                ProgressOnlinePO.Visible = True
                ProgressOnlinePO.Value = 0
                ProgressOnlinePO.Maximum = lists.Rows.Count
                RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
                Try
                    For Each r As DataRow In lists.Rows
                        RCon.Open()
                        RTran = RCon.BeginTransaction()
                        RCom.Transaction = RTran
                        RCom.Connection = RCon
                        RCom.CommandType = CommandType.Text
                        query = <SQL>
                                    <![CDATA[
                                DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                                DECLARE @CusName AS NVARCHAR(100) = N'';
                                DECLARE @DelTo AS NVARCHAR(100) = N'{2}';
                                DECLARE @DateOrd AS DATE = N'{3:yyyy-MM-dd}';
                                DECLARE @DateRequired AS DATE = N'{4:yyyy-MM-dd}';
                                DECLARE @ProNumy AS NVARCHAR(15) = N'{5}';
                                DECLARE @ProName AS NVARCHAR(100) = N'';
                                DECLARE @ProPackSize AS NVARCHAR(10) = N'';
                                DECLARE @ProQtyPCase AS INT = 0;
                                DECLARE @ProCat AS NVARCHAR(200) = N'';
                                DECLARE @PcsFree AS INT = {6};
                                DECLARE @PcsOrder AS INT = {7};
                                DECLARE @CTNOrder AS FLOAT = {8};
                                DECLARE @TotalPcsOrder AS INT = 0;
                                DECLARE @PONumber AS NVARCHAR(50) = N'{9}';
                                DECLARE @LogInName AS NVARCHAR(50) = N'{10}';
                                DECLARE @PromotionMachanic AS NVARCHAR(500) = N'{11}';
                                DECLARE @TranDate AS DATETIME = N'{12}';
                                DECLARE @RemarkExpiry AS NVARCHAR(100) = N'{13}';
                                DECLARE @Saleman AS NVARCHAR(100) = N'{14}';
                                DECLARE @UserId AS INT = {15};
                                DECLARE @UOM AS NVARCHAR(10) = N'{16}';
                                DECLARE @OrderNum AS NVARCHAR(25) = N'{17}';
                                DECLARE @STATUS AS NVARCHAR(25) = N'{18}';

                                SELECT @CusName=[CusName] FROM [Stock].[dbo].[TPRCustomer] WHERE [CusNum] = @CusNum;
                                SELECT @ProName=ISNULL([ProName],''),@ProPackSize=ISNULL([ProPacksize],''),@ProQtyPCase=ISNULL([ProQtyPCase],1),@ProCat=ISNULL([ProCat],'')
                                FROM (
	                                SELECT [ProNumY],[ProName],[ProPacksize],[ProQtyPCase],[ProCat]
	                                FROM [Stock].[dbo].[TPRProducts]
	                                WHERE [ProNumY] = @ProNumy
	                                UNION ALL
	                                SELECT [ProNumY],[ProName],[ProPacksize],[ProQtyPCase],[ProCat]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                                WHERE [ProNumY] = @ProNumy
	                                UNION ALL
	                                SELECT B.[OldProNumy] AS [ProNumY],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProCat]
	                                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                                WHERE B.[OldProNumy] = @ProNumy
	                                UNION ALL
	                                SELECT B.[OldProNumy] AS [ProNumY],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProCat]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                                WHERE B.[OldProNumy] = @ProNumy
                                ) LISTS;
                                SET @TotalPcsOrder = (@CTNOrder * @ProQtyPCase) + @PcsOrder;

                                INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrder_Online]([CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[PromotionMachanic],[ProCat],[TranDate],[RemarkExpiry],[Saleman],[UserId],[UOM],[OrderNum],[Status],[CreatedDate])
                                VALUES(@CusNum,@CusName,@DelTo,@DateOrd,@DateRequired,@ProNumy,@ProName,@ProPackSize,@ProQtyPCase,@PcsFree,@PcsOrder,@CTNOrder,@TotalPcsOrder,@PONumber,@LogInName,@PromotionMachanic,@ProCat,@TranDate,@RemarkExpiry,@Saleman,@UserId,@UOM,@OrderNum,@STATUS,GETDATE());
                            ]]>
                                </SQL>
                        query = String.Format(query, DatabaseName, _
                                              Trim(IIf(IsDBNull(r.Item("CusNum")) = True, "", r.Item("CusNum"))), _
                                              Trim(IIf(IsDBNull(r.Item("DelTo")) = True, "", r.Item("DelTo"))), _
                                              CDate(IIf(IsDBNull(r.Item("DateOrd")) = True, Todate, r.Item("DateOrd"))), _
                                              CDate(IIf(IsDBNull(r.Item("DateRequired")) = True, Todate, r.Item("DateRequired"))), _
                                              Trim(IIf(IsDBNull(r.Item("ProNumy")) = True, "", r.Item("ProNumy"))), _
                                              CInt(IIf(IsDBNull(r.Item("PcsFree")) = True, 0, r.Item("PcsFree"))), _
                                              CInt(IIf(IsDBNull(r.Item("PcsOrder")) = True, 0, r.Item("PcsOrder"))), _
                                              CInt(IIf(IsDBNull(r.Item("CTNOrder")) = True, 0, r.Item("CTNOrder"))), _
                                              Trim(IIf(IsDBNull(r.Item("PONumber")) = True, "", r.Item("PONumber"))), _
                                              Trim(IIf(IsDBNull(r.Item("LogInName")) = True, "", r.Item("LogInName"))), _
                                              Trim(IIf(IsDBNull(r.Item("PromotionMachanic")) = True, "", r.Item("PromotionMachanic"))), _
                                              CDate(IIf(IsDBNull(r.Item("TranDate")) = True, Todate, r.Item("TranDate"))), _
                                              Trim(IIf(IsDBNull(r.Item("RemarkExpiry")) = True, "", r.Item("RemarkExpiry"))), _
                                              Trim(IIf(IsDBNull(r.Item("Saleman")) = True, "", r.Item("Saleman"))), _
                                              Trim(IIf(IsDBNull(r.Item("UserId")) = True, "", r.Item("UserId"))), _
                                              Trim(IIf(IsDBNull(r.Item("UOM")) = True, "", r.Item("UOM"))), _
                                              Trim(IIf(IsDBNull(r.Item("OrderNum")) = True, "", r.Item("OrderNum"))), _
                                              Trim(IIf(IsDBNull(r.Item("STATUS")) = True, "", r.Item("STATUS"))))
                        RCom.CommandText = query
                        RCom.ExecuteNonQuery()

                        query = <SQL>
                                    <![CDATA[
                                DELETE 
                                FROM `untapp`.`tprdeliverytakeorder` 
                                WHERE `ID`={1};
                            ]]>
                                </SQL>
                        query = String.Format(query, DatabaseName, CLng(IIf(IsDBNull(r.Item("ID")) = True, 0, r.Item("ID"))))
                        MyData.Execute(query)
                        RTran.Commit()
                        RCon.Close()
                        If ProgressOnlinePO.Value < ProgressOnlinePO.Maximum Then ProgressOnlinePO.Value += 1
                        Application.DoEvents()
                    Next
                Catch ex As SqlException
                    RTran.Rollback()
                    RCon.Close()
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    RTran.Rollback()
                    RCon.Close()
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try
                ProgressOnlinePO.Value = ProgressOnlinePO.Maximum
                ProgressOnlinePO.Visible = False
            End If

            query = <SQL>
                        <![CDATA[
                    SELECT [CusNum],[CusName]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online]
                    {1}WHERE [CusNum] IN (SELECT [CusNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetBillTo] GROUP BY [CusNum])
                    GROUP BY [CusNum],[CusName]
                    ORDER BY [CusName];
                ]]>
                    </SQL>
        End If
        query = String.Format(query, DatabaseName, IIf(Initialized.vIsNestleOnly = True, "", "--"))
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbBillTo, lists, "CusName", "CusNum")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TimerDeltoLoading_Tick(sender As Object, e As EventArgs) Handles TimerDeltoLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerDeltoLoading.Enabled = False
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
        If RdbManually.Checked = True Then
            query = <SQL>
                        <![CDATA[
                            SELECT [DefId] [Id],[DelTo]
                            FROM [Stock].[dbo].[TPRDelto]
                            GROUP BY [DefId],[DelTo]
                            ORDER BY [DelTo];
                        ]]>
                    </SQL>
            query = String.Format(query, DatabaseName)

            'query = _
            '<SQL>
            '    <![CDATA[
            '        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
            '        WITH v AS (
            '        SELECT [DeltoId] AS [Id],[Delto]
            '        FROM [{0}].[dbo].[TblCustomerLinkToDeltoSetting]
            '        WHERE [CusNum] = @vCusNum
            '        GROUP BY [DeltoId],[Delto])
            '        SELECT v.Id, v.Delto
            '        FROM v
            '        ORDER BY v.Delto;
            '    ]]>
            '</SQL>
            'query = String.Format(query, DatabaseName, CusNum)
        Else
            query = <SQL>
                        <![CDATA[
                            DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                            SELECT [DefId] [Id],[DelTo]
                            FROM [Stock].[dbo].[TPRDelto]
                            WHERE [DelTo] IN (SELECT [DelTo] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [DelTo])
                            GROUP BY [DefId],[DelTo]
                            ORDER BY [DelTo];
                        ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, CusNum)
        End If
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, lists, "DelTo", "Id")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TimerCategoryLoading_Tick(sender As Object, e As EventArgs) Handles TimerCategoryLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCategoryLoading.Enabled = False
        App.ClearController(TxtIdDelto, TxtZone, TxtKhmerName, TxtNote)
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
        query = <SQL>
                    <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                IF EXISTS (SELECT * FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
                BEGIN
                    SELECT [Index],[ProCat]
                    FROM (
	                    SELECT 0 AS [Index],'All Categories' AS [ProCat]
	                    UNION ALL 
	                    SELECT 1 AS [Index],[ProCat]
	                    FROM [Stock].[dbo].[TPRProducts]
                        WHERE [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE ([CusNum] = @CusNum))
                        {3}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        {2} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillSetting])
                    ) lists
                    GROUP BY [Index],[ProCat]
                    ORDER BY [Index],[ProCat];
                END
                ELSE
                BEGIN
                    SELECT [Index],[ProCat]
                    FROM (
	                    SELECT 0 AS [Index],'All Categories' AS [ProCat]
	                    UNION ALL 
	                    SELECT 1 AS [Index],[ProCat]
	                    FROM [Stock].[dbo].[TPRProducts]
                        {3}WHERE LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        {2} WHERE LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                    ) lists
                    GROUP BY [Index],[ProCat]
                    ORDER BY [Index],[ProCat];
                END
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CusNum, IIf(IsDutchmill = True, "", "--"), IIf(Initialized.vIsNestleOnly = True, "", "--"))
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbCategory, lists, "ProCat", "ProCat")
        If CmbCategory.Items.Count > 0 Then CmbCategory.SelectedIndex = 0
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TimerProductLoading_Tick(sender As Object, e As EventArgs) Handles TimerProductLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerProductLoading.Enabled = False
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
        Dim Category As String = ""
        If TypeOf CmbCategory.SelectedValue Is DataRowView Or CmbCategory.SelectedValue Is Nothing Then
            Category = ""
        Else
            If CmbCategory.Text.Trim() = "" Then
                Category = ""
            Else
                Category = CmbCategory.SelectedValue
            End If
        End If
        If RdbManually.Checked = True Then
            query = <SQL>
                        <![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Category AS NVARCHAR(100) = N'{2}';
                    --IF EXISTS (SELECT * FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
                    --BEGIN
                        --SELECT [Barcode],[ProName],[Display]
                        --FROM (
	                        --SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProducts]
                            --WHERE ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL 
	                        --SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProducts]
	                        --WHERE ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL 
	                        --SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProducts]
	                        --WHERE ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL
                            --SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProductsDeactivated]
                            --WHERE ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL 
	                        --SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        --WHERE ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL 
	                        --SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        --WHERE ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            --AND [ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
	                        --GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        --UNION ALL 
	                        --SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                            --WHERE (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                            --AND ([B].[OldProNumy] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
                            --OR A.[ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum))
	                        --GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                            --UNION ALL 
	                        --SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        --FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                            --WHERE (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                            --AND ([B].[OldProNumy] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum)
                            --OR A.[ProNumY] IN (SELECT [ProNumY] FROM [Stock].[dbo].[TPRWSCusProductList] WHERE [CusNum] = @CusNum))
	                        --GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                        --) Lists
                        --GROUP BY [Barcode],[ProName],[Display]
                        --ORDER BY [ProName];
                    --END
                    --ELSE
                    --BEGIN
                        SELECT [Barcode],[ProName],[Display]
                        FROM (
	                        SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProducts]
                            WHERE ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL 
	                        SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProducts]
	                        WHERE ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL 
	                        SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProducts]
	                        WHERE ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL
                            SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated]
                            WHERE (ISNULL([ProTotQty],0) <> 0) AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL 
	                        SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        WHERE (ISNULL([ProTotQty],0) <> 0) AND ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL 
	                        SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        WHERE (ISNULL([ProTotQty],0) <> 0) AND ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                        UNION ALL 
	                        SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                            WHERE (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                            UNION ALL 
	                        SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                            WHERE (ISNULL(B.[Stock],0) <> 0) AND (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                            {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                            {3} AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryDutchmill])
                            GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                        ) Lists
                        GROUP BY [Barcode],[ProName],[Display]
                        ORDER BY [ProName];
                    --END
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, CusNum, Category, IIf(IsDutchmill = True, "", "--"), IIf(Initialized.vIsNestleOnly = True, "", "--"))
        Else
            Dim SelectedBarcode As String = ""
            If Not (DeliveryTakeOrderList Is Nothing) Then
                For Each r As DataRow In DeliveryTakeOrderList.Rows
                    If CInt(IIf(IsDBNull(r.Item("PcsFree")) = True, 0, r.Item("PcsFree"))) = 0 Then
                        SelectedBarcode &= String.Format("'{0}',", Trim(IIf(IsDBNull(r.Item("Barcode")) = True, "", r.Item("Barcode"))))
                    End If
                Next
            End If
            If SelectedBarcode.Trim() <> "" Then
                SelectedBarcode = SelectedBarcode.Trim()
                SelectedBarcode = Mid(SelectedBarcode, 1, SelectedBarcode.Length - 1).Trim()
            Else
                SelectedBarcode = "''"
            End If
            query = <SQL>
                        <![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Category AS NVARCHAR(100) = N'{2}';
                    SELECT [Barcode],[ProName],[Display]
                    FROM (
	                    SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
                        WHERE ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumY] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumYP] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumYC] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
                        SELECT ISNULL([ProNumY],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
                        WHERE (ISNULL([ProTotQty],0) <> 0) AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumY] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumY],''),ISNULL([ProName],''),ISNULL([ProNumY],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYP],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE (ISNULL([ProTotQty],0) <> 0) AND ISNULL([ProNumYP],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumYP] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumYP],''),ISNULL([ProName],''),ISNULL([ProNumYP],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([ProNumYC],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE (ISNULL([ProTotQty],0) <> 0) AND ISNULL([ProNumYC],'') <> '' AND ([ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [ProNumYC] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([ProNumYC],''),ISNULL([ProName],''),ISNULL([ProNumYC],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
	                    UNION ALL 
	                    SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                        WHERE (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [B].[OldProNumy] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                        UNION ALL 
	                    SELECT ISNULL([B].[OldProNumy],'') AS [Barcode],ISNULL([ProName],'') AS [ProName],ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'') AS [Display]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
                        WHERE (ISNULL(B.[Stock],0) <> 0) AND (A.[ProCat] = @Category OR N'' = @Category OR N'All Categories' = @Category)  
                        {4}AND LEFT([Sup1],8) IN (SELECT [SupNum] FROM [Stock].[dbo].[TPRDeliveryTakeOrder_SetSupplier] GROUP BY [SupNum])
                        AND [B].[OldProNumy] IN (SELECT [ProNumY] FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online] WHERE ([CusNum] = @CusNum OR '' = @CusNum) GROUP BY [ProNumY])
	                    GROUP BY ISNULL([B].[OldProNumy],''),ISNULL([ProName],''),ISNULL([B].[OldProNumy],'') + SPACE(3) + ISNULL([ProName],'') + SPACE(3) + ISNULL([ProPacksize],'')
                    ) Lists
                    WHERE [Barcode] NOT IN ({3})
                    GROUP BY [Barcode],[ProName],[Display]
                    ORDER BY [ProName];
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, CusNum, Category, SelectedBarcode, IIf(Initialized.vIsNestleOnly = True, "", "--"))
        End If
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbProducts, lists, "Display", "Barcode")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCategory.SelectedIndexChanged
        If CmbBillTo.Text.Trim() = "" Then Exit Sub
        TimerProductLoading.Enabled = True
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
        If CmbProducts.Items.Count <= 0 Then Exit Sub
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
        TxtCTNOrder.Focus()
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

    Private QtyPerPack As Integer
    Private Currency As String
    Private Rate As Single
    Private Buyin As Double
    Private BuyDis As Single
    Private BuyVAT As Single
    Private TotalBuyin As Double
    Private Average As Double
    Private WS As Double
    Private WSAfter As Double
    Private rSupNum As String
    Private rSupName As String
    Private vCity As String
    Private vAdditionalCost As Boolean
    Private oFixedPriceQtyInvoicing As Integer
    Private oIsFixedPrice As Boolean

    Private Sub ClearProductItems()
        rSupNum = ""
        rSupName = ""
        Currency = ""
        Rate = 0
        Buyin = 0
        BuyDis = 0
        BuyVAT = 0
        TotalBuyin = 0
        Average = 0
        WS = 0
        WSAfter = 0
        oFixedPriceQtyInvoicing = 0
        oIsFixedPrice = False
        BtnAdd.Enabled = True
        PicProducts.Image = Nothing
        App.ClearController(TxtQtyPerCase, TxtStock, TxtWSPrice, TxtBarcodeFree, TxtPcsFree, TxtItemDiscount, TxtPcsOrder, TxtPackOrder, TxtCTNOrder, TxtTotalPcsOrder, TxtUnitPrice, TxtPackPrice, TxtTotalAmount, TxtRemark)
        App.SetReadOnlyController(False, TxtPackOrder)
    End Sub
    Private Sub CmbProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbProducts.SelectedIndexChanged
        ClearProductItems()
        If CmbProducts.Text.Trim() = "" Then Exit Sub
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then Exit Sub
        TimerCheckPromotion.Enabled = True
        TimerChecking.Enabled = True
    End Sub

    Private Function CheckNewCodeForCustomer() As Boolean
        Dim IsNewCode As Boolean = False
        query = <SQL>
                    <![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                SELECT [Id],[OldProNumy],[ProName],[ProPackSize],[NewProNumy],[Delto],[Telephone],[CusNum]
                FROM [Stock].[dbo].[TPRProductNewCodeAlert]
                WHERE ([OldProNumy] = @Barcode OR [NewProNumy] = @Barcode) AND [CusNum] = @CusNum;
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue, CmbBillTo.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then IsNewCode = True
        End If
        Return IsNewCode
    End Function

    Private Function QueryCCEmail() As DataTable
        Dim IsDeactivated As Boolean = False
        query =
        <SQL>
            <![CDATA[
SELECT Email
FROM dbo.TblEmailCCProductDC;
            ]]>
        </SQL>
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        Return lists
    End Function

    Private Function CheckDeactivatedItem() As DataTable
        Dim IsDeactivated As Boolean = False
        query =
        <SQL>
            <![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                SELECT * 
                FROM [Stock].[dbo].[TPRProductsDeactivated] 
                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode);
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        Return lists
    End Function

    Private Function CheckCustomerEmail() As DataTable
        Dim IsDeactivated As Boolean = False
        query =
        <SQL>
            <![CDATA[
                SELECT ISNULL(CusEmail, '') AS Email
                FROM Stock.dbo.TPRCustomer
                WHERE CusNum = '{0}';
            ]]>
        </SQL>
        query = String.Format(query, CmbBillTo.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        Return lists
    End Function

    Private Function CheckOldItem() As Boolean
        Dim IsOldItem As Boolean = False
        Dim NewBarcode As String = ""
        Dim ProId As Long = 0
        query = _
        <SQL>
            <![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                SELECT [Id],[ProId],[OldProNumy],[Stock]
                FROM [Stock].[dbo].[TPRProductsOldCode]
                WHERE [OldProNumy] = @Barcode;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                ProId = CLng(IIf(IsDBNull(lists.Rows(0).Item("ProId")) = True, 0, lists.Rows(0).Item("ProId")))
                query = _
                <SQL>
                    <![CDATA[
                        DECLARE @ProId AS DECIMAL(18,0) = {1};
                        SELECT [ProNumY]
                        FROM (
	                        SELECT [ProNumY]
	                        FROM [Stock].[dbo].[TPRProducts]
	                        WHERE [ProID] = @ProId
	                        UNION ALL
	                        SELECT [ProNumY]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        WHERE [ProID] = @ProId
                        ) lists;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, ProId)
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then
                        NewBarcode = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumY")) = True, "", lists.Rows(0).Item("ProNumY")))
                        If MessageBox.Show(String.Format("This item is old code <{0}>." & vbCrLf & "Do you want to change the old code <{0}> to new code <{1}>?(Yes/No)", CmbProducts.SelectedValue, NewBarcode), "Confirm Change Old Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            IsOldItem = False
                        Else
                            CmbProducts.SelectedValue = NewBarcode
                            IsOldItem = True
                        End If
                    End If
                End If

            End If
        End If
        Return IsOldItem
    End Function

    Private Sub TxtPcsOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtPcsOrder.TextChanged
        TotalPcsOrder()
    End Sub

    Private Sub TxtPackOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtPackOrder.TextChanged
        TotalPcsOrder()
    End Sub

    Private Sub TxtCTNOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtCTNOrder.TextChanged
        TotalPcsOrder()
    End Sub

    Private Sub TotalPcsOrder()
        Dim PcsOrder As Integer = CInt(IIf(TxtPcsOrder.Text.Trim() = "", 0, TxtPcsOrder.Text.Trim()))
        Dim PackOrder As Integer = CInt(IIf(TxtPackOrder.Text.Trim() = "", 0, TxtPackOrder.Text.Trim()))
        Dim CTNOrder As Single = CSng(IIf(TxtCTNOrder.Text.Trim() = "", 0, TxtCTNOrder.Text.Trim()))
        Dim QtyPerCase As Integer = CInt(IIf(TxtQtyPerCase.Text.Trim() = "", 1, TxtQtyPerCase.Text.Trim()))
        Dim TotalPcsOrder As Integer = ((CTNOrder * QtyPerCase) + (PackOrder * QtyPerPack) + PcsOrder)
        TxtTotalPcsOrder.Text = String.Format("{0:N0}", TotalPcsOrder)
    End Sub

    Private Sub TxtPcsOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPcsOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtPackOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPackOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtCTNOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCTNOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Float, , 10)
    End Sub

    Private Sub TxtWSPrice_TextChanged(sender As Object, e As EventArgs) Handles TxtWSPrice.TextChanged
        TotalAmount()
    End Sub

    Private Sub TxtTotalPcsOrder_TextChanged(sender As Object, e As EventArgs) Handles TxtTotalPcsOrder.TextChanged
        TotalAmount()
    End Sub

    Private Sub TotalAmount()
        Dim WSPrice As Double = WSAfter 'CDbl(IIf(TxtWSPrice.Text.Trim() = "", 0, TxtWSPrice.Text.Trim()))
        Dim QtyPerCase As Decimal = CDec(IIf(TxtQtyPerCase.Text.Trim() = "", 1, TxtQtyPerCase.Text.Trim()))
        Dim UnitPrice As Double = (WSPrice / QtyPerCase)
        Dim PcsOrder As Integer = CInt(IIf(TxtPcsOrder.Text.Trim() = "", 0, TxtPcsOrder.Text.Trim()))
        Dim PackOrder As Integer = CInt(IIf(TxtPackOrder.Text.Trim() = "", 0, TxtPackOrder.Text.Trim()))
        Dim CTNOrder As Single = CSng(IIf(TxtCTNOrder.Text.Trim() = "", 0, TxtCTNOrder.Text.Trim()))
        Dim ItemDis As Single = CSng(IIf(TxtItemDiscount.Text.Trim() = "", 0, TxtItemDiscount.Text.Trim()))
        ItemDis = Math.Abs((100 - ItemDis) / 100)
        If QtyPerPack = 0 Then QtyPerPack = 1
        'UnitPrice = String.Format("{0:N2}", UnitPrice)
        Dim PackPrice As Double = ((WSPrice / QtyPerCase) * QtyPerPack)
        If TxtPackOrder.ReadOnly = True Then
            TxtPackPrice.Text = ""
        Else
            TxtPackPrice.Text = String.Format("{0:N2}", PackPrice)
        End If
        PackPrice = CDbl(IIf(TxtPackPrice.Text.Trim().Equals("") = True, 0, TxtPackPrice.Text.Trim()))
        Dim TotalAmount As Double = (((PcsOrder * UnitPrice) + (PackOrder * PackPrice) + (CTNOrder * WSPrice)) * ItemDis)
        Dim vTotalPcs As Decimal = PcsOrder + (PackOrder * QtyPerPack) + (CTNOrder * QtyPerCase)
        'If (vIssueUnitPrice = True And TxtPackOrder.ReadOnly = True) Or (vIsFormatUnitPrice = True) Then
        Dim oDigited As Decimal = GetDigitsValue(vDigit)
        Dim oWS As Double = 0
        If (vIssueUnitPrice = True) Or (vIsFormatUnitPrice = True) Or (vIssueUnitPrice_ = True) Or (vIssuePackPrice = True) Then
            If oIsFixedPrice = True Then
                UnitPrice = (WSPrice / QtyPerCase)
                TotalAmount = ((vTotalPcs * UnitPrice) * ItemDis)
            Else
                oWS = Math.Round((WSPrice / QtyPerCase), 8)
                UnitPrice = (Math.Ceiling(oWS * oDigited) / oDigited)
                TotalAmount = ((vTotalPcs * UnitPrice) * ItemDis)
                TxtWSPrice.Text = String.Format("{0:N2}", (UnitPrice * QtyPerCase))
            End If
            If vIssuePackPrice = True Then
                TxtPackPrice.Text = String.Format("{0:N2}", (UnitPrice * QtyPerPack))
                PackPrice = CDbl(IIf(TxtPackPrice.Text.Trim().Equals("") = True, 0, TxtPackPrice.Text.Trim()))
                WSPrice = CDbl(IIf(TxtWSPrice.Text.Trim() = "", 0, TxtWSPrice.Text.Trim()))
                TotalAmount = (((PcsOrder * UnitPrice) + (PackOrder * PackPrice) + (CTNOrder * WSPrice)) * ItemDis)
            End If
        Else
            TotalAmount = (((vTotalPcs / QtyPerCase) * WSPrice) * ItemDis)

            'query = <SQL>
            '            <![CDATA[
            '            DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
            '            DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';                        
            '         WITH v AS (
            '             SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumY = @vBarcode AND ISNULL(v.ProNumY,N'') <> N''
            '             UNION ALL
            '                SELECT N'PACK.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumYP = @vBarcode AND ISNULL(v.ProNumYP,N'') <> N''
            '             UNION ALL
            '                SELECT N'CASE.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumYC = @vBarcode AND ISNULL(v.ProNumYC,N'') <> N''
            '             UNION ALL
            '             SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumY = @vBarcode AND ISNULL(v.ProNumY,N'') <> N''
            '             UNION ALL
            '                SELECT N'PACK.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumYP = @vBarcode AND ISNULL(v.ProNumYP,N'') <> N''
            '             UNION ALL
            '                SELECT N'CASE.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumYC = @vBarcode AND ISNULL(v.ProNumYC,N'') <> N''
            '             UNION ALL
            '             SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsOldCode] AS v WHERE v.OldProNumy = @vBarcode AND ISNULL(v.OldProNumy,N'') <> N'')
            '             SELECT v.*
            '         FROM v;                        
            '        ]]>
            '        </SQL>
            'query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            'lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            'If Not (lists Is Nothing) Then
            '    If lists.Rows.Count > 0 Then
            '        Dim oValue As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Value")) = True, "", lists.Rows(0).Item("Value")))
            '        If oValue.Equals("UNIT.NUMBER") = True Then
            '            vIssuePackPrice = False
            '            vIssueCasePrice = False
            '            TxtPcsOrder.ReadOnly = False
            '            TxtCTNOrder.ReadOnly = True
            '            TxtPackOrder.ReadOnly = True
            '            TxtPcsOrder.Focus()
            '        ElseIf oValue.Equals("PACK.NUMBER") = True Then
            '            vIssuePackPrice = True
            '            vIssueCasePrice = False
            '            TxtPcsOrder.ReadOnly = True
            '            TxtCTNOrder.ReadOnly = True
            '            TxtPackOrder.ReadOnly = False
            '            TxtPackOrder.Focus()
            '        ElseIf oValue.Equals("CASE.NUMBER") = True Then
            '            vIssuePackPrice = False
            '            vIssueCasePrice = True
            '            TxtPcsOrder.ReadOnly = True
            '            TxtCTNOrder.ReadOnly = False
            '            TxtPackOrder.ReadOnly = True
            '            TxtCTNOrder.Focus()
            '        End If
            '    End If
            'End If
            'If vIssuePackPrice = True Then
            '    oWS = Math.Round((WSPrice / (QtyPerCase / QtyPerPack)), 8)
            '    oWS = (Math.Ceiling(oWS * oDigited) / oDigited)
            '    txtpackprice.Text = String.Format("{0:N2}", oWS)
            '    PackPrice = CDbl(IIf(txtpackprice.Text.Trim().Equals("") = True, 0, txtpackprice.Text.Trim()))
            '    WSPrice = CDbl(IIf(txtwsprice.Text.Trim() = "", 0, txtwsprice.Text.Trim()))
            '    TotalAmount = (((PcsOrder * UnitPrice) + (PackOrder * PackPrice) + (CTNOrder * WSPrice)) * ItemDis)
            'End If
        End If
        TxtUnitPrice.Text = String.Format("{0:N" & vDigit & "}", UnitPrice)
        TxtTotalAmount.Text = String.Format("{0:N2}", TotalAmount)
        IsCheckPromotion = False
        TimerCheckPromotion.Enabled = True
    End Sub

    Private Function GetDigitsValue(ByVal Digit As Integer) As Decimal
        Dim vZero As String = ""
        Dim value As Decimal = 1
        For vR As Integer = 1 To Digit
            vZero &= "0"
        Next
        value = CDec(value.ToString() & vZero)
        Return value
    End Function

    Private Sub RdbManually_CheckedChanged(sender As Object, e As EventArgs) Handles RdbManually.CheckedChanged
        TimerCustomerLoading.Enabled = True
        TimerDeltoLoading.Enabled = True
    End Sub

    Private Sub RdbOnlinePO_CheckedChanged(sender As Object, e As EventArgs) Handles RdbOnlinePO.CheckedChanged
        TimerCustomerLoading.Enabled = True
        TimerDeltoLoading.Enabled = True
    End Sub

    Private CusItemDis As Single
    Private CusInvoiceDis As Single
    Private CusVAT As String
    Private CusServiceRebate As Single
    Private Terms As Integer
    Private CreditLimit As Double
    Private MaxMonthAllow As Integer
    Private CreditLimitAllow As Double
    Private vIssueUnitPrice As Boolean
    Private vIssueUnitPrice_ As Boolean
    Private vIssuePackPrice As Boolean
    Private vIssueCasePrice As Boolean
    Private vDigit As Integer
    Private vServiceCost As Double
    Private vIsFormatUnitPrice As Boolean
    Private Sub CmbBillTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBillTo.SelectedIndexChanged
        PCustomerRemark.Visible = False
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        DTPRequiredDate.Value = String.Format("{0:dd-MMM-yyyy}", Todate)
        DTPDeliverDate.Value = Todate
        vServiceCost = 0
        CusItemDis = 0
        CusInvoiceDis = 0
        CusVAT = ""
        CusServiceRebate = 0
        Terms = 0
        CreditLimit = 0
        MaxMonthAllow = 0
        CreditLimitAllow = 0
        vCity = ""
        vAdditionalCost = False
        vIssueUnitPrice = False
        vIssueUnitPrice_ = False
        vIssuePackPrice = False
        vIssueCasePrice = False
        vDigit = 2
        PicProducts.Image = Nothing
        vIsFormatUnitPrice = False
        App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
        App.ClearController(TxtIdDelto, CmbDelto, TxtZone, TxtKhmerName, TxtNote, CmbProducts, TxtQtyPerCase, TxtStock, TxtBarcodeFree, TxtPcsOrder, TxtPackOrder, TxtCTNOrder, TxtTotalPcsOrder, TxtUnitPrice, TxtPcsFree, TxtItemDiscount, TxtWSPrice, TxtTotalAmount, TxtRequiredDate)
        If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then Exit Sub
        If CmbBillTo.Text.Trim() = "" Then Exit Sub
        If IsKeyPress = True Then
            IsKeyPress = False
            Exit Sub
        End If
        FrmAlertCreditAmount.Close()
        FrmAlertBankGarantee.Close()
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
        REM Customer Formation UnitPrice
        query = <SQL>
                    <![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                SELECT [Id],[CusNum],[CusName],[CreatedDate]
                FROM [{0}].[dbo].[TblCustomer.FormatUnitPrice]
                WHERE ([CusNum] = @vCusNum);
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        Dim oList As DataTable = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (oList Is Nothing) Then
            If oList.Rows.Count > 0 Then
                vIsFormatUnitPrice = True
            End If
        End If

        REM Alert Customer Bad Payment
        Panel18.Enabled = True
        query = <SQL>
                    <![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                SELECT [Id],[CusNum],[CusName],[Remark],[AlertDate],[BlockDate],[CreatedDate]
                FROM [{0}].[dbo].[TblCustomerRemarkSetting]
                WHERE ([CusNum] = @vCusNum) 
                AND (([Status] = N'Both') OR ([Status] = N'Dry Items'))
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
                    Panel18.Enabled = False
                Else
                    Application.DoEvents()
                    Threading.Thread.Sleep(5000)
                    PCustomerRemark.Visible = False
                    TxtCustomerRemark.Text = ""
                End If
            End If
        End If

        'Check Info Customer
        If CheckInfoCustomer(CusNum) = False Then
            BtnAdd.Enabled = False
            MessageBox.Show("This customer has been deleted! Please report to administrator!", "Not Existed Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If vIssueUnitPrice = True Then
            PanelUnitPrice.BackColor = Color.Brown
        Else
            PanelUnitPrice.BackColor = Color.FromName("Control")
        End If

        'DTPRequiredDate.Value = DateAdd(DateInterval.Day, Terms, CDate(TxtOrderDate.Text))

        'Check Bank Garantee
        If CheckBankGaranteeCustomer(CusNum) = True Then Exit Sub
        'Check Credit Amount
        If CheckCreditAmountCustomer(CusNum) = False Then Exit Sub

        App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)

        If RdbManually.Checked = False Then TimerDeltoLoading.Enabled = True
        'TimerDeltoLoading.Enabled = True
        TimerCategoryLoading.Enabled = True
    End Sub

    Private Function CheckCreditAmountCustomer(CusNum As String, Optional ByVal Amount As Double = 0) As Boolean
        Dim IsAllow As Boolean = False
        Dim InvNumber As Long = 0
        Dim ShipDate As Date = Nothing
        Dim GrandTotal As Double = 0
        Dim CreditAmount As Double = 0
        query = <SQL>
                    <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
                DECLARE @query AS NVARCHAR(MAX) = '';
                DECLARE @CreditLists AS TABLE
                (
	                [InvNumber] [DECIMAL](18,0) NULL,
	                [PONumber] [NVARCHAR](100) NULL,
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

        query = <SQL>
                    <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10);
                DECLARE @query AS NVARCHAR(MAX) = '';
                DECLARE @CreditLists AS TABLE
                (
	                [InvNumber] [DECIMAL](18,0) NULL,
	                [PONumber] [NVARCHAR](100) NULL,
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
        query = <SQL>
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
            App.SetEnableController(False, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
            With FrmAlertCreditAmount
                .Text = "Reminder(Need manager to access)"
                .LblMsg.Text = "Due Date " & MaxMonthAllow & " days. Latest day " & Days & " due invoice. Must notify customer. Latest Invoice Number = " & InvNumber & ", Ship Date = " & String.Format("{0:dd-MMM-yyyy}", ShipDate) & ",Amount $" & String.Format("{0:N2}", GrandTotal) & "." & vbCrLf & "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & ". This customer owes $" & CreditAmount & ". Not allow issue another invoice."
                .PicAlert.Visible = True
                .BtnOK.Visible = True
                .BtnYes.Visible = False
                .BtnNo.Visible = False
                .oCusNum = CusNum
                .loading.Enabled = True
                .DgvShow.DataSource = CreditAmountList
                .DgvShow.Refresh()
                .ShowDialog()
            End With
            IsAllow = False
        ElseIf (Days >= Terms) Then
            App.SetEnableController(False, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
            With FrmAlertCreditAmount
                .Text = "Reminder(Need account manager or manager to access)"
                .LblMsg.Text = "Terms Allow " & Terms & " days.  Maximum " & MaxMonthAllow & " days is allow due on latest invoice. Should notify customer. Latest Invoice Number = " & InvNumber & ", Ship Date = " & String.Format("{0:dd-MMM-yyyy}", ShipDate) & ",Amount $" & String.Format("{0:N2}", GrandTotal) & "." & vbCrLf & "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & ". This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Not allow issue another invoice."
                .PicAlert.Visible = False
                .BtnOK.Visible = False
                .BtnYes.Visible = True
                .BtnNo.Visible = True
                .oCusNum = CusNum
                .loading.Enabled = True
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
                        .oCusNum = CusNum
                        .loading.Enabled = True
                        .DgvShow.DataSource = CreditAmountList
                        .DgvShow.Refresh()
                        .ShowDialog()

                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
                            IsAllow = True
                        Else
                            IsAllow = False
                        End If
                    Else
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
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
            App.SetEnableController(False, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
            If CreditAmount >= CreditLimitAllow Then
                With FrmAlertCreditAmount
                    .Text = "Credit Limit (Need manager to access)"
                    .LblMsg.Text = "Credit limit allow for this customer = $" & String.Format("{0:N2}", CreditLimitAllow) & "." & vbCrLf & "This customer owes $" & String.Format("{0:N2}", CreditAmount) & ". Not allow issue another invoice."
                    .PicAlert.Visible = True
                    .BtnOK.Visible = True
                    .BtnYes.Visible = False
                    .BtnNo.Visible = False
                    .oCusNum = CusNum
                    .loading.Enabled = True
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
                    .oCusNum = CusNum
                    .loading.Enabled = True
                    .DgvShow.DataSource = CreditAmountList
                    .DgvShow.Refresh()
                    If .ShowDialog() = Windows.Forms.DialogResult.Yes Then
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = True Then
                            App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
                            IsAllow = True
                        Else
                            IsAllow = False
                        End If
                    Else
                        IsAllow = False
                    End If
                End With
            Else
                App.SetEnableController(True, BtnAdd, CmbProducts, CmbDelto, CmbCategory)
                IsAllow = True
            End If
        End If
CreditAllows:
        Return IsAllow
    End Function

    Private Function CheckBankGaranteeCustomer(CusNum As String) As Boolean
        Dim IsExpiry As Boolean = False
        Dim DateExpiry As Date
        Dim DateAlert As Date
        Dim CurDate As Date
        Dim IsAlertForm As Boolean = False
        Dim Msg As String = ""
        Dim Title As String = ""
        query = <SQL>
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

    Private Function CheckInfoCustomer(CusNum As String) As Boolean
        Dim IsExisted As Boolean = True
        query = <SQL>
                    <![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT [CusID],[CusNum],[CusName],[CusVat],[Terms],[Discount],[InvoiceDiscount],[CreditLimit],[CreditLimitAllow],[MaxMonthAllow],[ServiceRebate],[City],[AdditionalCost],[IssueUnitPrice],[Digit],[ServiceCost]
                FROM [Stock].[dbo].[TPRCustomer]
                WHERE [CusNum] = @CusNum;
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vServiceCost = CDbl(IIf(IsDBNull(lists.Rows(0).Item("ServiceCost")) = True, 0, lists.Rows(0).Item("ServiceCost")))
                CusItemDis = CSng(IIf(IsDBNull(lists.Rows(0).Item("Discount")) = True, 0, lists.Rows(0).Item("Discount")))
                CusInvoiceDis = CSng(IIf(IsDBNull(lists.Rows(0).Item("InvoiceDiscount")) = True, 0, lists.Rows(0).Item("InvoiceDiscount")))
                CusVAT = Trim(IIf(IsDBNull(lists.Rows(0).Item("CusVat")) = True, "", lists.Rows(0).Item("CusVat")))
                CusServiceRebate = CSng(IIf(IsDBNull(lists.Rows(0).Item("ServiceRebate")) = True, 0, lists.Rows(0).Item("ServiceRebate")))
                Terms = CInt(IIf(IsDBNull(lists.Rows(0).Item("Terms")) = True, 0, lists.Rows(0).Item("Terms")))
                CreditLimit = CDbl(IIf(IsDBNull(lists.Rows(0).Item("CreditLimit")) = True, 0, lists.Rows(0).Item("CreditLimit")))
                MaxMonthAllow = CInt(IIf(IsDBNull(lists.Rows(0).Item("MaxMonthAllow")) = True, 0, lists.Rows(0).Item("MaxMonthAllow")))
                CreditLimitAllow = CDbl(IIf(IsDBNull(lists.Rows(0).Item("CreditLimitAllow")) = True, 0, lists.Rows(0).Item("CreditLimitAllow")))
                vCity = Trim(IIf(IsDBNull(lists.Rows(0).Item("City")) = True, "", lists.Rows(0).Item("City")))
                vAdditionalCost = CBool(IIf(IsDBNull(lists.Rows(0).Item("AdditionalCost")) = True, 0, lists.Rows(0).Item("AdditionalCost")))
                vIssueUnitPrice = CBool(IIf(IsDBNull(lists.Rows(0).Item("IssueUnitPrice")) = True, 0, lists.Rows(0).Item("IssueUnitPrice")))
                vDigit = CInt(IIf(IsDBNull(lists.Rows(0).Item("Digit")) = True, 2, lists.Rows(0).Item("Digit")))
                IsExisted = True
            Else
                IsExisted = False
            End If
        Else
            IsExisted = False
        End If
        Return IsExisted
    End Function

    Private Sub TxtIdDelto_TextChanged(sender As Object, e As EventArgs) Handles TxtIdDelto.TextChanged
        CmbDelto.SelectedIndex = -1
        If TxtIdDelto.Text.Trim() = "" Then Exit Sub
        Dim Id As Long = CLng(IIf(TxtIdDelto.Text.Trim() = "", 0, TxtIdDelto.Text.Trim()))
        CmbDelto.SelectedValue = Id
    End Sub

    Private Sub TxtIdDelto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtIdDelto.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub CmbDelto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDelto.SelectedIndexChanged
        App.ClearController(TxtZone, TxtKhmerName)
        If TypeOf CmbDelto.SelectedValue Is DataRowView Or CmbDelto.SelectedValue Is Nothing Then Exit Sub
        If CmbDelto.Text.Trim() = "" Then Exit Sub
        Dim vCity As String = ""
        Dim vPeroid As Decimal = 0
        query = <SQL>
                    <![CDATA[
                        DECLARE @Id AS DECIMAL(18,0) = {1};
                        SELECT [DelTo],[Zone],[KhmerUnicode],[City]
                        FROM [Stock].[dbo].[TPRDelto]
                        WHERE [DefId] = @Id;
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, CmbDelto.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtZone.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("Zone")) = True, "", lists.Rows(0).Item("Zone")))
                TxtKhmerName.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("KhmerUnicode")) = True, "", lists.Rows(0).Item("KhmerUnicode")))
                vCity = Trim(IIf(IsDBNull(lists.Rows(0).Item("City")) = True, "", lists.Rows(0).Item("City")))
            End If
        End If
        If vCity.Trim().Equals("Phnom Penh") = False Then vCity = "Province"
        query = <SQL>
                    <![CDATA[
                        DECLARE @vLocation AS NVARCHAR(100) = N'{1}';
                        SELECT [Id],[Location],[Period],[CreatedDate]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrder_PeroidToProcess]
                        WHERE ([Location] = @vLocation)
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vCity)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vPeroid = CDec(IIf(IsDBNull(lists.Rows(0).Item("Period")) = True, 0, lists.Rows(0).Item("Period")))
            End If
        End If
        DTPRequiredDate.Value = Todate.AddHours(vPeroid)
        DTPDeliverDate.Value = Todate.AddHours(vPeroid)
        TxtRequiredDate.Text = String.Format("{0:dd-MMM-yyyy}", DTPRequiredDate.Value)

        'Dim vDeltoId As Decimal = CmbDelto.SelectedValue
        'query = _
        '<SQL>
        '    <![CDATA[
        '        DECLARE @query AS NVARCHAR(MAX) = N'';
        '        WITH v AS (
        '        SELECT [TableName],[Division]
        '        FROM [Stock].[dbo].[AAllMainDivision]
        '        WHERE ISNULL([Division],N'') <> N''
        '        UNION ALL
        '        SELECT [TableName],[Division]
        '        FROM [Stock].[dbo].[AAllMainDivisionInvoiceing]
        '        WHERE ISNULL([Division],N'') <> N'')

        '        SELECT @query += N'SELECT MAX([InvNumber]) AS [InvNumber],MAX([ShipDate]) AS [ShipDate],[CusNum],[CusCom],N''' + v.Division +  N''' AS [Division]
        '        FROM [Stock].[dbo].[' + v.TableName + N']
        '        WHERE [DeltoId] = @vDeltoId
        '        GROUP BY [CusNum],[CusCom]
        '        UNION ALL '
        '        FROM v 
        '        ORDER BY v.Division;
        '        SET @query += N'SELECT NULL AS [InvNumber],NULL AS [ShipDate],NULL AS [CusNum],NULL AS [CusCom],NULL AS [Division]';
        '        SELECT @query = N'
        '        DECLARE @vDeltoId AS DECIMAL(18,0) = {1};
        '        WITH o as (
        '        ' + @query + N'
        '        )
        '        SELECT *
        '        INTO #vCustomer
        '        FROM o
        '        WHERE o.InvNumber is not null;
        '        with v as (
        '        SELECT MAX(o.InvNumber) as InvNumber,MAX(o.ShipDate) as ShipDate,o.CusNum,o.CusCom
        '        FROM #vCustomer as o
        '        WHERE o.InvNumber is not null
        '        GROUP BY o.CusNum,o.CusCom)
        '        SELECT MAX(o.InvNumber) as InvNumber,MAX(o.ShipDate) as ShipDate,o.CusNum,o.CusCom,v.Division
        '        FROM v as o
        '        INNER JOIN #vCustomer as v on o.InvNumber = v.InvNumber and o.CusNum = v.CusNum
        '        WHERE o.InvNumber is not null
        '        GROUP BY o.CusNum,o.CusCom,v.Division
        '        ORDER BY MAX(o.ShipDate) DESC,o.CusCom;
        '        DROP TABLE #vCustomer;
        '        ';
        '        EXEC(@query);
        '    ]]>
        '</SQL>
        'query = String.Format(query, DatabaseName, vDeltoId)
        'lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        'If Not (lists Is Nothing) Then
        '    If lists.Rows.Count > 0 Then
        '        Dim vFrm As New FrmAlertCustomerHistory With {.vDeltoId = vDeltoId}
        '        vFrm.DgvShow.DataSource = lists
        '        vFrm.DgvShow.Refresh()
        '        vFrm.ShowDialog(MDI)
        '    End If
        'End If
    End Sub

    Private Sub TimerSalemanLoading_Tick(sender As Object, e As EventArgs) Handles TimerSalemanLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerSalemanLoading.Enabled = False
        query = <SQL>
                    <![CDATA[
                SELECT [Value],[Display],[SalesmanName]
                FROM (
	                SELECT 0 AS [Index],N'CUS00000' AS [Value], N'Admin' AS [Display], N'Admin' AS [SalesmanName]
	                UNION ALL
	                SELECT 0 AS [Index],ISNULL([SalesmanCode],'') AS [Value],ISNULL([SalesmanName],'') + ' (' + ISNULL([SalesmanNumber],'') + ')' AS [Display],[SalesmanName]
	                FROM [{0}].[dbo].[TblSetSalesmanToSalesManager]
	                WHERE [SalesmanCode] IN (SELECT [CusNum] FROM [Stock].[dbo].[TPRCustomer] WHERE [Status] = N'Activate')
	                GROUP BY ISNULL([SalesmanCode],''),ISNULL([SalesmanName],'') + ' (' + ISNULL([SalesmanNumber],'') + ')',[SalesmanName]
                ) lists
                GROUP BY [Index],[value],[Display],[SalesmanName]
                ORDER BY [Index],[SalesmanName];
            ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbSaleman, lists, "Display", "Value")
        Me.Cursor = Cursors.Default
    End Sub

    Private IsKeyPress As Boolean
    Private Sub CmbBillTo_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles CmbBillTo.PreviewKeyDown
        IsKeyPress = True
    End Sub

    Private Sub CmbBillTo_MouseDown(sender As Object, e As MouseEventArgs) Handles CmbBillTo.MouseDown
        IsKeyPress = False
        If Not (DeliveryTakeOrderList Is Nothing) Then
            If DeliveryTakeOrderList.Rows.Count > 0 Then
                If MessageBox.Show("Are you sure, you want to change Bill To?(Yes/No)" & vbCrLf & "Remark: If you change Bill To, All datas will lose.", "Confirm Change Bill To", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
                CreateDeliveryTakeOrderList()
            End If
        End If
    End Sub

    Private Sub TimerChecking_Tick(sender As Object, e As EventArgs) Handles TimerChecking.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerChecking.Enabled = False
        If CmbProducts.Text.Trim() = "" Then
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        'Check Customer Cash Van Existed Fixed Price Or Not
        query = <SQL><![CDATA[
                DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';
                WITH v AS (
	                SELECT [CusNum],[Barcode]
	                FROM [{0}].[dbo].[TblProductsPriceSetting]
	                WHERE ([CusNum] = @vCusNum) AND ([Barcode] = @vBarcode)
	                UNION ALL
	                SELECT o.[CusNum],v.[Barcode]
	                FROM [{0}].[dbo].[TblProductsPriceSetting] v
	                INNER JOIN [{0}].[dbo].[TblFixedPriceCustomerGroupSetting] o ON o.[GroupNumber] = ISNULL(v.[GroupNumber],0)
	                WHERE (o.[CusNum] = @vCusNum) AND (v.[Barcode] = @vBarcode)
                )
                SELECT v.*
                INTO #oPriceSetting
                FROM v;

                IF EXISTS(SELECT [CusNum] FROM [Stock].[dbo].[TPRCustomer] WHERE ([CusNum] = @vCusNum) AND (ISNULL([IsCashVan],0) = 1))
                BEGIN
	                IF EXISTS(SELECT * FROM #oPriceSetting)
	                BEGIN
		                SELECT N'NO_NEED' [Reason];
	                END
	                ELSE
	                BEGIN
		                SELECT N'NEED' [Reason];
	                END
                END
                ELSE
                BEGIN
	                SELECT N'NO_NEED' [Reason];
                END
                DROP TABLE #oPriceSetting;
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                Dim vReason As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Reason")) = True, "NO_NEED", lists.Rows(0).Item("Reason")))
                If (vReason.Trim().Equals("NEED") = True) Then
                    MessageBox.Show("Please contact to admin to set price for cash van first!", "Contact To Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    BtnAdd.Enabled = False
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If

        'Separate By Supplier
        Dim SupNumSeparate As String = ""
        query = <SQL><![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                SELECT [SupNum] FROM [{0}].[dbo].[TblSuppliers_IssuePOSeparately] WHERE [SupNum] IN (
                SELECT LEFT([Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                UNION ALL
                SELECT LEFT([Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                UNION ALL
                SELECT LEFT(A.[Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                UNION ALL
                SELECT LEFT(A.[Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode)
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                SupNumSeparate = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupNum")) = True, "", lists.Rows(0).Item("SupNum")))
            End If
        End If
        If SupNumSeparate.Trim() <> "" Then
            If Not (DeliveryTakeOrderList Is Nothing) Then
                Dim iSupNum As String = ""
                For Each r As DataRow In DeliveryTakeOrderList.Rows
                    iSupNum = Trim(IIf(IsDBNull(r.Item("SupNum")) = True, "", r.Item("SupNum")))
                    If SupNumSeparate.Trim.Equals(iSupNum) = False Then
                        MessageBox.Show("Please check product again!" & vbCrLf & "The supplier is separate from other supplier.", "Must Separate Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        BtnAdd.Enabled = False
                        Exit Sub
                    End If
                Next
            End If
        End If

        'Add Shipping Cost
        Dim AddShippingCost As Double = 0
        query = <SQL><![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                SELECT * 
                FROM [Stock].[dbo].[TPRCustomer] 
                WHERE [IsAddShippingCost] = 1 AND [CusNum] = @CusNum;
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                query = <SQL><![CDATA[
                        DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                        DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                        SELECT * 
                        FROM [Stock].[dbo].[TPRAddShippingCost] 
                        WHERE [CusNum] = @CusNum AND [Barcode] = @Barcode;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then
                        AddShippingCost = CDbl(IIf(IsDBNull(lists.Rows(0).Item("AmountAdd")) = True, 0, lists.Rows(0).Item("AmountAdd")))
                    Else
                        GoTo Msg_Check
                    End If
                Else
Msg_Check:
                    MessageBox.Show("Please contact to Office Manager to add shipping cost of product for this customer!", "Contact to Office Manager", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    BtnAdd.Enabled = False
                    Exit Sub
                End If
            End If
        End If

        'Deactivated Item
        Dim dtDC As DataTable = CheckDeactivatedItem()
        If dtDC.Rows.Count > 0 Then
            If MessageBox.Show("This Barcode is in Product Deactivated! Do you want to send email to customer?", "Confirm Deactivated Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            Dim email As String = CheckCustomerEmail().Rows(0)(0)
            If email.Equals(String.Empty) Then
                MessageBox.Show("This customer does not have email address.")
                Cursor = Cursors.Default
                Return
            End If

            Dim dcReport As New MailDCSKUReport
            Dim dr As DataRow = dtDC.Rows(0)
            With dcReport
                .paramDear.Value = String.Format("Dear {0},", CmbBillTo.Text)
                .paramPO.Value = TxtPONo.Text
                .paramBarcode.Value = dr("ProNumY")
                .paramName.Value = dr("ProName")
                .paramSize.Value = dr("ProPacksize")

                .CreateDocument(True)
                Try
                    Using client As New SmtpClient("mail.untwholesale.com", 26)
                        Using message As MailMessage = .ExportToMail("sales@untwholesale.com", email, "Product Discontinued")
                            Dim cc As New MailAddressCollection
                            For Each drEmail As DataRow In QueryCCEmail.Rows
                                message.CC.Add(drEmail(0))
                            Next

                            client.Credentials = New System.Net.NetworkCredential("sales@untwholesale.com", "UNT@@!@#12345678")
                            client.EnableSsl = True
                            client.Send(message)
                            MessageBox.Show("Your email has been sent successfully.")
                        End Using
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Send email failed.")
                End Try


            End With
        End If

        'Old Code
        If CheckOldItem() = True Then Exit Sub

        'Alert New Code
        If CheckNewCodeForCustomer() = True Then
            MessageBox.Show("The Barcode has been changed! Please inform customer first!", "Not Allow To Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BtnAdd.Enabled = False
            Exit Sub
        End If

        'Products
        Dim PackNumber As String = ""
        Dim vCaseNumber As String = ""
        query = <SQL><![CDATA[
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
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vCaseNumber = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumYC")) = True, "", lists.Rows(0).Item("ProNumYC")))
                PackNumber = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumYP")) = True, "", lists.Rows(0).Item("ProNumYP")))
                TxtQtyPerCase.Text = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPCase")) = True, 1, lists.Rows(0).Item("ProQtyPCase")))
                If IsDBNull(lists.Rows(0).Item("ProQtyPPack")) = True Then
                    QtyPerPack = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPPack")) = True, 1, lists.Rows(0).Item("ProQtyPPack")))
                Else
                    QtyPerPack = CInt(IIf(Trim(lists.Rows(0).Item("ProQtyPPack")) = "", 1, lists.Rows(0).Item("ProQtyPPack")))
                End If
                TxtStock.Text = String.Format("{0:N0}", CLng(IIf(IsDBNull(lists.Rows(0).Item("ProTotQty")) = True, 0, lists.Rows(0).Item("ProTotQty"))))
                WS = CDbl(IIf(IsDBNull(lists.Rows(0).Item("ProUPriSeH")) = True, 0, lists.Rows(0).Item("ProUPriSeH")))
                Currency = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProCurr")) = True, "CUR00001   USD   1", lists.Rows(0).Item("ProCurr")))
                Buyin = CDbl(IIf(IsDBNull(lists.Rows(0).Item("ProImpPri")) = True, 0, lists.Rows(0).Item("ProImpPri")))
                BuyDis = CSng(IIf(IsDBNull(lists.Rows(0).Item("ProDis")) = True, 0, lists.Rows(0).Item("ProDis")))
                BuyVAT = CSng(IIf(IsDBNull(lists.Rows(0).Item("ProVAT")) = True, 0, lists.Rows(0).Item("ProVAT")))
                Average = CDbl(IIf(IsDBNull(lists.Rows(0).Item("Average")) = True, 0, lists.Rows(0).Item("Average")))
                rSupNum = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupNum")) = True, "", lists.Rows(0).Item("SupNum")))
                rSupName = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupName")) = True, "", lists.Rows(0).Item("SupName")))
                If Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumYP")) = True, "", lists.Rows(0).Item("ProNumYP"))).Equals("") = True Then App.SetReadOnlyController(True, TxtPackOrder)
            Else
                GoTo Check_Item
            End If
        Else
Check_Item:
            MessageBox.Show("The Item is wrong. Please check item again...", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        'If CmbProducts.SelectedValue.Equals(PackNumber) = True Then
        '    TxtPcsOrder.ReadOnly = True
        '    TxtCTNOrder.ReadOnly = True
        '    TxtPackOrder.ReadOnly = False
        'Else
        '    TxtPcsOrder.ReadOnly = False
        '    TxtCTNOrder.ReadOnly = False
        '    TxtPackOrder.ReadOnly = True
        'End If
        TxtPcsOrder.ReadOnly = False
        TxtCTNOrder.ReadOnly = False
        TxtPackOrder.ReadOnly = False
        Dim CusDiscount As Single = (100 - CusItemDis) / 100
        Rate = CSng(Mid(Currency, 15, Currency.Length))
        Buyin = (Buyin / Rate)
        BuyDis = ((100 - BuyDis) / 100)
        BuyVAT = ((BuyVAT / 100) + 1)
        TotalBuyin = ((Buyin * BuyDis) * BuyVAT)
        WSAfter = ((WS * CusDiscount) + AddShippingCost)

        'If vIssueUnitPrice = True And (CmbProducts.SelectedValue.Equals(PackNumber) = True Or _
        '    CmbProducts.SelectedValue.Equals(vCaseNumber) = True) Then
        '    MessageBox.Show("Please check the barcode again!" & vbCrLf & "The customer's requested to unit price only." & vbCrLf & "Please select Unit Number for the customer.", "Not Allow To Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    BtnAdd.Enabled = False
        '    Me.Cursor = Cursors.Default
        '    Exit Sub
        'End If

        REM Products Delivery Logistic
        Dim vDeliveryLogistic As Double = 0
        If vAdditionalCost = True Then
            query = <SQL><![CDATA[
                    DECLARE @vBarcode AS NVARCHAR(MAX) = N'{1}';
                    DECLARE @vCity AS NVARCHAR(100) = N'{2}';
                    SELECT ISNULL([DeliveryCost],0) AS [DeliveryCost] 
                    FROM [{0}].[dbo].[TblProductsDeliveryLogistic] 
                    WHERE [ProId] IN (
                    SELECT [ProID] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @vBarcode OR ISNULL([ProNumYP],'') = @vBarcode OR ISNULL([ProNumYC],'') = @vBarcode)
                    UNION ALL
                    SELECT [ProId] FROM [Stock].[dbo].[TPRProductsOldCode] WHERE [OldProNumy] = @vBarcode) 
                    AND [City] = @vCity;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbProducts.SelectedValue, vCity)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    vDeliveryLogistic = CDbl(IIf(IsDBNull(lists.Rows(0).Item("DeliveryCost")) = True, 0, lists.Rows(0).Item("DeliveryCost")))
                    If vDeliveryLogistic = 0 Then GoTo Err_CheckAdditionalCost
                    WSAfter += vDeliveryLogistic
                    WS += vDeliveryLogistic
                    BtnAdd.Enabled = True
                Else
                    GoTo Err_CheckAdditionalCost
                End If
            Else
Err_CheckAdditionalCost:
                MessageBox.Show("Please check Additional Cost of product for this customer first!" & vbCrLf & "Please contact to Administrator.", "Not Allow To Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                BtnAdd.Enabled = False
                Exit Sub
            End If
        End If

        'Product Image
        Dim img() As Byte = Nothing
        query = <SQL><![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = N'{1}';
                SELECT [ProId],[ProImage]
                FROM [Stock].[dbo].[TPRProductsPicture]
                WHERE [ProId] IN (
                SELECT [ProID] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                UNION ALL
                SELECT [ProId] FROM [Stock].[dbo].[TPRProductsOldCode] WHERE [OldProNumy] = @Barcode);
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                img = IIf(IsDBNull(lists.Rows(0).Item("ProImage")) = True, Nothing, lists.Rows(0).Item("ProImage"))
                PicProducts.Image = App.BytetoImage(img)
            End If
        End If

        Dim ExclusiveImpSupList As DataTable = Nothing
        Dim ExclusiveCusList As DataTable = Nothing
        Dim SerRebate As Single = 0
        Dim vFixedId As Decimal = 0
        Dim Row As DataRow = Nothing
        Dim vWSTemp As Double = 0
        oFixedPriceQtyInvoicing = 0
        oIsFixedPrice = False
        'Fixed Prices
        query = <SQL><![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @Barcode AS NVARCHAR(MAX) = N'{2}';
                SELECT v.[Id],v.[ByinPrice],v.[WSPrice],v.[QtyInvoicing]
                FROM [{0}].[dbo].[TblProductsPriceSetting] AS v
                LEFT OUTER JOIN [{0}].[dbo].[TblFixedPriceCustomerGroupSetting] AS o ON ISNULL(v.[GroupNumber],0) = ISNULL(o.[GroupNumber],0)
                WHERE (DATEDIFF(DAY,GETDATE(),v.[PeriodFrom]) <= 0 AND DATEDIFF(DAY,GETDATE(),v.[PeriodTo]) >= 0) AND (ISNULL(v.[CusNum],o.[CusNum]) = @CusNum) AND v.[Barcode] = @Barcode;
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vFixedId = CInt(IIf(IsDBNull(lists.Rows(0).Item("Id")) = True, 0, lists.Rows(0).Item("Id")))
                vWSTemp = CDbl(IIf(IsDBNull(lists.Rows(0).Item("WSPrice")) = True, 0, lists.Rows(0).Item("WSPrice")))
                oFixedPriceQtyInvoicing = CInt(IIf(IsDBNull(lists.Rows(0).Item("QtyInvoicing")) = True, 0, lists.Rows(0).Item("QtyInvoicing")))
                Dim vTotalTO As Decimal = 0
                If (oFixedPriceQtyInvoicing > 0) Then
                    query = <SQL><![CDATA[
                            DECLARE @vFixedIdLink AS DECIMAL(18,0) = {1};
                            WITH v AS (
	                            SELECT SUM([QtyInvoicing]) [Qty]
	                            FROM [{0}].[dbo].[TblProductsPriceSetting.AllowableInvoicing]
	                            WHERE ([FixedIdLink] = @vFixedIdLink)
	                            UNION ALL
	                            SELECT SUM([QtyInvoicing]) [Qty]
	                            FROM [{0}].[dbo].[TblProductsPriceSetting.AllowableInvoicing.Completed]
	                            WHERE ([FixedIdLink] = @vFixedIdLink)
                            )
                            SELECT SUM(v.[Qty]) [Qty]
                            FROM v;
                        ]]></SQL>
                    query = String.Format(query, DatabaseName, vFixedId)
                    lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                    If Not (lists Is Nothing) Then
                        If lists.Rows.Count > 0 Then
                            vTotalTO = CInt(IIf(IsDBNull(lists.Rows(0).Item("Qty")) = True, 0, lists.Rows(0).Item("Qty")))
                        End If
                    End If

                    If (oFixedPriceQtyInvoicing > vTotalTO) Then
                        Dim vMsg As String = String.Format("~ Total Fixed Price: {4:C2} ({1:N0}){0}~ Used Fixed Price: {2:N0}{0}~ Left Fixed Price: {3:N0}{0}{0}Do you want to set fixed price for Item in the Take Order? (Yes/No)", vbCrLf, oFixedPriceQtyInvoicing, vTotalTO, (oFixedPriceQtyInvoicing - vTotalTO), vWSTemp)
                        If MessageBox.Show(vMsg, "Confirm Fixed Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            oIsFixedPrice = True
                            WSAfter = vWSTemp
                            Row = vFixedPriceList.NewRow
                            Row("FixedIdLink") = vFixedId
                            Row("Barcode") = CmbProducts.SelectedValue
                            Row("QtyInvoicing") = 1
                            vFixedPriceList.Rows.Add(Row)
                            GoTo WSPrice_Final
                        End If
                    End If
                Else
                    oIsFixedPrice = True
                    WSAfter = vWSTemp
                    GoTo WSPrice_Final
                End If
            End If
        End If
        'Fixed Prices Expiry Date
        query = <SQL><![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @Barcode AS NVARCHAR(MAX) = N'{2}';
                SELECT v.*
                FROM [{0}].[dbo].[TblProductsPriceSetting] AS v
                LEFT OUTER JOIN [{0}].[dbo].[TblFixedPriceCustomerGroupSetting] AS o ON ISNULL(v.[GroupNumber],0) = ISNULL(o.[GroupNumber],0)
                WHERE (DATEDIFF(DAY,GETDATE(),v.[PeriodFrom]) <= 0 AND DATEDIFF(DAY,GETDATE(),v.[PeriodTo]) < 0) AND (ISNULL(v.[CusNum],o.[CusNum]) = @CusNum) AND v.[Barcode] = @Barcode;
            ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                Dim oCusName As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("CusName")) = True, "", lists.Rows(0).Item("CusName")))
                Dim oBarcode As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Barcode")) = True, "", lists.Rows(0).Item("Barcode")))
                Dim oProName As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Description")) = True, "", lists.Rows(0).Item("Description")))
                Dim oWSFixed As Double = CDbl(IIf(IsDBNull(lists.Rows(0).Item("WSPrice")) = True, 0, lists.Rows(0).Item("WSPrice")))
                BtnAdd.Enabled = False
                MessageBox.Show(String.Format("The Fixed Price was expired Date.{0}Please contact to Office Manager to confirm price again!{0}► Customer Name: {1}{0}► Barcode: {2} {3}{0}► WS Fixed Price: {4:C2}", vbCrLf, oCusName, oBarcode, oProName, oWSFixed), "Expiry Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        'Customer Prices    
        'ExclusiveImpSup
        query = <SQL><![CDATA[
                        DECLARE @SupNum AS NVARCHAR(8) = '{1}';
                        SELECT [SupNo],[SupName]
                        FROM [Stock].[dbo].[TPRExclusiveImpSup]
                        WHERE [SupNo] = @SupNum;
                    ]]></SQL>
        query = String.Format(query, DatabaseName, rSupNum)
        ExclusiveImpSupList = Data.Selects(query, Initialized.GetConnectionType(Data, App))

        'ExclusiveCus        
        query = <SQL><![CDATA[
                        DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                        SELECT [CusNum],[CusName],[AddPercentImport],[BirthDate]
                        FROM [Stock].[dbo].[TPRExclusiveCus]
                        WHERE [CusNum] = @CusNum;
                    ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue)
        ExclusiveCusList = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If CusVAT.Trim.Equals("") = False Or CusVAT.Trim.Equals("0") = False Then
            SerRebate = ((CusServiceRebate / 100) + 1)
            If Not (ExclusiveImpSupList Is Nothing) Then
                If ExclusiveImpSupList.Rows.Count > 0 Then
                    If WSAfter > (Buyin * BuyDis) Then
                    Else
                        WSAfter = ((Buyin * BuyDis) * SerRebate)
                    End If
                    GoTo WSPrice_Final
                End If
            End If
            If Not (ExclusiveCusList Is Nothing) Then
                If ExclusiveCusList.Rows.Count > 0 Then WSAfter = ((Buyin * BuyDis) * SerRebate)
            End If
        Else
            If Not (ExclusiveImpSupList Is Nothing) Then
                If ExclusiveImpSupList.Rows.Count > 0 Then
                    If WSAfter > (Buyin * BuyDis) Then
                    Else
                        SerRebate = (((CusServiceRebate / 100) - (BuyDis / 100)) + 1)
                        WSAfter = (WSAfter * SerRebate)
                    End If
                    GoTo WSPrice_Final
                End If
            End If
            If (CusServiceRebate <> 0) Then
                SerRebate = ((CusServiceRebate / 100) + 1)
                WSAfter = (((Buyin * BuyDis) * BuyVAT) * SerRebate)
            Else
                If Not (ExclusiveCusList Is Nothing) Then
                    If ExclusiveCusList.Rows.Count > 0 Then WSAfter = ((Buyin * BuyDis) * BuyVAT)
                End If
            End If
        End If
WSPrice_Final:
        If Not (ExclusiveImpSupList Is Nothing) Then
            If ExclusiveImpSupList.Rows.Count > 0 Then
                If Not (ExclusiveCusList Is Nothing) Then
                    If ExclusiveCusList.Rows.Count > 0 Then
                        Dim AddPercentImport As Double = CSng(IIf(IsDBNull(ExclusiveCusList.Rows(0).Item("AddPercentImport")) = True, 0, ExclusiveCusList.Rows(0).Item("AddPercentImport")))
                        AddPercentImport = ((WSAfter * AddPercentImport) / 100)
                        WSAfter = (WSAfter + AddPercentImport)
                    End If
                End If
            End If
        End If
        WS += vServiceCost
        WSAfter += vServiceCost
        WSAfter = CDbl(String.Format("{0:N2}", WSAfter))
        TxtWSPrice.Text = String.Format("{0:N2}", WSAfter)
        IsCheckPromotion = True

        If RdbOnlinePO.Checked = True Then
            query = <SQL><![CDATA[
                            DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                            DECLARE @Delto AS NVARCHAR(100) = '{2}';
                            DECLARE @Barcode AS NVARCHAR(MAX) = '{3}';
                            SELECT [DateOrd],[DateRequired],[PONumber],[RemarkExpiry],SUM([PcsOrder]) AS [PcsOrder],SUM([CTNOrder]) AS [CTNOrder]
                            FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online]
                            WHERE [CusNum] = @CusNum AND [DelTo] = @Delto AND [ProNumy] = @Barcode
                            GROUP BY [DateOrd],[DateRequired],[PONumber],[RemarkExpiry];
                        ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbDelto.Text.Trim(), CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    TxtPcsOrder.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("PcsOrder")) = True, "", lists.Rows(0).Item("PcsOrder")))
                    TxtCTNOrder.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("CTNOrder")) = True, "", lists.Rows(0).Item("CTNOrder")))
                    TxtOrderDate.Text = String.Format("{0:dd-MMM-yyyy}", CDate(IIf(IsDBNull(lists.Rows(0).Item("DateOrd")) = True, Todate, lists.Rows(0).Item("DateOrd"))))
                    'DTPRequiredDate.Value = CDate(IIf(IsDBNull(lists.Rows(0).Item("DateRequired")) = True, Todate, lists.Rows(0).Item("DateRequired")))
                    'TxtPONo.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("PONumber")) = True, "", lists.Rows(0).Item("PONumber")))
                    TxtRemark.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("RemarkExpiry")) = True, "", lists.Rows(0).Item("RemarkExpiry")))
                End If
            End If
        End If

        query = <SQL><![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';
                        IF EXISTS(SELECT * FROM [{0}].[dbo].[TblCustomerInputQtyByBarcodeSetting] WHERE [CusNum] = @vCusNum)
                        BEGIN
                            IF EXISTS(SELECT * FROM [{0}].[dbo].[TblBarcodeSettingForCustomers] WHERE ([CusNum] = @vCusNum) AND (([UnitNumber] = @vBarcode) OR ([PackNumber] = @vBarcode) OR ([CaseNumber] = @vBarcode)))
                            BEGIN
	                            WITH v AS (
	                                SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumY = @vBarcode AND ISNULL(v.ProNumY,N'') <> N''
	                                UNION ALL
                                    SELECT N'PACK.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumYP = @vBarcode AND ISNULL(v.ProNumYP,N'') <> N''
	                                UNION ALL
                                    SELECT N'CASE.NUMBER' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumYC = @vBarcode AND ISNULL(v.ProNumYC,N'') <> N''
	                                UNION ALL
	                                SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumY = @vBarcode AND ISNULL(v.ProNumY,N'') <> N''
	                                UNION ALL
                                    SELECT N'PACK.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumYP = @vBarcode AND ISNULL(v.ProNumYP,N'') <> N''
	                                UNION ALL
                                    SELECT N'CASE.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumYC = @vBarcode AND ISNULL(v.ProNumYC,N'') <> N''
	                                UNION ALL
	                                SELECT N'UNIT.NUMBER' AS Value FROM [Stock].[dbo].[TPRProductsOldCode] AS v WHERE v.OldProNumy = @vBarcode AND ISNULL(v.OldProNumy,N'') <> N'')
	                                SELECT v.*
	                            FROM v;
                            END
                            ELSE
                            BEGIN
	                            SELECT TOP 0 N'' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumY = @vBarcode OR v.ProNumYP = @vBarcode OR v.ProNumYC = @vBarcode;
                            END
                        END
                        ELSE
                        BEGIN
	                        SELECT TOP 0 N'' AS Value FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumY = @vBarcode OR v.ProNumYP = @vBarcode OR v.ProNumYC = @vBarcode;
                        END
                    ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                Dim oValue As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Value")) = True, "", lists.Rows(0).Item("Value")))
                If oValue.Equals("UNIT.NUMBER") = True Then
                    vIssueUnitPrice_ = True
                    vIssuePackPrice = False
                    vIssueCasePrice = False
                    TxtPcsOrder.ReadOnly = False
                    TxtCTNOrder.ReadOnly = True
                    TxtPackOrder.ReadOnly = True
                    TxtPcsOrder.Focus()
                ElseIf oValue.Equals("PACK.NUMBER") = True Then
                    vIssueUnitPrice_ = False
                    vIssuePackPrice = True
                    vIssueCasePrice = False
                    TxtPcsOrder.ReadOnly = True
                    TxtCTNOrder.ReadOnly = True
                    TxtPackOrder.ReadOnly = False
                    TxtPackOrder.Focus()
                ElseIf oValue.Equals("CASE.NUMBER") = True Then
                    vIssueUnitPrice_ = False
                    vIssuePackPrice = False
                    vIssueCasePrice = True
                    TxtPcsOrder.ReadOnly = True
                    TxtCTNOrder.ReadOnly = False
                    TxtPackOrder.ReadOnly = True
                    TxtCTNOrder.Focus()
                End If
                TotalAmount()
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private IsCheckPromotion As Boolean
    Private oQtyInvoicing As Integer
    Private oPromotionId As Decimal
    Private Sub TimerCheckPromotion_Tick(sender As Object, e As EventArgs) Handles TimerCheckPromotion.Tick
        If CmbProducts.Text.Trim() = "" Then Exit Sub
        TimerCheckPromotion.Enabled = False
        Dim Barcode As String = CmbProducts.SelectedValue
        Dim CusNum As String = CmbBillTo.SelectedValue
        Dim iMechanic As String = ""
        Dim iCusNum As String = ""
        Dim iRemark As String = ""
        Dim iQtyBuy As Long = 0
        Dim iQtyFree As Long = 0
        Dim iDiscount As Single = 0
        Dim iBarcodeFree As String = ""
        Dim iProNameFree As String = ""
        Dim iSizeFree As String = ""
        Dim iCode As String = ""
        Dim iPeriodTo As Date = Todate
        Dim iIsBuyGroup As Boolean = False
        Dim iTotalPcsOrder As Long = CLng(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim()))
        Dim RQtyFree As Integer = 0
        Dim iIsLimited As Boolean = False
        Dim iLimitStock As Long = 0
        Dim iTotalFree As Long = 0
        Dim iFreeTemp As Long = 0
        Dim iGroupNum As Long = 0
        Dim ilists As DataTable = Nothing
        Dim iQtyPerCase As Integer = 0
        oQtyInvoicing = 0
        oPromotionId = 0
        App.ClearController(TxtBarcodeFree, TxtPcsFree, TxtItemDiscount)
        query = <SQL><![CDATA[
                DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                SELECT @Barcode = ISNULL([ProNumY],N'')
                FROM (
	                SELECT [ProNumY]
	                FROM [Stock].[dbo].[TPRProducts]
	                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                UNION ALL
	                SELECT B.[OldProNumy] AS [ProNumY]
	                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                WHERE B.[OldProNumy] = @Barcode
                    UNION ALL
                    SELECT [ProNumY]
	                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                UNION ALL
	                SELECT B.[OldProNumy] AS [ProNumY]
	                FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                WHERE B.[OldProNumy] = @Barcode
                ) LISTS;

                IF (@Barcode = '')
                BEGIN
                    SELECT TOP 0 * 
                    FROM [{0}].[dbo].[TblSuppliers_NoPromotionOrDiscount];
                END
                ELSE
                BEGIN
                    SELECT * 
                    FROM [{0}].[dbo].[TblSuppliers_NoPromotionOrDiscount] 
                    WHERE [SupNum] IN (
                    SELECT LEFT([Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                    UNION ALL
                    SELECT LEFT([Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                    UNION ALL
                    SELECT LEFT(A.[Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProID] WHERE B.[OldProNumy] = @Barcode
                    UNION ALL
                    SELECT LEFT(A.[Sup1],8) AS [SupNum] FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProID] WHERE B.[OldProNumy] = @Barcode);
                END
            ]]></SQL>
        query = String.Format(query, DatabaseName, Barcode)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                iRemark = "No Promotion/Discount For Supplier"
                GoTo Return_Promotion
            Else
                GoTo Promotion
            End If
        Else
Promotion:
            query = <SQL><![CDATA[
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                    SELECT @Barcode = ISNULL([ProNumY],N'')
                    FROM (
	                    SELECT [ProNumY]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                    UNION ALL
	                    SELECT B.[OldProNumy] AS [ProNumY]
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                    WHERE B.[OldProNumy] = @Barcode
                        UNION ALL
                        SELECT [ProNumY]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                    UNION ALL
	                    SELECT B.[OldProNumy] AS [ProNumY]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                    WHERE B.[OldProNumy] = @Barcode
                    ) LISTS;

                    SELECT [Id],[IsBuyGroup],[GroupNum],[Code],[Description],[Size],[QtyPerCase],[QtyBuy],[Mechanic],[BarcodeFree],[ProNameFree],[SizeFree],[QtyPerCaseFree],[IsPromotionGroup],[CusNum],[CusName],[QtyFree],[Discount],[CusNumInvolve],[CusNameInvolve],[IsLimited],[LimitedStock],[QtyInvoicing],[PeriodFrom],[PeriodTo]
                    FROM (
	                    SELECT [Id],[IsBuyGroup],0 AS [GroupNum],[Code],[Description],[Size],[QtyPerCase],[QtyBuy],[Mechanic],[BarcodeFree],[ProNameFree],[SizeFree],[QtyPerCaseFree],[IsPromotionGroup],[CusNum],[CusName],[QtyFree],[Discount],[CusNumInvolve],[CusNameInvolve],[IsLimited],[LimitedStock],[QtyInvoicing],[PeriodFrom],[PeriodTo]
	                    FROM [{0}].[dbo].[TblProductsPromotionSetting]
	                    WHERE (DATEDIFF(DAY,GETDATE(),[PeriodFrom]) <= 0 AND DATEDIFF(DAY,GETDATE(),[PeriodTo]) >= 0) AND [IsBuyGroup] = 0 AND [Code] = @Barcode
	                    UNION ALL
	                    SELECT A.[Id],A.[IsBuyGroup],A.[Code] AS [GroupNum],B.[Barcode] AS [Code],B.[ProName] AS [Description],B.[Size],B.[QtyPerCase],A.[QtyBuy],A.[Mechanic],A.[BarcodeFree],A.[ProNameFree],A.[SizeFree],A.[QtyPerCaseFree],A.[IsPromotionGroup],A.[CusNum],A.[CusName],A.[QtyFree],A.[Discount],A.[CusNumInvolve],A.[CusNameInvolve],A.[IsLimited],A.[LimitedStock],A.[QtyInvoicing],A.[PeriodFrom],A.[PeriodTo]
	                    FROM [{0}].[dbo].[TblProductsPromotionSetting] AS A INNER JOIN [{0}].[dbo].[TblProductsPromotionBarcodeGroupSetting] AS B ON A.[Code] = B.[GroupNumber]
	                    WHERE (DATEDIFF(DAY,GETDATE(),A.[PeriodFrom]) <= 0 AND DATEDIFF(DAY,GETDATE(),A.[PeriodTo]) >= 0) AND A.[IsBuyGroup] = 1 AND B.[Barcode] = @Barcode
                    ) LISTS
                    ORDER BY [IsBuyGroup],[Mechanic],[QtyBuy] DESC;
                ]]></SQL>
            query = String.Format(query, DatabaseName, Barcode)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                For Each r As DataRow In lists.Rows
                    iMechanic = Trim(IIf(IsDBNull(r.Item("Mechanic")) = True, "", r.Item("Mechanic")))
                    oPromotionId = CDec(IIf(IsDBNull(r.Item("ID")) = True, 0, r.Item("ID")))
                    iQtyBuy = CLng(IIf(IsDBNull(r.Item("QtyBuy")) = True, 0, r.Item("QtyBuy")))
                    iQtyFree = CLng(IIf(IsDBNull(r.Item("QtyFree")) = True, 0, r.Item("QtyFree")))
                    iDiscount = CSng(IIf(IsDBNull(r.Item("Discount")) = True, 0, r.Item("Discount")))
                    iBarcodeFree = Trim(IIf(IsDBNull(r.Item("barcodeFree")) = True, "", r.Item("barcodeFree")))
                    iProNameFree = Trim(IIf(IsDBNull(r.Item("ProNameFree")) = True, "", r.Item("ProNameFree")))
                    iSizeFree = Trim(IIf(IsDBNull(r.Item("SizeFree")) = True, "", r.Item("SizeFree")))
                    iPeriodTo = CDate(IIf(IsDBNull(r.Item("PeriodTo")) = True, Todate, r.Item("PeriodTo")))
                    iCode = Trim(IIf(IsDBNull(r.Item("Code")) = True, "", r.Item("Code")))
                    iIsBuyGroup = CBool(IIf(IsDBNull(r.Item("IsBuyGroup")) = True, 0, r.Item("IsBuyGroup")))
                    iIsLimited = CBool(IIf(IsDBNull(r.Item("IsLimited")) = True, 0, r.Item("IsLimited")))
                    iLimitStock = CLng(IIf(IsDBNull(r.Item("LimitedStock")) = True, 0, r.Item("LimitedStock")))
                    iGroupNum = CLng(IIf(IsDBNull(r.Item("GroupNum")) = True, 0, r.Item("GroupNum")))
                    iQtyPerCase = CInt(IIf(IsDBNull(r.Item("QtyPerCase")) = True, 1, r.Item("QtyPerCase")))
                    oQtyInvoicing = CInt(IIf(IsDBNull(r.Item("QtyInvoicing")) = True, 0, r.Item("QtyInvoicing")))

                    If iIsBuyGroup = True And (iMechanic.Trim().Equals("Discount And Free") = True Or iMechanic.Trim().Equals("Discount And Free For Customer") = True Or iMechanic.Trim().Equals("Free") = True Or iMechanic.Trim().Equals("Free For Customer") = True) Then
                        iTotalPcsOrder = 0
                        iTotalFree = 0
                        iFreeTemp = 0
                        If Not (DeliveryTakeOrderList Is Nothing) Then
                            For Each row As DataRow In DeliveryTakeOrderList.Rows
                                If CLng(IIf(IsDBNull(row.Item("PcsFree")) = True, 0, row.Item("PcsFree"))) = 0 Then
                                    query = <SQL>
                                                <![CDATA[
                                            DECLARE @GroupNumber AS INT = {1};
                                            DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                                            SELECT [Barcode] 
                                            FROM [{0}].[dbo].[TblProductsPromotionBarcodeGroupSetting] 
                                            WHERE [GroupNumber] = @GroupNumber AND [Barcode] = @Barcode;
                                        ]]>
                                            </SQL>
                                    query = String.Format(query, DatabaseName, iGroupNum, Trim(IIf(IsDBNull(row.Item("ProNumy")) = True, "", row.Item("ProNumy"))))
                                    ilists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                                    If Not (ilists Is Nothing) Then
                                        If ilists.Rows.Count > 0 Then
                                            iTotalPcsOrder += CLng(IIf(IsDBNull(row.Item("TotalPcsOrder")) = True, 0, row.Item("TotalPcsOrder")))
                                            iFreeTemp = 0
                                        Else
                                            iTotalFree -= iFreeTemp
                                        End If
                                    Else
                                        iTotalFree -= iFreeTemp
                                    End If
                                    If iFreeTemp < 0 Then iFreeTemp = 0
                                End If
                                iTotalFree += CLng(IIf(IsDBNull(row.Item("PcsFree")) = True, 0, row.Item("PcsFree")))
                                iFreeTemp += CLng(IIf(IsDBNull(row.Item("PcsFree")) = True, 0, row.Item("PcsFree")))
                                If iTotalFree > 0 Then
                                    If (iTotalPcsOrder >= iQtyBuy) Then
                                        iTotalPcsOrder -= (((iQtyBuy / iQtyPerCase) * (iTotalFree / iQtyFree)) * iQtyPerCase)
                                        iTotalFree = 0
                                    End If
                                End If
                            Next
                        End If
                        iTotalPcsOrder += CLng(IIf(Trim(TxtTotalPcsOrder.Text) = "", 0, TxtTotalPcsOrder.Text))
                        If iTotalPcsOrder <= 0 Then iTotalPcsOrder = CLng(IIf(Trim(TxtTotalPcsOrder.Text) = "", 0, TxtTotalPcsOrder.Text))
                    End If

                    REM Check Exception
                    If iMechanic.Trim.Equals("Discount And Free") = True Or iMechanic.Trim.Equals("Free") = True Or iMechanic.Trim.Equals("Volume Discount") = True Then
                        query = <SQL>
                                    <![CDATA[
                                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                                DECLARE @PromotionId AS DECIMAL(18,0) = {2};
                                SELECT [CusNum]
                                FROM (
	                                SELECT B.[CusNum] FROM [{0}].[dbo].[TblProductsPromotionCustomerException] AS A INNER JOIN [{0}].[dbo].[TblProductsPromotionGroupSetting] AS B ON CONVERT(INT,A.[CusNum]) = B.[GroupNumber] AND ISNUMERIC(A.[CusNum]) > 0 WHERE B.[CusNum] = @CusNum AND ([PromotionId] IS NOT NULL AND [PromotionId] = @PromotionId)
	                                UNION ALL
	                                SELECT B.[CusNum] FROM [{0}].[dbo].[TblProductsPromotionCustomerException] AS A INNER JOIN [{0}].[dbo].[TblProductsPromotionGroupSetting] AS B ON CONVERT(INT,A.[CusNum]) = B.[GroupNumber] AND ISNUMERIC(A.[CusNum]) > 0 WHERE B.[CusNum] = @CusNum AND [PromotionId] IS NULL 
	                                UNION ALL
	                                SELECT [CusNum] FROM [{0}].[dbo].[TblProductsPromotionCustomerException] WHERE ISNUMERIC([CusNum]) = 0 AND [CusNum] = @CusNum AND ([PromotionId] IS NOT NULL AND [PromotionId] = @PromotionId)
	                                UNION ALL
	                                SELECT [CusNum] FROM [{0}].[dbo].[TblProductsPromotionCustomerException] WHERE ISNUMERIC([CusNum]) = 0 AND [CusNum] = @CusNum AND [PromotionId] IS NULL
                                ) Lists;
                            ]]>
                                </SQL>
                        query = String.Format(query, DatabaseName, CusNum, oPromotionId)
                        ilists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                        If Not (ilists Is Nothing) Then
                            If ilists.Rows.Count > 0 Then
                                iRemark = ""
                                GoTo Return_Promotion
                            End If
                        End If
                    End If

                    REM Check Quantity Buy
                    If IsCheckPromotion = False And (iTotalPcsOrder < iQtyBuy) Then
                        iRemark = ""
                        GoTo Err_Skip_Step
                    End If

                    REM Customer Discount
                    If iMechanic.Trim.Equals("Customer Discount") = True Then
                        iCusNum = Trim(IIf(IsDBNull(r.Item("CusNum")) = True, "", r.Item("CusNum")))
                        If IsNumeric(iCusNum) = True Then
                            query = <SQL><![CDATA[
                                    DECLARE @GroupNumber AS INT = {1};
                                    DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                                    SELECT [CusNum] 
                                    FROM [{0}].[dbo].[TblProductsPromotionGroupSetting] 
                                    WHERE [GroupNumber] = @GroupNumber AND [CusNum] = @CusNum;
                                ]]></SQL>
                            query = String.Format(query, DatabaseName, iCusNum, CusNum)
                            ilists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                            If Not (ilists Is Nothing) Then
                                If ilists.Rows.Count > 0 Then
                                    iCusNum = Trim(IIf(IsDBNull(ilists.Rows(0).Item("CusNum")) = True, "", ilists.Rows(0).Item("CusNum")))
                                End If
                            End If
                        End If
                        If iCusNum.Trim.Equals(CusNum) = True Then
                            If IsCheckPromotion = False Then
                                TxtItemDiscount.Text = iDiscount
                                iRemark = ""
                                GoTo Return_Promotion
                            Else
                                iRemark = iRemark & "¤ Buy " & iQtyBuy & " pcs, discount " & iDiscount & " %. The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                            End If
                        End If
                    End If

                    REM Discount And Free
                    If iMechanic.Trim.Equals("Discount And Free") = True Then
                        If IsCheckPromotion = False Then
                            RQtyFree = (iTotalPcsOrder Mod iQtyBuy)
                            RQtyFree = ((iTotalPcsOrder - RQtyFree) / iQtyBuy)
                            RQtyFree = (RQtyFree * iQtyFree)
                            TxtBarcodeFree.Text = iBarcodeFree & Space(3) & iProNameFree & Space(3) & iSizeFree
                            TxtPcsFree.Text = RQtyFree
                            TxtItemDiscount.Text = iDiscount
                            iRemark = ""
                            GoTo Return_Promotion
                        Else
                            iRemark = iRemark & "¤ Buy " & Barcode & " (" & iQtyBuy & " pcs), free " & iBarcodeFree & " (" & iQtyFree & " pcs) & discount (" & iDiscount & "%). The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                        End If
                    End If

                    REM Discount And Free For Customer
                    If iMechanic.Trim.Equals("Discount And Free For Customer") = True Then
                        iCusNum = Trim(IIf(IsDBNull(r.Item("CusNumInvolve")) = True, 0, r.Item("CusNumInvolve")))
                        If IsNumeric(iCusNum) = True Then
                            query = <SQL>
                                        <![CDATA[
                                    DECLARE @GroupNumber AS INT = {1};
                                    DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                                    SELECT [CusNum] 
                                    FROM [{0}].[dbo].[TblProductsPromotionGroupSetting] 
                                    WHERE [GroupNumber] = @GroupNumber AND [CusNum] = @CusNum;
                                ]]>
                                    </SQL>
                            query = String.Format(query, DatabaseName, iCusNum, CusNum)
                            ilists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                            If Not (ilists Is Nothing) Then
                                If ilists.Rows.Count > 0 Then
                                    iCusNum = Trim(IIf(IsDBNull(ilists.Rows(0).Item("CusNum")) = True, "", ilists.Rows(0).Item("CusNum")))
                                End If
                            End If
                        End If
                        If iCusNum.Trim.Equals(CusNum) = True Then
                            If IsCheckPromotion = False Then
                                RQtyFree = (iTotalPcsOrder Mod iQtyBuy)
                                RQtyFree = ((iTotalPcsOrder - RQtyFree) / iQtyBuy)
                                RQtyFree = (RQtyFree * iQtyFree)
                                TxtBarcodeFree.Text = iBarcodeFree & Space(3) & iProNameFree & Space(3) & iSizeFree
                                TxtPcsFree.Text = RQtyFree
                                TxtItemDiscount.Text = iDiscount
                                iRemark = ""
                                GoTo Return_Promotion
                            Else
                                iRemark = iRemark & "¤ Buy " & Barcode & " (" & iQtyBuy & " pcs), free " & iBarcodeFree & " (" & iQtyFree & " pcs) & discount (" & iDiscount & " %). The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                            End If
                        End If
                    End If

                    REM Free
                    If iMechanic.Trim.Equals("Free") = True Then
                        If IsCheckPromotion = False Then
                            RQtyFree = (iTotalPcsOrder Mod iQtyBuy)
                            RQtyFree = ((iTotalPcsOrder - RQtyFree) / iQtyBuy)
                            RQtyFree = (RQtyFree * iQtyFree)
                            TxtBarcodeFree.Text = iBarcodeFree & Space(3) & iProNameFree & Space(3) & iSizeFree
                            TxtPcsFree.Text = RQtyFree
                            iRemark = ""
                            GoTo Return_Promotion
                        Else
                            iRemark = iRemark & "¤ Buy " & Barcode & " (" & iQtyBuy & " pcs), free " & iBarcodeFree & " (" & iQtyFree & " pcs). The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                        End If
                    End If

                    REM Free For Customer
                    If iMechanic.Trim.Equals("Free For Customer") = True Then
                        iCusNum = Trim(IIf(IsDBNull(r.Item("CusNumInvolve")) = True, 0, r.Item("CusNumInvolve")))
                        If IsNumeric(iCusNum) = True Then
                            query = <SQL>
                                        <![CDATA[
                                    DECLARE @GroupNumber AS INT = {1};
                                    DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                                    SELECT [CusNum] 
                                    FROM [{0}].[dbo].[TblProductsPromotionGroupSetting] 
                                    WHERE [GroupNumber] = @GroupNumber AND [CusNum] = @CusNum;
                                ]]>
                                    </SQL>
                            query = String.Format(query, DatabaseName, iCusNum, CusNum)
                            ilists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                            If Not (ilists Is Nothing) Then
                                If ilists.Rows.Count > 0 Then
                                    iCusNum = Trim(IIf(IsDBNull(ilists.Rows(0).Item("CusNum")) = True, "", ilists.Rows(0).Item("CusNum")))
                                End If
                            End If
                        End If
                        If iIsLimited = True And iLimitStock = 0 Then
                            iRemark = ""
                            GoTo Return_Promotion
                        End If
                        If StrComp(iCusNum, CusNum, vbTextCompare) = 0 Then
                            If IsCheckPromotion = False Then
                                RQtyFree = (iTotalPcsOrder Mod iQtyBuy)
                                RQtyFree = ((iTotalPcsOrder - RQtyFree) / iQtyBuy)
                                RQtyFree = (RQtyFree * iQtyFree)
                                TxtBarcodeFree.Text = iBarcodeFree & Space(3) & iProNameFree & Space(3) & iSizeFree
                                TxtPcsFree.Text = RQtyFree
                                iRemark = ""
                                GoTo Return_Promotion
                            Else
                                iRemark = iRemark & "¤ Buy " & Barcode & " (" & iQtyBuy & " pcs), free " & iBarcodeFree & " (" & iQtyFree & " pcs). The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                            End If
                        End If
                    End If

                    REM Volume Discount
                    If StrComp(iMechanic, "Volume Discount", vbTextCompare) = 0 Then
                        If IsCheckPromotion = False Then
                            TxtItemDiscount.Text = iDiscount
                            iRemark = ""
                            GoTo Return_Promotion
                        Else
                            iRemark = iRemark & "¤ Buy " & Barcode & " (" & iQtyBuy & " pcs), discount (" & iDiscount & "%). The end promotion on " & Format(iPeriodTo, "dd-MMM-yy") & "" & vbCrLf
                        End If
                    End If
Err_Skip_Step:
                Next
            End If
        End If
Return_Promotion:
        If IsCheckPromotion = True Then TxtNote.Text = iRemark
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If TxtPONo.Text.Trim() = "" Then
            MessageBox.Show("Please enter the P.O Number!", "Enter P.O Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtPONo.Focus()
            Exit Sub
        ElseIf TxtRequiredDate.Text.Trim() = "" Then
            MessageBox.Show("Please select Required Date!", "Select Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtRequiredDate.Focus()
            Exit Sub
        ElseIf CmbBillTo.Text.Trim() = "" Then
            MessageBox.Show("Please select any Bill To!", "Select Bill To", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbBillTo.Focus()
            Exit Sub
        ElseIf CmbDelto.Text.Trim() = "" Then
            MessageBox.Show("Please select any Delto!", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbDelto.Focus()
            Exit Sub
        ElseIf CmbSaleman.Text.Trim() = "" Then
            MessageBox.Show("Please select any saleman!", "Select Saleman", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbSaleman.Focus()
            Exit Sub
        ElseIf CLng(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim())) = 0 Then
            MessageBox.Show("Please enter the Quantity Order!", "Enter Quantity Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtCTNOrder.Focus()
            Exit Sub
            'ElseIf CLng(IIf(TxtStock.Text.Trim() = "", 0, TxtStock.Text.Trim())) = 0 Then
            '    MessageBox.Show("Please check stock again!" & vbCrLf & "Stock is not enough.", "Not Enough", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbProducts.Focus()
            '    Exit Sub
            'ElseIf CLng(IIf(TxtStock.Text.Trim() = "", 0, TxtStock.Text.Trim())) < CLng(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim())) Then
            '    MessageBox.Show("Please check stock again!" & vbCrLf & "Stock is not enough.", "Not Enough", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbProducts.Focus()
            '    Exit Sub
        Else
            query = <SQL><![CDATA[
                    DECLARE @vcusnum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vdeltoid AS DECIMAL(18,0) = {3};
                    DECLARE @vbarcode AS NVARCHAR(15) = N'{2}';
                    DECLARE @vponumber AS NVARCHAR(15) = N'{4}';
                    WITH v AS (
	                    SELECT [ponumber],[cusnum],[cusname]
	                    FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
	                    WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([ponumber] = @vponumber)
	                    GROUP BY [ponumber],[cusnum],[cusname]
	                    UNION ALL
	                    SELECT [ponumber],[cusnum],[cusname]
	                    FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
	                    WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([ponumber] = @vponumber)
	                    GROUP BY [ponumber],[cusnum],[cusname]
	                    UNION ALL
	                    SELECT [ponumber],[cusnum],[cusname]
	                    FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
	                    WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([ponumber] = @vponumber)
	                    GROUP BY [ponumber],[cusnum],[cusname]
	                    UNION ALL
	                    SELECT [ponumber],[cusnum],[cusname]
	                    FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.finish]
	                    WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([ponumber] = @vponumber) AND CONVERT(DATE,[dateorder]) >= CONVERT(DATE,DATEADD(MONTH,-1,GETDATE()))
	                    GROUP BY [ponumber],[cusnum],[cusname]
                    )
                    SELECT v.*
                    FROM v;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue, CmbDelto.SelectedValue, TxtPONo.Text.Replace("'", "").Trim())
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    MessageBox.Show("Please check PO Number again!" & vbCrLf & "Duplicated PO Number!", "Duplicated PO Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    With TxtPONo
                        .SelectionStart = 0
                        .SelectionLength = .TextLength
                        .Focus()
                    End With
                    Initialized.R_CorrectPassword = False
                    FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager'"
                    FrmPasswordContinues.ShowDialog()
                    If Initialized.R_CorrectPassword = False Then Exit Sub
                End If
            End If

            'Check Barcode Setting
            query = <SQL><![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                    IF EXISTS (
                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode
                    UNION ALL
                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
                    UNION ALL
                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
                    UNION ALL
                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode)
                    BEGIN
	                    SELECT [UnitNumber] AS [ProNumY]
	                    FROM [{0}].[dbo].[TblBarcodeSettingForCustomers]
	                    WHERE [CusNum] = @CusNum AND [UnitNumber] IN (
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode);
                    END
                    ELSE
                    BEGIN
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') <> '' AND ISNULL([ProNumY],'') = @Barcode) 
	                    UNION ALL
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE (ISNULL([ProNumY],'') <> '' AND ISNULL([ProNumY],'') = @Barcode)
	                    UNION ALL
	                    SELECT [OldProNumy] AS [ProNumY] FROM [Stock].[dbo].[TPRProductsOldCode] WHERE ISNULL([OldProNumy],'') <> '' AND ISNULL([OldProNumy],'') = @Barcode
                    END
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count <= 0 Then
                    MessageBox.Show("The barcode is Pack Number/Case Number." & vbCrLf & "Please set barcode for customer!", "Set Barcode For Customer First", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    With CmbProducts
                        .SelectionStart = 0
                        .SelectionLength = .Text.Length
                        .Focus()
                    End With
                    Exit Sub
                End If
            End If

            'Check Takeorder
            Dim iAllowDay As Integer = 2
            Dim iExisted As Boolean = False
            Dim Msg As String = ""
            Dim iIndex As Integer = 1
            Dim iHours As Integer = 0
            Dim iPONumber As String = ""
            query = <SQL><![CDATA[
                            DECLARE @vcusnum AS NVARCHAR(8) = N'{1}';
                            DECLARE @vdeltoid AS DECIMAL(18,0) = {3};
                            DECLARE @vbarcode AS NVARCHAR(15) = N'{2}';
                            DECLARE @vtotalpcsorder AS DECIMAL(18,0) = {4};
                            WITH v AS (
	                            SELECT [ponumber],[cusnum],[cusname],[takeordernumber] [TakeOrderInvoiceNumber],[dateorder] [DateOrd],DATEDIFF(DAY,[dateorder],GETDATE()) AS [NumberOfOrder] 
	                            FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
	                            WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([totalpcsorder] = @vtotalpcsorder)
	                            GROUP BY [ponumber],[cusnum],[cusname],[takeordernumber],[dateorder],DATEDIFF(DAY,[dateorder],GETDATE())
	                            UNION ALL
	                            SELECT [ponumber],[cusnum],[cusname],[takeordernumber] [TakeOrderInvoiceNumber],[dateorder] [DateOrd],DATEDIFF(DAY,[dateorder],GETDATE()) AS [NumberOfOrder] 
	                            FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
	                            WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([totalpcsorder] = @vtotalpcsorder)
	                            GROUP BY [ponumber],[cusnum],[cusname],[takeordernumber],[dateorder],DATEDIFF(DAY,[dateorder],GETDATE())
	                            UNION ALL
	                            SELECT [ponumber],[cusnum],[cusname],[takeordernumber] [TakeOrderInvoiceNumber],[dateorder] [DateOrd],DATEDIFF(DAY,[dateorder],GETDATE()) AS [NumberOfOrder] 
	                            FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
	                            WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([totalpcsorder] = @vtotalpcsorder)
	                            GROUP BY [ponumber],[cusnum],[cusname],[takeordernumber],[dateorder],DATEDIFF(DAY,[dateorder],GETDATE())	                            
                            )
                            SELECT v.*
                            FROM v;
                        ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue, CmbDelto.SelectedValue, CDec(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim())))
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    iPONumber = Trim(IIf(IsDBNull(lists.Rows(0).Item("ponumber")) = True, "", lists.Rows(0).Item("ponumber")))
                    For Each r As DataRow In lists.Rows
                        Msg &= iIndex & "). Take Order " & Trim(IIf(IsDBNull(r.Item("TakeOrderInvoiceNumber")) = True, "", r.Item("TakeOrderInvoiceNumber"))) & " On " & String.Format("{0:dd-MMM-yyyy}", CDate(IIf(IsDBNull(r.Item("DateOrd")) = True, Todate, r.Item("DateOrd")))) & vbCrLf
                        iIndex = iIndex + 1
                        iHours = CInt(IIf(IsDBNull(lists.Rows(0).Item("NumberOfOrder")) = True, 1, lists.Rows(0).Item("NumberOfOrder")))
                        iHours = (iHours * 24)
                        iExisted = True
                    Next
                End If
            End If
            If iExisted = True Then
                If iHours <= 72 Then
                    If TxtPONo.Text.Replace("'", "").Trim().Equals(iPONumber.Trim()) = True Then
                        If MessageBox.Show("Please check this take order again!" & vbCrLf & "The Take Order is existed!" & vbCrLf & Msg, "Duplicated Take Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
                        Initialized.R_CorrectPassword = False
                        Dim of1 As New FrmPasswordContinues
                        of1.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager'"
                        of1.ShowDialog()
                        If Initialized.R_CorrectPassword = False Then Exit Sub
                    Else
                        MessageBox.Show("Please check this take order again!" & vbCrLf & "The Take Order is existed!" & vbCrLf & Msg, "Duplicated Take Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                iAllowDay = 2
                iExisted = False
                Msg = ""
                iIndex = 1
                iHours = 0
                iPONumber = ""
                query = <SQL><![CDATA[
                                DECLARE @vcusnum AS NVARCHAR(8) = N'{1}';
                                DECLARE @vdeltoid AS DECIMAL(18,0) = {3};
                                DECLARE @vbarcode AS NVARCHAR(15) = N'{2}';
                                DECLARE @vtotalpcsorder AS DECIMAL(18,0) = {4};
                                WITH v AS (	                                
	                                SELECT [ponumber],[cusnum],[cusname],[takeordernumber] [TakeOrderInvoiceNumber],[dateorder] [DateOrd],DATEDIFF(DAY,[dateorder],GETDATE()) AS [NumberOfOrder] 
	                                FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.finish]
	                                WHERE ([cusnum] = @vcusnum) AND ([deltoid] = @vdeltoid) AND ([barcode] = @vbarcode) AND ([totalpcsorder] = @vtotalpcsorder) AND CONVERT(DATE,[dateorder]) >= CONVERT(DATE,DATEADD(MONTH,-1,GETDATE()))
	                                GROUP BY [ponumber],[cusnum],[cusname],[takeordernumber],[dateorder],DATEDIFF(DAY,[dateorder],GETDATE())
                                )
                                SELECT v.*
                                FROM v;
                            ]]></SQL>
                query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue, CmbDelto.SelectedValue, CDec(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim())))
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then
                        iPONumber = Trim(IIf(IsDBNull(lists.Rows(0).Item("ponumber")) = True, "", lists.Rows(0).Item("ponumber")))
                        For Each r As DataRow In lists.Rows
                            Msg &= iIndex & "). Take Order " & Trim(IIf(IsDBNull(r.Item("TakeOrderInvoiceNumber")) = True, "", r.Item("TakeOrderInvoiceNumber"))) & " On " & String.Format("{0:dd-MMM-yyyy}", CDate(IIf(IsDBNull(r.Item("DateOrd")) = True, Todate, r.Item("DateOrd")))) & vbCrLf
                            iIndex = iIndex + 1
                            iHours = CInt(IIf(IsDBNull(lists.Rows(0).Item("NumberOfOrder")) = True, 1, lists.Rows(0).Item("NumberOfOrder")))
                            iHours = (iHours * 24)
                            iExisted = True
                        Next
                    End If
                End If
                If iExisted = True Then
                    If iHours <= 48 Then
                        If TxtPONo.Text.Replace("'", "").Trim().Equals(iPONumber.Trim()) = True Then
                            If MessageBox.Show("Please check this take order <Finish Already> again!" & vbCrLf & "The Take Order <Finish Already> is existed!" & vbCrLf & Msg, "Duplicated Take Order <Finish Already>", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
                            Initialized.R_CorrectPassword = False
                            Dim of2 As New FrmPasswordContinues
                            of2.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager',N'TakeOrder'"
                            of2.ShowDialog()
                            If Initialized.R_CorrectPassword = False Then Exit Sub
                        Else
                            MessageBox.Show("Please check this take order <Finish Already> again!" & vbCrLf & "The Take Order <Finish Already> is existed!" & vbCrLf & Msg, "Duplicated Take Order <Finish Already>", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End If
            End If

            If Not (DeliveryTakeOrderList Is Nothing) Then
                Dim vBarcode As String = CmbProducts.SelectedValue
                For Each vRow As DataRow In DeliveryTakeOrderList.Rows
                    If Trim(IIf(IsDBNull(vRow("Barcode")) = True, "", vRow("Barcode"))).Equals(vBarcode) = True Then
                        MessageBox.Show("Please check the barcode again!", "Duplicated Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next
                query = <SQL><![CDATA[
                        DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                        SELECT [Id],[CusNum],[CusName],[CreatedDate]
                        FROM [{0}].[dbo].[TblCustomerSetting_SpecialInvoices]
                        WHERE [CusNum] = @CusNum;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue)
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then GoTo Err_Skip_SpecialInvoice
                End If

                If CusVAT.Trim.Equals("") = True Or CusVAT.Trim.Equals("0") = True Then
                    If DeliveryTakeOrderList.Rows.Count >= 21 Then
                        MessageBox.Show("The Invoice allow 21 rows only!", "21 Rows", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    If DeliveryTakeOrderList.Rows.Count >= 12 Then
                        MessageBox.Show("The Invoice allow 12 rows only!", "12 Rows", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
Err_Skip_SpecialInvoice:
            If TxtKhmerName.Text.Trim() = "" Then
                MessageBox.Show("Please, set delTo in khmer language." & vbCrLf & "Before continue...", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmbDelto.Focus()
                Exit Sub
            End If

            'Check Info Customer
            'If CheckInfoCustomer(CmbBillTo.SelectedValue) = False Then
            '    MessageBox.Show("This customer has been deleted! Please report to administrator!", "Not Existed Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    CmbBillTo.Focus()
            '    Exit Sub
            'End If

            'Check Credit Amount
            If IsDutchmill = False Then
                If CheckCreditAmountCustomer(CmbBillTo.SelectedValue, CDbl(IIf(TxtTotalAmount.Text.Trim() = "", 0, TxtTotalAmount.Text.Trim()))) = False Then Exit Sub
            End If

            query = <SQL><![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                    SELECT * 
                    FROM [Stock].[dbo].[TPRCustomerDiscontinueSKU]
                    WHERE [CusNum] = @CusNum AND [Barcode] = @Barcode;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    MessageBox.Show("This customer has notify UNT of discontinue already!", "Discontinue Selling By Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            'not allow when stock smaller than reserve for special customer
            query = <SQL><![CDATA[
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{1}';
                    DECLARE @QtyReserve AS INT = 0;
                    DECLARE @QtyOnHand AS INT = 0;
                    DECLARE @QtyPerCase AS INT = 1;

                    SELECT @QtyPerCase = ISNULL([ProQtyPCase],1)
                    FROM (
	                    SELECT [ProQtyPCase]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE ISNULL([ProNumY],'') = @Barcode AND ISNULL([ProNumYP],'') = @Barcode AND ISNULL([ProNumYC],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProQtyPCase]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE ISNULL([ProNumY],'') = @Barcode AND ISNULL([ProNumYP],'') = @Barcode AND ISNULL([ProNumYC],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProQtyPCase]
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON B.[ProId] = A.[ProID]
	                    WHERE ISNULL(B.[OldProNumy],'') = @Barcode
	                    UNION ALL
	                    SELECT [ProQtyPCase]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON B.[ProId] = A.[ProID]
	                    WHERE ISNULL(B.[OldProNumy],'') = @Barcode
                    ) LISTS
                    GROUP BY [ProQtyPCase];
                    SELECT @QtyReserve = SUM([CTNQtyReserve]) FROM [Stock].[dbo].[TPRMap_ProductReserveForSpecialCustomer] WHERE [ProNumy] = @Barcode;
                    SELECT @QtyOnHand = SUM([QtyOnHand]) FROM [Stock].[dbo].[TPRWarehouseStockIn] WHERE [ProNumy] = @Barcode;
                    SELECT CASE WHEN @QtyOnHand <= (@QtyReserve * @QtyPerCase) THEN 'NOT ENOUGH' ELSE 'ENOUGH' END AS [Result];
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    Dim Result As String = Trim(IIf(IsDBNull(lists.Rows(0).Item("Result")) = True, "", lists.Rows(0).Item("Result")))
                    If Result.Trim.Equals("NOT ENOUGH") = True Then
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = False Then Exit Sub
                    End If
                End If
            End If

            'not allow to go ahead when no special code
            query = <SQL><![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                    
                    SELECT @Barcode = [Barcode]
                    FROM (
	                    SELECT ISNULL([ProNumY],'') AS [Barcode] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumY],'') <> '' AND ISNULL([ProNumY],'') = @Barcode
                        UNION ALL
						SELECT ISNULL([ProNumYP],'') AS [Barcode] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode
                        UNION ALL
						SELECT ISNULL([ProNumYC],'') AS [Barcode] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
						UNION ALL
						SELECT ISNULL([ProNumY],'') AS [Barcode] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumY],'') <> '' AND ISNULL([ProNumY],'') = @Barcode
                        UNION ALL
						SELECT ISNULL([ProNumYP],'') AS [Barcode] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYP],'') <> '' AND ISNULL([ProNumYP],'') = @Barcode
                        UNION ALL
						SELECT ISNULL([ProNumYC],'') AS [Barcode] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYC],'') <> '' AND ISNULL([ProNumYC],'') = @Barcode
                        UNION ALL
                        SELECT B.[OldProNumy] AS [Barcode] FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                        UNION ALL
                        SELECT B.[OldProNumy] AS [Barcode] FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                    ) lists;

                    IF EXISTS (SELECT * FROM [{0}].[dbo].[TblCustomerCodes] WHERE [CusNum] = @CusNum)
                    BEGIN
	                    SELECT * 
	                    FROM [{0}].[dbo].[TblCustomerCodes]
	                    WHERE [CusNum] = @CusNum AND [Barcode] = @Barcode;
                    END
                    ELSE
                    BEGIN
	                    SELECT * 
	                    FROM [{0}].[dbo].[TblCustomerCodes];
                    END
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                Else
                    GoTo Err_msg
                End If
            Else
Err_msg:
                'If MessageBox.Show("Need Add special code first." & vbCrLf & "Do you want to go ahead?(Yes/No)", "Need Special Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
                MessageBox.Show("Need Add special code first." & vbCrLf & "Please contact to Office Manager...", "Not Allow To Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Fixed Prices
            query = <SQL><![CDATA[
                DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                DECLARE @Barcode AS NVARCHAR(MAX) = N'{2}';
                SELECT v.[ByinPrice],v.[WSPrice]
                FROM [{0}].[dbo].[TblProductsPriceSetting] AS v
                LEFT OUTER JOIN [{0}].[dbo].[TblFixedPriceCustomerGroupSetting] AS o ON ISNULL(v.[GroupNumber],0) = ISNULL(o.[GroupNumber],0)
                WHERE (DATEDIFF(DAY,GETDATE(),v.[PeriodFrom]) <= 0 AND DATEDIFF(DAY,GETDATE(),v.[PeriodTo]) >= 0) AND (ISNULL(v.[CusNum],o.[CusNum]) = @CusNum) AND v.[Barcode] = @Barcode;
            ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    If CDbl(IIf(IsDBNull(lists.Rows(0).Item("WSPrice")) = True, 0, lists.Rows(0).Item("WSPrice"))) = CDbl(IIf(TxtWSPrice.Text.Trim() = "", 0, TxtWSPrice.Text.Trim())) Then GoTo WSFixedPrice_Final
                End If
            End If

            'Check 1wk pricing
            Dim ProductList As DataTable = Nothing
            Dim ProductList1Week As DataTable = Nothing
            Dim IsProductListExisted As Boolean = False
            Dim IsProductList1WeekExisted As Boolean = False
            query = <SQL><![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';

                    SELECT @Barcode = [ProNumY]
                    FROM (
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                        UNION ALL
                        SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                        UNION ALL
                        SELECT B.[OldProNumy] AS [ProNumY] FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                        UNION ALL
                        SELECT B.[OldProNumy] AS [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                    ) lists;

                    SELECT * 
                    FROM [Stock].[dbo].[TPRWSCusProductList] 
                    WHERE [Cusnum] = @CusNum AND [ProNumY] = @Barcode;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            ProductList = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (ProductList Is Nothing) Then
                If ProductList.Rows.Count > 0 Then IsProductListExisted = True
            End If

            query = <SQL><![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                    DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';

                    SELECT @Barcode = [ProNumY]
                    FROM (
	                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                        UNION ALL
                        SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
                        UNION ALL
                        SELECT B.[OldProNumy] AS [ProNumY] FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                        UNION ALL
                        SELECT B.[OldProNumy] AS [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId] WHERE B.[OldProNumy] = @Barcode
                    ) lists;

                    SELECT * 
                    FROM [Stock].[dbo].[TPRWSCusProductList1Week] 
                    WHERE [Cusnum] = @CusNum AND [ProNumY] = @Barcode;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
            ProductList1Week = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (ProductList1Week Is Nothing) Then
                If ProductList1Week.Rows.Count > 0 Then IsProductList1WeekExisted = True
            End If
            Dim iWSAfter As Double = WSAfter 'CDbl(IIf(TxtWSPrice.Text.Trim() = "", 0, TxtWSPrice.Text.Trim()))
            Dim iWSCurrent As Double = 0
            Dim iWS1WKCurrent As Double = 0
            Dim iDate As Date = Todate
            Dim i1WKDate As Date = Todate
            Dim iQtyPerCase As Integer = 1
            If IsProductListExisted = False And IsProductList1WeekExisted = False Then
                MessageBox.Show("No price was set for this customer.", "Wholesale Price", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf IsProductListExisted = True And IsProductList1WeekExisted = True Then
                iWSCurrent = CDbl(IIf(IsDBNull(ProductList.Rows(0).Item("ProUPriSeH")) = True, 0, ProductList.Rows(0).Item("ProUPriSeH"))) + vServiceCost
                iDate = CDate(IIf(IsDBNull(ProductList.Rows(0).Item("Date")) = True, Todate, ProductList.Rows(0).Item("Date")))
                iQtyPerCase = CInt(IIf(IsDBNull(ProductList.Rows(0).Item("ProQtyPCase")) = True, 1, ProductList.Rows(0).Item("ProQtyPCase")))
                iWS1WKCurrent = CDbl(IIf(IsDBNull(ProductList1Week.Rows(0).Item("ProUPriSeH")) = True, 0, ProductList1Week.Rows(0).Item("ProUPriSeH"))) + vServiceCost
                i1WKDate = CDate(IIf(IsDBNull(ProductList1Week.Rows(0).Item("Date")) = True, Todate, ProductList1Week.Rows(0).Item("Date")))
                If (iWSAfter <> iWSCurrent) And (iWSCurrent <> iWS1WKCurrent) And (iWSAfter <> iWS1WKCurrent) Then
                    MessageBox.Show("Last Updated price in one week was on '" & String.Format("{0:dd-MMM-yyyy}", i1WKDate) & "' for this customer is " & String.Format("{0:N2}", iWS1WKCurrent) & "/" & iQtyPerCase & " PCS." & vbCrLf & "New current price is " & iWSAfter & "/" & iQtyPerCase & " PCS and your last update price was on '" & String.Format("{0:dd-MMM-yyyy}", iDate) & "' is " & iWSCurrent & "/" & iQtyPerCase & " PCS." & vbCrLf & "Please to go update price first.", "Not Allow Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                ElseIf (iWSAfter <> iWSCurrent) Then
                    MessageBox.Show("Last Updated price in one week was on '" & String.Format("{0:dd-MMM-yyyy}", i1WKDate) & "' for this customer is " & String.Format("{0:N2}", iWS1WKCurrent) & "/" & iQtyPerCase & " PCS." & vbCrLf &
                    "Your last previous updated price was on '" & String.Format("{0:dd-MMM-yyyy}", iDate) & "' is " & String.Format("{0:N2}", iWSCurrent) & "/" & iQtyPerCase & " PCS. " & vbCrLf &
                    "Once is one month old, you must update Customer product list first.", "Confirm Update Price", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    query = <SQL><![CDATA[
                            DECLARE @DateOrd AS DATE = '{1:yyyy-MM-dd}';
                            DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                            DECLARE @CusName AS NVARCHAR(100) = '';
                            DECLARE @Barcode AS NVARCHAR(MAX) = '{3}';
                            DECLARE @ProName AS NVARCHAR(100) = '';
                            DECLARE @Size AS NVARCHAR(10) = '';
                            DECLARE @QtyPerCase AS INT = 0;
                            DECLARE @WsPrice AS MONEY = {4};
                            DECLARE @PreviousWsPrice AS MONEY = {5};
                            DECLARE @OnewkWsPrice AS MONEY = {6};
                            SELECT @CusName=ISNULL([CusName],'') FROM [Stock].[dbo].[TPRCustomer] WHERE [CusNum] = @CusNum;
                            SELECT @ProName=[ProName],@Size=[ProPacksize],@QtyPerCase=[ProQtyPCase]
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
                            INSERT INTO [Stock].[dbo].[TPRWSCusProductListReminder]([DateOrd],[CusNum],[CusName],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[WsPrice],[PreviousWsPrice],[OnewkWsPrice])
                            VALUES(@DateOrd,@CusNum,@CusName,@Barcode,@ProName,@Size,@QtyPerCase,@WsPrice,@PreviousWsPrice,@OnewkWsPrice);
                        ]]></SQL>
                    query = String.Format(query, DatabaseName, CDate(IIf(TxtOrderDate.Text.Trim() = "", Todate, TxtOrderDate.Text.Trim())), CmbBillTo.SelectedValue, CmbProducts.SelectedValue, iWSAfter, iWSCurrent, iWS1WKCurrent)
                    Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                ElseIf (iWSAfter <> iWS1WKCurrent) Then
                    query = <SQL><![CDATA[
                            DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                            DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                            DELETE 
                            FROM [Stock].[dbo].[TPRDeliveryTakeOrderAcceptWholesalePrice] 
                            WHERE [CusNum] = @CusNum AND [Barcode] = @Barcode;
                        ]]></SQL>
                    query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
                    Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                    If MessageBox.Show("Last Updated price for this customer is " & String.Format("{0:N2}", iWS1WKCurrent) & "/" & iQtyPerCase & " PCS." & vbCrLf & "Do you want to update old pricing?(Yes/No)", "Confirm Update Old Price", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = False Then Exit Sub
                        If iQtyPerCase <> CInt(IIf(TxtQtyPerCase.Text.Trim() = "", iQtyPerCase, TxtQtyPerCase.Text.Trim())) Then TotalBuyin = (TotalBuyin / CInt(IIf(TxtQtyPerCase.Text.Trim() = "", iQtyPerCase, TxtQtyPerCase.Text.Trim()))) * iQtyPerCase
                        TxtWSPrice.Text = String.Format("{0:N2}", iWS1WKCurrent)
                        TxtQtyPerCase.Text = iQtyPerCase
                        TotalAmount()
                        query = <SQL><![CDATA[
                                DECLARE @CusNum AS NVARCHAR(8) = '{1}';
                                DECLARE @Barcode AS NVARCHAR(MAX) = '{2}';
                                DELETE FROM [Stock].[dbo].[TPRDeliveryTakeOrderAcceptWholesalePrice] WHERE [CusNum] = @CusNum AND [Barcode] = @Barcode;
                                INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrderAcceptWholesalePrice]([CusNum],[Barcode],[AcceptDate])
                                VALUES(@CusNum,@Barcode,GETDATE());
                            ]]></SQL>
                        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbProducts.SelectedValue)
                        Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                    ElseIf MsgBoxResult.No = Windows.Forms.DialogResult.No Then
                        Initialized.R_CorrectPassword = False
                        FrmPasswordContinues.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','TakeOrder'"
                        FrmPasswordContinues.ShowDialog()
                        If Initialized.R_CorrectPassword = False Then Exit Sub
                    Else
                        Exit Sub
                    End If
                End If
            ElseIf IsProductListExisted = True And IsProductList1WeekExisted = False Then
                iWSCurrent = CDbl(IIf(IsDBNull(ProductList.Rows(0).Item("ProUPriSeH")) = True, 0, ProductList.Rows(0).Item("ProUPriSeH"))) + vServiceCost
                iDate = CDate(IIf(IsDBNull(ProductList.Rows(0).Item("Date")) = True, Todate, ProductList.Rows(0).Item("Date")))
                iQtyPerCase = CInt(IIf(IsDBNull(ProductList.Rows(0).Item("ProQtyPCase")) = True, 1, ProductList.Rows(0).Item("ProQtyPCase")))
                If iWSAfter <> iWSCurrent Then
                    Dim Diff As Integer = DateDiff(DateInterval.Day, iDate, Todate)
                    If Diff > 30 Then
                        MessageBox.Show("Please update 'Wholesale Customer product list' for this customer first!", "Need Update Price List First", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    MessageBox.Show("Your last previous updated price was on '" & String.Format("{0:dd-MMM-yyyy}", iDate) & "' is " & String.Format("{0:N2}", iWSCurrent) & "/" & iQtyPerCase & " PCS. " & vbCrLf & "Once is one month old, you must update Customer product list first.", "Confirm Update Price", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    query = <SQL><![CDATA[
                            DECLARE @DateOrd AS DATE = '{1:yyyy-MM-dd}';
                            DECLARE @CusNum AS NVARCHAR(8) = '{2}';
                            DECLARE @CusName AS NVARCHAR(100) = '';
                            DECLARE @Barcode AS NVARCHAR(MAX) = '{3}';
                            DECLARE @ProName AS NVARCHAR(100) = '';
                            DECLARE @Size AS NVARCHAR(10) = '';
                            DECLARE @QtyPerCase AS INT = 0;
                            DECLARE @WsPrice AS MONEY = {4};
                            DECLARE @PreviousWsPrice AS MONEY = {5};
                            SELECT @CusName=ISNULL([CusName],'') FROM [Stock].[dbo].[TPRCustomer] WHERE [CusNum] = @CusNum;
                            SELECT @ProName=[ProName],@Size=[ProPacksize],@QtyPerCase=[ProQtyPCase]
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
                            INSERT INTO [Stock].[dbo].[TPRWSCusProductListReminder]([DateOrd],[CusNum],[CusName],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[WsPrice],[PreviousWsPrice])
                            VALUES(@DateOrd,@CusNum,@CusName,@Barcode,@ProName,@Size,@QtyPerCase,@WsPrice,@PreviousWsPrice);
                        ]]></SQL>
                    query = String.Format(query, DatabaseName, CDate(IIf(TxtOrderDate.Text.Trim() = "", Todate, TxtOrderDate.Text.Trim())), CmbBillTo.SelectedValue, CmbProducts.SelectedValue, iWSAfter, iWSCurrent)
                    Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
                End If
            ElseIf IsProductListExisted = False And IsProductList1WeekExisted = True Then
                iWSCurrent = CDbl(IIf(IsDBNull(ProductList1Week.Rows(0).Item("ProUPriSeH")) = True, 0, ProductList1Week.Rows(0).Item("ProUPriSeH")))
                iDate = CDate(IIf(IsDBNull(ProductList1Week.Rows(0).Item("Date")) = True, Todate, ProductList1Week.Rows(0).Item("Date")))
                iQtyPerCase = CInt(IIf(IsDBNull(ProductList1Week.Rows(0).Item("ProQtyPCase")) = True, 1, ProductList1Week.Rows(0).Item("ProQtyPCase")))
                If iWSAfter <> iWSCurrent Then
                    MessageBox.Show("No previous price, only have 1wk price, Updated was on '" & String.Format("{0:dd-MMM-yyyy}", iDate) & "' for this customer is " & iWSCurrent & "/" & iQtyPerCase & " PCS.", "Not Allow Continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
WSFixedPrice_Final:
            If DeliveryTakeOrderList.Rows.Count > 0 Then
                Dim iMapSupplier1 As DataTable = Nothing
                Dim iMapSupplier2 As DataTable = Nothing
                Dim iGroupName1 As String = ""
                Dim iGroupName2 As String = ""
                query = <SQL><![CDATA[
                        DECLARE @SupNum AS NVARCHAR(8) = '{1}';
                        SELECT *
                        FROM [Stock].[dbo].[TPRMap_SupplierToDelivery]
                        WHERE [SupNum] = @SupNum;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, rSupNum)
                iMapSupplier1 = Data.Selects(query, Initialized.GetConnectionType(Data, App))

                query = <SQL><![CDATA[
                        DECLARE @SupNum AS NVARCHAR(8) = '{1}';
                        SELECT *
                        FROM [Stock].[dbo].[TPRMap_SupplierToDelivery]
                        WHERE [SupNum] = @SupNum;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, Trim(IIf(IsDBNull(DeliveryTakeOrderList.Rows(0).Item("SupNum")) = True, "", DeliveryTakeOrderList.Rows(0).Item("SupNum"))))
                iMapSupplier2 = Data.Selects(query, Initialized.GetConnectionType(Data, App))

                If iMapSupplier1.Rows.Count > 0 Then
                    If iMapSupplier2.Rows.Count > 0 Then
                        iGroupName1 = Trim(IIf(IsDBNull(iMapSupplier1.Rows(0).Item("GroupName")) = True, "", iMapSupplier1.Rows(0).Item("GroupName")))
                        iGroupName2 = Trim(IIf(IsDBNull(iMapSupplier2.Rows(0).Item("GroupName")) = True, "", iMapSupplier2.Rows(0).Item("GroupName")))
                        If iGroupName1.Equals(iGroupName2) = True Then
                        Else
                            GoTo Err_WrongGroup
                        End If
                    Else
Err_WrongGroup:
                        MessageBox.Show("Wrong group of division products!", "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    If iMapSupplier2.Rows.Count > 0 Then GoTo Err_WrongGroup
                End If

                'Prevent Duplicate
                Dim iBarcode As String = ""
                For Each r As DataRow In DeliveryTakeOrderList.Rows
                    iBarcode = Trim(IIf(IsDBNull(r.Item("ProNumy")) = True, "", r.Item("ProNumy")))
                    If CmbProducts.SelectedValue.Equals(iBarcode) = True Then
                        MessageBox.Show("This product already existed in this order!" & vbCrLf & "You cannot add this product anymore.", "Duplicated Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next
            End If

            Dim Row As DataRow = Nothing
            Dim vTotalTO As Integer = 0
            Dim vAcceptPromotionSetting As Boolean = False
            REM CHECK T.O FOR PROMOTION
            If (oQtyInvoicing > 0) Then
                query = <SQL><![CDATA[
                        DECLARE @vPromotionIdLink AS DECIMAL(18, 0) = {1};
DECLARE @vCusNum AS NVARCHAR(8) = N'{2}';

WITH    v AS ( SELECT   SUM([QtyInvoicing]) [Qty]
               FROM     [{0}].[dbo].[TblProductsPromotionSetting.AllowableInvoicing]
               WHERE    ( [PromotionIdLink] = @vPromotionIdLink )
               UNION ALL
               SELECT   SUM([QtyInvoicing]) [Qty]
               FROM     [{0}].[dbo].[TblProductsPromotionSetting.AllowableInvoicing.Completed]
               WHERE    ( [PromotionIdLink] = @vPromotionIdLink )
             )
    SELECT  SUM(v.[Qty]) [Qty]
    INTO    #oPromotion
    FROM    v;

DECLARE @oCusNum_ AS NVARCHAR(8)= N'';
SELECT  @oCusNum_ = [CusNumInvolve]
FROM    [{0}].[dbo].[TblProductsPromotionSetting]
WHERE   ( [Id] = @vPromotionIdLink )
        AND ( [CusNum] = @vCusNum );

IF ( @oCusNum_ = @vCusNum )
    BEGIN
        UPDATE  v
        SET     v.[Qty] = 0
        FROM    #oPromotion v;
    END;                        
                    

SELECT  SUM(v.[Qty]) [Qty]
FROM    #oPromotion v;

DROP TABLE #oPromotion;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, oPromotionId, CmbBillTo.SelectedValue)
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then
                        vTotalTO = CInt(IIf(IsDBNull(lists.Rows(0).Item("Qty")) = True, 0, lists.Rows(0).Item("Qty")))
                    End If
                End If

                If (oQtyInvoicing > vTotalTO) Then
                    Dim vMsg As String = String.Format("~ Total Promotion: {1:N0}{0}~ Used Promotion: {2:N0}{0}~ Left Promotion: {3:N0}{0}{0}Do you want to set promotion for Take Order? (Yes/No)", vbCrLf, oQtyInvoicing, vTotalTO, (oQtyInvoicing - vTotalTO))
                    If MessageBox.Show(vMsg, "Confirm Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Row = vPromotionList.NewRow
                        Row("PromotionIdLink") = oPromotionId
                        Row("QtyInvoicing") = 1
                        vPromotionList.Rows.Add(Row)
                        vAcceptPromotionSetting = True
                    Else
                        TxtItemDiscount.Text = "0"
                        vAcceptPromotionSetting = False
                        GoTo err_skippromotion
                    End If
                Else
                    vAcceptPromotionSetting = False
                    GoTo err_skippromotion
                End If
            End If

            If TxtBarcodeFree.Text.Trim() <> "" And CInt(IIf(TxtPcsFree.Text.Trim() = "", 0, TxtPcsFree.Text.Trim())) <> 0 Then
                Dim iBarcodeFree() As String = TxtBarcodeFree.Text.Split(Space(3))
                query = <SQL><![CDATA[
                        DECLARE @Barcode AS NVARCHAR(MAX) = N'{1}';
                        SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],[SupNum],[SupName],[ProCat]
                        FROM (
	                        SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                        FROM [Stock].[dbo].[TPRProducts]
	                        WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                        UNION ALL
	                        SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                        FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                        WHERE B.[OldProNumy] = @Barcode
                            UNION ALL
                            SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated]
	                        WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                        UNION ALL
	                        SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                        FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                        WHERE B.[OldProNumy] = @Barcode
                        ) LISTS;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, iBarcodeFree(0).Trim())
                lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
                If Not (lists Is Nothing) Then
                    If lists.Rows.Count > 0 Then
                        Row = DeliveryTakeOrderList.NewRow
                        Row("Barcode") = iBarcodeFree(0).Trim()
                        Row("ProNumy") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumy")) = True, "", lists.Rows(0).Item("ProNumy")))
                        Row("ProNumyP") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumyP")) = True, "", lists.Rows(0).Item("ProNumyP")))
                        Row("ProNumyC") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumyC")) = True, "", lists.Rows(0).Item("ProNumyC")))
                        Row("ProName") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProName")) = True, "", lists.Rows(0).Item("ProName")))
                        Row("ProPackSize") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProPackSize")) = True, "", lists.Rows(0).Item("ProPackSize")))
                        Row("ProQtyPCase") = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPCase")) = True, 1, lists.Rows(0).Item("ProQtyPCase")))
                        Row("ProCat") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProCat")) = True, "", lists.Rows(0).Item("ProCat")))
                        Row("SupNum") = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupNum")) = True, "", lists.Rows(0).Item("SupNum")))
                        Row("SupName") = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupName")) = True, "", lists.Rows(0).Item("SupName")))
                        Row("PcsFree") = CInt(IIf(TxtPcsFree.Text.Trim() = "", 0, TxtPcsFree.Text.Trim()))
                        Row("PcsOrder") = 0
                        Row("PackOrder") = 0
                        Row("CTNOrder") = 0
                        Row("TotalPcsOrder") = CInt(IIf(TxtPcsFree.Text.Trim() = "", 0, TxtPcsFree.Text.Trim()))
                        Row("ItemDiscount") = 0
                        Row("PromotionMachanic") = ""
                        Row("Remark") = ""
                        Row("TotalAmount") = 0
                        DeliveryTakeOrderList.Rows.Add(Row)
                    End If
                End If
            End If
err_skippromotion:

            query = <SQL><![CDATA[
                    DECLARE @Barcode AS NVARCHAR(MAX) = N'{1}';
                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],[SupNum],[SupName],[ProCat]
                    FROM (
	                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                    FROM [Stock].[dbo].[TPRProducts]
	                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                    UNION ALL
	                    SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                    WHERE B.[OldProNumy] = @Barcode
                        UNION ALL
                        SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated]
	                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                    UNION ALL
	                    SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                    WHERE B.[OldProNumy] = @Barcode
                    ) LISTS;
                ]]></SQL>
            query = String.Format(query, DatabaseName, CmbProducts.SelectedValue)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    Row = DeliveryTakeOrderList.NewRow
                    Row("Barcode") = CmbProducts.SelectedValue
                    Row("ProNumy") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumy")) = True, "", lists.Rows(0).Item("ProNumy")))
                    Row("ProNumyP") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumyP")) = True, "", lists.Rows(0).Item("ProNumyP")))
                    Row("ProNumyC") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProNumyC")) = True, "", lists.Rows(0).Item("ProNumyC")))
                    Row("ProName") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProName")) = True, "", lists.Rows(0).Item("ProName")))
                    Row("ProPackSize") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProPackSize")) = True, "", lists.Rows(0).Item("ProPackSize")))
                    Row("ProQtyPCase") = CInt(IIf(IsDBNull(lists.Rows(0).Item("ProQtyPCase")) = True, 1, lists.Rows(0).Item("ProQtyPCase")))
                    Row("ProCat") = Trim(IIf(IsDBNull(lists.Rows(0).Item("ProCat")) = True, "", lists.Rows(0).Item("ProCat")))
                    Row("SupNum") = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupNum")) = True, "", lists.Rows(0).Item("SupNum")))
                    Row("SupName") = Trim(IIf(IsDBNull(lists.Rows(0).Item("SupName")) = True, "", lists.Rows(0).Item("SupName")))
                    Row("PcsFree") = 0
                    Row("PcsOrder") = CInt(IIf(TxtPcsOrder.Text.Trim() = "", 0, TxtPcsOrder.Text.Trim()))
                    Row("PackOrder") = CInt(IIf(TxtPackOrder.Text.Trim() = "", 0, TxtPackOrder.Text.Trim()))
                    Row("CTNOrder") = CSng(IIf(TxtCTNOrder.Text.Trim() = "", 0, TxtCTNOrder.Text.Trim()))
                    Row("TotalPcsOrder") = CInt(IIf(TxtTotalPcsOrder.Text.Trim() = "", 0, TxtTotalPcsOrder.Text.Trim()))
                    Row("ItemDiscount") = CSng(IIf(TxtItemDiscount.Text.Trim() = "", 0, TxtItemDiscount.Text.Trim()))
                    Row("PromotionMachanic") = TxtNote.Text.Trim()
                    Row("Remark") = TxtRemark.Text.Trim()
                    Row("TotalAmount") = CDbl(IIf(TxtTotalAmount.Text.Trim().Equals("") = True, 0, TxtTotalAmount.Text.Trim()))
                    DeliveryTakeOrderList.Rows.Add(Row)
                Else
                    GoTo Err_Item
                End If
            Else
Err_Item:
                MessageBox.Show("The Item is wrong. Please check item again...", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If RdbOnlinePO.Checked = True Then TimerProductLoading.Enabled = True
            ClearProductItems()
            App.SetEnableController(False, PanelHeader, PanelBillTo, CmbSaleman)
            CmbProducts.Focus()
            LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        End If
    End Sub

    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles BtnFinish.Click
        If DeliveryTakeOrderList.Rows.Count <= 0 Then
            MessageBox.Show("No record to process finish.", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim vMaxDay As Integer = App.GetDayPerMonth(Todate.Date)
        Dim vAlertDay As Integer = 0
        Dim vIncludeDay As Integer = 0
        Dim vDeliveryDate As Date = Todate.Date
        query = <SQL><![CDATA[
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        SELECT [Id],[CusNum],[CusName],[AlertDay],[IncludeDay],[CreatedDate]
                        FROM [{0}].[dbo].[TblCustomerAlertDeliveryDate]
                        WHERE ([CusNum] = @vCusNum);
                    ]]></SQL>
        query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                vAlertDay = CInt(IIf(DBNull.Value.Equals(lists.Rows(0).Item("AlertDay")) = True, (vMaxDay - 2), lists.Rows(0).Item("AlertDay")))
                vIncludeDay = CInt(IIf(DBNull.Value.Equals(lists.Rows(0).Item("IncludeDay")) = True, 0, lists.Rows(0).Item("IncludeDay")))
                vDeliveryDate = vDeliveryDate.AddDays(vIncludeDay)
                If String.Format("{0:ddd}", vDeliveryDate).Trim().Equals("Sun") = True Then vDeliveryDate = vDeliveryDate.AddDays(1)
                If Todate.Date.Day >= vAlertDay Then
                    If MessageBox.Show(String.Format("This is on {0:dddd, dd-MMM-yyyy}.{2}For P.O Number, Do you want to change Delivery Date to <{1:dddd, dd-MMM-yyyy}>?(Yes/No)", Todate, vDeliveryDate, vbCrLf), "Confirm Delivery Date", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        DTPDeliverDate.Value = vDeliveryDate
                    End If
                End If
            End If
        End If

        REM Prevent Caltex smaller then 10
        Dim vTotalInvoicing As Double = 0
        For Each r As DataRow In DeliveryTakeOrderList.Rows
            vTotalInvoicing += CDbl(IIf(DBNull.Value.Equals(r.Item("TotalAmount")) = True, 0, r.Item("TotalAmount")))
        Next
        Dim oisprevent As Boolean = False
        query = <SQL><![CDATA[
                            DECLARE @vcusnum AS NVARCHAR(8) = N'{3}';
                            DECLARE @vbarcode AS NVARCHAR(15) = N'{4}';
                            IF EXISTS(SELECT * FROM [DBInvoicing].[dbo].[.tblcustomer.prevent.amount] WHERE ([cusnum] = @vcusnum))
                            BEGIN
	                            IF EXISTS(SELECT * FROM [DBInvoicing].[dbo].[.tblcustomer.prevent.amount] WHERE ([cusnum] = @vcusnum) AND ([barcode] = @vbarcode))
	                            BEGIN
		                            SELECT N'' [Status];
	                            END
	                            ELSE
	                            BEGIN
		                            SELECT N'Stop' [Status];
	                            END
                            END
                            ELSE
                            BEGIN
	                            SELECT N'' [Status];
                            END
                        ]]></SQL>
        query = String.Format(query, DatabaseName, "", "", CmbBillTo.SelectedValue, DeliveryTakeOrderList.Rows(0).Item("Barcode"))
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                If Trim(IIf(DBNull.Value.Equals(lists.Rows(0).Item("Status")) = True, "", lists.Rows(0).Item("Status"))).Trim().Equals("") = True Then
                    oisprevent = False
                Else
                    oisprevent = True
                End If
            End If
        End If

        If (vTotalInvoicing < 10) And (oisprevent = True) Then
            MessageBox.Show("The total price is smaller than 10 is not allow for caltex." & vbCrLf & "Please fill password delivery before continue!", "Need password", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim vF As New FrmPasswordContinues
            vF.R_PasswordPermission = "'Managing Director','MD Assistant','IT Manager','Office Manager','Delivery Manager'"
            If vF.ShowDialog() = DialogResult.Cancel Then Exit Sub
        End If

        If MessageBox.Show("Are you sure, you already to process this take order?(Yes/No)", "Confirm Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Initialized.R_MessageAlert = ""
        Initialized.R_DocumentNumber = ""
        Initialized.R_LineCode = ""
        Initialized.R_DeptCode = ""

        If IsDutchmill = False Then
            If FrmDeliveryTakeOrderMessage.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            Dim vF As New FrmDeliveryTakeOrderInfoAeon
            vF.TxtDocumentNumber.Text = TxtPONo.Text.Trim()
            vF.ShowDialog()
        End If

        'Check Invoice Number
        Dim InvNo As Long = 0
        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RCom.Connection = RCon
        RCom.CommandType = CommandType.Text
        RCom.CommandText = String.Format("SELECT * FROM [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo]", Data.PrefixDatabase, Data.DatabaseName)
        lists = New DataTable
        lists.Load(RCom.ExecuteReader())
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                If CBool(IIf(IsDBNull(lists.Rows(0).Item("IsBusy")) = True, 0, lists.Rows(0).Item("IsBusy"))) = True Then
                    RCon.Close()
                    MessageBox.Show("Printer is busy!" & vbCrLf & "Please wait a few minutes..." & vbCrLf & "Another PC is using...", "Printer Is Busy - Invoice Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    InvNo = CLng(IIf(IsDBNull(lists.Rows(0).Item("PrintInvNo")) = True, 0, lists.Rows(0).Item("PrintInvNo")))
                    RCom.CommandText = String.Format("UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 1", Data.PrefixDatabase, Data.DatabaseName)
                    RCom.ExecuteNonQuery()
                End If
            Else
                GoTo Err_Insert
            End If
        Else
Err_Insert:
            RCom.CommandText = String.Format("INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo]([PrintInvNo],[IsBusy]) VALUES(0,0)", Data.PrefixDatabase, Data.DatabaseName)
            RCom.ExecuteNonQuery()
            InvNo = 0
        End If
        RCon.Close()
        InvNo += 1

        'Key
        Dim Key As Long = 0
        query = <SQL><![CDATA[
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
                    ]]></SQL>
        query = String.Format(query, DatabaseName, rSupNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                Key = CLng(IIf(IsDBNull(lists.Rows(0).Item("AutoNumber")) = True, 0, lists.Rows(0).Item("AutoNumber")))
            End If
        End If
        Dim RelatedKey As String = String.Format("{0:yyMMdd}{1}", Todate, Key)

        RCon.Open()
        RTran = RCon.BeginTransaction()
        Try
            RCom.Transaction = RTran
            RCom.Connection = RCon
            RCom.CommandType = CommandType.Text
            For Each r As DataRow In vPromotionList.Rows
                query = <SQL><![CDATA[
                                DECLARE @vTakeOrderNumber AS DECIMAL(18,0) = {1};
                                DECLARE @vPromotionIdLink AS DECIMAL(18,0) = {2};
                                DECLARE @vQtyInvoicing AS INT = 1;
                                INSERT INTO [{0}].[dbo].[TblProductsPromotionSetting.AllowableInvoicing]([TakeOrderNumber],[PromotionIdLink],[QtyInvoicing],[CreatedDate])
                                VALUES(@vTakeOrderNumber,@vPromotionIdLink,@vQtyInvoicing,GETDATE());
                            ]]></SQL>
                query = String.Format(query, DatabaseName, InvNo,
                                      CInt(IIf(IsDBNull(r.Item("PromotionIdLink")) = True, 0, r.Item("PromotionIdLink"))),
                                      CInt(IIf(IsDBNull(r.Item("QtyInvoicing")) = True, 0, r.Item("QtyInvoicing"))))
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                Application.DoEvents()
            Next

            For Each r As DataRow In DeliveryTakeOrderList.Rows
                query = <SQL><![CDATA[
                                DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                                DECLARE @CusName AS NVARCHAR(100) = N'';
                                DECLARE @DelToId AS DECIMAL(18,0) = {2} ;
                                DECLARE @DelTo AS NVARCHAR(100) = N'';
                                DECLARE @DateOrd AS DATE = N'{3:yyyy-MM-dd}';
                                DECLARE @DateRequired AS DATE = N'{4:yyyy-MM-dd}';
                                DECLARE @UnitNumber AS NVARCHAR(15) = N'';
                                DECLARE @Barcode AS NVARCHAR(15) = N'{5}';
                                DECLARE @ProName AS NVARCHAR(100) = N'';
                                DECLARE @Size AS NVARCHAR(10) = N'';
                                DECLARE @QtyPCase AS INT = 1;
                                DECLARE @QtyPPack AS INT = 1;
                                DECLARE @Category AS NVARCHAR(100) = N'';
                                DECLARE @PcsFree AS INT = {6};
                                DECLARE @PcsOrder AS INT = {7};
                                DECLARE @PackOrder AS INT = {8};
                                DECLARE @CTNOrder AS FLOAT = {9};
                                DECLARE @TotalPcsOrder AS INT = 0;
                                DECLARE @PONumber AS NVARCHAR(50) = N'{10}';
                                DECLARE @LogInName AS NVARCHAR(100) = N'{11}';
                                DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {12};
                                DECLARE @PromotionMachanic AS NVARCHAR(500) = N'{13}';
                                DECLARE @ItemDiscount AS FLOAT = {14};
                                DECLARE @Remark AS NVARCHAR(100) = N'{15}';
                                DECLARE @Saleman AS NVARCHAR(100) = N'{16}';
                                DECLARE @OnlinePO AS BIT = {17};
                                DECLARE @RelatedKey AS NVARCHAR(10) = N'{18}';
                                DECLARE @IsDutchmill AS BIT = {19};
                                DECLARE @DeliveryDate AS DATE = N'{20:yyyy-MM-dd}';

                                SELECT @CusName = [CusName] FROM [Stock].[dbo].[TPRCustomer] WHERE [CusNum] = @CusNum;
                                SELECT @DelTo = [DelTo] FROM [Stock].[dbo].[TPRDelto] WHERE [DefId] = @DelToId;
                                SELECT @UnitNumber=[ProNumY],@ProName=[ProName],@Size=[ProPacksize],@QtyPCase=ISNULL([ProQtyPCase],1),@QtyPPack=ISNULL([ProQtyPPack],1),@Category=[ProCat]
                                FROM (
	                                SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                                FROM [Stock].[dbo].[TPRProducts]
	                                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                                UNION ALL
	                                SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                                FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                                WHERE B.[OldProNumy] = @Barcode
	                                UNION ALL
	                                SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[ProQtyPPack],[ProTotQty],[ProCurr],[ProImpPri],[ProDis],[ProVAT],[ProFinBuyin],[Average],[ProUPrSE],[ProUPriSeH],LEFT([Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING([Sup1],9,LEN([Sup1])))) AS [SupName],[ProCat]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated]
	                                WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                                UNION ALL
	                                SELECT B.[OldProNumy] AS [ProNumY],A.[ProNumYP],A.[ProNumYC],A.[ProName],A.[ProPacksize],A.[ProQtyPCase],A.[ProQtyPPack],B.[Stock] AS [ProTotQty],A.[ProCurr],A.[ProImpPri],A.[ProDis],A.[ProVAT],A.[ProFinBuyin],A.[Average],A.[ProUPrSE],A.[ProUPriSeH],LEFT(A.[Sup1],8) AS [SupNum],LTRIM(RTRIM(SUBSTRING(A.[Sup1],9,LEN(A.[Sup1])))) AS [SupName],A.[ProCat]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]
	                                WHERE B.[OldProNumy] = @Barcode
                                ) LISTS;
                                SET @TotalPcsOrder = ((@CTNOrder*@QtyPCase) + (@PackOrder*@QtyPPack) + @PcsOrder + @PcsFree);
                                IF (@IsDutchmill = 0)
                                BEGIN
                                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeliveryDate])
                                    VALUES(@CusNum,@CusName,@DelToId,@DelTo,@DateOrd,@DateRequired,@UnitNumber,@Barcode,@ProName,@Size,@QtyPCase,@QtyPPack,@Category,@PcsFree,@PcsOrder,@PackOrder,@CTNOrder,@TotalPcsOrder,@PONumber,@LogInName,@TakeOrderNumber,@PromotionMachanic,@ItemDiscount,@Remark,@Saleman,GETDATE(),@DeliveryDate);

                                    IF (@OnlinePO = 1)
                                    BEGIN
                                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrder_OnlineFinish]([CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[PromotionMachanic],[ProCat],[TranDate],[RemarkExpiry],[Saleman],[UserId],[UOM],[OrderNum],[Status],[CreatedDate],[FinishDate],[DeliveryDate])
                                        SELECT [CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[PromotionMachanic],[ProCat],[TranDate],[RemarkExpiry],[Saleman],[UserId],[UOM],[OrderNum],[Status],[CreatedDate],GETDATE(),@DeliveryDate
                                        FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online]
                                        WHERE [CusNum] = @CusNum AND [DelTo] = @DelTo AND [ProNumy] = @Barcode AND ISNULL([TotalPcsOrder],0) = @TotalPcsOrder;

                                        DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrder_Online]
                                        WHERE [CusNum] = @CusNum AND [DelTo] = @DelTo AND [ProNumy] = @Barcode AND ISNULL([TotalPcsOrder],0) = @TotalPcsOrder;
                                    END

                                    INSERT INTO [DBStockHistory].[dbo].[TblProcessHistory]([UnitNumber],[PackNumber],[CaseNumber],[ProName],[Size],[QtyPerCase],[Supplier],[ProgramName],[TransDate],[BeforeStock],[TransQty],[EndStock],[InvNumber],[Name],[Batchcode],[Location],[RelatedKey],[CreatedDate])  
                                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],N'Delivery Take Order' AS [ProgramName],[TransDate],[BeforeStock], @TotalPcsOrder  AS [TransQty],[EndStock],@TakeOrderNumber AS [InvNumber],@CusNum + SPACE(3) + @CusName AS [Name],NULL AS [Batchcode],NULL AS [Location],@RelatedKey,GETDATE()  
                                    FROM (  
	                                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([ProTotQty],0) AS [BeforeStock],ISNULL([ProTotQty],0) AS [EndStock]  
	                                    FROM [Stock].[dbo].[TPRProducts]  
	                                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                                    UNION ALL  
	                                    SELECT [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([ProTotQty],0) AS [BeforeStock],ISNULL([ProTotQty],0) AS [EndStock]  
	                                    FROM [Stock].[dbo].[TPRProductsDeactivated]  
	                                    WHERE (ISNULL([ProNumY],'') = @Barcode OR ISNULL([ProNumYP],'') = @Barcode OR ISNULL([ProNumYC],'') = @Barcode)
	                                    UNION ALL  
	                                    SELECT [OldProNumy] AS [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([Stock],0) AS [BeforeStock],ISNULL([Stock],0) AS [EndStock]  
	                                    FROM [Stock].[dbo].[TPRProducts] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]  
	                                    WHERE B.[OldProNumy] = @Barcode
	                                    UNION ALL  
	                                    SELECT [OldProNumy] AS [ProNumY],[ProNumYP],[ProNumYC],[ProName],[ProPacksize],[ProQtyPCase],[Sup1],CONVERT(DATE,GETDATE()) AS [TransDate],ISNULL([Stock],0) AS [BeforeStock],ISNULL([Stock],0) AS [EndStock]  
	                                    FROM [Stock].[dbo].[TPRProductsDeactivated] AS A INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS B ON A.[ProID] = B.[ProId]  
	                                    WHERE B.[OldProNumy] = @Barcode
                                    ) LISTS;

                                    DELETE FROM [Stock].[dbo].[TPRDeliveryTakeOrderUrgentCome]
                                    WHERE [CusNum] = @CusNum AND [Delto] = @DelTo AND [ProNumy] IN (
                                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumY],'') = @Barcode
                                    UNION ALL  
                                    SELECT [ProNumYP] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYP],'') = @Barcode
                                    UNION ALL  
                                    SELECT [ProNumYC] FROM [Stock].[dbo].[TPRProducts] WHERE ISNULL([ProNumYC],'') = @Barcode
                                    UNION ALL 
                                    SELECT [ProNumY] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumY],'') = @Barcode
                                    UNION ALL  
                                    SELECT [ProNumYP] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYP],'') = @Barcode
                                    UNION ALL  
                                    SELECT [ProNumYC] FROM [Stock].[dbo].[TPRProductsDeactivated] WHERE ISNULL([ProNumYC],'') = @Barcode
                                    UNION ALL  
                                    SELECT [OldProNumy] FROM [Stock].[dbo].[TPRProductsOldCode] WHERE [OldProNumy] = @Barcode);
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate],[DeliveryDate])
                                    VALUES(@CusNum,@CusName,@DelToId,@DelTo,@DateOrd,@DateRequired,@UnitNumber,@Barcode,@ProName,@Size,@QtyPCase,@QtyPPack,@Category,@PcsFree,@PcsOrder,@PackOrder,@CTNOrder,@TotalPcsOrder,@PONumber,@LogInName,@TakeOrderNumber,@PromotionMachanic,@ItemDiscount,@Remark,@Saleman,GETDATE(),@DeliveryDate);
                                END

                                DECLARE @oSupNum AS NVARCHAR(8) = N'';
                                DECLARE @oStock AS DECIMAL(18,0) = 0;
                                WITH oList AS (
	                                SELECT LEFT(v.Sup1,8) AS [SupNum],v.ProTotQty AS Stock FROM [Stock].[dbo].[TPRProducts] AS v WHERE v.ProNumY = @UnitNumber
	                                UNION 
	                                SELECT LEFT(v.Sup1,8) AS [SupNum],v.ProTotQty AS Stock FROM [Stock].[dbo].[TPRProductsDeactivated] AS v WHERE v.ProNumY = @UnitNumber
	                                UNION 
	                                SELECT LEFT(v.Sup1,8) AS [SupNum],o.Stock  FROM [Stock].[dbo].[TPRProducts] AS v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS o ON o.ProId = v.ProID WHERE o.OldProNumy = @UnitNumber
	                                UNION 
	                                SELECT LEFT(v.Sup1,8) AS [SupNum],o.Stock FROM [Stock].[dbo].[TPRProductsDeactivated] AS v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] AS o ON o.ProId = v.ProID WHERE o.OldProNumy = @UnitNumber
                                )
                                SELECT o.SupNum, ISNULL(SUM(o.Stock),0) AS Stock
                                INTO #vList
                                FROM oList AS o
                                GROUP BY o.SupNum;
                                SELECT @oSupNum = o.SupNum, @oStock = ISNULL(SUM(o.Stock),0)
                                FROM #vList AS o
                                GROUP BY o.SupNum;
                                IF NOT EXISTS (SELECT * FROM [Stock].[dbo].[TPRDeliveryDutchmill] WHERE SupNum IN (SELECT o.SupNum FROM #vList AS o GROUP BY o.SupNum))
                                BEGIN
	                                DECLARE @oTotalPcsOrder AS DECIMAL(18,0) = 0;
	                                WITH oList AS (
		                                --SELECT ISNULL(SUM([TotalPcsOrder]),0) AS [TotalPcsOrder]
		                                --FROM [Stock].[dbo].[TPRDeliveryTakeOrder]
		                                --WHERE [ProNumy] = @UnitNumber
		                                --UNION ALL
		                                --SELECT ISNULL(SUM([TotalPcsOrder]),0) AS [TotalPcsOrder]
		                                --FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaitingPicking]
		                                --WHERE [ProNumy] = @UnitNumber
		                                --UNION ALL
		                                --SELECT ISNULL(SUM([TotalPcsOrder]),0) AS [TotalPcsOrder]
		                                --FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaiting]
		                                --WHERE [ProNumy] = @UnitNumber
		                                --UNION ALL
		                                --SELECT ISNULL(SUM([TotalPcsOrder]),0) AS [TotalPcsOrder]
		                                --FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrint]
		                                --WHERE [ProNumy] = @UnitNumber
                                        --UNION ALL
                                        SELECT SUM([totalpcsorder]) [TotalPcsOrder]
                                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry]
                                        WHERE ([unitnumber] = @UnitNumber)
                                        UNION ALL
                                        SELECT SUM([totalpcsorder]) [TotalPcsOrder]
                                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.picking]
                                        WHERE ([unitnumber] = @UnitNumber)
                                        UNION ALL
                                        SELECT SUM([totalpcsorder]) [TotalPcsOrder]
                                        FROM [DBPickers].[dbo].[.tbldeliverytakeorders.dry.invoicing]
                                        WHERE ([unitnumber] = @UnitNumber)
	                                )
	                                SELECT @oTotalPcsOrder = ISNULL(SUM(o.TotalPcsOrder),0)
	                                FROM oList AS o;

	                                IF (@oStock < @oTotalPcsOrder)
	                                BEGIN    
		                                INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_AlertNotEnoughStock]([CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],[CreatedDate])
		                                VALUES(@CusNum,@CusName,@DelToId,@DelTo,@DateOrd,@DateRequired,@UnitNumber,@Barcode,@ProName,@Size,@QtyPCase,@QtyPPack,@Category,@PcsFree,@PcsOrder,@PackOrder,@CTNOrder,@TotalPcsOrder,@PONumber,@LogInName,@TakeOrderNumber,@PromotionMachanic,@ItemDiscount,@Remark,@Saleman,GETDATE());
	                                END
                                END
                                DROP TABLE #vList;
                            ]]></SQL>
                query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, CmbDelto.SelectedValue, _
                                      CDate(IIf(TxtOrderDate.Text.Trim() = "", Todate, TxtOrderDate.Text.Trim())), _
                                      DTPRequiredDate.Value, r.Item("Barcode"), _
                                      CInt(IIf(IsDBNull(r.Item("PcsFree")) = True, 0, r.Item("PcsFree"))), _
                                      CInt(IIf(IsDBNull(r.Item("PcsOrder")) = True, 0, r.Item("PcsOrder"))), _
                                      CInt(IIf(IsDBNull(r.Item("PackOrder")) = True, 0, r.Item("PackOrder"))), _
                                      CSng(IIf(IsDBNull(r.Item("CTNOrder")) = True, 0, r.Item("CTNOrder"))), _
                                      TxtPONo.Text.Trim(), TxtEmpName.Text.Trim(), InvNo, _
                                      Trim(IIf(IsDBNull(r.Item("PromotionMachanic")) = True, "", r.Item("PromotionMachanic"))), _
                                      CSng(IIf(IsDBNull(r.Item("ItemDiscount")) = True, 0, r.Item("ItemDiscount"))), _
                                      Trim(IIf(IsDBNull(r.Item("Remark")) = True, "", r.Item("Remark"))), _
                                      CmbSaleman.Text.Trim(), IIf(RdbOnlinePO.Checked = True, 1, 0), RelatedKey, _
                                      IIf(IsDutchmill = False, 0, 1), DTPDeliverDate.Value)
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                Application.DoEvents()

                For Each o As DataRow In vFixedPriceList.Rows
                    If (Trim(IIf(IsDBNull(o.Item("Barcode")) = True, "", o.Item("Barcode"))).Equals(Trim(IIf(IsDBNull(r.Item("Barcode")) = True, "", r.Item("Barcode")))) = True) Then
                        query = <SQL><![CDATA[
                                        DECLARE @vTakeOrderNumber AS DECIMAL(18,0) = {1};
                                        DECLARE @vFixedIdLink AS DECIMAL(18,0) = {2};
                                        DECLARE @vQtyInvoicing AS INT = 1;
                                        INSERT INTO [{0}].[dbo].[TblProductsPriceSetting.AllowableInvoicing]([TakeOrderNumber],[FixedIdLink],[QtyInvoicing],[CreatedDate])
                                        VALUES(@vTakeOrderNumber,@vFixedIdLink,@vQtyInvoicing,GETDATE());
                                    ]]></SQL>
                        query = String.Format(query, DatabaseName, InvNo,
                                              CInt(IIf(IsDBNull(o.Item("FixedIdLink")) = True, 0, o.Item("FixedIdLink"))),
                                              CInt(IIf(IsDBNull(o.Item("QtyInvoicing")) = True, 0, o.Item("QtyInvoicing"))))
                        RCom.CommandText = query
                        RCom.ExecuteNonQuery()
                        Application.DoEvents()
                        Exit For
                    End If
                Next
            Next

            If IsDutchmill = False Then
                Initialized.R_MessageAlert = Initialized.R_MessageAlert.Replace("'", "").Trim()
                query = <SQL><![CDATA[
                                DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                                DECLARE @RelatedKey AS NVARCHAR(10) = N'{2}';
                                DECLARE @vMessageAlert AS NVARCHAR(MAX) = N'{3}';

                                WITH oList AS (
	                                SELECT v.ProNumY [UnitNumber],LEFT(v.Sup1,8) [SupNum]
	                                FROM [Stock].[dbo].[TPRProducts] v 
	                                GROUP BY v.ProNumY,LEFT(v.Sup1,8)
	                                UNION ALL
	                                SELECT v.ProNumY [UnitNumber],LEFT(v.Sup1,8) [SupNum]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated] v 
	                                GROUP BY v.ProNumY,LEFT(v.Sup1,8)
	                                UNION ALL
	                                SELECT o.OldProNumy [UnitNumber],LEFT(v.Sup1,8) [SupNum]
	                                FROM [Stock].[dbo].[TPRProducts] v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] o ON o.ProId = v.ProID
	                                GROUP BY o.OldProNumy,LEFT(v.Sup1,8)
	                                UNION ALL
	                                SELECT o.OldProNumy [UnitNumber],LEFT(v.Sup1,8) [SupNum]
	                                FROM [Stock].[dbo].[TPRProductsDeactivated] v INNER JOIN [Stock].[dbo].[TPRProductsOldCode] o ON o.ProId = v.ProID
	                                GROUP BY o.OldProNumy,LEFT(v.Sup1,8)
                                )
                                SELECT o.*
                                INTO #vProducts
                                FROM oList o;

                                WITH l AS (
	                                SELECT [SupNum],[SupName]
	                                FROM [Stock].[dbo].[TPRDeliveryDutchmill]
	                                GROUP BY [SupNum],[SupName]
	                                UNION ALL
	                                SELECT [SupNum],[SupName]
	                                FROM [Stock].[dbo].[TPRDeliveryDutchmill.Location]
	                                GROUP BY [SupNum],[SupName]
                                )
                                SELECT l.*
                                INTO #vDutchmill
                                FROM l;

                                IF NOT EXISTS (SELECT * FROM [{0}].[dbo].[TblDeliveryTakeOrders] WHERE ([TakeOrderNumber] = @TakeOrderNumber)AND [UnitNumber] IN (SELECT o.UnitNumber FROM #vProducts o WHERE o.SupNum IN (SELECT v.SupNum FROM #vDutchmill v GROUP BY v.SupNum) GROUP BY o.UnitNumber))
                                BEGIN
                                    INSERT INTO [DBPickers].[dbo].[.tbldeliverytakeorders.dry]([cusnum],[cusname],[deltoid],[delto],[dateorder],[daterequired],[deliverydate],[unitnumber],[barcode],[proname],[size],[qtypercase],[qtyperpack],[category],[pcsfree],[pcsorder],[packorder],[ctnorder],[totalpcsorder],[ponumber],[loginname],[takeordernumber],[promotionmachanic],[itemdiscount],[remark],[saleman],[note],[createddate])
                                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[DeliveryDate],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],@vMessageAlert,[CreatedDate]
                                    FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                                    WHERE ([TakeOrderNumber] = @TakeOrderNumber);
                                END
                                ELSE
                                BEGIN
	                                INSERT INTO [DBPickers].[dbo].[.tbldeliverytakeorders.dutchmill]([cusnum],[cusname],[deltoid],[delto],[dateorder],[daterequired],[deliverydate],[unitnumber],[barcode],[proname],[size],[qtypercase],[qtyperpack],[category],[pcsfree],[pcsorder],[packorder],[ctnorder],[totalpcsorder],[ponumber],[loginname],[takeordernumber],[promotionmachanic],[itemdiscount],[remark],[saleman],[note],[createddate])
                                    SELECT [CusNum],[CusName],[DelToId],[DelTo],[DateOrd],[DateRequired],[DeliveryDate],[UnitNumber],[Barcode],[ProName],[Size],[QtyPCase],[QtyPPack],[Category],[PcsFree],[PcsOrder],[PackOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],[PromotionMachanic],[ItemDiscount],[Remark],[Saleman],@vMessageAlert,[CreatedDate]
                                    FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                                    WHERE ([TakeOrderNumber] = @TakeOrderNumber);
                                END

                                DROP TABLE #vDutchmill;
                                DROP TABLE #vProducts;

                                --INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrder]([CusName],[CusNum],[DelTo],[DateOrd],[DateRequired],[ProNumy],[ProName],[ProPackSize],[ProQtyPCase],[PcsFree],[PcsOrder],[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderInvoiceNumber],[PrintInvoiceNumber],[PromotionMachanic],[ProCat],[ItemDiscount],[TranDate],[RemarkExpiry],[RelatedKey],[Saleman],[DeliveryDate])
                                --SELECT [CusName],[CusNum],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[ProName],[Size],[QtyPCase],[PcsFree],(ISNULL([PcsOrder],0) + (ISNULL([PackOrder],0) * ISNULL([QtyPPack],1))),[CTNOrder],[TotalPcsOrder],[PONumber],[LogInName],[TakeOrderNumber],NULL,[PromotionMachanic],[Category],[ItemDiscount],[CreatedDate],[Remark],@RelatedKey,[Saleman],[DeliveryDate]
                                --FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                                --WHERE [TakeOrderNumber] = @TakeOrderNumber;

                                INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrderServiceLevel]([CusNum],[CusName],[DelTo],[DateOrd],[ShipDate],[Barcode],[ProName],[Size],[QtyPerCase],[PcsFree],[PcsOrder],[CTNOrder],[ActualOrder],[ActualDelivered],[TakeOrderNo],[TakeOrderDate],[PrintInvNo],[PrintDate],[InvoiceNo],[InvoiceDate],[RemarkStatus])
                                SELECT [CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[UnitNumber],[ProName],[Size],[QtyPCase],[PcsFree],(ISNULL([PcsOrder],0) + (ISNULL([PackOrder],0) * ISNULL([QtyPPack],1))),[CTNOrder],[TotalPcsOrder],0,[TakeOrderNumber],[CreatedDate],NULL,NULL,NULL,NULL,[Remark]
                                FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                                WHERE [TakeOrderNumber] = @TakeOrderNumber;
                            ]]></SQL>
                query = String.Format(query, DatabaseName, InvNo, RelatedKey, Initialized.R_MessageAlert)
                RCom.CommandText = query
                RCom.ExecuteNonQuery()


                If Initialized.R_MessageAlert.Trim() <> "" Then
                    query = <SQL><![CDATA[
                                    DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                                    DECLARE @Message AS NVARCHAR(100) = N'{2}';
                                    INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrderAlertToQsDelivery]([InvNo],[CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],[Message],[RegisterDate])
                                    SELECT [TakeOrderNumber],[CusNum],[CusName],[DelTo],[DateOrd],[DateRequired],@Message,GETDATE()
                                    FROM [{0}].[dbo].[TblDeliveryTakeOrders]
                                    WHERE [TakeOrderNumber] = @TakeOrderNumber;
                                ]]></SQL>
                    query = String.Format(query, DatabaseName, InvNo, Initialized.R_MessageAlert.Trim())
                    RCom.CommandText = query
                    RCom.ExecuteNonQuery()
                End If

                If Initialized.R_DocumentNumber.Trim() <> "" And Initialized.R_LineCode.Trim() <> "" And Initialized.R_DeptCode.Trim <> "" Then
                    query = <SQL><![CDATA[
                                    DECLARE @TakeOrderNumber AS DECIMAL(18,0) = {1};
                                    DECLARE @CusNum AS NVARCHAR(8) = N'{2}';
                                    DECLARE @DocumentNumber AS NVARCHAR(20) = N'{3}';
                                    DECLARE @LineCode AS NVARCHAR(14) = N'{4}';
                                    DECLARE @DeptCode AS NVARCHAR(14) = N'{5}';
                                    INSERT INTO [Stock].[dbo].[TPRDeliveryTakeOrder_ForAEON]([SupervisorID],[CusNum],[DocumentNumber],[LineCode],[DeptCode],[TakeOrderID],[DeliveryID])
                                    VALUES(NULL,@CusNum,@DocumentNumber,@LineCode,@DeptCode,@TakeOrderNumber,NULL);
                                ]]></SQL>
                    query = String.Format(query, DatabaseName, InvNo, CmbBillTo.SelectedValue, Initialized.R_DocumentNumber.Trim(), Initialized.R_LineCode.Trim(), Initialized.R_DeptCode.Trim())
                    RCom.CommandText = query
                    RCom.ExecuteNonQuery()
                End If
            End If

            RCom.CommandText = "UPDATE [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] SET [IsBusy] = 0,[PrintInvNo] = " & InvNo & " "
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()
            MessageBox.Show("The processing have been finished.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If RdbOnlinePO.Checked = True Then TimerCustomerLoading.Enabled = True
            TimerLoading.Enabled = True
            CreateDeliveryTakeOrderList()
            ClearProductItems()
            App.SetEnableController(True, PanelHeader, PanelBillTo, CmbSaleman)
            CmbProducts.Focus()
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

    Private Sub TxtPcsOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtPcsOrder.PreviewKeyDown
        If e.KeyCode = Keys.Return Then BtnAdd_Click(BtnAdd, New System.EventArgs)
    End Sub

    Private Sub TxtPackOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtPackOrder.PreviewKeyDown
        If e.KeyCode = Keys.Return Then BtnAdd_Click(BtnAdd, New System.EventArgs)
    End Sub

    Private Sub TxtCTNOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtCTNOrder.PreviewKeyDown
        If e.KeyCode = Keys.Return Then BtnAdd_Click(BtnAdd, New System.EventArgs)
    End Sub

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.RowCount <= 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            If Not (DeliveryTakeOrderList Is Nothing) Then
                Dim Barcode As String = ""
                Dim TotalPcs As Integer = 0
                With DgvShow.Rows(DgvShow.CurrentRow.Index)
                    Barcode = Trim(IIf(IsDBNull(.Cells("Barcode").Value) = True, "", .Cells("Barcode").Value))
                    TotalPcs = CInt(IIf(IsDBNull(.Cells("TotalPcsOrders").Value) = True, 0, .Cells("TotalPcsOrders").Value))
                End With
                For Each r As DataRow In DeliveryTakeOrderList.Rows
                    If Barcode.Equals(r.Item("Barcode")) = True And TotalPcs = CInt(IIf(IsDBNull(r.Item("TotalPcsOrder")) = True, 0, r.Item("TotalPcsOrder"))) Then
                        DeliveryTakeOrderList.Rows.Remove(r)
                        Exit For
                    End If
                Next
            End If
            LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        End If
    End Sub

    Private Sub LblProductsRefresh_Click(sender As Object, e As EventArgs) Handles LblProductsRefresh.Click
        TimerProductLoading.Enabled = True
    End Sub

    Private Sub TxtSearchStoreCode_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtSearchStoreCode.PreviewKeyDown
        If e.KeyCode = Keys.Return Then
            If TxtSearchStoreCode.Text.Trim() = "" Then Exit Sub
            e.IsInputKey = True
            Dim StoreCode As String = Trim(IIf(TxtSearchStoreCode.Text.Trim() = "", "", TxtSearchStoreCode.Text.Trim()))
            Dim LinkBarcode As String = ""
            query = <SQL>
                        <![CDATA[
                    DECLARE @CusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @StoreCode AS NVARCHAR(MAX) = N'{2}';
                    IF EXISTS (SELECT * FROM [{0}].[dbo].[TblCustomerSettingForStoreCode] WHERE [CusNum] = @CusNum)
                    BEGIN
	                    SELECT [Id],[CusNum],[Barcode],[ProName],[Size],[CusCode],[CreatedDate]
	                    FROM [{0}].[dbo].[TblCustomerCodes]
	                    WHERE [CusNum] = @CusNum AND [CusCode] = @StoreCode;
                    END
                ]]>
                    </SQL>
            query = String.Format(query, DatabaseName, CmbBillTo.SelectedValue, StoreCode)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    LinkBarcode = Trim(IIf(IsDBNull(lists.Rows(0).Item("Barcode")) = True, "", lists.Rows(0).Item("Barcode")))
                    CmbProducts.SelectedValue = LinkBarcode
                Else
                    GoTo Err_CheckStorecode
                End If
            Else
Err_CheckStorecode:
                MessageBox.Show("The Store code is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub TxtCusNumSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtCusNumSearch.KeyDown
        BtnAdd.Enabled = False
        CmbBillTo.SelectedIndex = -1
        If TxtCusNumSearch.Text.Trim().Equals("") = True Then Exit Sub
        If e.KeyCode = Keys.Return Then
            Dim vCusNum As String = String.Format("CUS{0:00000}", CDec(IIf(TxtCusNumSearch.Text.Trim().Equals("") = True, 0, TxtCusNumSearch.Text.Trim())))
            CmbBillTo.SelectedValue = vCusNum
        End If
    End Sub

    Private Sub TxtCusNumSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCusNumSearch.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    ' Loops for a specificied period of time (milliseconds)
    'Threading.Thread.Sleep(3000)
    Private Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub

    Private Sub DTPDeliverDate_ValueChanged(sender As Object, e As EventArgs) Handles DTPDeliverDate.ValueChanged
        If DTPDeliverDate.Value.Date < Todate.Date Then
            MessageBox.Show("Please check Delivery Date again!", "Invalid Delivery Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DTPDeliverDate.Value = Todate
            Exit Sub
        End If
    End Sub

    Private Sub LblCheckExpiryDate_Click(sender As Object, e As EventArgs) Handles LblCheckExpiryDate.Click
        If CmbProducts.Text.Trim() = "" Then Exit Sub
        If TypeOf CmbProducts.SelectedValue Is DataRowView Or CmbProducts.SelectedValue Is Nothing Then Exit Sub
        Dim vBarcode As String = CmbProducts.SelectedValue
        Dim vCusNum As String = CmbBillTo.SelectedValue
        Dim vCusName As String = CmbBillTo.Text.Trim()
        Dim vFrm As New FrmCheckExpiryDate With {.vBarcode = vBarcode, .vCusNum = vCusNum, .vCusname = vCusName}
        vFrm.ShowDialog()
    End Sub

    Private Sub CmbProducts_KeyDown(sender As Object, e As KeyEventArgs) Handles CmbProducts.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            DeactivatedProduct()
        End If
    End Sub

    Private Sub DeactivatedProduct()
        If CmbProducts.Items.Count = 0 Then Return

        Dim dtDC As DataTable = CheckDeactivatedItem()
        If dtDC.Rows.Count > 0 Then
            If MessageBox.Show("This Barcode is in Product Deactivated! Do you want to send email to customer?", "Confirm Deactivated Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            Dim email As String = CheckCustomerEmail().Rows(0)(0)
            If email.Equals(String.Empty) Then
                MessageBox.Show("This customer does not have email address.")
                Cursor = Cursors.Default
                Return
            End If

            Dim dcReport As New MailDCSKUReport
            Dim dr As DataRow = dtDC.Rows(0)
            With dcReport
                .paramDear.Value = String.Format("Dear {0},", CmbBillTo.Text)
                .paramPO.Value = TxtPONo.Text
                .paramBarcode.Value = dr("ProNumY")
                .paramName.Value = dr("ProName")
                .paramSize.Value = dr("ProPacksize")

                .CreateDocument(True)
                Try
                    Using client As New SmtpClient("mail.untwholesale.com", 26)
                        Using message As MailMessage = .ExportToMail("sales@untwholesale.com", email, "Product Discontinued")
                            Dim cc As New MailAddressCollection
                            For Each drEmail As DataRow In QueryCCEmail.Rows
                                message.CC.Add(drEmail(0))
                            Next

                            client.Credentials = New System.Net.NetworkCredential("sales@untwholesale.com", "UNT@@!@#12345678")
                            client.EnableSsl = True
                            client.Send(message)
                            MessageBox.Show("Your email has been sent successfully.")
                        End Using
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Send email failed.")
                End Try
            End With
        End If
    End Sub
End Class