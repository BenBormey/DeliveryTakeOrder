Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmProcessTakeOrderDelto
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private MyData As New DatabaseFramework_MySQL
    Private Todate As Date
    Private Printer As New PrintToPrinter
    Private Export As New ConvertReport
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Private Report As LocalReport
    Private RParameter As ReportParameter
    Private DatabaseName As String
    Private query As String
    Private lists As DataTable
    Public Property vTakeOrder As Decimal
    Public Property vCusNum As String
    Public Property vDeltoId As String
    Public Property vDeltoName As String

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

    Private Sub FrmDeliveryTakeOrderInfoAeon_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmProcessTakeOrderDelto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TxtDeltoId.Text = vDeltoId
        TxtDeltoName.Text = vDeltoName
        TimerCustomerLoading.Enabled = True
    End Sub

    Private Sub TimerCustomerLoading_Tick(sender As Object, e As EventArgs) Handles TimerCustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCustomerLoading.Enabled = False
        query = <SQL>
                    <![CDATA[
                        DECLARE @vTakeOrderNumber AS DECIMAL(18,0) = {1};
                        SELECT [DefId] [Id],[DelTo]
                        FROM [Stock].[dbo].[TPRDelto]
                        --WHERE [DefId] NOT IN (SELECT [DelToId] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] WHERE [TakeOrderNumber] = @vTakeOrderNumber)
                        ORDER BY [DelTo];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, vTakeOrder)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, lists, "DelTo", "Id")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbDelto.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any customer!", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbDelto.Focus()
            Exit Sub
        Else
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                query = _
                <SQL>
                    <![CDATA[
                        DECLARE @vTakeOrder AS DECIMAL(18,0) = {1};
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                        DECLARE @vDeltoName AS NVARCHAR(100) = N'{3}';
                        DECLARE @vCusNum_ AS NVARCHAR(8) = N'{4}';
                        DECLARE @vDeltoId_ AS DECIMAL(18,0) = {5};
                        DECLARE @vAll AS BIT = {6};
                        IF (@vAll = 0)
                        BEGIN
                            UPDATE v
                            SET v.DelToId = @vDeltoId,v.DelTo = @vDeltoName
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                            WHERE (v.TakeOrderNumber = @vTakeOrder) AND (v.CusNum = @vCusNum_) AND (v.DelToId = @vDeltoId_);
                        END
                        ELSE
                        BEGIN
                            UPDATE v
                            SET v.DelToId = @vDeltoId,v.DelTo = @vDeltoName
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                            WHERE (v.TakeOrderNumber = @vTakeOrder) AND (v.CusNum = @vCusNum_);
                        END
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vTakeOrder, CmbDelto.SelectedValue, CmbDelto.Text.Trim(), vCusNum, vDeltoId, IIf(ChkChangeAll.Checked = True, 1, 0))
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Catch ex As SqlException
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub
End Class