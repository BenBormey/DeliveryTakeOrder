Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmAlertBankGarantee
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
            DateAlert = CDate(IIf(IsDBNull(.Cells("AlertDate").Value) = True, Todate, .Cells("AlertDate").Value))
            CurDate = CDate(IIf(IsDBNull(.Cells("CurDate").Value) = True, Todate, .Cells("CurDate").Value))
            If DateDiff(DateInterval.Day, CurDate, DateExpiry) <= 0 Then
                .DefaultCellStyle.ForeColor = Color.Red
            ElseIf DateDiff(DateInterval.Day, CurDate, DateAlert) <= 0 Then
                .DefaultCellStyle.ForeColor = Color.YellowGreen
            End If
        End With
    End Sub

    Private Sub FrmAlertBankGarantee_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Me.Dispose()
    End Sub

    Private Sub FrmAlertBankGarantee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
    End Sub
End Class