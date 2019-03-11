Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmUnlockPrinter
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private DatabaseName As String
    Private Todate As Date
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Public RProgramList As Dictionary(Of String, Object)

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

    Private Sub FrmUnlockPrinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TimerLoading.Enabled = True
    End Sub

    Private Sub FrmUnlockPrinter_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_CompanyName)
    End Sub

    Private Sub TimerLoading_Tick(sender As Object, e As EventArgs) Handles TimerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerLoading.Enabled = False
        Dim RTableProgramList As New DataTable
        With RTableProgramList.Columns
            .Add("display", GetType(String))
            .Add("value", GetType(Object))
        End With
        Dim DRow As DataRow
        If Not (RProgramList Is Nothing) Then
            For Each field As KeyValuePair(Of String, Object) In RProgramList
                DRow = RTableProgramList.NewRow
                DRow("display") = field.Key
                DRow("value") = field.Value
                RTableProgramList.Rows.Add(DRow)
            Next
        End If
        DataSources(CmbProgramName, RTableProgramList, "display", "value")
        Me.Cursor = Cursors.Default
    End Sub

    Private RSQL As String
    Private Sub BtnUnlockPrinter_Click(sender As Object, e As EventArgs) Handles BtnUnlockPrinter.Click
        If Trim(CmbProgramName.Text) = "" Then
            MessageBox.Show("Please select any program name!", "Select Program Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbProgramName.Focus()
            Exit Sub
        End If
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
                    UPDATE [{0}].[dbo].[TblAutoNumber] 
                    SET [Active] = 0 
                    WHERE [ProgramName] = N'{1}';
                ]]>
            </SQL>
            RSQL = String.Format(RSQL, DatabaseName, CmbProgramName.SelectedValue)
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

    Private Sub CmbProgramName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbProgramName.SelectedIndexChanged
        If TypeOf CmbProgramName.SelectedValue Is DataRowView Or CmbProgramName.SelectedValue Is Nothing Then Exit Sub
        RSQL = _
            <SQL>
                <![CDATA[
                    SELECT *
                    FROM [{0}].[dbo].[TblAutoNumber]
                    WHERE [ProgramName] = N'{1}' AND [Active] = 1;
                ]]>
            </SQL>
        RSQL = String.Format(RSQL, DatabaseName, CmbProgramName.SelectedValue)
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
    End Sub
End Class