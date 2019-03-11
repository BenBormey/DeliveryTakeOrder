Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Public Class FrmSelectedPayment
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private DatabaseName As String
    Private Todate As Date
    Public DTable As DataTable

    Private Sub LoadingInitialized()
        Initialized.LoadingInitialized(Data, App)
        DatabaseName = String.Format("{0}{1}", Data.PrefixDatabase, Data.DatabaseName)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Initialized.R_IsCancel = True
        Me.Close()
    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As EventArgs) Handles BtnPreview.Click
        If RdbAllUnpaid.Checked = True Then
            Initialized.R_AllUnpaid = True
        Else
            Initialized.R_AllUnpaid = False
        End If
        Initialized.R_DateFrom = DTPFrom.Value
        Initialized.R_DateTo = DTPTo.Value
        Initialized.R_IsCancel = False
        Me.Close()
    End Sub

    Private Sub FrmSelectedPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        DTPTo.Value = Todate
        DTPFrom.Value = Todate
    End Sub

    Private Sub RdbAllTransaction_CheckedChanged(sender As Object, e As EventArgs) Handles RdbAllTransaction.CheckedChanged
        If Not (DTable Is Nothing) Then
            If DTable.Rows.Count > 0 Then
                DTPFrom.Value = CDate(IIf(IsDBNull(DTable.Rows(0).Item(0)) = True, Todate, DTable.Rows(0).Item(0)))
            End If
        End If
    End Sub
End Class