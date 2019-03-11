<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAlertBadPayment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlertBadPayment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMsg = New System.Windows.Forms.Label()
        Me.PicAlert = New System.Windows.Forms.PictureBox()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnNo = New System.Windows.Forms.Button()
        Me.BtnYes = New System.Windows.Forms.Button()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlertDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BlockDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PicAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LblMsg)
        Me.Panel1.Controls.Add(Me.PicAlert)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 98)
        Me.Panel1.TabIndex = 0
        '
        'LblMsg
        '
        Me.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMsg.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblMsg.Location = New System.Drawing.Point(38, 0)
        Me.LblMsg.Name = "LblMsg"
        Me.LblMsg.Padding = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.LblMsg.Size = New System.Drawing.Size(758, 98)
        Me.LblMsg.TabIndex = 0
        Me.LblMsg.Text = resources.GetString("LblMsg.Text")
        '
        'PicAlert
        '
        Me.PicAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicAlert.Image = Global.DeliveryTakeOrder.My.Resources.Resources.alert
        Me.PicAlert.Location = New System.Drawing.Point(0, 0)
        Me.PicAlert.Name = "PicAlert"
        Me.PicAlert.Size = New System.Drawing.Size(38, 98)
        Me.PicAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicAlert.TabIndex = 1
        Me.PicAlert.TabStop = False
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvShow.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.CusNum, Me.CusName, Me.Remark, Me.AlertDate, Me.BlockDate, Me.CreatedDate, Me.Status})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 103)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(796, 201)
        Me.DgvShow.TabIndex = 111
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnExit)
        Me.Panel2.Controls.Add(Me.BtnNo)
        Me.Panel2.Controls.Add(Me.BtnYes)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 304)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(796, 36)
        Me.Panel2.TabIndex = 113
        '
        'BtnNo
        '
        Me.BtnNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnNo.DialogResult = System.Windows.Forms.DialogResult.No
        Me.BtnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnNo.Image = Global.DeliveryTakeOrder.My.Resources.Resources.minus_16
        Me.BtnNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNo.Location = New System.Drawing.Point(401, 2)
        Me.BtnNo.Name = "BtnNo"
        Me.BtnNo.Size = New System.Drawing.Size(108, 32)
        Me.BtnNo.TabIndex = 115
        Me.BtnNo.Text = "&No"
        Me.BtnNo.UseVisualStyleBackColor = True
        '
        'BtnYes
        '
        Me.BtnYes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.BtnYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnYes.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnYes.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnYes.Location = New System.Drawing.Point(287, 2)
        Me.BtnYes.Name = "BtnYes"
        Me.BtnYes.Size = New System.Drawing.Size(108, 32)
        Me.BtnYes.TabIndex = 114
        Me.BtnYes.Text = "&Yes"
        Me.BtnYes.UseVisualStyleBackColor = True
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "CusNum"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Width = 79
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "CusName"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        Me.CusName.Width = 86
        '
        'Remark
        '
        Me.Remark.DataPropertyName = "Remark"
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        Me.Remark.Width = 76
        '
        'AlertDate
        '
        Me.AlertDate.DataPropertyName = "AlertDate"
        DataGridViewCellStyle1.Format = "dd-MMM-yy"
        Me.AlertDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.AlertDate.HeaderText = "Alert Date"
        Me.AlertDate.Name = "AlertDate"
        Me.AlertDate.ReadOnly = True
        Me.AlertDate.Width = 83
        '
        'BlockDate
        '
        Me.BlockDate.DataPropertyName = "BlockDate"
        DataGridViewCellStyle2.Format = "dd-MMM-yy"
        Me.BlockDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.BlockDate.HeaderText = "Block Date"
        Me.BlockDate.Name = "BlockDate"
        Me.BlockDate.ReadOnly = True
        Me.BlockDate.Width = 87
        '
        'CreatedDate
        '
        Me.CreatedDate.DataPropertyName = "CreatedDate"
        Me.CreatedDate.HeaderText = "CreatedDate"
        Me.CreatedDate.Name = "CreatedDate"
        Me.CreatedDate.ReadOnly = True
        Me.CreatedDate.Visible = False
        '
        'Status
        '
        Me.Status.DataPropertyName = "Status"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 69
        '
        'BtnExit
        '
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnExit.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExit.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExit.Location = New System.Drawing.Point(696, 2)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(100, 32)
        Me.BtnExit.TabIndex = 116
        Me.BtnExit.Text = "&Cancel"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'FrmAlertBadPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 345)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmAlertBadPayment"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bad Payment"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PicAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblMsg As System.Windows.Forms.Label
    Friend WithEvents PicAlert As System.Windows.Forms.PictureBox
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnNo As System.Windows.Forms.Button
    Friend WithEvents BtnYes As System.Windows.Forms.Button
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents CusNum As DataGridViewTextBoxColumn
    Friend WithEvents CusName As DataGridViewTextBoxColumn
    Friend WithEvents Remark As DataGridViewTextBoxColumn
    Friend WithEvents AlertDate As DataGridViewTextBoxColumn
    Friend WithEvents BlockDate As DataGridViewTextBoxColumn
    Friend WithEvents CreatedDate As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents BtnExit As Button
End Class
