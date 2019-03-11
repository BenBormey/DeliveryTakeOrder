Public Class FrmDeliveryTakeOrderInfoAeon

    Private Sub FrmDeliveryTakeOrderInfoAeon_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles BtnFinish.Click
        Initialized.R_DocumentNumber = TxtDocumentNumber.Text.Trim()
        Initialized.R_LineCode = TxtLineCode.Text.Trim()
        Initialized.R_DeptCode = TxtDeptCode.Text.Trim()
    End Sub
End Class