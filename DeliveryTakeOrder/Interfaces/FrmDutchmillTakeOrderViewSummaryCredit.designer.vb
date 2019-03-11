<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDutchmillTakeOrderViewSummaryCredit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDutchmillTakeOrderViewSummaryCredit))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DelTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrandTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreditLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreditLimitAllow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LatestInvoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OverCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DisplayLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel44.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.LblCountRow)
        Me.Panel44.Controls.Add(Me.BtnExportToExcel)
        Me.Panel44.Controls.Add(Me.BtnCancel)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(6, 388)
        Me.Panel44.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel44.Size = New System.Drawing.Size(845, 36)
        Me.Panel44.TabIndex = 110
        '
        'LblCountRow
        '
        Me.LblCountRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblCountRow.Location = New System.Drawing.Point(2, 3)
        Me.LblCountRow.Name = "LblCountRow"
        Me.LblCountRow.Size = New System.Drawing.Size(122, 30)
        Me.LblCountRow.TabIndex = 12
        Me.LblCountRow.Text = "Count Row : 0"
        Me.LblCountRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(566, 3)
        Me.BtnExportToExcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnExportToExcel.Name = "BtnExportToExcel"
        Me.BtnExportToExcel.Size = New System.Drawing.Size(184, 30)
        Me.BtnExportToExcel.TabIndex = 10
        Me.BtnExportToExcel.Text = "&Export To Excel"
        Me.BtnExportToExcel.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCancel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.Location = New System.Drawing.Point(750, 3)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(93, 30)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CusNum, Me.CusName, Me.DelTo, Me.GrandTotal, Me.CreditLimit, Me.CreditLimitAllow, Me.LatestInvoice, Me.OverCredit})
        Me.DgvShow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(6, 7)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(845, 381)
        Me.DgvShow.TabIndex = 112
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "Customer #"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Visible = False
        Me.CusNum.Width = 106
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "Customer Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        Me.CusName.Width = 105
        '
        'DelTo
        '
        Me.DelTo.DataPropertyName = "DelTo"
        Me.DelTo.HeaderText = "DelTo"
        Me.DelTo.Name = "DelTo"
        Me.DelTo.ReadOnly = True
        Me.DelTo.Width = 106
        '
        'GrandTotal
        '
        Me.GrandTotal.DataPropertyName = "GrandTotal"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "C2"
        Me.GrandTotal.DefaultCellStyle = DataGridViewCellStyle1
        Me.GrandTotal.HeaderText = "Grand Total"
        Me.GrandTotal.Name = "GrandTotal"
        Me.GrandTotal.ReadOnly = True
        Me.GrandTotal.Width = 106
        '
        'CreditLimit
        '
        Me.CreditLimit.DataPropertyName = "CreditLimit"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "C2"
        Me.CreditLimit.DefaultCellStyle = DataGridViewCellStyle2
        Me.CreditLimit.HeaderText = "Credit Limit"
        Me.CreditLimit.Name = "CreditLimit"
        Me.CreditLimit.ReadOnly = True
        '
        'CreditLimitAllow
        '
        Me.CreditLimitAllow.DataPropertyName = "CreditLimitAllow"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        Me.CreditLimitAllow.DefaultCellStyle = DataGridViewCellStyle3
        Me.CreditLimitAllow.HeaderText = "Credit Allow"
        Me.CreditLimitAllow.Name = "CreditLimitAllow"
        Me.CreditLimitAllow.ReadOnly = True
        '
        'LatestInvoice
        '
        Me.LatestInvoice.DataPropertyName = "LatestInvoice"
        Me.LatestInvoice.HeaderText = "Latest Invoice"
        Me.LatestInvoice.Name = "LatestInvoice"
        Me.LatestInvoice.ReadOnly = True
        '
        'OverCredit
        '
        Me.OverCredit.DataPropertyName = "OverCredit"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        Me.OverCredit.DefaultCellStyle = DataGridViewCellStyle4
        Me.OverCredit.HeaderText = "Over Credit"
        Me.OverCredit.Name = "OverCredit"
        Me.OverCredit.ReadOnly = True
        '
        'DisplayLoading
        '
        Me.DisplayLoading.Interval = 5
        '
        'FrmDutchmillTakeOrderViewSummaryCredit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(857, 431)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel44)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmDutchmillTakeOrderViewSummaryCredit"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Over Credit ~ Customer List"
        Me.Panel44.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents DisplayLoading As System.Windows.Forms.Timer
    Friend WithEvents CusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DelTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrandTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreditLimit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreditLimitAllow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LatestInvoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OverCredit As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
