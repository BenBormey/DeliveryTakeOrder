Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmPODutchmillDate
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
    Public RequiredDate As Date

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

    Private Sub FrmPODutchmillDate_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmPODutchmillDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()

        query = _
        <SQL>
            <![CDATA[
                SELECT [DateRequired]
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                GROUP BY [DateRequired]
                ORDER BY [DateRequired];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbRequiredDate, lists, "DateRequired", "DateRequired")
        If CmbRequiredDate.Items.Count > 0 Then
            CmbRequiredDate.SelectedIndex = 0
            If CmbRequiredDate.Items.Count = 1 Then BtnExportToExcel_Click(BtnExportToExcel, New System.EventArgs)
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnExportToExcel_Click(sender As Object, e As EventArgs) Handles BtnExportToExcel.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If CmbRequiredDate.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any required date.", "Select Required Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbRequiredDate.Focus()
            Exit Sub
        Else
            If ChkLock.Checked = True Then
                query = _
                <SQL>
                    <![CDATA[
                        DECLARE @vDateRequired AS DATE = N'{1:yyyy-MM-dd}';
                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder_Locked]([DateRequired],[Department],[PlanningOrder],[CreatedDate])
                        SELECT [DateRequired],[Remark],[PromotionMachanic],GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                        WHERE DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0
                        GROUP BY [DateRequired],[Remark],[PromotionMachanic];
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, CmbRequiredDate.SelectedValue)
                RCon = New SqlConnection(Data.ConnectionString(Initialized.GetConnectionType(Data, App)))
                RCon.Open()
                RTran = RCon.BeginTransaction()
                Try
                    RCom = New SqlCommand
                    RCom.Transaction = RTran
                    RCom.Connection = RCon
                    RCom.CommandType = CommandType.Text
                    RCom.CommandText = query
                    RCom.ExecuteNonQuery()
                    RTran.Commit()
                    RCon.Close()
                Catch ex As SqlException
                    RTran.Rollback()
                    RCon.Close()
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Catch ex As Exception
                    RTran.Rollback()
                    RCon.Close()
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
            End If
            Me.RequiredDate = CmbRequiredDate.SelectedValue
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class