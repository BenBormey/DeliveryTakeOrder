<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDutchmillTakeOrderPONumber
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDutchmillTakeOrderPONumber))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.PanelPO = New System.Windows.Forms.Panel()
        Me.TxtPONo = New System.Windows.Forms.TextBox()
        Me.PicRefreshPO = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TxtOrderDate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DTPRequiredDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel44.SuspendLayout()
        Me.PanelPO.SuspendLayout()
        CType(Me.PicRefreshPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnUpdate)
        Me.Panel44.Controls.Add(Me.BtnCancel)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(6, 115)
        Me.Panel44.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel44.Size = New System.Drawing.Size(282, 36)
        Me.Panel44.TabIndex = 110
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnUpdate.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnUpdate.Image = Global.DeliveryTakeOrder.My.Resources.Resources.renew
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdate.Location = New System.Drawing.Point(100, 3)
        Me.BtnUpdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(96, 30)
        Me.BtnUpdate.TabIndex = 10
        Me.BtnUpdate.Text = "&Go Ahead"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCancel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.Location = New System.Drawing.Point(196, 3)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 30)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'PanelPO
        '
        Me.PanelPO.Controls.Add(Me.TxtPONo)
        Me.PanelPO.Controls.Add(Me.PicRefreshPO)
        Me.PanelPO.Controls.Add(Me.Label7)
        Me.PanelPO.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPO.Location = New System.Drawing.Point(6, 7)
        Me.PanelPO.Name = "PanelPO"
        Me.PanelPO.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.PanelPO.Size = New System.Drawing.Size(282, 32)
        Me.PanelPO.TabIndex = 111
        '
        'TxtPONo
        '
        Me.TxtPONo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPONo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtPONo.Location = New System.Drawing.Point(100, 2)
        Me.TxtPONo.Name = "TxtPONo"
        Me.TxtPONo.ReadOnly = True
        Me.TxtPONo.Size = New System.Drawing.Size(162, 28)
        Me.TxtPONo.TabIndex = 1
        '
        'PicRefreshPO
        '
        Me.PicRefreshPO.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicRefreshPO.Dock = System.Windows.Forms.DockStyle.Right
        Me.PicRefreshPO.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.PicRefreshPO.Location = New System.Drawing.Point(262, 2)
        Me.PicRefreshPO.Name = "PicRefreshPO"
        Me.PicRefreshPO.Size = New System.Drawing.Size(20, 28)
        Me.PicRefreshPO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicRefreshPO.TabIndex = 4
        Me.PicRefreshPO.TabStop = False
        Me.PicRefreshPO.Visible = False
        '
        'Label7
        '
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 28)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "P.O #"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TxtOrderDate)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(6, 39)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel3.Size = New System.Drawing.Size(282, 32)
        Me.Panel3.TabIndex = 112
        '
        'TxtOrderDate
        '
        Me.TxtOrderDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtOrderDate.Location = New System.Drawing.Point(100, 2)
        Me.TxtOrderDate.Name = "TxtOrderDate"
        Me.TxtOrderDate.ReadOnly = True
        Me.TxtOrderDate.Size = New System.Drawing.Size(182, 28)
        Me.TxtOrderDate.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 28)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Order Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DTPRequiredDate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(6, 71)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel1.Size = New System.Drawing.Size(282, 32)
        Me.Panel1.TabIndex = 113
        '
        'DTPRequiredDate
        '
        Me.DTPRequiredDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DTPRequiredDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPRequiredDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DTPRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPRequiredDate.Location = New System.Drawing.Point(100, 2)
        Me.DTPRequiredDate.Name = "DTPRequiredDate"
        Me.DTPRequiredDate.Size = New System.Drawing.Size(182, 28)
        Me.DTPRequiredDate.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(0, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 28)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Required Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmDutchmillTakeOrderPONumber
        '
        Me.AcceptButton = Me.BtnUpdate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(294, 158)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PanelPO)
        Me.Controls.Add(Me.Panel44)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmDutchmillTakeOrderPONumber"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Required Date"
        Me.Panel44.ResumeLayout(False)
        Me.PanelPO.ResumeLayout(False)
        Me.PanelPO.PerformLayout()
        CType(Me.PicRefreshPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents PanelPO As System.Windows.Forms.Panel
    Friend WithEvents PicRefreshPO As System.Windows.Forms.PictureBox
    Friend WithEvents TxtPONo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TxtOrderDate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPRequiredDate As System.Windows.Forms.DateTimePicker
End Class
