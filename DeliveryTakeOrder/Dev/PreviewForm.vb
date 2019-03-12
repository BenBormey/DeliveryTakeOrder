Imports DevExpress.XtraTab.ViewInfo
Imports DevExpress.XtraTab
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraSplashScreen

Public Class PreviewForm
    Public Property db As RMDB

    Public Sub New()
        InitializeComponent()
        db = New RMDB(AppSetting.ConnectionString)
        Me.WindowState = FormWindowState.Maximized
    End Sub


    Private Sub tabMain_CloseButtonClick(sender As Object, e As EventArgs) Handles tabMain.CloseButtonClick
        Dim arg As ClosePageButtonEventArgs = TryCast(e, ClosePageButtonEventArgs)
        ' hide
        'TryCast(arg.Page, XtraTabPage).PageVisible = False

        ' remove
        If Me.tabMain.TabPages.Count = 1 Then
            Me.Close()
            Return
        End If
        Me.tabMain.TabPages.Remove(TryCast(arg.Page, XtraTabPage))
    End Sub

    Public Overridable Sub LoadReport()

    End Sub

    Private Sub PreviewForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'SplashScreenManager.ShowForm(GetType(LoadingDataForm))
        LoadReport()
        'SplashScreenManager.CloseForm()
    End Sub
End Class