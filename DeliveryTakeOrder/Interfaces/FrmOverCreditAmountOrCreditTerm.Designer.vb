<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOverCreditAmountOrCreditTerm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOverCreditAmountOrCreditTerm))
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusCom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrandTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Overday = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreditLimitAllow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxMonthAllow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Average = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.PicAlert = New System.Windows.Forms.PictureBox()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PicAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvShow.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CusNum, Me.CusCom, Me.GrandTotal, Me.Overday, Me.CreditLimitAllow, Me.MaxMonthAllow, Me.Average})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(43, 5)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(794, 326)
        Me.DgvShow.TabIndex = 111
        Me.DgvShow.Visible = False
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "Cus. #"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Width = 62
        '
        'CusCom
        '
        Me.CusCom.DataPropertyName = "CusCom"
        Me.CusCom.HeaderText = "Customer"
        Me.CusCom.Name = "CusCom"
        Me.CusCom.ReadOnly = True
        Me.CusCom.Width = 86
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
        Me.GrandTotal.Width = 92
        '
        'Overday
        '
        Me.Overday.DataPropertyName = "Overday"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Format = "N0"
        Me.Overday.DefaultCellStyle = DataGridViewCellStyle2
        Me.Overday.HeaderText = "Over Day"
        Me.Overday.Name = "Overday"
        Me.Overday.ReadOnly = True
        Me.Overday.Width = 79
        '
        'CreditLimitAllow
        '
        Me.CreditLimitAllow.DataPropertyName = "CreditLimitAllow"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        Me.CreditLimitAllow.DefaultCellStyle = DataGridViewCellStyle3
        Me.CreditLimitAllow.HeaderText = "Credit (Allow)"
        Me.CreditLimitAllow.Name = "CreditLimitAllow"
        Me.CreditLimitAllow.ReadOnly = True
        '
        'MaxMonthAllow
        '
        Me.MaxMonthAllow.DataPropertyName = "MaxMonthAllow"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Format = "N0"
        Me.MaxMonthAllow.DefaultCellStyle = DataGridViewCellStyle4
        Me.MaxMonthAllow.HeaderText = "Term (Allow)"
        Me.MaxMonthAllow.Name = "MaxMonthAllow"
        Me.MaxMonthAllow.ReadOnly = True
        Me.MaxMonthAllow.Width = 95
        '
        'Average
        '
        Me.Average.DataPropertyName = "Average"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "C2"
        Me.Average.DefaultCellStyle = DataGridViewCellStyle5
        Me.Average.HeaderText = "Average (Last 3 Months)"
        Me.Average.Name = "Average"
        Me.Average.ReadOnly = True
        Me.Average.Width = 114
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnExportToExcel)
        Me.Panel2.Controls.Add(Me.BtnOK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 331)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(832, 36)
        Me.Panel2.TabIndex = 113
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(696, 2)
        Me.BtnExportToExcel.Name = "BtnExportToExcel"
        Me.BtnExportToExcel.Size = New System.Drawing.Size(136, 32)
        Me.BtnExportToExcel.TabIndex = 114
        Me.BtnExportToExcel.Text = "&Export To Excel"
        Me.BtnExportToExcel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnOK.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnOK.Location = New System.Drawing.Point(340, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(115, 32)
        Me.BtnOK.TabIndex = 113
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'PicAlert
        '
        Me.PicAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicAlert.Image = Global.DeliveryTakeOrder.My.Resources.Resources.alert
        Me.PicAlert.Location = New System.Drawing.Point(5, 5)
        Me.PicAlert.Name = "PicAlert"
        Me.PicAlert.Size = New System.Drawing.Size(38, 326)
        Me.PicAlert.TabIndex = 114
        Me.PicAlert.TabStop = False
        '
        'FrmOverCreditAmountOrCreditTerm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 372)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.PicAlert)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmOverCreditAmountOrCreditTerm"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Credit Amount"
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PicAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents PicAlert As System.Windows.Forms.PictureBox
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents CusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusCom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrandTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Overday As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreditLimitAllow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxMonthAllow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Average As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
