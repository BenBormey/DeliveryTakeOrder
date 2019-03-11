<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDeliveryTakeOrderInfoAeon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDeliveryTakeOrderInfoAeon))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnFinish = New System.Windows.Forms.Button()
        Me.Panel42 = New System.Windows.Forms.Panel()
        Me.TxtDocumentNumber = New System.Windows.Forms.TextBox()
        Me.Panel43 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtLineCode = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TxtDeptCode = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel44.SuspendLayout()
        Me.Panel42.SuspendLayout()
        Me.Panel43.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnFinish)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(5, 122)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel44.Size = New System.Drawing.Size(209, 35)
        Me.Panel44.TabIndex = 110
        '
        'BtnFinish
        '
        Me.BtnFinish.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFinish.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnFinish.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnFinish.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFinish.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.BtnFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFinish.Location = New System.Drawing.Point(2, 2)
        Me.BtnFinish.Name = "BtnFinish"
        Me.BtnFinish.Size = New System.Drawing.Size(205, 31)
        Me.BtnFinish.TabIndex = 10
        Me.BtnFinish.Text = "&Finish Take Order"
        Me.BtnFinish.UseVisualStyleBackColor = True
        '
        'Panel42
        '
        Me.Panel42.Controls.Add(Me.TxtDocumentNumber)
        Me.Panel42.Controls.Add(Me.Panel43)
        Me.Panel42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel42.Location = New System.Drawing.Point(5, 5)
        Me.Panel42.Name = "Panel42"
        Me.Panel42.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel42.Size = New System.Drawing.Size(209, 52)
        Me.Panel42.TabIndex = 111
        '
        'TxtDocumentNumber
        '
        Me.TxtDocumentNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtDocumentNumber.Location = New System.Drawing.Point(2, 30)
        Me.TxtDocumentNumber.MaxLength = 20
        Me.TxtDocumentNumber.Name = "TxtDocumentNumber"
        Me.TxtDocumentNumber.Size = New System.Drawing.Size(205, 20)
        Me.TxtDocumentNumber.TabIndex = 2
        Me.TxtDocumentNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel43
        '
        Me.Panel43.Controls.Add(Me.Label26)
        Me.Panel43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel43.Location = New System.Drawing.Point(2, 0)
        Me.Panel43.Name = "Panel43"
        Me.Panel43.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel43.Size = New System.Drawing.Size(205, 30)
        Me.Panel43.TabIndex = 1
        '
        'Label26
        '
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label26.Location = New System.Drawing.Point(0, 2)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(205, 26)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "Document Number"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtLineCode)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(5, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel1.Size = New System.Drawing.Size(105, 65)
        Me.Panel1.TabIndex = 112
        '
        'TxtLineCode
        '
        Me.TxtLineCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtLineCode.Location = New System.Drawing.Point(2, 30)
        Me.TxtLineCode.MaxLength = 14
        Me.TxtLineCode.Name = "TxtLineCode"
        Me.TxtLineCode.Size = New System.Drawing.Size(101, 20)
        Me.TxtLineCode.TabIndex = 2
        Me.TxtLineCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(2, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(101, 30)
        Me.Panel2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Line Code"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TxtDeptCode)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(110, 57)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel3.Size = New System.Drawing.Size(104, 65)
        Me.Panel3.TabIndex = 113
        '
        'TxtDeptCode
        '
        Me.TxtDeptCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtDeptCode.Location = New System.Drawing.Point(2, 30)
        Me.TxtDeptCode.MaxLength = 14
        Me.TxtDeptCode.Name = "TxtDeptCode"
        Me.TxtDeptCode.Size = New System.Drawing.Size(100, 20)
        Me.TxtDeptCode.TabIndex = 2
        Me.TxtDeptCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(2, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel4.Size = New System.Drawing.Size(100, 30)
        Me.Panel4.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 26)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Dept Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmDeliveryTakeOrderInfoAeon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(219, 162)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel42)
        Me.Controls.Add(Me.Panel44)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDeliveryTakeOrderInfoAeon"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Information For AEON"
        Me.Panel44.ResumeLayout(False)
        Me.Panel42.ResumeLayout(False)
        Me.Panel42.PerformLayout()
        Me.Panel43.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnFinish As System.Windows.Forms.Button
    Friend WithEvents Panel42 As System.Windows.Forms.Panel
    Friend WithEvents TxtDocumentNumber As System.Windows.Forms.TextBox
    Friend WithEvents Panel43 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TxtLineCode As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TxtDeptCode As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
