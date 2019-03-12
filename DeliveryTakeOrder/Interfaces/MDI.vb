Imports System.Windows.Forms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Public Class MDI
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private DatabaseName As String
    Public Property ProgramName As String
    Private query As String
    Private lists As DataTable

    Private Sub LoadingInitialized()
        Initialized.LoadingInitialized(Data, App)
        DatabaseName = String.Format("{0}{1}", Data.PrefixDatabase, Data.DatabaseName)
        AppSetting.InitialCompany()
    End Sub

    Private Sub MnuExit_Click(sender As Object, e As EventArgs) Handles MnuExit.Click
        End
    End Sub

    Private Sub MnuChangePassword_Click(sender As Object, e As EventArgs) Handles MnuChangePassword.Click
        Dim Frm As New FrmChangePassword With {.MdiParent = Me}
        Frm.ProgramName = "TakeOrder"
        Frm.Show()
    End Sub

    Private Sub MDIFrmGM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub MDIFrmRequestAdvanceBankTT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        If Initialized.CheckCompaniesExistOrNot(Data, App) = True Then
            Me.WindowState = FormWindowState.Maximized
        Else
            MessageBox.Show("Cannot find company name!" & vbCrLf & "Please contact to IT Assistant to create company name!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End
        End If
    End Sub

    Private Sub MDIFrmGM_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.Text = "Delivery Take Order (" & Replace(Initialized.R_DatabaseName, "&&", "&") & ")"
    End Sub

    Private Sub MnuDeliveryTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuDeliveryTakeOrder.Click
        Dim Frm As New FrmDeliveryTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Maximized, .IsDutchmill = False, .ProgramName = ProgramName}
        Frm.Show()
    End Sub

    Private Sub MnuTakeOrderForDutchmill_Click(sender As Object, e As EventArgs) Handles MnuTakeOrderForDutchmill.Click
        Dim Frm As New FrmDeliveryTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Maximized, .IsDutchmill = True, .ProgramName = ProgramName}
        Frm.Show()
    End Sub

    Private Sub MnuProcessTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuProcessTakeOrder.Click
        Dim Frm As New FrmProcessTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        Frm.Show()
    End Sub

    Private Sub MnuDeltoListForDutchmillPO_Click(sender As Object, e As EventArgs) Handles MnuDeltoListForDutchmillPO.Click
        Dim Frm As New FrmDeltoListForDutchmillPO With {.MdiParent = Me}
        Frm.Show()
    End Sub

    Private Sub MnuSetPiecesPerTray_Click(sender As Object, e As EventArgs) Handles MnuSetPiecesPerTray.Click
        Dim Frm As New FrmSetPiecesPerTray With {.MdiParent = Me}
        Frm.Show()
    End Sub

    Private Sub MnuDownloadTakeOrderFromSaleTeam_Click(sender As Object, e As EventArgs) Handles MnuDownloadTakeOrderFromSaleTeam.Click
        Dim Frm As New FrmDownloadSaleTeam With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        Frm.Show()
    End Sub

    Private Sub MnuDutchmillOrder_Click(sender As Object, e As EventArgs) Handles MnuDutchmillOrder.Click
        Dim Frm As New FrmDutchmillTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Maximized, .vDepartment = "Admin Team"}
        Frm.Show()
    End Sub

    Private Sub MnuUnlockPrinterTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuUnlockPrinterTakeOrder.Click
        Dim vFrm As New FrmUnlockPrinterTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Normal}
        vFrm.Show()
    End Sub

    Private Sub Loading_Tick(sender As Object, e As EventArgs) Handles Loading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.Loading.Enabled = False
        query = <SQL>
                    <![CDATA[
                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [Stock].[dbo].[TPRDeliveryTakeOrder] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrint] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaiting] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET  v.[CusName] = o.CusName
                        FROM [Stock].[dbo].[TPRDeliveryTakeOrderAfterPrintWaitingPicking] v
                        INNER JOIN [Stock].[dbo].[TPRCustomer] o ON (o.CusNum = v.CusNum);

                        UPDATE v
                        SET v.[Delto] = o.Delto
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] v
                        INNER JOIN [Stock].[dbo].[TPRDelto] o ON (o.DefId = v.DeltoId);

                        UPDATE v
                        SET v.[Delto] = o.Delto
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] v
                        INNER JOIN [Stock].[dbo].[TPRDelto] o ON (o.DefId = v.DeltoId);

                        UPDATE v
                        SET v.[Delto] = o.Delto
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders] v
                        INNER JOIN [Stock].[dbo].[TPRDelto] o ON (o.DefId = v.DeltoId);
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        Data.ExecuteCommand(query, Initialized.GetConnectionType(Data, App))
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub MnuViewProcessingTakeOrder_Click(sender As Object, e As EventArgs) Handles MnuViewProcessingTakeOrder.Click
        Dim vFrm As New FrmViewProcessingTakeOrder With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        vFrm.Show()
    End Sub
End Class
