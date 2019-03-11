Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmDutchmillTakeOrderRetrieve
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
    Public Property vDateRequired As Date
    Public Property vList As DataTable
    Public Property vPlanning As String
    Public Property vCusNum As String
    Public Property vDeltoId As Decimal
    Public Property vDepartment As String

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

    Private Sub FrmDutchmillTakeOrderRetrieve_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        DataSources(CmbPONumber, lists, "PONumber", "DateRequired")
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbPONumber.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select the P.O Number!", "Select P.O Number", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbPONumber.Focus()
            Exit Sub
        Else
            Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
            Dim vDateRequired As Date = Todate
            If TypeOf CmbPONumber.SelectedValue Is DataRowView Or CmbPONumber.SelectedValue Is Nothing Then
                vDateRequired = Todate
            Else
                If CmbPONumber.Text.Trim() = "" Then
                    vDateRequired = Todate
                Else
                    vDateRequired = CmbPONumber.SelectedValue
                End If
            End If

            query = _
            <SQL>
                <![CDATA[
                    DECLARE @vCusNum AS NVARCHAR(8) = N'{1}';
                    DECLARE @vDeltoId AS DECIMAL(18,0) = {2};
                    DECLARE @vPlanningOrder AS NVARCHAR(50) = N'{3}';
                    DECLARE @vDepartment AS NVARCHAR(50) = N'{4}';                    
                    DECLARE @vRequiredDate AS DATE = N'{5:yyyy-MM-dd}';

                    SELECT [Id],[DateRequired],[Department],[PlanningOrder],[CreatedDate]
                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder_Locked]
                    WHERE ([Department] = @vDepartment)
                    --AND ([CusNum] = @vCusNum) 
                    --AND ([DeltoId] = @vDeltoId)
                    AND ([PlanningOrder] = @vPlanningOrder)
                    AND (DATEDIFF(DAY,[DateRequired],@vRequiredDate) = 0)
                    ORDER BY [DateRequired];
                ]]>
            </SQL>
            query = String.Format(query, DatabaseName, vCusNum, vDeltoId, vPlanning, vDepartment, vDateRequired)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    MessageBox.Show("Sorry, The Takeorder <" & vDateRequired & "> cannot retrieve." & vbCrLf & "Because of the takeorder was processed." & vbCrLf & "Please check it again...", "Invalid Retrieve", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CmbPONumber.Focus()
                    Exit Sub
                End If
            End If
            Me.vDateRequired = vDateRequired
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

End Class