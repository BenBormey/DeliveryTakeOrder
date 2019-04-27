Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDutchmillTakeOrderPlanningOrder
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
    Public Property vDelto As String
    Public Property vId As Decimal

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

    Private Sub FrmDutchmillTakeOrderPlanningOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LoadingInitialized()
        Me.TxtCusNum.Text = vCusNum
        Me.TxtCusName.Text = vCusName
        Me.TxtDeltoId.Text = vDeltoId
        Me.TxtDelto.Text = vDelto
        Me.PlanningOrderLoading.Enabled = True
    End Sub

    Private Sub FrmDutchmillTakeOrderPlanningOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any planning order!", "Select Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPlanningOrder.Focus()
            Exit Sub
        Else
            Dim oPlanningOrder As String = ""
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                oPlanningOrder = ""
            Else
                If CmbPlanningOrder.Text.Trim().Equals("") = True Then
                    oPlanningOrder = ""
                Else
                    oPlanningOrder = CmbPlanningOrder.SelectedValue
                End If
            End If
            RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
            RCon.Open()
            RTran = RCon.BeginTransaction()
            Try
                RCom.Transaction = RTran
                RCom.Connection = RCon
                RCom.CommandType = CommandType.Text
                query = <SQL><![CDATA[                        
                        DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                        DECLARE @vPlanningOrder_ AS NVARCHAR(50) = N'{2}';
                        DECLARE @vDepartment AS NVARCHAR(50) = N'{3}';
                        DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{4}';
                        DECLARE @vDeltoId AS DECIMAL(18,0) = {5};
                        DECLARE @vId AS DECIMAL(18,0) = {6};

                        UPDATE v
                        SET v.[PlanningOrder] = @vPlanningOrder
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] AS v
                        WHERE v.[Id] = @vId;
                    ]]></SQL>
                query = String.Format(query, DatabaseName, vCusNum, vPlanning, vDepartment, oPlanningOrder, vDeltoId, vId)
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

    Private Sub PlanningOrderLoading_Tick(sender As Object, e As EventArgs) Handles PlanningOrderLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.PlanningOrderLoading.Enabled = False
        Me.query = <SQL><![CDATA[
                        WITH v AS (
	                        SELECT [PlanningOrder]
	                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
	                        GROUP BY [PlanningOrder]
                        )
                        SELECT v.[PlanningOrder]
                        FROM v
                        GROUP BY v.[PlanningOrder]
                        ORDER BY v.[PlanningOrder];           
                    ]]></SQL>
        Me.query = String.Format(query, DatabaseName)
        Dim oLists As DataTable = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        Me.DataSources(CmbPlanningOrder, oLists, "PlanningOrder", "PlanningOrder")
        Me.Cursor = Cursors.Default
    End Sub
End Class