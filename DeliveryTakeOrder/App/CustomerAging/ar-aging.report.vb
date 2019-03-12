Imports System.Drawing.Printing
Imports DevExpress.XtraReports.UI

Public Class AgingReport
    Private Sub cellInvoice_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles _
        cellCurrent.BeforePrint,
        cellAA.BeforePrint,
        cellAB.BeforePrint,
        cellA.BeforePrint,
        cellB.BeforePrint,
        cellC.BeforePrint,
        cellD.BeforePrint,
        cellE.BeforePrint,
        cellCustomer.BeforePrint,
        cellTotal.BeforePrint

        Dim o As ARAging = GetCurrentRow()
        DirectCast(sender, XRTableCell).Tag = o

        If sender Is cellAA Then
            CType(sender, XRTableCell).ForeColor = o.AA_Color
        End If

        If sender Is cellAB Then
            CType(sender, XRTableCell).ForeColor = o.AB_Color
        End If

        If sender Is cellA Then
            CType(sender, XRTableCell).ForeColor = o.A_Color
        End If

        If sender Is cellB Then
            CType(sender, XRTableCell).ForeColor = o.B_Color
        End If

        If sender Is cellC Then
            CType(sender, XRTableCell).ForeColor = o.C_Color
        End If

        If sender Is cellD Then
            CType(sender, XRTableCell).ForeColor = o.D_Color
        End If

        If sender Is cellE Then
            CType(sender, XRTableCell).ForeColor = o.E_Color
        End If

        If sender Is cellTotal Then
            CType(sender, XRTableCell).ForeColor = o.Total_Color
        End If
    End Sub

    Private Sub cellInvoice_PreviewMouseMove(sender As Object, e As PreviewMouseEventArgs) Handles _
         cellCurrent.PreviewMouseMove,
        cellAA.PreviewMouseMove,
        cellAB.PreviewMouseMove,
        cellA.PreviewMouseMove,
        cellB.PreviewMouseMove,
        cellC.PreviewMouseMove,
        cellD.PreviewMouseMove,
        cellE.PreviewMouseMove,
        cellCustomer.PreviewMouseMove,
        cellTotal.PreviewMouseMove
        e.PreviewControl.Cursor = Cursors.Hand
    End Sub

    Dim rowNum As Integer = 0
    Private Sub AgingCallcardReport_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        Me.PrintingSystem.Document.AutoFitToPagesWidth = 1
        Me.PaperKind = Printing.PaperKind.A4
        Me.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Zoom, New Object() {0.95F})
        'Me.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToTextWidth)


    End Sub

    Private Sub cellRowNumber_BeforePrint(sender As Object, e As PrintEventArgs) Handles cellRowNumber.BeforePrint
        rowNum += 1
        cellRowNumber.Text = rowNum.ToString("N0")
    End Sub

    Private Sub cellTerm_BeforePrint(sender As Object, e As PrintEventArgs) Handles cellCredit.BeforePrint, cellTerm.BeforePrint
        If CType(CType(sender, XRTableCell).Text, Integer) = 0 Then
            CType(sender, XRTableCell).ForeColor = Color.Red
        Else
            CType(sender, XRTableCell).ForeColor = Color.Black
        End If
    End Sub
End Class