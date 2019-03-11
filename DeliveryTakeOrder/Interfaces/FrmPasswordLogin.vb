Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Public Class FrmPasswordLogin
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        End
    End Sub

    Private Sub TxtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then BtnLogIn_Click(BtnLogIn, New System.EventArgs)
    End Sub

    Private Sub BtnLogIn_Click(sender As Object, e As EventArgs) Handles BtnLogIn.Click
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
            'Check TakeOrder
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", "TakeOrder")
            End With
            If DirectCast(Data.Selects("PasswordLogin", , Dic, , , , , Initialized.GetConnectionType(Data, App)), DataTable).Rows.Count <= 0 Then
                Dic = New Dictionary(Of String, Object)
                With Dic
                    .Add("ProgramName", "TakeOrder")
                    .Add("UserName", "TakeOrder")
                    .Add("Password", App.ConvertTextToPassword("to", R_KeyPassword))
                    .Add("CreatedDate", "GETDATE()")
                End With
                Dim R_Result As Boolean = Data.Inserts("PasswordLogin", Dic, Initialized.GetConnectionType(Data, App))
            End If
            'Check Password Login
            Dic = New Dictionary(Of String, Object)
            With Dic
                .Add("ProgramName", "N'Managing Director',N'MD Assistant',N'IT Manager',N'TakeOrder', N'Software Developer'")
                .Add("Password", String.Format("'{0}'", App.ConvertTextToPassword(TxtPassword.Text, R_KeyPassword)))
            End With
            Dim lists As DataTable = DirectCast(Data.Selects("PasswordLogin", , Dic, True, , , , Initialized.GetConnectionType(Data, App)), DataTable)
            If lists.Rows.Count > 0 Then
                Dim ProgramName As String = "TakeOrder"
                Me.Hide()
                Dim Frm As New MDI
                Frm.Show()
                Frm.ProgramName = ProgramName
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
        Initialized.LoadingInitialized(Data, App)
        If Initialized.CheckCompaniesExistOrNot(Data, App) = True Then
            App.ClearController(TxtPassword)
        Else
            MessageBox.Show("Cannot find company name!" & vbCrLf & "Please contact to IT Assistant to create company name!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End
        End If
    End Sub

    Private Sub PasswordContinues_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub
End Class