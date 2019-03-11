<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAlertCreditAmount
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlertCreditAmount))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMsg = New System.Windows.Forms.Label()
        Me.PicAlert = New System.Windows.Forms.PictureBox()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.InvNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PONumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DelTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShipDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrandTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnNo = New System.Windows.Forms.Button()
        Me.BtnYes = New System.Windows.Forms.Button()
        Me.lblbankgarantee = New System.Windows.Forms.Label()
        Me.loading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PicAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LblMsg)
        Me.Panel1.Controls.Add(Me.lblbankgarantee)
        Me.Panel1.Controls.Add(Me.PicAlert)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(571, 87)
        Me.Panel1.TabIndex = 0
        '
        'LblMsg
        '
        Me.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblMsg.Location = New System.Drawing.Point(38, 0)
        Me.LblMsg.Name = "LblMsg"
        Me.LblMsg.Size = New System.Drawing.Size(533, 62)
        Me.LblMsg.TabIndex = 0
        '
        'PicAlert
        '
        Me.PicAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicAlert.Image = Global.DeliveryTakeOrder.My.Resources.Resources.alert
        Me.PicAlert.Location = New System.Drawing.Point(0, 0)
        Me.PicAlert.Name = "PicAlert"
        Me.PicAlert.Size = New System.Drawing.Size(38, 87)
        Me.PicAlert.TabIndex = 1
        Me.PicAlert.TabStop = False
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvShow.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InvNumber, Me.PONumber, Me.CusNum, Me.CusName, Me.DelTo, Me.ShipDate, Me.GrandTotal})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 92)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(571, 268)
        Me.DgvShow.TabIndex = 111
        '
        'InvNumber
        '
        Me.InvNumber.DataPropertyName = "InvNumber"
        Me.InvNumber.HeaderText = "Invoice #"
        Me.InvNumber.Name = "InvNumber"
        Me.InvNumber.ReadOnly = True
        '
        'PONumber
        '
        Me.PONumber.DataPropertyName = "PONumber"
        Me.PONumber.HeaderText = "P.O #"
        Me.PONumber.Name = "PONumber"
        Me.PONumber.ReadOnly = True
        Me.PONumber.Visible = False
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "Cus. #"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "Customer Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        '
        'DelTo
        '
        Me.DelTo.DataPropertyName = "DelTo"
        Me.DelTo.HeaderText = "DelTo"
        Me.DelTo.Name = "DelTo"
        Me.DelTo.ReadOnly = True
        '
        'ShipDate
        '
        Me.ShipDate.DataPropertyName = "ShipDate"
        DataGridViewCellStyle1.Format = "dd-MMM-yyyy"
        Me.ShipDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.ShipDate.HeaderText = "Ship Date"
        Me.ShipDate.Name = "ShipDate"
        Me.ShipDate.ReadOnly = True
        '
        'GrandTotal
        '
        Me.GrandTotal.DataPropertyName = "GrandTotal"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        Me.GrandTotal.DefaultCellStyle = DataGridViewCellStyle2
        Me.GrandTotal.HeaderText = "Amount"
        Me.GrandTotal.Name = "GrandTotal"
        Me.GrandTotal.ReadOnly = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnOK)
        Me.Panel2.Controls.Add(Me.BtnNo)
        Me.Panel2.Controls.Add(Me.BtnYes)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 360)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(571, 36)
        Me.Panel2.TabIndex = 113
        '
        'BtnOK
        '
        Me.BtnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnOK.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnOK.Location = New System.Drawing.Point(228, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(115, 32)
        Me.BtnOK.TabIndex = 113
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        Me.BtnOK.Visible = False
        '
        'BtnNo
        '
        Me.BtnNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnNo.DialogResult = System.Windows.Forms.DialogResult.No
        Me.BtnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnNo.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNo.Location = New System.Drawing.Point(288, 2)
        Me.BtnNo.Name = "BtnNo"
        Me.BtnNo.Size = New System.Drawing.Size(115, 32)
        Me.BtnNo.TabIndex = 115
        Me.BtnNo.Text = "&No"
        Me.BtnNo.UseVisualStyleBackColor = True
        Me.BtnNo.Visible = False
        '
        'BtnYes
        '
        Me.BtnYes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.BtnYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnYes.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnYes.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnYes.Location = New System.Drawing.Point(167, 2)
        Me.BtnYes.Name = "BtnYes"
        Me.BtnYes.Size = New System.Drawing.Size(115, 32)
        Me.BtnYes.TabIndex = 114
        Me.BtnYes.Text = "&Yes"
        Me.BtnYes.UseVisualStyleBackColor = True
        Me.BtnYes.Visible = False
        '
        'lblbankgarantee
        '
        Me.lblbankgarantee.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblbankgarantee.ForeColor = System.Drawing.Color.Blue
        Me.lblbankgarantee.Location = New System.Drawing.Point(38, 62)
        Me.lblbankgarantee.Name = "lblbankgarantee"
        Me.lblbankgarantee.Size = New System.Drawing.Size(533, 25)
        Me.lblbankgarantee.TabIndex = 2
        Me.lblbankgarantee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'loading
        '
        Me.loading.Interval = 5
        '
        'FrmAlertCreditAmount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 401)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmAlertCreditAmount"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Credit Amount"
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
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnNo As System.Windows.Forms.Button
    Friend WithEvents BtnYes As System.Windows.Forms.Button
    Friend WithEvents InvNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PONumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DelTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShipDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrandTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblbankgarantee As Label
    Friend WithEvents loading As Timer
End Class
