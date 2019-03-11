<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCheckExpiryDate
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCheckExpiryDate))
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.Expiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.Loading = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvShow.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Expiry, Me.Stock, Me.Status, Me.CurDate})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 5)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(323, 214)
        Me.DgvShow.TabIndex = 111
        '
        'Expiry
        '
        Me.Expiry.DataPropertyName = "Expiry"
        DataGridViewCellStyle1.Format = "dd-MMM-yyyy"
        Me.Expiry.DefaultCellStyle = DataGridViewCellStyle1
        Me.Expiry.HeaderText = "Expiry"
        Me.Expiry.Name = "Expiry"
        Me.Expiry.ReadOnly = True
        Me.Expiry.Width = 69
        '
        'Stock
        '
        Me.Stock.DataPropertyName = "Stock"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Format = "N0"
        Me.Stock.DefaultCellStyle = DataGridViewCellStyle2
        Me.Stock.HeaderText = "Stock"
        Me.Stock.Name = "Stock"
        Me.Stock.ReadOnly = True
        Me.Stock.Width = 66
        '
        'Status
        '
        Me.Status.DataPropertyName = "Status"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 69
        '
        'CurDate
        '
        Me.CurDate.DataPropertyName = "CurDate"
        Me.CurDate.HeaderText = "CurDate"
        Me.CurDate.Name = "CurDate"
        Me.CurDate.ReadOnly = True
        Me.CurDate.Visible = False
        Me.CurDate.Width = 79
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnOK)
        Me.Panel2.Controls.Add(Me.BtnExport)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 219)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel2.Size = New System.Drawing.Size(323, 36)
        Me.Panel2.TabIndex = 113
        '
        'BtnOK
        '
        Me.BtnOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnOK.Image = Global.DeliveryTakeOrder.My.Resources.Resources.OK
        Me.BtnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnOK.Location = New System.Drawing.Point(74, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(123, 32)
        Me.BtnOK.TabIndex = 113
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnExport
        '
        Me.BtnExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExport.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnExport.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExport.Image = Global.DeliveryTakeOrder.My.Resources.Resources.view_takeorder
        Me.BtnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExport.Location = New System.Drawing.Point(197, 2)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(124, 32)
        Me.BtnExport.TabIndex = 114
        Me.BtnExport.Text = "&Export"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'Loading
        '
        Me.Loading.Interval = 5
        '
        'FrmCheckExpiryDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 260)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmCheckExpiryDate"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Expiry Date"
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Loading As Timer
    Friend WithEvents Expiry As DataGridViewTextBoxColumn
    Friend WithEvents Stock As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents CurDate As DataGridViewTextBoxColumn
    Friend WithEvents BtnExport As Button
End Class
