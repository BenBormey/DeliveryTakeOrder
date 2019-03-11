Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDutchmillTakeOrderCustomer
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
    Public Property vDepartment As String
    Public Property vPlanning As String
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

    Private Sub FrmDutchmillTakeOrderCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TxtCusNum.Text = vCusNum
        TxtCusName.Text = vCusName
        BillToLoading.Enabled = True
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbBillTo.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any customer!", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbBillTo.Focus()
            Exit Sub
        Else
            Dim xCusNum As String = ""
            If TypeOf CmbBillTo.SelectedValue Is DataRowView Or CmbBillTo.SelectedValue Is Nothing Then
                xCusNum = ""
            Else
                If CmbBillTo.Text.Trim().Equals("") = True Then
                    xCusNum = ""
                Else
                    xCusNum = CmbBillTo.SelectedValue
                End If
            End If
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
                        DECLARE @OldCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{2}';
                        DECLARE @vCusName AS NVARCHAR(100) = N'';
                        DECLARE @vDepartment AS NVARCHAR(50) = N'{3}';
                        DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{4}';
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {5};

                        SELECT @vCusName=v.CusName
                        FROM [Stock].[dbo].[TPRCustomer] AS v
                        WHERE v.CusNum = @vCusNum;

                        UPDATE v
                        SET v.[CusNum] = @vCusNum,v.[CusName] = @vCusName
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] AS v
                        WHERE v.[CusNum] = @OldCusNum AND v.[Department] = @vDepartment AND v.[PlanningOrder] = @vPlanningOrder AND v.[DeltoId] = @vDeltoId;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vCusNum, xCusNum, vDepartment, vPlanning, vDeltoId)
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

    Private Sub TxtNewPcsOrder_KeyPress(sender As Object, e As KeyPressEventArgs)
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtNewPackOrder_KeyPress(sender As Object, e As KeyPressEventArgs)
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtNewCTNOrder_KeyPress(sender As Object, e As KeyPressEventArgs)
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Float, , 10)
    End Sub

    Private Sub BillToLoading_Tick(sender As Object, e As EventArgs) Handles BillToLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        BillToLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                SELECT [CusNum],[CusName]
                FROM [Stock].[dbo].[TPRCustomer]
                WHERE [Status] = N'Activate'
                GROUP BY [CusNum],[CusName]
                ORDER BY [CusName];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbBillTo, lists, "CusName", "CusNum")
        Me.Cursor = Cursors.Default
    End Sub
End Class