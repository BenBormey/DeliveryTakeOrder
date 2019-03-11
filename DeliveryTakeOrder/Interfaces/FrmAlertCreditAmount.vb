Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmAlertCreditAmount
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
    Public Property oCusNum As String
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

    Private Sub FrmAlertCreditAmount_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Me.Dispose()
    End Sub

    Private Sub FrmAlertCreditAmount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
    End Sub

    Private Sub loading_Tick(sender As Object, e As EventArgs) Handles loading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.loading.Enabled = False
        Dim BankGarantee As Double = 0
        query = <SQL>
                    <![CDATA[
                        DECLARE @CusId AS NVARCHAR(8) = N'{1}';
                        SELECT SUM([CreditLimit]) AS [BankGarantee]
                        FROM [Stock].[dbo].[TPRCustomerBankGarantee]
                        WHERE [CusId] = @CusId;
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, oCusNum)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                BankGarantee = CDbl(IIf(IsDBNull(lists.Rows(0).Item("BankGarantee")) = True, 0, lists.Rows(0).Item("BankGarantee")))
            End If
        End If
        lblbankgarantee.Text = String.Format("Bank Garantee = {0:C2}", BankGarantee)
        Me.Cursor = Cursors.Default
    End Sub
End Class