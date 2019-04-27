Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmPlanningOrder
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
    Private MyData As New DatabaseFramework_MySQL
    Private Todate As Date
    Private Printer As New PrintToPrinter
    Private RCon As SqlConnection
    Private RCom As New SqlCommand
    Private RTran As SqlTransaction
    Private Report As LocalReport
    Private RParameter As ReportParameter
    Private DatabaseName As String
    Private query As String
    Private lists As DataTable
    Private vHostName As String = System.Net.Dns.GetHostName()
    Private vIPAddress As String = System.Net.Dns.GetHostByName(vHostName).AddressList(0).ToString()
    Private vCheckBox As CheckBox
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

    Private Sub DisplayLoading_Tick(sender As Object, e As EventArgs) Handles DisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.DisplayLoading.Enabled = False
        Me.query = <SQL><![CDATA[
                        SELECT [Id],[PlanningOrder],[DayOfWeek],[CreatedDate]
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
                        ORDER BY [PlanningOrder];
                    ]]></SQL>
        Me.query = String.Format(Me.query, DatabaseName)
        Me.lists = Data.Selects(Me.query, Initialized.GetConnectionType(Data, App))
        Me.DgvShow.DataSource = Me.lists
        Me.DgvShow.Refresh()
        Me.LblCountRow.Text = String.Format("Count Row : {0}", Me.DgvShow.Rows.Count)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FrmPlanningOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Me.dayofweekloading.Enabled = True
        Me.DisplayLoading.Enabled = True
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If TxtPlanningOrder.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please enter the planning order...", "Enter Planning Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TxtPlanningOrder.Focus()
            Exit Sub
        ElseIf CmbDayOfWeek.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any day of week...", "Select Day of Week", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbDayOfWeek.Focus()
            Exit Sub
        Else
            Dim oPlanningOrder As String = TxtPlanningOrder.Text.Replace("'", "").Trim()
            Dim oDayOfWeek As String = CmbDayOfWeek.Text.Replace("'", "").Trim()
            Dim oId As Decimal = 0
            If BtnAdd.Text.Trim().Equals("&Add") = True Then
                oId = 0
            Else
                oId = CDec(IIf(DBNull.Value.Equals(DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value) = True, 0, DgvShow.Rows(DgvShow.CurrentRow.Index).Cells("Id").Value))
            End If
            query = <SQL><![CDATA[
                            DECLARE @oPlanningOrder AS NVARCHAR(50) = N'{1}';
                            DECLARE @oId AS DECIMAL(18,0) = {2};
                            SELECT [Id],[PlanningOrder],[CreatedDate]
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
                            WHERE ([PlanningOrder] = @oPlanningOrder) AND ([Id] <> @oId);
                        ]]></SQL>
            query = String.Format(query, DatabaseName, oPlanningOrder, oId)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    MessageBox.Show("Please check the Planning Order again!" & vbCrLf & "The Planning Order is existed already.", "Duplicated Barcode", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TxtPlanningOrder.Focus()
                    Exit Sub
                End If
            End If

            query = <SQL><![CDATA[
                            DECLARE @oPlanningOrder AS NVARCHAR(50) = N'{1}';                            
                            DECLARE @oId AS DECIMAL(18,0) = {2};
                            DECLARE @oDayOfWeek AS NVARCHAR(25) = N'{3}';
                            IF NOT EXISTS (SELECT * FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder] WHERE ([PlanningOrder] = @oPlanningOrder)) AND (@oId = 0)
                            BEGIN
	                            INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]([PlanningOrder],[DayOfWeek],[CreatedDate])
	                            VALUES(@oPlanningOrder,@oDayOfWeek,GETDATE());
                            END
                            ELSE
                            BEGIN
	                            IF (@oId <> 0)
                                BEGIN
		                            DECLARE @oPlanningOrder_Old AS NVARCHAR(50) = N'';
		                            SELECT @oPlanningOrder_Old = ISNULL(o.[PlanningOrder],N'')
		                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder] o
		                            WHERE [Id] = @oId;

		                            UPDATE o
		                            SET o.[PlanningOrder] = @oPlanningOrder,
                                    o.[DayOfWeek] = @oDayOfWeek
		                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder] o
		                            WHERE [Id] = @oId;

		                            UPDATE o
		                            SET o.[PlanningOrder] = @oPlanningOrder
		                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder] o
		                            WHERE o.[PlanningOrder] = @oPlanningOrder_Old;

                                    UPDATE o
		                            SET o.[PromotionMachanic] = @oPlanningOrder
		                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill] o
		                            WHERE o.[PromotionMachanic] = @oPlanningOrder_Old;
	                            END
                            END
                        ]]></SQL>
            query = String.Format(query, DatabaseName, oPlanningOrder, oId, oDayOfWeek)
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
                Me.DisplayLoading.Enabled = True
                BtnAdd.Text = "&Add"
                BtnAdd.Image = My.Resources.add16
                With TxtPlanningOrder
                    .Text = ""
                    .Focus()
                End With
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

    Private Sub DgvShow_DoubleClick(sender As Object, e As EventArgs) Handles DgvShow.DoubleClick
        If DgvShow.Rows.Count <= 0 Then
            MessageBox.Show("No record to update!", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim oPlanning As String = Trim(IIf(DBNull.Value.Equals(.Cells("PlanningOrder").Value) = True, "", .Cells("PlanningOrder").Value))
            Dim oDayofWeek As String = Trim(IIf(DBNull.Value.Equals(.Cells("DayOfWeek").Value) = True, "", .Cells("DayOfWeek").Value))
            Me.TxtPlanningOrder.Text = oPlanning.Trim()
            Me.CmbDayOfWeek.SelectedValue = oDayofWeek
            BtnAdd.Text = "&Update"
            BtnAdd.Image = My.Resources.update_blue
        End With

    End Sub

    Private Sub FrmPlanningOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then
            BtnAdd.Text = "&Add"
            BtnAdd.Image = My.Resources.add16
            With TxtPlanningOrder
                .Text = ""
                .Focus()
            End With
        End If
    End Sub

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.Rows.Count <= 0 Then
            MessageBox.Show("No record to delete!", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then FrmPlanningOrder_PreviewKeyDown(Me, e)
        If e.KeyCode <> Keys.Delete Then Exit Sub

        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim oId As Decimal = CDec(IIf(DBNull.Value.Equals(.Cells("Id").Value) = True, 0, .Cells("Id").Value))
            Dim oPlanning As String = Trim(IIf(DBNull.Value.Equals(.Cells("PlanningOrder").Value) = True, "", .Cells("PlanningOrder").Value))
            query = <SQL><![CDATA[
                            DECLARE @oPlanningOrder AS NVARCHAR(50) = N'{1}';
                            SELECT [CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],GETDATE()
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                            WHERE [PlanningOrder] = @oPlanningOrder;                            
                        ]]></SQL>
            query = String.Format(query, DatabaseName, oPlanning)
            lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
            If Not (lists Is Nothing) Then
                If lists.Rows.Count > 0 Then
                    MessageBox.Show("Cannot delete this planning!" & vbCrLf & "Because the planning is process order...", "Invalid Order", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            If MessageBox.Show("Are you sure, you want to delete this (" & oPlanning & ")?(Yes/No)", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            query = <SQL><![CDATA[
                            DECLARE @oId AS DECIMAL(18,0) = {1};
                            DECLARE @oPlanningOrder_Old AS NVARCHAR(50) = N'';
		                    SELECT @oPlanningOrder_Old = ISNULL(o.[PlanningOrder],N'')
		                    FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder] o
		                    WHERE [Id] = @oId;

                            INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder_Deleting]([CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],[DeletedDate])
                            SELECT [CusNum],[CusName],[DeltoId],[Delto],[UnitNumber],[Barcode],[ProName],[Size],[QtyPerCase],[PcsOrder],[CTNOrder],[TotalPcsOrder],[SupNum],[SupName],[Renew],[NotAccept],[ChangeQty],[Department],[PlanningOrder],[MachineName],[IPAddress],[CreatedDate],[VerifyDate],GETDATE()
                            FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                            WHERE [PlanningOrder] = @oPlanningOrder_Old;

                            DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_DutchmillOrder]
                            WHERE [PlanningOrder] = @oPlanningOrder_Old;

                            INSERT  INTO [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder.Deleted]
        ( [PlanningOrder] ,
          [DayOfWeek] ,
          [CreatedDate] ,
          [DeletedDate]
        )
        SELECT  [PlanningOrder] ,
                [DayOfWeek] ,
                [CreatedDate] ,
                GETDATE()
        FROM    [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
        WHERE   ( [Id] = @oId );

                            DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_PlanningOrder]
		                    WHERE [Id] = @oId;
                        ]]></SQL>
            query = String.Format(query, DatabaseName, oId)
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
                Me.DisplayLoading.Enabled = True
                BtnAdd.Text = "&Add"
                BtnAdd.Image = My.Resources.add16
                With TxtPlanningOrder
                    .Text = ""
                    .Focus()
                End With
            Catch ex As SqlException
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                RTran.Rollback()
                RCon.Close()
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End With
    End Sub

    Private Sub TxtPlanningOrder_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtPlanningOrder.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then FrmPlanningOrder_PreviewKeyDown(Me, e)
    End Sub

    Private Sub Panel15_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Panel15.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then FrmPlanningOrder_PreviewKeyDown(Me, e)
    End Sub

    Private Sub BtnAdd_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles BtnAdd.PreviewKeyDown
        If e.KeyCode = Keys.Escape Then FrmPlanningOrder_PreviewKeyDown(Me, e)
    End Sub

    Private Sub FrmPlanningOrder_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private oDayOfWeekList As DataTable
    Private Sub dayofweekloading_Tick(sender As Object, e As EventArgs) Handles dayofweekloading.Tick
        Me.Cursor = Cursors.WaitCursor
        Me.dayofweekloading.Enabled = False
        If Not (Me.oDayOfWeekList Is Nothing) Then Me.oDayOfWeekList = Nothing
        Me.oDayOfWeekList = New DataTable
        With Me.oDayOfWeekList.Columns
            .Add("value", GetType(String))
            .Add("display", GetType(String))
        End With
        Dim oRow As DataRow = Nothing
        For o As Decimal = 1 To 7
            Dim oDayName As String = WeekdayName(o, False, FirstDayOfWeek.Monday).ToUpper()
            oRow = Me.oDayOfWeekList.NewRow()
            oRow("value") = oDayName
            oRow("display") = oDayName
            Me.oDayOfWeekList.Rows.Add(oRow)
        Next
        Me.DataSources(CmbDayOfWeek, Me.oDayOfWeekList, "display", "value")
        Me.Cursor = Cursors.Default
    End Sub
End Class