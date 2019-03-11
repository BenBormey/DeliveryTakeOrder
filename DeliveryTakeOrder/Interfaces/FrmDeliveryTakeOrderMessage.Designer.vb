<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDeliveryTakeOrderMessage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDeliveryTakeOrderMessage))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnFinish = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtRemark = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.PanelPONumber = New System.Windows.Forms.Panel()
        Me.TxtPONumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DTPDeliveryDate = New System.Windows.Forms.DateTimePicker()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Panel44.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelPONumber.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnFinish)
        Me.Panel44.Controls.Add(Me.BtnClose)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(5, 278)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel44.Size = New System.Drawing.Size(238, 35)
        Me.Panel44.TabIndex = 110
        '
        'BtnFinish
        '
        Me.BtnFinish.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFinish.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnFinish.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnFinish.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFinish.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.BtnFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFinish.Location = New System.Drawing.Point(3, 2)
        Me.BtnFinish.Name = "BtnFinish"
        Me.BtnFinish.Size = New System.Drawing.Size(141, 31)
        Me.BtnFinish.TabIndex = 10
        Me.BtnFinish.Text = "&Finish Take Order"
        Me.BtnFinish.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(144, 2)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(92, 31)
        Me.BtnClose.TabIndex = 9
        Me.BtnClose.Text = "&Cancel"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtRemark)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(5, 109)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(238, 169)
        Me.Panel1.TabIndex = 112
        '
        'TxtRemark
        '
        Me.TxtRemark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtRemark.Font = New System.Drawing.Font("Khmer OS Siemreap", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(2, 28)
        Me.TxtRemark.Multiline = True
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtRemark.Size = New System.Drawing.Size(234, 139)
        Me.TxtRemark.TabIndex = 113
        '
        'Label28
        '
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Location = New System.Drawing.Point(2, 2)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(234, 26)
        Me.Label28.TabIndex = 112
        Me.Label28.Text = "Enter the message to alert"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelPONumber
        '
        Me.PanelPONumber.Controls.Add(Me.TxtPONumber)
        Me.PanelPONumber.Controls.Add(Me.Label1)
        Me.PanelPONumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelPONumber.Location = New System.Drawing.Point(5, 57)
        Me.PanelPONumber.Name = "PanelPONumber"
        Me.PanelPONumber.Padding = New System.Windows.Forms.Padding(2)
        Me.PanelPONumber.Size = New System.Drawing.Size(238, 52)
        Me.PanelPONumber.TabIndex = 113
        '
        'TxtPONumber
        '
        Me.TxtPONumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtPONumber.Location = New System.Drawing.Point(2, 28)
        Me.TxtPONumber.Name = "TxtPONumber"
        Me.TxtPONumber.Size = New System.Drawing.Size(234, 20)
        Me.TxtPONumber.TabIndex = 113
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(2, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 26)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "P.O Number"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DTPDeliveryDate)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(5, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel2.Size = New System.Drawing.Size(238, 52)
        Me.Panel2.TabIndex = 114
        '
        'DTPDeliveryDate
        '
        Me.DTPDeliveryDate.CalendarFont = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDeliveryDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DTPDeliveryDate.CustomFormat = "dd-MMM-yyyy"
        Me.DTPDeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DTPDeliveryDate.Enabled = False
        Me.DTPDeliveryDate.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPDeliveryDate.Location = New System.Drawing.Point(2, 19)
        Me.DTPDeliveryDate.Name = "DTPDeliveryDate"
        Me.DTPDeliveryDate.Size = New System.Drawing.Size(234, 28)
        Me.DTPDeliveryDate.TabIndex = 114
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CheckBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBox1.ForeColor = System.Drawing.Color.Blue
        Me.CheckBox1.Location = New System.Drawing.Point(2, 2)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(234, 17)
        Me.CheckBox1.TabIndex = 113
        Me.CheckBox1.Text = "Delivery Date"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'FrmDeliveryTakeOrderMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 318)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelPONumber)
        Me.Controls.Add(Me.Panel44)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDeliveryTakeOrderMessage"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Remark"
        Me.Panel44.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.PanelPONumber.ResumeLayout(False)
        Me.PanelPONumber.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnFinish As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TxtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents PanelPONumber As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtPONumber As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DTPDeliveryDate As DateTimePicker
    Friend WithEvents CheckBox1 As CheckBox
End Class
