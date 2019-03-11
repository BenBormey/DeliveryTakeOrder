Public Class FrmDeliveryTakeOrderMessage
    Public Property vPONumber As String
    Public Property vDeliveryDate As Date
    Public Property vTodate As Date

    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles BtnFinish.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If vPONumber.Trim().Equals("") = False And TxtPONumber.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please enter the P.O Number!", "Enter P.O Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtPONumber.Focus()
            Exit Sub
        End If
        Initialized.R_MessageAlert = TxtRemark.Text.Trim()
        vPONumber = TxtPONumber.Text.Trim()
        If CheckBox1.Checked = True Then
            vDeliveryDate = DTPDeliveryDate.Value
        Else
            vDeliveryDate = vTodate.Date
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmDeliveryTakeOrderMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not (vPONumber Is Nothing) Then
            If vPONumber.Trim.Equals("") = True Then
                PanelPONumber.Visible = False
            Else
                PanelPONumber.Visible = True
            End If
        Else
            vPONumber = ""
            PanelPONumber.Visible = False
        End If
        TxtPONumber.Text = vPONumber
        If CheckBox1.Checked = True Then
            vDeliveryDate = DTPDeliveryDate.Value
        Else
            vDeliveryDate = Nothing
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        DTPDeliveryDate.Enabled = CheckBox1.Checked
    End Sub

    Private Sub DTPDeliveryDate_ValueChanged(sender As Object, e As EventArgs) Handles DTPDeliveryDate.ValueChanged
        If DTPDeliveryDate.Value.Date < vTodate.Date Then DTPDeliveryDate.Value = vTodate.Date
    End Sub
End Class