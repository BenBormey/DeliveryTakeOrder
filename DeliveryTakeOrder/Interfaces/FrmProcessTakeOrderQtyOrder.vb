Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop
Imports System.IO

Public Class FrmProcessTakeOrderQtyOrder
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
    Public Property vBarcode As String
    Public Property vProName As String
    Public Property vSize As String
    Public Property vQtyPerCase As Integer
    Public Property vPcsOrder As Decimal
    Public Property vPackOrder As Decimal
    Public Property vCTNOrder As Decimal
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

    Private Sub FrmDeliveryTakeOrderInfoAeon_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmProcessTakeOrderQtyOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        TxtBarcode.Text = vBarcode
        TxtProName.Text = vProName
        TxtSize.Text = vSize
        TxtQtyPerCase.Text = vQtyPerCase
        TxtOldPcsOrder.Text = String.Format("{0:N0}", vPcsOrder)
        TxtOldPackOrder.Text = String.Format("{0:N0}", vPackOrder)
        TxtOldCTNOrder.Text = String.Format("{0:N2}", vCTNOrder)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click
        Me.DialogResult = Windows.Forms.DialogResult.None
        If TxtNewPcsOrder.Text.Trim().Equals("") = True And _
            TxtNewPackOrder.Text.Trim().Equals("") = True And _
            TxtNewCTNOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please enter the quantity order!", "Enter Quantity Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtNewPcsOrder.Focus()
            Exit Sub
        Else
            Dim vNewPcsOrder As Decimal = CDec(IIf(TxtNewPcsOrder.Text.Trim() = "", 0, TxtNewPcsOrder.Text.Trim()))
            Dim vNewPackOrder As Decimal = CDec(IIf(TxtNewPackOrder.Text.Trim() = "", 0, TxtNewPackOrder.Text.Trim()))
            Dim vNewCTNOrder As Decimal = CDec(IIf(TxtNewCTNOrder.Text.Trim() = "", 0, TxtNewCTNOrder.Text.Trim()))
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
                        DECLARE @vBarcode AS NVARCHAR(MAX) = N'{2}';
                        DECLARE @vNewPcsOrder AS DECIMAL(18,0) = {3};
                        DECLARE @vNewPackOrder AS DECIMAL(18,0) = {4};
                        DECLARE @vNewCTNOrder AS DECIMAL(18,0) = {5};
                        DECLARE @vId AS DECIMAL(18,0) = {6};
                        IF (@vNewPcsOrder IS NULL) SET @vNewPcsOrder = 0;
                        IF (@vNewPackOrder IS NULL) SET @vNewPackOrder = 0;
                        IF (@vNewCTNOrder IS NULL) SET @vNewCTNOrder = 0;
                        UPDATE v
                        SET v.PcsOrder = @vNewPcsOrder,v.PackOrder = @vNewPackOrder,v.CTNOrder = @vNewCTNOrder,v.TotalPcsOrder = (@vNewPcsOrder + (@vNewPackOrder * ISNULL(v.QtyPPack,1)) + (@vNewCTNOrder * ISNULL(v.QtyPCase,1))) 
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] AS v
                        --WHERE v.TakeOrderNumber = @vTakeOrder AND v.Barcode = @vBarcode;
                        WHERE v.Id = @vId;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, vTakeOrder, vBarcode, vNewPcsOrder, vNewPackOrder, vNewCTNOrder, vId)
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

    Private Sub TxtNewPcsOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNewPcsOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtNewPackOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNewPackOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, , 10)
    End Sub

    Private Sub TxtNewCTNOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtNewCTNOrder.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Float, , 10)
    End Sub
End Class