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
    Public Property iRequiredDate As Date
    Public Property iPlanningOrder As String

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
        Me.planningloading.Enabled = True

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
            Dim oplanningorder As String = ""
            If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
                oplanningorder = ""
            Else
                If CmbPlanningOrder.Text.Trim().Equals("") = True Then
                    oplanningorder = ""
                Else
                    oplanningorder = CmbPlanningOrder.SelectedValue
                End If
            End If
            If ChkLock.Checked = True Then
                query = <SQL>
                            <![CDATA[
                                DECLARE @oPlanningOrder AS NVARCHAR(100) = N'{1}';
                                DECLARE @vDateRequired AS DATE = N'{2:yyyy-MM-dd}';
                                INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder_Locked]([DateRequired],[Department],[PlanningOrder],[CreatedDate])
                                SELECT [DateRequired],[Remark],[PromotionMachanic],GETDATE()
                                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                                WHERE (ISNULL([PlanningOrder],N'') = @oPlanningOrder) AND (DATEDIFF(DAY,[DateRequired],@vDateRequired) = 0)
                                GROUP BY [DateRequired],[Remark],[PromotionMachanic];
                            ]]>
                        </SQL>
                query = String.Format(query, DatabaseName, oplanningorder, CmbRequiredDate.SelectedValue)
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
            Me.iPlanningOrder = oplanningorder.Trim()
            Me.iRequiredDate = CmbRequiredDate.SelectedValue
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub planningloading_Tick(sender As Object, e As EventArgs) Handles planningloading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.planningloading.Enabled = False
        query = <SQL>
                    <![CDATA[
                        SELECT [PromotionMachanic] [PlanningOrder]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                        GROUP BY [PromotionMachanic]
                        ORDER BY [PromotionMachanic];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbPlanningOrder, lists, "PlanningOrder", "PlanningOrder")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub requireddateloading_Tick(sender As Object, e As EventArgs) Handles requireddateloading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.requireddateloading.Enabled = False
        Dim oplanningorder As String = ""
        If TypeOf CmbPlanningOrder.SelectedValue Is DataRowView Or CmbPlanningOrder.SelectedValue Is Nothing Then
            oplanningorder = ""
        Else
            If CmbPlanningOrder.Text.Trim().Equals("") = True Then
                oplanningorder = ""
            Else
                oplanningorder = CmbPlanningOrder.SelectedValue
            End If
        End If
        query = <SQL>
                    <![CDATA[
                        DECLARE @oPlanningOrder AS NVARCHAR(100) = N'{1}';
                        SELECT [DateRequired]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill]
                        WHERE (ISNULL([PromotionMachanic],N'') = @oPlanningOrder)
                        GROUP BY [DateRequired]
                        ORDER BY [DateRequired];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName, oplanningorder)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbRequiredDate, lists, "DateRequired", "DateRequired")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CmbPlanningOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPlanningOrder.SelectedIndexChanged
        Me.requireddateloading.Enabled = True
    End Sub
End Class