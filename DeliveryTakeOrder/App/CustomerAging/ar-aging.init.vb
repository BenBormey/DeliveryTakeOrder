
Public Class InitARAgingForm

    Dim db As New RMDB

    '    Dim dtCustomer As DataTable
    '    Dim bsCustomer As BindingSource

    Dim bsSupplier As BindingSource
    Dim bsTeam As BindingSource
    Dim bsCustomer As BindingSource


#Region "Properties"

    Public Property IsCancel As Boolean = True
    Public Property DrCustomer As DataRow
    Public Property DueDate As Date
    Public Property DrSupplier As DataRow
    Public Property SupNum As String

    Public Property IsAll As Boolean
    Public Property IsCurrent As Boolean
    Public Property IsA As Boolean
    Public Property IsB As Boolean
    Public Property IsC As Boolean
    Public Property IsD As Boolean
    Public Property IsE As Boolean
    Public Property IsExcludeZero As Boolean


    Public Property IsTeam As Boolean = False
    Public Property DrTeam As DataRow

    '    Public Property DrCustomer As DataRow
    '    Public Property Status As Integer
#End Region

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.IsCancel = False

        Me.DrCustomer = bsCustomer.Current.Row
        Me.DueDate = dtpStartDate.Value
        DrSupplier = bsSupplier.Current.Row
        SupNum = GetSupplierSelected()

        IsAll = chkSelectAll.Checked
        IsCurrent = chkCurrent.Checked
        IsA = chkA.Checked
        IsB = chkB.Checked
        IsC = chkC.Checked
        IsD = chkD.Checked
        IsE = chkE.Checked
        IsExcludeZero = chkExludeZero.Checked

        IsTeam = rbTeam.Checked
        DrTeam = bsTeam.Current.Row
        Me.Close()
    End Sub

    Private Sub InitDailyCallcard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db = New RMDB(AppSetting.ConnectionString)
        LoadCustomerCombo()
        LoadSupplierCombo()
        LoadTeamCombo()
    End Sub

    Private Sub LoadCustomerCombo()
        Dim sqlDivision As String = <SQL><![CDATA[
SELECT ID,
       TableName,
       Division
FROM dbo.Sys_AllDetailTables
WHERE Division NOT IN ( N'Division8', N'Division8 VAT', N'Division9', N'Division9 VAT', N'Division11',
                        N'Division11 VAT'
                      )
ORDER BY TableName;
]]></SQL>

        Dim dtDivision As DataTable = db.GetDataTable(sqlDivision)

        Dim sqlTmp As String = <SQL><![CDATA[
UNION ALL
SELECT  C.CusNum ,
        C.CusName
FROM    [Stock].[dbo].[{0}] AS D
        INNER JOIN [Stock].dbo.TPRCustomer AS C ON C.CusNum = D.CusNum
GROUP BY C.CusNum ,
        C.CusName
]]></SQL>

        Dim sqlCus As String = String.Empty
        For Each DRow As DataRow In dtDivision.Rows
            sqlCus &= String.Format(sqlTmp, Trim(IIf(IsDBNull(DRow("TableName")) = True, String.Empty, DRow("TableName"))))
        Next


        Dim sqlQuery As String = <SQL><![CDATA[
;With vCustomer AS(
SELECT  N'CUS00000' AS CusNum ,
        N'(Select all)' AS CusName
{0}
)SELECT * FROM vCustomer GROUP BY vCustomer.CusNum, vCustomer.CusName ORDER BY vCustomer.CusName
]]></SQL>

        sqlQuery = String.Format(sqlQuery, sqlCus)
        BindSalesmanCombo(sqlQuery)
    End Sub

    Private Sub LoadSupplierCombo()
        Dim sqlQuery As String = <SQL><![CDATA[
        SELECT N'SUP00000' AS SupNum,
               N'(Select All)' AS SupName
        UNION ALL
        SELECT SupNum,
               SupName
        FROM dbo.TblSetSaleManagerToSupplier
        ORDER BY SupName;
        ]]></SQL>

        Dim dt As DataTable = db.GetDataTable(sqlQuery)
        bsSupplier = New BindingSource(dt, Nothing)
        BindCombo(cboSupplier, bsSupplier, "SupNum", "SupName")
    End Sub

    Private Sub LoadTeamCombo()
        Dim sqlQuery As String = <SQL><![CDATA[
        --SELECT 0 AS TeamID,
        --       N'(Select All)' AS TeamName
        --UNION ALL
        SELECT DISTINCT
               TeamID,
               TeamName
        FROM dbo.TblSetSaleManagerToSupplier
        WHERE TeamID IS NOT NULL
        ORDER BY TeamName;
        ]]></SQL>

        Dim dt As DataTable = db.GetDataTable(sqlQuery)
        bsTeam = New BindingSource(dt, Nothing)
        BindCombo(cboTeam, bsTeam, "TeamID", "TeamName")
    End Sub


    Private Function GetSupplierSelected() As String
        Dim drSup As DataRow = bsSupplier.Current.Row

        Return String.Format("N'{0}'", drSup("SupNum"))
    End Function

    Private Sub BindSalesmanCombo(pSqlQuery As String)
        If AppSetting.DtArCustomer Is Nothing Then AppSetting.DtArCustomer = db.GetDataTable(pSqlQuery)
        bsCustomer = New BindingSource(AppSetting.DtArCustomer, Nothing)
        BindCombo(cboCustomer, bsCustomer, "CusNum", "CusName", True)
    End Sub

    Private Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelectAll.CheckedChanged
        For Each ctrl As Control In grpAging.Controls
            If TypeOf (ctrl) Is CheckBox Then
                Dim chk As CheckBox = ctrl
                If Not (chk.Name.Equals("chkSelectAll") Or chk.Name.Equals("chkExludeZero") Or chk.Name.Equals(String.Empty)) Then
                    If chkSelectAll.Checked Then
                        chk.Enabled = False
                    Else
                        chk.Enabled = True
                    End If
                    chk.Checked = chkSelectAll.Checked
                End If
            End If
        Next

        chkCurrent.Enabled = False
        chkCurrent.Checked = True
    End Sub

    Private Sub rbTeam_CheckedChanged(sender As Object, e As EventArgs) Handles rbTeam.CheckedChanged
        If rbTeam.Checked Then
            cboSupplier.Enabled = False
            cboTeam.Enabled = True
        Else
            cboSupplier.Enabled = True
            cboTeam.Enabled = False
        End If

        bsTeam.Position = 0
        bsSupplier.Position = 0
    End Sub
End Class