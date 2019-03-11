Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDutchmillTakeOrderPONumber
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
    Public Property vDateOrder As Date
    Public Property vRequiredDate As Date
    Public Property vPONumber As String

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

    Private Sub FrmDutchmillTakeOrderPONumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TxtPONo.Text = vPONumber
        TxtOrderDate.Text = String.Format("{0:dd-MMM-yyyy}", vDateOrder)
        DTPRequiredDate.Value = vRequiredDate
        PicRefreshPO_Click(PicRefreshPO, e)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If TxtPONo.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please enter the P.O Number!", "Enter P.O Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtPONo.Focus()
            Exit Sub
        Else
            Me.vDateOrder = CDate(TxtOrderDate.Text.Trim())
            Me.vRequiredDate = CDate(DTPRequiredDate.Value)
            Me.vPONumber = TxtPONo.Text.Trim()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub PicRefreshPO_Click(sender As Object, e As EventArgs) Handles PicRefreshPO.Click
        lists = Data.ExecuteStoredProc("Stock.dbo.AutoPONumber", Initialized.GetConnectionType(Data, App))
        If Not (lists Is Nothing) Then
            If lists.Rows.Count > 0 Then
                TxtPONo.Text = Trim(IIf(IsDBNull(lists.Rows(0).Item("Auto")) = True, "", lists.Rows(0).Item("Auto")))
            End If
        End If
    End Sub
End Class