Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraTab

Public Class ARAgingForm
    Inherits PreviewForm
    Dim bsData As New BindingSource

    Dim generalCtrl As GeneralController

    Dim lsData As New List(Of ARAging)
    Dim xReport As AgingReport
    Friend WithEvents btnAudit As System.Windows.Forms.Button
    Friend WithEvents btnHome As System.Windows.Forms.Button
    Friend WithEvents btnSelectInvoice As System.Windows.Forms.Button
    Friend WithEvents btnSetRemark As Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents panBoth As System.Windows.Forms.Panel
    Friend WithEvents panCredit As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents panTerm As System.Windows.Forms.Panel

    Public Sub New()
        InitializeComponent()
        generalCtrl = New GeneralController

        Me.DueDate = AppSetting.GetCurrentServerDate

        panBoth.BackColor = ColorTranslator.FromHtml("#9A0089")
        panTerm.BackColor = ColorTranslator.FromHtml("#0099BC")
        panCredit.BackColor = ColorTranslator.FromHtml("#E81123")
    End Sub

    Private Sub AgingCallcardForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ARAgingForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.H Then
            GoHome()
        End If
    End Sub

    Private Sub ARAgingForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnAudit_Click(sender As Object, e As EventArgs) Handles btnAudit.Click
        Dim page As XtraTabPage = Me.tabMain.SelectedTabPage
        Dim bsDatasource As BindingSource = DirectCast(page.Controls(0), PreviewControl).Datasource
        NewAuditReport(bsDatasource, String.Format("Audit Form {0}", page.Text.Replace("Invoice", String.Empty)))
    End Sub

    'Private Sub btnAllInvoice_Click(sender As Object, e As EventArgs) Handles btnHome.Click
    '    Dim bs As New BindingSource(GetARAgingDetail(1, Integer.MaxValue, "CUS00000"), Nothing)
    '    NewAllDetailReport(bs, String.Format("All Invoices - {0} {1}", DrCustomer("SalesmanCode"), DrCustomer("SalesmanName")))
    'End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        GoHome()
    End Sub

    Private Sub btnSelectInvoice_Click(sender As Object, e As EventArgs) Handles btnSelectInvoice.Click
        Dim page As XtraTabPage = Me.tabMain.SelectedTabPage
        Dim bsDatasource As BindingSource = DirectCast(page.Controls(0), PreviewControl).Datasource
        Dim frmFilter As New AgingCallcardFilterForm()
        frmFilter.FillSortableList(Of ARAgingDetail)(bsDatasource)
        frmFilter.ShowDialog(Me)
        Dim lsTmp As List(Of ARAgingDetail) = frmFilter.GetDatasource(Of ARAgingDetail)()
        If frmFilter.IsCancel Then
            lsTmp.ForEach(Sub(x) x.IsChecked = True)
            Return
        End If

        Dim ls As List(Of ARAgingDetail) = lsTmp.Where(Function(x) x.IsChecked = True).Select(Function(y) y.Clone).Cast(Of ARAgingDetail).ToList
        Dim bs As New BindingSource(ls, Nothing)
        If page.Name.Contains("All Invoices") Then
            NewAllDetailReport(bs, String.Format("Filtered {0}", page.Text))
        Else
            NewDetailReport(bs, String.Format("Filtered {0}", page.Text))
        End If
    End Sub

    Private Sub cellAAClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 1, 7)
    End Sub

    Private Sub cellABClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 8, 15)
    End Sub

    Private Sub cellAClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 16, 30)
    End Sub

    Private Sub cellBClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 31, 45)
    End Sub

    Private Sub cellCClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 46, 60)
    End Sub
    Private Sub cellCurrentClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 0, 0)
    End Sub

    Private Sub cellCustomerClick(sender As Object, e As PreviewMouseEventArgs)
        Dim oAge As ARAging = e.Brick.Value
        Dim frm As New BankGuaranteeForm(oAge.CusNum, Me.DueDate)
        If frm.IsClose Then Return
        frm.ShowDialog(Me.MdiParent)
    End Sub

    Private Sub cellDClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 61, 90)
    End Sub

    Private Sub cellEClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 91, Integer.MaxValue)
    End Sub

    Private Sub cellTotalClick(sender As Object, e As PreviewMouseEventArgs)
        RenderDetailReport(e.Brick.Value, 1, Integer.MaxValue)
    End Sub

    Private Sub DoPageChanged(sender As Object, e As TabPageChangedEventArgs)
        If e.Page.Name.Contains("Invoice") Then
            'btnHome.Enabled = False
            btnSelectInvoice.Enabled = True
            If e.Page.Name.Contains("Filtered") Then
                btnAudit.Enabled = False
            Else
                btnAudit.Enabled = True
            End If
            btnSetRemark.Enabled = True
        Else
            btnSelectInvoice.Enabled = False
            btnAudit.Enabled = False
            btnSetRemark.Enabled = False
            'btnHome.Enabled = True
        End If
    End Sub

    Private Sub FillData()
        Dim sqlQuery As String = <SQl><![CDATA[
    -- {0}
    DECLARE @dueDate DATE
    = GETDATE(),
        @Query NVARCHAR(MAX) = N'',
        @StatementInv NVARCHAR(MAX) = N'',
        @SelectClause NVARCHAR(MAX) = N'
            SELECT  P.InvNumber,
                    P.PONumber,
                    P.ShipDate,
                    P.DelTo,
                    P.GrandTotal,
                    P.PAID,
                    P.CusNum,
                    D.SupNum,
                    D.DeltoId';

SELECT @Query += @SelectClause + N', N''' + D.TableName + N''' AS TableName, N''' + D.Division + N''' AS Division '
                 + N' FROM Stock.dbo.' + D.TableName + ' AS D INNER JOIN Stock.dbo.' + P.TableName
                 + ' AS P ON P.InvNumber = D.InvNumber UNION ALL'
FROM dbo.Sys_AllDetailTables AS D
    INNER JOIN dbo.Sys_AllPaymentTables AS P
        ON P.Paired = D.Paired
WHERE D.TypeValue = 1
      AND D.Division NOT IN ( N'Division8', N'Division8 VAT', N'Division9', N'Division9 VAT', N'Division11',
                              N'Division11 VAT'
                            );

SET @Query = LEFT(@Query, LEN(@Query) - LEN(' UNION ALL '));

SELECT @StatementInv += N'SELECT InvNumber , CusNum, CAST(DateRegister AS DATE) AS StatementDate,''' + Division
                        + ''' AS Division FROM Stock.dbo.' + TableName + N' UNION ALL '
FROM dbo.Sys_AllStatementTables
WHERE TypeValue = 1;

SET @StatementInv = LEFT(@StatementInv, LEN(@StatementInv) - LEN(' UNION ALL '));

SELECT @Query
    = N'
            DECLARE @dueDate DATE = ''' + REPLACE(CONVERT(NVARCHAR(20), @dueDate, 111), '/', '-') + ''';'
      + N'

;WITH vDelivery AS (' + @Query
      + N'),
     vARAging
AS (SELECT D.InvNumber,
           C.CusNum,
           C.CusName,
           CASE
               WHEN C.Status = ''Activate'' THEN
                   C.Terms
               ELSE
                   0
           END AS Terms,
           CASE
               WHEN C.Status = ''Activate'' THEN
                   C.CreditLimit
               ELSE
                   0
           END AS CreditLimit,
           DATEDIFF(DAY, D.ShipDate, @dueDate) AS DaysOver,
           D.GrandTotal AS GrandTotal,
           D.Division
    FROM vDelivery AS D
        LEFT OUTER JOIN Stock.dbo.TPRCustomer AS C
            ON C.CusNum = D.CusNum
        LEFT OUTER JOIN (' + @StatementInv
      + ') AS Stm
            ON Stm.InvNumber = D.InvNumber
               AND Stm.CusNum = C.CusNum
               AND Stm.Division = D.Division
    WHERE D.PAID <> N''PAID''
          AND CONVERT(DATE, D.ShipDate) <= @dueDate
          AND Stm.InvNumber IS NULL
          {2}
    GROUP BY C.CusNum,
             C.CusName,
             C.Terms,
             C.CreditLimit,
             DATEDIFF(DAY, D.ShipDate, @dueDate),
             D.InvNumber,
             D.GrandTotal,
             D.Division,
             C.Status),
     vARAging2
AS (SELECT vARAging.InvNumber,
           vARAging.CusNum,
           vARAging.CusName,
           vARAging.Terms,
           vARAging.CreditLimit,
           CASE
               WHEN vARAging.DaysOver = 0 THEN
                   vARAging.GrandTotal
           END AS [Current],
           CASE
               WHEN vARAging.DaysOver >= 1
                    AND vARAging.DaysOver <= 7 THEN
                   vARAging.GrandTotal
           END AS AA,
           CASE
               WHEN vARAging.DaysOver >= 8
                    AND vARAging.DaysOver <= 15 THEN
                   vARAging.GrandTotal
           END AS AB,
           CASE
               WHEN vARAging.DaysOver >= 16
                    AND vARAging.DaysOver <= 30 THEN
                   vARAging.GrandTotal
           END AS A,
           CASE
               WHEN vARAging.DaysOver >= 31
                    AND vARAging.DaysOver <= 45 THEN
                   vARAging.GrandTotal
           END AS B,
           CASE
               WHEN vARAging.DaysOver >= 46
                    AND vARAging.DaysOver <= 60 THEN
                   vARAging.GrandTotal
           END AS C,
           CASE
               WHEN vARAging.DaysOver >= 61
                    AND vARAging.DaysOver <= 90 THEN
                   vARAging.GrandTotal
           END AS D,
           CASE
               WHEN vARAging.DaysOver > 90 THEN
                   vARAging.GrandTotal
           END AS E,
           CASE
               WHEN vARAging.DaysOver >= 1
                    AND vARAging.DaysOver <= 7 THEN
                   vARAging.DaysOver
           END AS AA_Max,
           CASE
               WHEN vARAging.DaysOver >= 8
                    AND vARAging.DaysOver <= 18 THEN
                   vARAging.DaysOver
           END AS AB_Max,
           CASE
               WHEN vARAging.DaysOver >= 16
                    AND vARAging.DaysOver <= 30 THEN
                   vARAging.DaysOver
           END AS A_Max,
           CASE
               WHEN vARAging.DaysOver >= 31
                    AND vARAging.DaysOver <= 45 THEN
                   vARAging.DaysOver
           END AS B_Max,
           CASE
               WHEN vARAging.DaysOver >= 46
                    AND vARAging.DaysOver <= 60 THEN
                   vARAging.DaysOver
           END AS C_Max,
           CASE
               WHEN vARAging.DaysOver >= 61
                    AND vARAging.DaysOver <= 90 THEN
                   vARAging.DaysOver
           END AS D_Max,
           CASE
               WHEN vARAging.DaysOver > 90 THEN
                   vARAging.DaysOver
           END AS E_Max,
           vARAging.DaysOver
    FROM vARAging)'
      + N'
SELECT	vARAging2.CusNum,
        vARAging2.CusName,
        vARAging2.Terms,
        vARAging2.CreditLimit,
        SUM(ISNULL(vARAging2.[Current], 0)) AS [Current],
        SUM(ISNULL(vARAging2.AA, 0)) AS AA,
        SUM(ISNULL(vARAging2.AB, 0)) AS AB,
        SUM(ISNULL(vARAging2.A, 0)) AS A,
        SUM(ISNULL(vARAging2.B, 0)) AS B,
        SUM(ISNULL(vARAging2.C, 0)) AS C,
        SUM(ISNULL(vARAging2.D, 0)) AS D,
        SUM(ISNULL(vARAging2.E, 0)) AS E,
        CASE WHEN -1 = {3} THEN SUM(ISNULL(vARAging2.A, 0)) + SUM(ISNULL(vARAging2.AA, 0)) + SUM(ISNULL(vARAging2.AB, 0)) ELSE 0 END 
        + CASE WHEN -1 = {4} THEN SUM(ISNULL(vARAging2.B, 0)) ELSE 0 END 
        + CASE WHEN -1 = {5} THEN SUM(ISNULL(vARAging2.C, 0)) ELSE 0 END
        + CASE WHEN -1 = {6} THEN SUM(ISNULL(vARAging2.D, 0)) ELSE 0 END 
        + CASE WHEN -1 = {7} THEN SUM(ISNULL(vARAging2.E, 0)) ELSE 0 END AS Total,
        MAX(ISNULL(vARAging2.AA_Max, 0)) AS AA_Max,
        MAX(ISNULL(vARAging2.AB_Max, 0)) AS AB_Max,
        MAX(ISNULL(vARAging2.A_Max, 0)) AS A_Max,
        MAX(ISNULL(vARAging2.B_Max, 0)) AS B_Max,
        MAX(ISNULL(vARAging2.C_Max, 0)) AS C_Max,
        MAX(ISNULL(vARAging2.D_Max, 0)) AS D_Max,
        MAX(ISNULL(vARAging2.E_Max, 0)) AS E_Max,
        MAX(vARAging2.DaysOver) AS Total_Max
FROM vARAging2
WHERE (
          vARAging2.CusNum = N''{1}''
          OR N''CUS00000'' = N''{1}''
      )
GROUP BY vARAging2.CusName,
         vARAging2.CusNum,
         vARAging2.Terms,
         vARAging2.CreditLimit
ORDER BY vARAging2.CusName';

EXEC (@Query);
    ]]></SQl>

        sqlQuery = String.Format(sqlQuery,
                                 RMDB.SqlDate(Me.DueDate),
                                 Me.CusNum,
                                 String.Empty,
                                 CType(-1, Integer),
                                 CType(-1, Integer),
                                 CType(-1, Integer),
                                 CType(-1, Integer),
                                 CType(-1, Integer))

        lsData = db.GetDataTableToObject(Of ARAging)(sqlQuery)

        For Each oAR As ARAging In lsData
            oAR.Remarks = QueryRemark(oAR.CusNum)
        Next

        bsData.DataSource = lsData
    End Sub

    Private Function QueryRemark(pCusNum As String) As List(Of ARAgingRemark)
        Dim sqlQuery As String = <SQL><![CDATA[
SELECT D.RemarkId,
       D.CusNum,
       D.DateCheck,
       D.Remark,
       D.FollowUpDate,
       D.Terminal,
       D.CreatedAt,
       D.ModifiedAt,
	   I.InvNumber
FROM dbo.TblARAgingRemarkDetail AS D
    INNER JOIN dbo.TblARAgingRemarkInvoice AS I
        ON I.RemarkId = D.RemarkId
WHERE D.CusNum = '{0}';
]]></SQL>

        sqlQuery = String.Format(sqlQuery, pCusNum)
        Dim ls As List(Of ARAgingRemark) = db.GetDataTableToObject(Of ARAgingRemark)(sqlQuery)
        If ls.Count > 0 Then
            Dim lsDetail As List(Of ARAgingDetail) = GetARAgingDetail(1, Integer.MaxValue, pCusNum)
            Dim lsToRemove As List(Of ARAgingRemark)
            For Each o As ARAgingRemark In ls.ToList
                Dim obj = lsDetail.Where(Function(x) (x.CusNum.Equals(o.CusNum) And x.InvNumber.ToString.Equals(o.InvNumber)))
                If Not obj.Any Then
                    ls.Remove(o)
                End If
            Next
        End If
        ls = (From r As ARAgingRemark In ls
              Group By r.DateCheck, r.FollowUpDate, r.Remark Into Group
              Select New ARAgingRemark With {.DateCheck = DateCheck, .FollowUpDate = FollowUpDate, .Remark = Remark}
              ).ToList
        Return ls
    End Function

    Private Function GetARAgingDetail(pStartAge As Integer, pEndAge As Integer, pCusNum As String) As List(Of ARAgingDetail)

        Dim sqlQuery As String = <SQL><![CDATA[
-- {0}
DECLARE @dueDate DATE
    = GETDATE(),
        @Query NVARCHAR(MAX) = N'',
        @StatementInv NVARCHAR(MAX) = N'',
        @SelectClause NVARCHAR(MAX) = N'
            SELECT  P.InvNumber,
                    P.PONumber,
                    P.ShipDate,
                    P.DueDate,
                    P.DelTo,
                    P.GrandTotal,
                    P.PAID,
                    P.CusNum,
                    D.SupNum,
                    D.DeltoId';

SELECT @Query += @SelectClause + N', N''' + D.TableName + N''' AS TableName, N''' + D.Division + N''' AS Division '
                 + N' FROM Stock.dbo.' + D.TableName + ' AS D INNER JOIN Stock.dbo.' + P.TableName
                 + ' AS P ON P.InvNumber = D.InvNumber UNION ALL'
FROM dbo.Sys_AllDetailTables AS D
    INNER JOIN dbo.Sys_AllPaymentTables AS P
        ON P.Paired = D.Paired
WHERE D.TypeValue = 1
      AND D.Division NOT IN ( N'Division8', N'Division8 VAT', N'Division9', N'Division9 VAT', N'Division11',
                              N'Division11 VAT'
                            );

SET @Query = LEFT(@Query, LEN(@Query) - LEN(' UNION ALL '));

SELECT @StatementInv += N'SELECT InvNumber , CusNum, CAST(DateRegister AS DATE) AS StatementDate,''' + Division
                        + ''' AS Division FROM Stock.dbo.' + TableName + N' UNION ALL '
FROM dbo.Sys_AllStatementTables
WHERE TypeValue = 1;

SET @StatementInv = LEFT(@StatementInv, LEN(@StatementInv) - LEN(' UNION ALL '));

SELECT @Query
    = N'
            DECLARE @dueDate DATE = ''' + REPLACE(CONVERT(NVARCHAR(20), @dueDate, 111), '/', '-')
      + ''';            

;WITH vDelivery AS (' + @Query
      + N'),
     vARAging
AS (SELECT D.InvNumber,
           D.PONumber,
           D.ShipDate,
           D.DueDate,
           C.CusNum,
           C.CusName,
           D.DeltoId,
           T.DelTo,
           DATEDIFF(DAY, D.ShipDate, @dueDate) AS DaysOver,
           D.GrandTotal,
           D.Division
    FROM vDelivery AS D
        LEFT OUTER JOIN Stock.dbo.TPRCustomer AS C
            ON C.CusNum = D.CusNum
        LEFT OUTER JOIN Stock.dbo.TPRDelto AS T
            ON D.DeltoId = T.Id
        LEFT OUTER JOIN (' + @StatementInv
      + ') AS Stm
            ON Stm.InvNumber = D.InvNumber
               AND Stm.CusNum = C.CusNum
               AND Stm.Division = D.Division
    WHERE D.PAID <> N''PAID''
          AND CONVERT(DATE, D.ShipDate) <= @dueDate
          AND Stm.InvNumber IS NULL
          {4}
    GROUP BY D.InvNumber,
             D.PONumber,
             D.ShipDate,
             D.DueDate,
             C.CusNum,
             C.CusName,
             D.DeltoId,
             T.DelTo,
             DATEDIFF(DAY, D.ShipDate, @dueDate),
             D.GrandTotal,
             D.Division)
SELECT *
FROM vARAging
WHERE vARAging.DaysOver
      BETWEEN {1} AND {2}
      AND (vARAging.CusNum = N''{3}'' OR N''CUS00000'' = N''{3}'')
ORDER BY vARAging.DaysOver DESC, vARAging.InvNumber';

EXEC (@Query);

]]></SQL>

        Dim sqlFilterSupplier As String = String.Empty
        

        sqlQuery = String.Format(sqlQuery,
                                 RMDB.SqlDate(Me.DueDate),
                                 pStartAge,
                                 pEndAge,
                                 pCusNum,
                                 sqlFilterSupplier)
        Dim lsAgingDetail As List(Of ARAgingDetail) = db.GetDataTableToObject(Of ARAgingDetail)(sqlQuery)
        Return lsAgingDetail
    End Function

    Private Sub GoHome()
        'Me.Close()
        'Me.Dispose()
        'Dim frmInit As New InitARAgingForm
        'frmInit.ShowDialog(Me.MdiParent)
        'If frmInit.IsCancel Then Return
        'Dim frm As New ARAgingForm
        'With frm
        '    .MdiParent = Me.MdiParent
        '    '.DrCustomer = frmInit.DrCustomer
        '    .DueDate = frmInit.DueDate
        '    .SupNum = frmInit.SupNum
        '    .DrSupplier = frmInit.DrSupplier
        '    .IsAll = frmInit.IsAll
        '    .IsA = frmInit.IsA
        '    .IsB = frmInit.IsB
        '    .IsC = frmInit.IsC
        '    .IsD = frmInit.IsD
        '    .IsE = frmInit.IsE
        '    .IsExcludeZero = frmInit.IsExcludeZero
        '    .IsTeam = frmInit.IsTeam
        '    .DrTeam = frmInit.DrTeam
        '    .Show()
        'End With
    End Sub

    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnSetRemark = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.panBoth = New System.Windows.Forms.Panel()
        Me.panTerm = New System.Windows.Forms.Panel()
        Me.panCredit = New System.Windows.Forms.Panel()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.btnAudit = New System.Windows.Forms.Button()
        Me.btnSelectInvoice = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Panel1.Controls.Add(Me.btnSetRemark)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.panBoth)
        Me.Panel1.Controls.Add(Me.panTerm)
        Me.Panel1.Controls.Add(Me.panCredit)
        Me.Panel1.Controls.Add(Me.btnHome)
        Me.Panel1.Controls.Add(Me.btnAudit)
        Me.Panel1.Controls.Add(Me.btnSelectInvoice)
        Me.Panel1.Location = New System.Drawing.Point(255, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(740, 44)
        Me.Panel1.TabIndex = 3
        '
        'btnSetRemark
        '
        Me.btnSetRemark.Enabled = False
        Me.btnSetRemark.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSetRemark.Location = New System.Drawing.Point(424, 12)
        Me.btnSetRemark.Name = "btnSetRemark"
        Me.btnSetRemark.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.btnSetRemark.Size = New System.Drawing.Size(101, 27)
        Me.btnSetRemark.TabIndex = 11
        Me.btnSetRemark.Text = "Set Remark"
        Me.btnSetRemark.UseVisualStyleBackColor = True
        Me.btnSetRemark.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(634, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Over Term"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(634, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Over Term && Credit"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(634, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Over Credit"
        '
        'panBoth
        '
        Me.panBoth.Location = New System.Drawing.Point(608, 30)
        Me.panBoth.Name = "panBoth"
        Me.panBoth.Size = New System.Drawing.Size(20, 10)
        Me.panBoth.TabIndex = 7
        '
        'panTerm
        '
        Me.panTerm.Location = New System.Drawing.Point(608, 6)
        Me.panTerm.Name = "panTerm"
        Me.panTerm.Size = New System.Drawing.Size(20, 10)
        Me.panTerm.TabIndex = 8
        '
        'panCredit
        '
        Me.panCredit.Location = New System.Drawing.Point(608, 18)
        Me.panCredit.Name = "panCredit"
        Me.panCredit.Size = New System.Drawing.Size(20, 10)
        Me.panCredit.TabIndex = 7
        '
        'btnHome
        '
        Me.btnHome.Image = Global.DeliveryTakeOrder.My.Resources.Resources.house
        Me.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHome.Location = New System.Drawing.Point(153, 12)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnHome.Size = New System.Drawing.Size(125, 27)
        Me.btnHome.TabIndex = 6
        Me.btnHome.Text = "Home (Ctrl + H)"
        Me.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHome.UseVisualStyleBackColor = True
        Me.btnHome.Visible = False
        '
        'btnAudit
        '
        Me.btnAudit.Enabled = False
        Me.btnAudit.Image = Global.DeliveryTakeOrder.My.Resources.Resources.clipboard
        Me.btnAudit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAudit.Location = New System.Drawing.Point(284, 12)
        Me.btnAudit.Name = "btnAudit"
        Me.btnAudit.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.btnAudit.Size = New System.Drawing.Size(134, 27)
        Me.btnAudit.TabIndex = 5
        Me.btnAudit.Text = "View Audit Form"
        Me.btnAudit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAudit.UseVisualStyleBackColor = True
        Me.btnAudit.Visible = False
        '
        'btnSelectInvoice
        '
        Me.btnSelectInvoice.Enabled = False
        Me.btnSelectInvoice.Image = Global.DeliveryTakeOrder.My.Resources.Resources.list
        Me.btnSelectInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelectInvoice.Location = New System.Drawing.Point(3, 12)
        Me.btnSelectInvoice.Name = "btnSelectInvoice"
        Me.btnSelectInvoice.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.btnSelectInvoice.Size = New System.Drawing.Size(120, 27)
        Me.btnSelectInvoice.TabIndex = 4
        Me.btnSelectInvoice.Text = "Select Invoice"
        Me.btnSelectInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSelectInvoice.UseVisualStyleBackColor = True
        Me.btnSelectInvoice.Visible = False
        '
        'ARAgingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1004, 584)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "ARAgingForm"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private Sub LoadMainReport()
        xReport = New AgingReport With {.DataSource = bsData}
        AddHandler CType(Me.xReport, AgingReport).cellCurrent.PreviewClick, AddressOf cellCurrentClick
        AddHandler CType(Me.xReport, AgingReport).cellAA.PreviewClick, AddressOf cellAAClick
        AddHandler CType(Me.xReport, AgingReport).cellAB.PreviewClick, AddressOf cellABClick
        AddHandler CType(Me.xReport, AgingReport).cellA.PreviewClick, AddressOf cellAClick
        AddHandler CType(Me.xReport, AgingReport).cellB.PreviewClick, AddressOf cellBClick
        AddHandler CType(Me.xReport, AgingReport).cellC.PreviewClick, AddressOf cellCClick
        AddHandler CType(Me.xReport, AgingReport).cellD.PreviewClick, AddressOf cellDClick
        AddHandler CType(Me.xReport, AgingReport).cellE.PreviewClick, AddressOf cellEClick
        AddHandler CType(Me.xReport, AgingReport).cellTotal.PreviewClick, AddressOf cellTotalClick
        AddHandler CType(Me.xReport, AgingReport).cellCustomer.PreviewClick, AddressOf cellCustomerClick

        'xReport.cellA.Visible = False
        'xReport.cellAHead.Visible = False

        If Not IsAll Then
            'If Not IsCurrent Then
            '    xReport.xtblData.DeleteColumn(xReport.cellCurrent, True)
            '    xReport.xtblHeader.DeleteColumn(xReport.cellCurrentHead, True)
            'End If
            Dim lsTmp As New List(Of ARAging)
            If Not IsA Then
                xReport.xtblData.DeleteColumn(xReport.cellA, True)
                xReport.xtblHeader.DeleteColumn(xReport.cellAHead, True)
                xReport.xtblSum.DeleteColumn(xReport.cellASum, True)
            End If

            If Not IsB Then
                xReport.xtblData.DeleteColumn(xReport.cellB, True)
                xReport.xtblHeader.DeleteColumn(xReport.cellBHead, True)
                xReport.xtblSum.DeleteColumn(xReport.cellBSum, True)
            End If

            If Not IsC Then
                xReport.xtblData.DeleteColumn(xReport.cellC, True)
                xReport.xtblHeader.DeleteColumn(xReport.cellCHead, True)
                xReport.xtblSum.DeleteColumn(xReport.cellCSum, True)
            End If

            If Not IsD Then
                xReport.xtblData.DeleteColumn(xReport.cellD, True)
                xReport.xtblHeader.DeleteColumn(xReport.cellDHead, True)
                xReport.xtblSum.DeleteColumn(xReport.cellDSum, True)
            End If

            If Not IsE Then
                xReport.xtblData.DeleteColumn(xReport.cellE, True)
                xReport.xtblHeader.DeleteColumn(xReport.CellEHead, True)
                xReport.xtblSum.DeleteColumn(xReport.cellESum, True)
            End If

            'If IsExcludeZero Then
            '    Dim aa As Decimal = If(IsA, 0, -0.0001)
            '    Dim bb As Decimal = If(IsB, 0, -0.0001)
            '    Dim cc As Decimal = If(IsC, 0, -0.0001)
            '    Dim dd As Decimal = If(IsD, 0, -0.0001)
            '    Dim ee As Decimal = If(IsE, 0, -0.0001)

            '    lsData = lsData.Where(Function(x) x.A <> aa And x.B <> bb And x.C <> cc And x.D <> dd And x.E <> ee).ToList
            'End If
            lsData = lsData.Where(Function(x) x.Total <> 0).ToList
            bsData.DataSource = lsData
        End If


        Dim filterName As String = String.Empty

        'If IsTeam Then
        '    filterName = String.Format("Team: {0}", DrTeam("TeamName"))
        'Else
        '    If Not DrSupplier("SupNum").Equals("SUP00000") Then
        '        filterName = String.Format("Supplier: {0}", DrSupplier("SupName"))
        '    End If
        'End If


        ''AddHandler CType(Me.xReport, AgingCallcardReport).cellAmount.PreviewClick, AddressOf invoice_preview_click
        AddHandler Me.tabMain.SelectedPageChanged, AddressOf DoPageChanged
        Me.xReport.RequestParameters = False
        Me.xReport.paramAsOfDate.Value = Me.DueDate
        xReport.paramFilterName.Value = filterName
        pcMain.docViewer.PrintingSystem = xReport.PrintingSystem
        xReport.CreateDocument()
    End Sub

    Private Sub NewAllDetailReport(pBs As BindingSource, pText As String)
        Dim rpt As New AgingCallcardAllDetailReport With {.Name = String.Format("{0} {1}", Now.Ticks, pText)}

        rpt.DataSource = pBs
        rpt.paramAsOfDate.Value = Me.DueDate
        PreviewReport(rpt, pText)
    End Sub


    Private Sub NewAuditReport(pBs As BindingSource, pText As String)
        Dim rpt As New AgingCallcardAuditReport With {.Name = String.Format("{0} {1}", Now.Ticks, pText)}

        rpt.DataSource = pBs
        rpt.paramAsOfDate.Value = Me.DueDate
        PreviewReport(rpt, pText)
    End Sub

    Private Sub NewDetailReport(pBs As BindingSource, pText As String)
        Dim rpt As New AgingDetailReport With {.Name = String.Format("{0} {1}", Now.Ticks, pText)}

        rpt.DataSource = pBs
        rpt.paramAsOfDate.Value = Me.DueDate

        AddHandler rpt.cellInvoiceNumber.PreviewClick, AddressOf ShowInvoiceDetail
        AddHandler rpt.cellGrandTotal.PreviewClick, AddressOf ShowInvoiceDetail
        PreviewReport(rpt, pText)
    End Sub

    Private Sub PreviewReport(pReport As XtraReport, pText As String)
        Dim pc As New PreviewControl
        pc.docViewer.PrintingSystem = pReport.PrintingSystem
        pc.Datasource = pReport.DataSource
        pReport.CreateDocument(True)

        Dim page As New XtraTabPage With {.Name = pText, .Text = pText}
        page.Controls.Add(pc)

        Me.tabMain.TabPages.Add(page)
        page.Show()
    End Sub

    'Me.Controls.Add(Me.Panel1)
    '    Me.Name = "AgingCallcardForm"
    '    Me.Controls.SetChildIndex(Me.Panel1, 0)

    Private Sub RenderDetailReport(pObjectAging As ARAging, pStartAge As Integer, pEndAge As Integer)
        Dim oAge As ARAging = pObjectAging
        Dim bs As New BindingSource(GetARAgingDetail(pStartAge, pEndAge, oAge.CusNum), Nothing)
        Dim text As String = String.Format("Invoice - {0}", oAge.CusName)
        NewDetailReport(bs, text)
    End Sub

    Private Sub ShowInvoiceDetail(sender As Object, e As PreviewMouseEventArgs)
        Dim o As ARAgingDetail = e.Brick.Value
        Dim bs As New BindingSource(generalCtrl.GetInvoiceDetail(o.Division, o.InvNumber), Nothing)

        Dim txt As String = String.Format("Inv {0}", o.InvNumber)
        Dim rpt As New AgingInvoiceDetailReport With {.Name = String.Format("{0} {1}", Now.Ticks, txt)}
        rpt.DataSource = bs
        PreviewReport(rpt, txt)
    End Sub

    Public Overrides Sub LoadReport()
        FillData()
        If lsData.Count = 0 Then
            MessageBox.Show("No data to show!")
            Me.Close()
            Return
        End If
        LoadMainReport()
    End Sub

    Public Property CusNum As String
    'Public Property DrCustomer As DataRow
    'Public Property DrSupplier As DataRow
    Public Property DrTeam As DataRow
    Public Property DueDate As Date
    Public Property IsA As Boolean = True

    Public Property IsAll As Boolean = True
    Public Property IsB As Boolean = True
    Public Property IsC As Boolean = True
    Public Property IsCurrent As Boolean = True
    Public Property IsD As Boolean = True
    Public Property IsE As Boolean = True
    Public Property IsExcludeZero As Boolean
    Public Property IsIncludeOther As Boolean

    Public Property IsTeam As Boolean = False

    Public Property SupNum As String

    Private Sub btnSetRemark_Click(sender As Object, e As EventArgs) Handles btnSetRemark.Click
        'Dim page As XtraTabPage = Me.tabMain.SelectedTabPage
        'Dim bsDatasource As BindingSource = DirectCast(page.Controls(0), PreviewControl).Datasource
        'If bsDatasource.Count = 0 Then Return
        'Dim frmRemark As New SetARAgingRemarkForm(bsDatasource)
        'frmRemark.ShowDialog(Me)
    End Sub
End Class
