Imports DevExpress.XtraReports.UI

Public Class AgingInvoiceDetailReport
    Dim rowNum As Integer = 0

    Private Sub cellRowNum_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellRowNum.BeforePrint
        rowNum += 1
        sender.Text = rowNum
    End Sub

    Private Sub AgingDetailReport_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        Me.PrintingSystem.Document.AutoFitToPagesWidth = 1
        Me.PaperKind = Printing.PaperKind.A4
    End Sub

    Private Sub cellInvoiceNumber_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellInvoiceNumber.BeforePrint, cellGrandTotal.BeforePrint
        DirectCast(sender, XRTableCell).Tag = GetCurrentRow()
    End Sub
End Class