<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAlertCustomerHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAlertCustomerHistory))
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.InvNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShipDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusCom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Division = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnOK = New System.Windows.Forms.Button()
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
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InvNumber, Me.ShipDate, Me.CusNum, Me.CusCom, Me.Division})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 5)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(583, 214)
        Me.DgvShow.TabIndex = 111
        '
        'InvNumber
        '
        Me.InvNumber.DataPropertyName = "InvNumber"
        Me.InvNumber.HeaderText = "Invoice #"
        Me.InvNumber.Name = "InvNumber"
        Me.InvNumber.ReadOnly = True
        Me.InvNumber.Width = 84
        '
        'ShipDate
        '
        Me.ShipDate.DataPropertyName = "ShipDate"
        DataGridViewCellStyle1.Format = "dd-MMM-yyyy"
        Me.ShipDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.ShipDate.HeaderText = "Ship Date"
        Me.ShipDate.Name = "ShipDate"
        Me.ShipDate.ReadOnly = True
        Me.ShipDate.Width = 87
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "Customer #"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Width = 95
        '
        'CusCom
        '
        Me.CusCom.DataPropertyName = "CusCom"
        Me.CusCom.HeaderText = "Customer Name"
        Me.CusCom.Name = "CusCom"
        Me.CusCom.ReadOnly = True
        Me.CusCom.Width = 120
        '
        'Division
        '
        Me.Division.DataPropertyName = "Division"
        Me.Division.HeaderText = "Division"
        Me.Division.Name = "Division"
        Me.Division.ReadOnly = True
        Me.Division.Width = 78
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnOK)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(5, 219)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(200, 2, 200, 2)
        Me.Panel2.Size = New System.Drawing.Size(583, 36)
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
        Me.BtnOK.Location = New System.Drawing.Point(200, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(183, 32)
        Me.BtnOK.TabIndex = 113
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'Loading
        '
        Me.Loading.Interval = 5
        '
        'FrmAlertCustomerHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 260)
        Me.ControlBox = False
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmAlertCustomerHistory"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "History Customer Delivery"
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents Loading As System.Windows.Forms.Timer
    Friend WithEvents InvNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShipDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusCom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Division As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
