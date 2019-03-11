Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmUnlockPrinterTakeOrder
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private DatabaseName As String
    Private Todate As Date
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction

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

    Private Sub FrmUnlockPrinterTakeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TimerLoading.Enabled = True
    End Sub

    Private Sub FrmUnlockPrinterTakeOrder_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_CompanyName)
    End Sub

    Private Sub TimerLoading_Tick(sender As Object, e As EventArgs) Handles TimerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerLoading.Enabled = False
        RSQL = _
        <SQL>
            <![CDATA[
                SELECT [PrintInvNo],[IsBusy]
                FROM [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo]
                WHERE ISNULL([IsBusy],0) = 1;
            ]]>
        </SQL>
        RSQL = String.Format(RSQL, DatabaseName)
        Dim DTable As DataTable = Data.Selects(RSQL, Initialized.GetConnectionType(Data, App))
        If Not (DTable Is Nothing) Then
            If DTable.Rows.Count > 0 Then
                PicStatus.Image = My.Resources.Lock_Printer
            Else
                PicStatus.Image = My.Resources.Unlock_Printer
            End If
        Else
            PicStatus.Image = My.Resources.Unlock_Printer
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private RSQL As String
    Private Sub BtnUnlockPrinter_Click(sender As Object, e As EventArgs) Handles BtnUnlockPrinter.Click
        RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
        RCon.Open()
        RTran = RCon.BeginTransaction()
        Try
            RCom.Transaction = RTran
            RCom.Connection = RCon
            RCom.CommandType = CommandType.Text
            RSQL = _
            <SQL>
                <![CDATA[
                    UPDATE v
                    SET v.[PrintInvNo] = (ISNULL(v.[PrintInvNo],0) + 1), v.[IsBusy] = 0
                    FROM [Stock].[dbo].[TPRDeliveryTakeOrdPrintInvNo] v
                    WHERE ISNULL(v.[IsBusy],0) = 1;
                ]]>
            </SQL>
            RSQL = String.Format(RSQL, DatabaseName)
            RCom.CommandText = RSQL
            RCom.ExecuteNonQuery()
            RTran.Commit()
            RCon.Close()
            MessageBox.Show("Unlock Printer have been completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            PicStatus.Image = My.Resources.Unlock_Printer
        Catch ex As SqlException
            RTran.Rollback()
            RCon.Close()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            RTran.Rollback()
            RCon.Close()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

End Class