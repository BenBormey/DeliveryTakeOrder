Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmDeltoListForDutchmillPO
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
    Private Const InvoiceName As String = ""

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

    Private Sub FrmDeltoListForDutchmillPO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        TxtId.Focus()
    End Sub

    Private Sub FrmDeltoListForDutchmillPO_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FrmDeltoListForDutchmillPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
        TimerDeltoLoading.Enabled = True
    End Sub

    Private Sub FrmDeltoListForDutchmillPO_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        PicLogo.Image = Initialized.R_Logo
        LblCompanyName.Text = UCase(Initialized.R_DatabaseName)
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub TimerDeltoLoading_Tick(sender As Object, e As EventArgs) Handles TimerDeltoLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerDeltoLoading.Enabled = False
        query = <SQL>
                    <![CDATA[
                        SELECT [DefId] [Id],[DelTo]
                        FROM [Stock].[dbo].[TPRDelto]
                        ORDER BY [DelTo];
                    ]]>
                </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DataSources(CmbDelto, lists, "DelTo", "Id")
        BtnCancel_Click(BtnCancel, e)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TxtId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtId.KeyPress
        App.KeyPress(sender, e, ApplicationFramework.TypeKeyPress.Format_Number, 10)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        App.ClearController(ChkMonday, ChkTuesday, ChkWednesday, ChkWednesday, ChkThursday, ChkFriday, ChkSaturday, ChkSaturday, ChkSunday, TxtId, CmbDelto)
        App.SetVisibleController(False, BtnUpdate, BtnCancel)
        App.SetVisibleController(True, BtnAdd)
        App.SetEnableController(True, DgvShow, BtnExportToExcel)
        TimerDisplayLoading.Enabled = True
    End Sub

    Private Sub TxtId_TextChanged(sender As Object, e As EventArgs) Handles TxtId.TextChanged
        Dim id As Decimal = Convert.ToDecimal(IIf(TxtId.Text.Trim().Equals("") = True, 0, TxtId.Text.Trim()))
        CmbDelto.SelectedValue = id
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If CmbDelto.Text.Trim().Equals("") = True Then
            MessageBox.Show("Please select any delto!", "Select Delto", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                        DECLARE @DeltoId AS DECIMAL(18,0) = {1};
                        DECLARE @Delto AS NVARCHAR(100) = N'{2}';
                        DECLARE @Monday AS BIT = {3};
                        DECLARE @Tuesday AS BIT = {4};
                        DECLARE @Wednesday AS BIT = {5};
                        DECLARE @Thursday AS BIT = {6};
                        DECLARE @Friday AS BIT = {7};
                        DECLARE @Saturday AS BIT = {8};
                        DECLARE @Sunday AS BIT = {9};

                        IF NOT EXISTS (SELECT * FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting] WHERE [DeltoId] = @DeltoId)
                        BEGIN
	                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting]([DeltoId],[Delto],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday],[Sunday],[CreatedDate])
	                        VALUES(@DeltoId,@Delto,@Monday,@Tuesday,@Wednesday,@Thursday,@Friday,@Saturday,@Sunday,GETDATE());
                        END
                        ELSE
                        BEGIN
	                        UPDATE [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting]
	                        SET [Delto]=@Delto,[Monday]=@Monday,[Tuesday]=@Tuesday,[Wednesday]=@Wednesday,[Thursday]=@Thursday,[Friday]=@Friday,[Saturday]=@Saturday,[Sunday]=@Sunday
	                        WHERE [DeltoId] = @DeltoId;
                        END
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, CmbDelto.SelectedValue, CmbDelto.Text.Trim(), _
                                      IIf(ChkMonday.Checked = True, 1, 0), _
                                      IIf(ChkTuesday.Checked = True, 1, 0), _
                                      IIf(ChkWednesday.Checked = True, 1, 0), _
                                      IIf(ChkThursday.Checked = True, 1, 0), _
                                      IIf(ChkFriday.Checked = True, 1, 0), _
                                      IIf(ChkSaturday.Checked = True, 1, 0), _
                                      IIf(ChkSunday.Checked = True, 1, 0))
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                BtnCancel_Click(BtnCancel, e)
                TxtId.Focus()
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

    Private Sub TimerDisplayLoading_Tick(sender As Object, e As EventArgs) Handles TimerDisplayLoading.Tick
        Me.Cursor = Cursors.WaitCursor
        TimerDisplayLoading.Enabled = False
        query = _
        <SQL>
            <![CDATA[
                SELECT [Id],[DeltoId],[Delto],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday],[Sunday],[CreatedDate]
                FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting]
                ORDER BY [Delto];
            ]]>
        </SQL>
        query = String.Format(query, DatabaseName)
        lists = Data.Selects(query, Initialized.GetConnectionType(Data, App))
        DgvShow.DataSource = lists
        DgvShow.Refresh()
        LblCountRow.Text = String.Format("Count Row : {0}", DgvShow.RowCount)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DgvShow_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DgvShow.PreviewKeyDown
        If DgvShow.RowCount <= 0 Then Exit Sub
        With DgvShow.Rows(DgvShow.CurrentRow.Index)
            Dim id As Decimal = Convert.ToDecimal(IIf(IsDBNull(.Cells("Id").Value) = True, 0, .Cells("Id").Value))
            Dim delto As String = Trim(IIf(IsDBNull(.Cells("Delto").Value) = True, "", .Cells("Delto").Value))
            If MessageBox.Show("Are you sure, you want to remove the " & delto & "?(Yes/No)", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
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
                        DECLARE @Id AS DECIMAL(18,0) = {1};

                        INSERT INTO [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting_Deleted]([DeltoId],[Delto],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday],[Sunday],[CreatedDate],[DeletedDate])
                        SELECT [DeltoId],[Delto],[Monday],[Tuesday],[Wednesday],[Thursday],[Friday],[Saturday],[Sunday],[CreatedDate],GETDATE()
                        FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting]
                        WHERE [Id] = @Id;

                        DELETE FROM [{0}].[dbo].[TblDeliveryTakeOrders_Dutchmill_DeltoSetting] 
                        WHERE [Id] = @Id;
                    ]]>
                </SQL>
                query = String.Format(query, DatabaseName, id)
                RCom.CommandText = query
                RCom.ExecuteNonQuery()
                RTran.Commit()
                RCon.Close()
                BtnCancel_Click(BtnCancel, e)
                TxtId.Focus()
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

    Private Sub BtnExportToExcel_Click(sender As Object, e As EventArgs) Handles BtnExportToExcel.Click
        If DgvShow.RowCount > 0 Then
            Dim RExcel As Excel.Application
            Dim RBook As Excel.Workbook
            Dim RSheet As Excel.Worksheet
            RExcel = CreateObject("Excel.Application")
            RBook = RExcel.Workbooks.Add(System.Type.Missing)
            RSheet = RBook.Worksheets(1)
            RSheet.Range("A:Z").Font.Name = "Arial"
            RSheet.Range("D:J").Font.Name = "Webdings"
            RSheet.Range("A4:Z4").Font.Name = "Arial"
            RSheet.Range("A:Z").Font.Size = 8
            RSheet.Range("D:J").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            RSheet.Range("D:J").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            RSheet.Range("A1").Value = "Delto List For Dutchmill P.O"
            RSheet.Range("A2").Value = "Export Date : " & Format(Now(), "dd-MMM-yy")
            RSheet.Range("A4:K4").Font.Bold = True
            RSheet.Range("A4").Value = "Nº"
            RSheet.Range("B4").Value = "#"
            RSheet.Range("C4").Value = "Delto"
            RSheet.Range("D4").Value = "Monday"
            RSheet.Range("E4").Value = "Tuesday"
            RSheet.Range("F4").Value = "Wednesday"
            RSheet.Range("G4").Value = "Thursday"
            RSheet.Range("H4").Value = "Friday"
            RSheet.Range("I4").Value = "Saturday"
            RSheet.Range("J4").Value = "Sunday"
            RSheet.Range("K4").Value = "Created Date"
            RSheet.Range("B:B").NumberFormat = "@"
            RSheet.Range("K:K").NumberFormat = "dd-MMM-yy hh:mm:ss AM/PM"
            Dim QsRow As Long = 5
            For Each QsDataRow As DataGridViewRow In DgvShow.Rows
                RSheet.Range("A" & QsRow).Value = (QsRow - 4)
                RSheet.Range("B" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("DeltoId").Value) = True, "", QsDataRow.Cells("DeltoId").Value)
                RSheet.Range("C" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Delto").Value) = True, "", QsDataRow.Cells("Delto").Value)
                RSheet.Range("D" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Monday").Value) = True, 0, QsDataRow.Cells("Monday").Value)) = True, "a", "")
                RSheet.Range("E" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Tuesday").Value) = True, 0, QsDataRow.Cells("Tuesday").Value)) = True, "a", "")
                RSheet.Range("F" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Wednesday").Value) = True, 0, QsDataRow.Cells("Wednesday").Value)) = True, "a", "")
                RSheet.Range("G" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Thursday").Value) = True, 0, QsDataRow.Cells("Thursday").Value)) = True, "a", "")
                RSheet.Range("H" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Friday").Value) = True, 0, QsDataRow.Cells("Friday").Value)) = True, "a", "")
                RSheet.Range("I" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Saturday").Value) = True, 0, QsDataRow.Cells("Saturday").Value)) = True, "a", "")
                RSheet.Range("J" & QsRow).Value = IIf(CBool(IIf(IsDBNull(QsDataRow.Cells("Sunday").Value) = True, 0, QsDataRow.Cells("Sunday").Value)) = True, "a", "")
                RSheet.Range("K" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CreatedDate").Value) = True, "", QsDataRow.Cells("CreatedDate").Value)
                QsRow = QsRow + 1
            Next
            RSheet.Columns.AutoFit()
            RSheet.Range("A:A").ColumnWidth = 4
            RExcel.Visible = True
            App.ReleaseObject(RSheet)
            App.ReleaseObject(RBook)
            App.ReleaseObject(RExcel)
        End If
    End Sub
End Class