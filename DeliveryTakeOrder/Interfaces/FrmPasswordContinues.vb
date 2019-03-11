﻿Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Public Class FrmPasswordContinues
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Public R_PasswordPermission As String

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Initialized.R_CorrectPassword = False
        Me.Close()
    End Sub

    Private Sub TxtUserName_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Return) Then TxtPassword.Focus()
    End Sub

    Private Sub TxtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then BtnOK_Click(BtnOK, New System.EventArgs)
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If Trim(TxtPassword.Text) = "" Then
            MessageBox.Show("Please enter the password login!", "Enter Password Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtPassword.Focus()
            Exit Sub
        Else
            Dim Dic As Dictionary(Of String, Object)
            'Check Password MD
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", "Managing Director")
            End With
            If DirectCast(Data.Selects("PasswordLogin", , Dic, , , , , Initialized.GetConnectionType(Data, App)), DataTable).Rows.Count <= 0 Then
                Dic = New Dictionary(Of String, Object)
                With Dic
                    .Add("ProgramName", "Managing Director")
                    .Add("UserName", "Managing Director")
                    .Add("Password", App.ConvertTextToPassword("Admin", R_KeyPassword))
                    .Add("CreatedDate", "GETDATE()")
                End With
                Dim R_Result As Boolean = Data.Inserts("PasswordLogin", Dic, Initialized.GetConnectionType(Data, App))
            End If
            'Check Password IT Manager
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", "IT Manager")
            End With
            If DirectCast(Data.Selects("PasswordLogin", , Dic, , , , , Initialized.GetConnectionType(Data, App)), DataTable).Rows.Count <= 0 Then
                Dic = New Dictionary(Of String, Object)
                With Dic
                    .Add("ProgramName", "IT Manager")
                    .Add("UserName", "IT Manager")
                    .Add("Password", App.ConvertTextToPassword("MSRITH$260689", R_KeyPassword))
                    .Add("CreatedDate", "GETDATE()")
                End With
                Dim R_Result As Boolean = Data.Inserts("PasswordLogin", Dic, Initialized.GetConnectionType(Data, App))
            End If
            'Check GM
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", "Products")
            End With
            If DirectCast(Data.Selects("PasswordLogin", , Dic, , , , , Initialized.GetConnectionType(Data, App)), DataTable).Rows.Count <= 0 Then
                Dic = New Dictionary(Of String, Object)
                With Dic
                    .Add("ProgramName", "Products")
                    .Add("UserName", "Products")
                    .Add("Password", App.ConvertTextToPassword("a", R_KeyPassword))
                    .Add("CreatedDate", "GETDATE()")
                End With
                Dim R_Result As Boolean = Data.Inserts("PasswordLogin", Dic, Initialized.GetConnectionType(Data, App))
            End If
            'Check Password Login
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", R_PasswordPermission)
                .Add("Password", String.Format("'{0}'", App.ConvertTextToPassword(TxtPassword.Text, R_KeyPassword)))
            End With
            If DirectCast(Data.Selects("PasswordLogin", , Dic, True, , , , Initialized.GetConnectionType(Data, App)), DataTable).Rows.Count > 0 Then
                Initialized.R_CorrectPassword = True
                Me.Close()
            Else
                MessageBox.Show("The Password are wrong!" & vbCrLf & "Please check password again...", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                With TxtPassword
                    .SelectionStart = 0
                    .SelectionLength = .TextLength
                    .Focus()
                End With
                Exit Sub
            End If
        End If
    End Sub

    Private Sub PasswordContinues_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Initialized.LoadingInitialized(Data, App)
        TxtPassword.Focus()
    End Sub

    Private Sub PasswordContinues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        App.ClearController(TxtPassword)
    End Sub

    Private Sub PasswordContinues_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_CompanyName)
    End Sub

End Class