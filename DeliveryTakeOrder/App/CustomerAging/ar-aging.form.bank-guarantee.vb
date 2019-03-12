Public Class BankGuaranteeForm
    Dim db As RMDB

    Dim cusNum As String
    Public Property IsClose As Boolean = False
    Public Sub New(pCusNum As String, pDueDate As Date)
        InitializeComponent()

        db = New RMDB(AppSetting.ConnectionString)
        Dim dt As DataTable = GetData(pCusNum)
        If dt.Rows.Count = 0 Then
            MessageBox.Show("No bank guarantee.")
            Me.IsClose = True
            Me.Close()
            Return
        End If
        lblCustomer.Text = dt.Rows(0)("CusName")
        lblCredit.Text = CType(dt.Rows(0)("CreditLimit"), Double).ToString("N2")
        lblAlertDate.Text = CType(dt.Rows(0)("AlertDate"), Date).ToString("yyyy-MM-dd")
        Dim expDate As Date = dt.Rows(0)("Expiry")
        Dim dayLeft As Integer = DateDiff(DateInterval.Day, pDueDate, expDate)
        Dim dayLeftText As String = String.Format("({0} days left)", dayLeft)
        If dayLeft <= 0 Then
            dayLeftText = "(Expired)"
            lblExpiryDate.ForeColor = Color.IndianRed
        End If
        lblExpiryDate.Text = String.Format("{0}   {1}", expDate.ToString("yyyy-MM-dd"), dayLeftText)
    End Sub

    Private Function GetData(pCusNum As String) As DataTable
        Dim sqlQuery As String = <SQL><![CDATA[
SELECT Id,
       CusId,
       CusName,
       CreditLimit,
       Expiry,
       AlertDate
FROM Stock.dbo.TPRCustomerBankGarantee
WHERE CusId = N'{0}';
]]></SQL>

        sqlQuery = String.Format(sqlQuery, pCusNum)
        Return db.GetDataTable(sqlQuery)
    End Function
End Class