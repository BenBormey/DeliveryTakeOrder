Public Class xOverCreditNOverTerm
    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        If (Detail.Report.RowCount <= 0) Then Exit Sub
        Dim vsaleman As String = Trim(IIf(DBNull.Value.Equals(Detail.Report.GetCurrentColumnValue("SalesmanName").ToString()) = True, "", Detail.Report.GetCurrentColumnValue("SalesmanName").ToString()))
        Dim vsalecode As String = Trim(IIf(DBNull.Value.Equals(Detail.Report.GetCurrentColumnValue("SalesmanNumber").ToString()) = True, "", Detail.Report.GetCurrentColumnValue("SalesmanNumber").ToString()))
        If vsaleman.Trim().Equals("") = True Then
            lblsaleman.Text = ""
        Else
            lblsaleman.Text = String.Format("{0} ({1})", vsaleman, vsalecode)
        End If

        Dim vDivision As String = Trim(IIf(DBNull.Value.Equals(Detail.Report.GetCurrentColumnValue("Division").ToString()) = True, "", Detail.Report.GetCurrentColumnValue("Division").ToString())).Replace("Division", "").Trim()
        lblDivision.Text = vDivision.Trim()
    End Sub

    Private oIndex As Decimal = 0
    Private oCusNum As String = ""
    Private Sub GroupHeader1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        If (GroupHeader1.Report.RowCount <= 0) Then Exit Sub
        Dim vCusNum As String = Trim(IIf(DBNull.Value.Equals(GroupHeader1.Report.GetCurrentColumnValue("CusNum").ToString()) = True, "", GroupHeader1.Report.GetCurrentColumnValue("CusNum").ToString()))
        If oCusNum.Trim().Equals(vCusNum) = False Then
            oCusNum = vCusNum
            oIndex += 1
        End If
        lblNo.Text = String.Format("{0:N0}", oIndex)
    End Sub
End Class