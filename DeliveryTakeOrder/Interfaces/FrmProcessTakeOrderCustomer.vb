Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmProcessTakeOrderCustomer
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
    Public Property vCusName As String
    Public Property vDeltoId As Decimal

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

    Private Sub FrmProcessTakeOrderCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TxtCusNum.Text = vCusNum
        TxtCusName.Text = vCusName
        TimerCustomerLoading.Enabled = True
    End Sub

    Private Sub TimerCustomerLoading_Tick(sender As Object, e As EventArgs) Handles TimerCustomerLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerCustomerLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                DECLARE @vTakeOrderNumber AS DECIMAL(18,0) = {1};
                SELECT [CusNum],[CusName],ISNULL([CusNum],N'') + SPACE(3) + ISNULL([CusName],N'') AS [Customer]
                FROM [Stock].[dbo].[TPRCustomer]
                WHERE [Status] = N'Activate'
                --AND [CusNum] NOT IN (SELECT [CusNum] FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] WHERE [TakeOrderNumber] = @vTakeOrderNumber)
                GROUP BY [CusNum],[CusName],ISNULL([CusNum],N'') + SPACE(3) + ISNULL([CusName],N'')
                ORDER BY [CusName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName, vTakeOrder)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbCustomer, lists, "Customer", "CusNum")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbCustomer.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any customer!", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbCustomer.Focus()
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
                        DECLARE @vTakeOrder AS DECIMAL(18,0) = {2};                        
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {3};
                        DECLARE @vCusNumOld AS NVARCHAR(8) = N'{4}';
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vCusName AS NVARCHAR(100) = N'';
                        DECLARE @vAll AS BIT = {5};
                        SELECT @vCusName = [CusName] FROM [Stock].[dbo].[TPRCustomer] WHERE [CusNum] = @vCusNum;
                        IF (@vAll = 0)
                        BEGIN
                            UPDATE v
                            SET v.CusNum = @vCusNum,v.CusName = @vCusName
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                            WHERE (v.TakeOrderNumber = @vTakeOrder) AND (v.DeltoId = @vDeltoId) and (v.CusNum = @vCusNumOld);
                        END
                        ELSE
                        BEGIN
                            UPDATE v
                            SET v.CusNum = @vCusNum,v.CusName = @vCusName
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                            WHERE (v.TakeOrderNumber = @vTakeOrder) AND (v.CusNum = @vCusNumOld);
                        END
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, CmbCustomer.SelectedValue, vTakeOrder, vDeltoId, vCusNum, IIf(ChkChangeAll.Checked = True, 1, 0))
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