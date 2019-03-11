Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Public Class FrmChangePassword
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Public Property ProgramName As String

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub TxtUserName_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Return) Then TxtConfirmPassword.Focus()
    End Sub

    Private Sub TxtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtConfirmPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then BtnOK_Click(BtnOK, New System.EventArgs)
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If Trim(TxtNewPassword.Text) = "" Then
            MessageBox.Show("Please enter the New Password!", "Enter New Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtNewPassword.Focus()
            Exit Sub
        ElseIf Trim(TxtConfirmPassword.Text) = "" Then
            MessageBox.Show("Please enter the Confirm Password!", "Enter Confirm Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtConfirmPassword.Focus()
            Exit Sub
        Else
            If StrComp(TxtNewPassword.Text, TxtConfirmPassword.Text, CompareMethod.Binary) <> 0 Then
                MessageBox.Show("The password is not match!" & vbCrLf & "Please check the Confirm Password again.", "Invalid Confirm Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                With TxtConfirmPassword
                    .SelectionStart = 0
                    .SelectionLength = .TextLength
                    .Focus()
                End With
                Exit Sub
            End If
            Dim Dic As New Dictionary(Of String, Object)
            With Dic
                .Add("Password", String.Format("{0}", App.ConvertTextToPassword(TxtNewPassword.Text, R_KeyPassword)))
            End With
            If Data.Updates("PasswordLogin", Dic, "[ProgramName] = '" & ProgramName & "'", Initialized.GetConnectionType(Data, App)) = True Then
                MessageBox.Show("Changing password have been completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("The Password are wrong!" & vbCrLf & "Please check password again...", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                With TxtConfirmPassword
                    .SelectionStart = 0
                    .SelectionLength = .TextLength
                    .Focus()
                End With
                Exit Sub
            End If
        End If
    End Sub

    Private Sub PasswordContinues_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        TxtNewPassword.Focus()
    End Sub

    Private Sub PasswordContinues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialized.LoadingInitialized(Data, App)
        App.ClearController(TxtConfirmPassword)
    End Sub

    Private Sub PasswordContinues_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_CompanyName)
    End Sub
End Class