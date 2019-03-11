Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmAlertCustomerHistory
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
    Public Property vDeltoId As Decimal

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

    Private Sub FrmAlertCustomerHistory_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Me.Dispose()
    End Sub

    Private Sub FrmAlertCustomerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
    End Sub

    Private Sub Loading_Tick(sender As Object, e As EventArgs) Handles Loading.Tick
        Me.Cursor = Cursors.WaitCursor
        Loading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                DECLARE @vQuery AS NVARCHAR(MAX) = N'';
                WITH v AS (
                SELECT [TableName],[Division]
                FROM [Stock].[dbo].[AAllMainDivision]
                WHERE ISNULL([Division],N'') <> N''
                UNION ALL
                SELECT [TableName],[Division]
                FROM [Stock].[dbo].[AAllMainDivisionInvoiceing]
                WHERE ISNULL([Division],N'') <> N'')

                SELECT @vQuery += N'SELECT MAX([InvNumber]) AS [InvNumber],MAX([ShipDate]) AS [ShipDate],[CusNum],[CusCom],N''' + v.Division +  N''' AS [Division]
                FROM [Stock].[dbo].[' + v.TableName + N']
                WHERE [DeltoId] = @vDeltoId
                GROUP BY [CusNum],[CusCom]
                UNION ALL '
                FROM v 
                ORDER BY v.Division;
                SET @vQuery += N'SELECT NULL AS [InvNumber],NULL AS [ShipDate],NULL AS [CusNum],NULL AS [CusCom],NULL AS [Division]';
                SELECT @vQuery = N'
                DECLARE @vDeltoId AS DECIMAL(18,0) = {1};
                WITH o as (
                ' + @vQuery + N'
                )
                SELECT *
                INTO #vCustomer
                FROM o
                WHERE o.InvNumber is not null;
                with v as (
                SELECT MAX(o.InvNumber) as InvNumber,MAX(o.ShipDate) as ShipDate,o.CusNum,o.CusCom
                FROM #vCustomer as o
                WHERE o.InvNumber is not null
                GROUP BY o.CusNum,o.CusCom)
                SELECT MAX(o.InvNumber) as InvNumber,MAX(o.ShipDate) as ShipDate,o.CusNum,o.CusCom,v.Division
                FROM v as o
                INNER JOIN #vCustomer as v on o.InvNumber = v.InvNumber and o.CusNum = v.CusNum
                WHERE o.InvNumber is not null
                GROUP BY o.CusNum,o.CusCom,v.Division
                ORDER BY o.ShipDate DESC,o.CusCom;
                DROP TABLE #vCustomer;
                ';
                EXEC(@vQuery);
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vDeltoId)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        Me.Cursor = Cursors.Default
    End Sub
End Class