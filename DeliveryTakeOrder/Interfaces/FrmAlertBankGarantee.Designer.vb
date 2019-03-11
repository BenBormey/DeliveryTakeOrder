<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAlertBankGarantee
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlertBankGarantee))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblMsg = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.CreditLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Expiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlertDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LblMsg)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(425, 51)
        Me.Panel1.TabIndex = 0
        '
        'LblMsg
        '
        Me.LblMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblMsg.Location = New System.Drawing.Point(38, 0)
        Me.LblMsg.Name = "LblMsg"
        Me.LblMsg.Size = New System.Drawing.Size(387, 51)
        Me.LblMsg.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = Global.DeliveryTakeOrder.My.Resources.Resources.alert1
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(38, 51)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvShow.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CreditLimit, Me.Expiry, Me.AlertDate, Me.CurDate})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 56)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(425, 163)
        Me.DgvShow.TabIndex = 111
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnOK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 219)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(155, 2, 155, 2)
        Me.Panel2.Size = New System.Drawing.Size(425, 36)
        Me.Panel2.TabIndex = 113
        '
        'BtnOK
        '
        Me.BtnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnOK.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnOK.Location = New System.Drawing.Point(155, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(115, 32)
        Me.BtnOK.TabIndex = 113
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'CreditLimit
        '
        Me.CreditLimit.DataPropertyName = "CreditLimit"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        Me.CreditLimit.DefaultCellStyle = DataGridViewCellStyle1
        Me.CreditLimit.HeaderText = "Credit Limit"
        Me.CreditLimit.Name = "CreditLimit"
        Me.CreditLimit.ReadOnly = True
        '
        'Expiry
        '
        Me.Expiry.DataPropertyName = "Expiry"
        DataGridViewCellStyle2.Format = "dd-MMM-yyyy"
        Me.Expiry.DefaultCellStyle = DataGridViewCellStyle2
        Me.Expiry.HeaderText = "Expiry"
        Me.Expiry.Name = "Expiry"
        Me.Expiry.ReadOnly = True
        '
        'AlertDate
        '
        Me.AlertDate.DataPropertyName = "AlertDate"
        DataGridViewCellStyle3.Format = "dd-MMM-yyyy"
        Me.AlertDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.AlertDate.HeaderText = "AlertDate"
        Me.AlertDate.Name = "AlertDate"
        Me.AlertDate.ReadOnly = True
        '
        'CurDate
        '
        Me.CurDate.DataPropertyName = "CurDate"
        Me.CurDate.HeaderText = "CurDate"
        Me.CurDate.Name = "CurDate"
        Me.CurDate.ReadOnly = True
        Me.CurDate.Visible = False
        '
        'FrmAlertBankGarantee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 260)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmAlertBankGarantee"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Garantee"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LblMsg As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents CreditLimit As DataGridViewTextBoxColumn
    Friend WithEvents Expiry As DataGridViewTextBoxColumn
    Friend WithEvents AlertDate As DataGridViewTextBoxColumn
    Friend WithEvents CurDate As DataGridViewTextBoxColumn
End Class
