Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports Microsoft.Office.Interop

Public Class FrmOverCreditAmountOrCreditTerm
    Private Data As New DatabaseFramework
    Private App As New ApplicationFramework
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
    Public Property RequiredDate As DateTime

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

    Private Sub FrmOverCreditAmountOrCreditTerm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadingInitialized()
        Todate = Data.Get_CURRENT_DATE(Initialized.GetConnectionType(Data, App))
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
            'RSheet.Range("D:J").Font.Name = "Webdings"
            RSheet.Range("A4:Z4").Font.Name = "Arial"
            RSheet.Range("A:Z").Font.Size = 8
            RSheet.Range("D:J").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            RSheet.Range("D:J").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            RSheet.Range("A1").Value = "Over Credit Amount | Over Credit Term"
            RSheet.Range("A2").Value = "Export Date : " & Format(Now(), "dd-MMM-yy")
            RSheet.Range("A4:K4").Font.Bold = True
            RSheet.Range("A4").Value = "Nº"
            RSheet.Range("B4").Value = "Cus.#"
            RSheet.Range("C4").Value = "Cus. Name"
            RSheet.Range("D4").Value = "Grand Total"
            RSheet.Range("E4").Value = "Over Day"
            RSheet.Range("F4").Value = "Credit (Allow)"
            RSheet.Range("G4").Value = "Term (Allow)"
            RSheet.Range("H4").Value = "Average (Last 3 Months)"
            RSheet.Range("I4").Value = "New Proposing Credit Limit"

            RSheet.Range("B:B").NumberFormat = "@"
            RSheet.Range("D:D, F:F, H:H").NumberFormat = "_($* #,##0.00_);_($* (#,##0.00);_($* ""-""??_);_(@_)"
            Dim QsRow As Long = 5
            Dim vGrandTotal As Double = 0
            Dim vCreditAllow As Double = 0
            Dim vIndex As Decimal = 1
            Dim vCusNum As String = ""
            For Each QsDataRow As DataGridViewRow In DgvShow.Rows
                vGrandTotal = CDbl(IIf(IsDBNull(QsDataRow.Cells("GrandTotal").Value) = True, "", QsDataRow.Cells("GrandTotal").Value))
                vCreditAllow = CDbl(IIf(IsDBNull(QsDataRow.Cells("CreditLimitAllow").Value) = True, "", QsDataRow.Cells("CreditLimitAllow").Value))
                vCusNum = Trim(IIf(IsDBNull(QsDataRow.Cells("CusNum").Value) = True, "", QsDataRow.Cells("CusNum").Value))
                RSheet.Range("A" & QsRow).Value = vIndex
                RSheet.Range("B" & QsRow).Value = vCusNum
                RSheet.Range("C" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CusCom").Value) = True, "", QsDataRow.Cells("CusCom").Value)
                RSheet.Range("D" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("GrandTotal").Value) = True, "", QsDataRow.Cells("GrandTotal").Value)
                RSheet.Range("E" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Overday").Value) = True, "", QsDataRow.Cells("Overday").Value)
                RSheet.Range("F" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("CreditLimitAllow").Value) = True, "", QsDataRow.Cells("CreditLimitAllow").Value)
                RSheet.Range("G" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("MaxMonthAllow").Value) = True, "", QsDataRow.Cells("MaxMonthAllow").Value)
                RSheet.Range("H" & QsRow).Value = IIf(IsDBNull(QsDataRow.Cells("Average").Value) = True, "", QsDataRow.Cells("Average").Value)
                If (vGrandTotal > vCreditAllow) Then
                    'RSheet.Range("A" & QsRow & ":G" & QsRow).EntireRow.Interior.ColorIndex = Color.Red
                    RSheet.Range("A" & QsRow & ":I" & QsRow).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                Else
                    RSheet.Range("A" & QsRow & ":I" & QsRow).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black)
                End If
                vIndex += 1
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