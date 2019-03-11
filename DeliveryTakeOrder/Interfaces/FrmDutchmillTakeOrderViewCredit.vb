Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDutchmillTakeOrderViewCredit
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
    Public Property vDepartment As String
    Public Property vPlanning As String
    Public Property vCusNum As String

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

    Private Sub FrmDeliveryTakeOrderInfoAeon_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmDutchmillTakeOrderViewCredit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        DisplayLoading.Enabled = True
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub DisplayLoading_Tick(sender As Object, e As EventArgs) Handles DisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        DisplayLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
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
	                SET @query = 'DECLARE @vDepartment AS NVARCHAR(50) = N''{1}'' ' + @NewLineChar;
	                SET @query = @query + 'DECLARE @vPlanningOrder AS NVARCHAR(50) = N''{2}'' ' + @NewLineChar;
                    SET @query = @query + 'DECLARE @vCusNum AS NVARCHAR(8) = N''{3}'' ' + @NewLineChar;
	                SET @query = @query + 'SELECT [InvNumber],[PONumber],[CusNum],[CusCom],[ShipDate],[DelTo],[GrandTotal] ' + @NewLineChar;
	                SET @query = @query + 'FROM [Stock].[dbo].[' + @TableName + '] ' + @NewLineChar;
	                SET @query = @query + 'WHERE ISNULL([PAID],''0'') = ''0'' AND [CusNum] = @vCusNum AND [InvNumber] NOT IN (' + @NewLineChar;
	                SET @Condition = '';
	                DECLARE CUR_STA CURSOR
	                FOR SELECT [TableName] FROM [Stock].[dbo].[AAllDivisionStatement] WHERE [Division] = @Division ORDER BY [TableName];
	                OPEN CUR_STA
	                FETCH NEXT FROM CUR_STA INTO @StatementName;
	                WHILE @@FETCH_STATUS = 0
	                BEGIN
		                SET @Condition = @Condition + 'SELECT [InvNumber] ' + @NewLineChar;
		                SET @Condition = @Condition + 'FROM [Stock].[dbo].[' + @StatementName + '] ' + @NewLineChar;
		                SET @Condition = @Condition + 'WHERE [CusNum] = @vCusNum ' + @NewLineChar;
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
                        SET @Condition = @Condition + 'WHERE [CusNum] = @vCusNum ' + @NewLineChar;
	                END
	                SET @query = @query + @Condition + ' ' + @NewLineChar;
	                SET @query = @query + '); ' + @NewLineChar;
	                INSERT INTO @CreditLists ([InvNumber],[PONumber],[CusNum],[CusName],[ShipDate],[DelTo],[GrandTotal])
	                EXEC(@query);
	                FETCH NEXT FROM CUR_DIV INTO @TableName,@Division;
                END
                CLOSE CUR_DIV;
                DEALLOCATE CUR_DIV;

                WITH v AS (
                SELECT x.CusNum,x.CusName,x.Terms,x.MaxMonthAllow,x.CreditLimit,ISNULL(v.MaxCredit,x.CreditLimitAllow) AS CreditLimitAllow
                FROM Stock.dbo.TPRCustomer AS x
                LEFT OUTER JOIN [{0}].[dbo].[TblCustomerAllowCredits] AS v ON v.CusNum = x.CusNum AND DATEDIFF(DAY,GETDATE(),ISNULL(v.[Expiry],GETDATE())) > 0 )

                SELECT o.CusNum,o.CusName,SUM(o.GrandTotal) AS GrandTotal,v.CreditLimit,v.CreditLimitAllow
                INTO #vCredit
                FROM @CreditLists AS o INNER JOIN v ON v.CusNum = o.CusNum
                GROUP BY o.CusNum,o.CusName,v.CreditLimit,v.CreditLimitAllow
                HAVING SUM(o.GrandTotal) >= v.CreditLimit OR SUM(o.GrandTotal) >= v.CreditLimitAllow;

                SELECT o.InvNumber,o.PONumber,o.CusNum,o.CusName,o.ShipDate,o.DelTo,o.GrandTotal
                FROM @CreditLists AS o
                WHERE o.CusNum IN (SELECT v.CusNum FROM #vCredit AS v)
                ORDER BY o.CusName,o.ShipDate ASC;

                DROP TABLE #vCredit;
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vDepartment, vPlanning, vCusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
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
            RSheet.Range("A1").Value = "Over Credit ~ Customer List"
            RSheet.Range("A2").Value = "Export Date : " & Format(Now(), "dd-MMM-yy")
            RSheet.Range("A4").Value = "Nº"
            RSheet.Range("B4").Value = "Invoice #"
            RSheet.Range("C4").Value = "P.O Number"
            RSheet.Range("D4").Value = "Customer #"
            RSheet.Range("E4").Value = "Customer Name"
            RSheet.Range("F4").Value = "Ship Date"
            RSheet.Range("G4").Value = "DelTo"
            RSheet.Range("H4").Value = "Amount"
            RSheet.Range("C:C").NumberFormat = "@"
            RSheet.Range("F:F").NumberFormat = "dd-MMM-yyyy"
            RSheet.Range("H:H").NumberFormat = "_(* #,##0.00_);_(* (#,##0.00);_(* ""-""??_);_(@_)"
            RSheet.Range("A4:O4").WrapText = True
            RSheet.Range("A4:O4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            RSheet.Range("A4:O4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            Dim QsRow As Long = 5
            For Each QsDataRow As DataGridViewRow In DgvShow.Rows
                RSheet.Range("A" & QsRow).Value = (QsRow - 4)
                RSheet.Range("B" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("InvNumber").Value) = True, "", QsDataRow.Cells("InvNumber").Value)
                RSheet.Range("C" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("PONumber").Value) = True, "", QsDataRow.Cells("PONumber").Value)
                RSheet.Range("D" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CusNum").Value) = True, "", QsDataRow.Cells("CusNum").Value)
                RSheet.Range("E" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CusName").Value) = True, "", QsDataRow.Cells("CusName").Value)
                RSheet.Range("F" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("ShipDate").Value) = True, "", QsDataRow.Cells("ShipDate").Value)
                RSheet.Range("G" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DelTo").Value) = True, "", QsDataRow.Cells("DelTo").Value)
                RSheet.Range("H" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("GrandTotal").Value) = True, "", QsDataRow.Cells("GrandTotal").Value)
                QsRow = QsRow + 1
            Next
            RSheet.Columns.AutoFit()
            RSheet.Range("A:A").ColumnWidth = 4
            RExcel.Visible = True
            App.ReleaseObject(RSheet)
            App.ReleaseObject(RBook)
            App.ReleaseObject(RExcel)
        Else
            MessageBox.Show("No record to export to excel!", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class