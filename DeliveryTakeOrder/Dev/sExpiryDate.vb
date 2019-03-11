Public Class sExpiryDate
    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim vstatus As String = Trim(IIf(DBNull.Value.Equals(Detail.Report.GetCurrentColumnValue("Status").ToString()) = True, "", Detail.Report.GetCurrentColumnValue("Status").ToString()))
        If vstatus.Trim().Equals("") = True Then
            XrLabel28.ForeColor = Color.Black
            XrLabel26.ForeColor = Color.Black
            XrLabel33.ForeColor = Color.Black
        Else
            XrLabel28.ForeColor = Color.Brown
            XrLabel26.ForeColor = Color.Brown
            XrLabel33.ForeColor = Color.Brown
        End If
    End Sub
End Class