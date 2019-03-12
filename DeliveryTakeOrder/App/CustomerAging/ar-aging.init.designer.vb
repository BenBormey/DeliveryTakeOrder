<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitARAgingForm
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.panLine = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.grpAging = New System.Windows.Forms.GroupBox()
        Me.chkExludeZero = New System.Windows.Forms.CheckBox()
        Me.chkE = New System.Windows.Forms.CheckBox()
        Me.chkD = New System.Windows.Forms.CheckBox()
        Me.chkB = New System.Windows.Forms.CheckBox()
        Me.chkC = New System.Windows.Forms.CheckBox()
        Me.chkA = New System.Windows.Forms.CheckBox()
        Me.chkCurrent = New System.Windows.Forms.CheckBox()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.cboSupplier = New System.Windows.Forms.ComboBox()
        Me.rbSupplier = New System.Windows.Forms.RadioButton()
        Me.rbTeam = New System.Windows.Forms.RadioButton()
        Me.cboTeam = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAging.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(78, 24)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(152, 24)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "AR Aging Report"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Logo
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(72, 72)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'panLine
        '
        Me.panLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panLine.BackColor = System.Drawing.SystemColors.Highlight
        Me.panLine.Location = New System.Drawing.Point(0, 71)
        Me.panLine.Name = "panLine"
        Me.panLine.Size = New System.Drawing.Size(440, 2)
        Me.panLine.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(210, 305)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(101, 30)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(53, 207)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "As of:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(92, 203)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(105, 20)
        Me.dtpStartDate.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(317, 305)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 30)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Customer:"
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(92, 122)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(313, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'grpAging
        '
        Me.grpAging.Controls.Add(Me.chkExludeZero)
        Me.grpAging.Controls.Add(Me.chkE)
        Me.grpAging.Controls.Add(Me.chkD)
        Me.grpAging.Controls.Add(Me.chkB)
        Me.grpAging.Controls.Add(Me.chkC)
        Me.grpAging.Controls.Add(Me.chkA)
        Me.grpAging.Controls.Add(Me.chkCurrent)
        Me.grpAging.Controls.Add(Me.chkSelectAll)
        Me.grpAging.Location = New System.Drawing.Point(24, 229)
        Me.grpAging.Name = "grpAging"
        Me.grpAging.Size = New System.Drawing.Size(391, 70)
        Me.grpAging.TabIndex = 22
        Me.grpAging.TabStop = False
        Me.grpAging.Text = "Aging"
        '
        'chkExludeZero
        '
        Me.chkExludeZero.AutoSize = True
        Me.chkExludeZero.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkExludeZero.Location = New System.Drawing.Point(81, 19)
        Me.chkExludeZero.Name = "chkExludeZero"
        Me.chkExludeZero.Size = New System.Drawing.Size(73, 17)
        Me.chkExludeZero.TabIndex = 7
        Me.chkExludeZero.Text = "Exclude 0"
        Me.chkExludeZero.UseVisualStyleBackColor = True
        Me.chkExludeZero.Visible = False
        '
        'chkE
        '
        Me.chkE.AutoSize = True
        Me.chkE.Checked = True
        Me.chkE.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkE.Enabled = False
        Me.chkE.Location = New System.Drawing.Point(338, 42)
        Me.chkE.Name = "chkE"
        Me.chkE.Size = New System.Drawing.Size(44, 17)
        Me.chkE.TabIndex = 6
        Me.chkE.Text = ">90"
        Me.chkE.UseVisualStyleBackColor = True
        '
        'chkD
        '
        Me.chkD.AutoSize = True
        Me.chkD.Checked = True
        Me.chkD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkD.Enabled = False
        Me.chkD.Location = New System.Drawing.Point(273, 42)
        Me.chkD.Name = "chkD"
        Me.chkD.Size = New System.Drawing.Size(53, 17)
        Me.chkD.TabIndex = 5
        Me.chkD.Text = "61-90"
        Me.chkD.UseVisualStyleBackColor = True
        '
        'chkB
        '
        Me.chkB.AutoSize = True
        Me.chkB.Checked = True
        Me.chkB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkB.Enabled = False
        Me.chkB.Location = New System.Drawing.Point(140, 42)
        Me.chkB.Name = "chkB"
        Me.chkB.Size = New System.Drawing.Size(56, 17)
        Me.chkB.TabIndex = 4
        Me.chkB.Text = "31-45 "
        Me.chkB.UseVisualStyleBackColor = True
        '
        'chkC
        '
        Me.chkC.AutoSize = True
        Me.chkC.Checked = True
        Me.chkC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkC.Enabled = False
        Me.chkC.Location = New System.Drawing.Point(208, 42)
        Me.chkC.Name = "chkC"
        Me.chkC.Size = New System.Drawing.Size(53, 17)
        Me.chkC.TabIndex = 3
        Me.chkC.Text = "46-60"
        Me.chkC.UseVisualStyleBackColor = True
        '
        'chkA
        '
        Me.chkA.AutoSize = True
        Me.chkA.Checked = True
        Me.chkA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkA.Enabled = False
        Me.chkA.Location = New System.Drawing.Point(81, 42)
        Me.chkA.Name = "chkA"
        Me.chkA.Size = New System.Drawing.Size(47, 17)
        Me.chkA.TabIndex = 2
        Me.chkA.Text = "1-30"
        Me.chkA.UseVisualStyleBackColor = True
        '
        'chkCurrent
        '
        Me.chkCurrent.AutoSize = True
        Me.chkCurrent.Checked = True
        Me.chkCurrent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCurrent.Enabled = False
        Me.chkCurrent.Location = New System.Drawing.Point(9, 42)
        Me.chkCurrent.Name = "chkCurrent"
        Me.chkCurrent.Size = New System.Drawing.Size(60, 17)
        Me.chkCurrent.TabIndex = 1
        Me.chkCurrent.Text = "Current"
        Me.chkCurrent.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Checked = True
        Me.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSelectAll.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.chkSelectAll.Location = New System.Drawing.Point(9, 19)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.chkSelectAll.TabIndex = 0
        Me.chkSelectAll.Text = "Select All"
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'cboSupplier
        '
        Me.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSupplier.FormattingEnabled = True
        Me.cboSupplier.Location = New System.Drawing.Point(92, 149)
        Me.cboSupplier.Name = "cboSupplier"
        Me.cboSupplier.Size = New System.Drawing.Size(313, 21)
        Me.cboSupplier.TabIndex = 20
        '
        'rbSupplier
        '
        Me.rbSupplier.Checked = True
        Me.rbSupplier.Location = New System.Drawing.Point(20, 150)
        Me.rbSupplier.Name = "rbSupplier"
        Me.rbSupplier.Size = New System.Drawing.Size(66, 17)
        Me.rbSupplier.TabIndex = 23
        Me.rbSupplier.TabStop = True
        Me.rbSupplier.Text = "Supplier:"
        Me.rbSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbSupplier.UseVisualStyleBackColor = True
        '
        'rbTeam
        '
        Me.rbTeam.Location = New System.Drawing.Point(20, 177)
        Me.rbTeam.Name = "rbTeam"
        Me.rbTeam.Size = New System.Drawing.Size(66, 17)
        Me.rbTeam.TabIndex = 25
        Me.rbTeam.Text = "Team:"
        Me.rbTeam.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbTeam.UseVisualStyleBackColor = True
        '
        'cboTeam
        '
        Me.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTeam.Enabled = False
        Me.cboTeam.FormattingEnabled = True
        Me.cboTeam.Location = New System.Drawing.Point(92, 176)
        Me.cboTeam.Name = "cboTeam"
        Me.cboTeam.Size = New System.Drawing.Size(313, 21)
        Me.cboTeam.TabIndex = 24
        '
        'InitARAgingForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(439, 346)
        Me.Controls.Add(Me.rbTeam)
        Me.Controls.Add(Me.cboTeam)
        Me.Controls.Add(Me.rbSupplier)
        Me.Controls.Add(Me.grpAging)
        Me.Controls.Add(Me.cboSupplier)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.panLine)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "InitARAgingForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Team Daily Report"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAging.ResumeLayout(False)
        Me.grpAging.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents panLine As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents grpAging As GroupBox
    Friend WithEvents chkE As CheckBox
    Friend WithEvents chkD As CheckBox
    Friend WithEvents chkB As CheckBox
    Friend WithEvents chkC As CheckBox
    Friend WithEvents chkA As CheckBox
    Friend WithEvents chkCurrent As CheckBox
    Friend WithEvents chkSelectAll As CheckBox
    Friend WithEvents chkExludeZero As CheckBox
    Friend WithEvents cboSupplier As ComboBox
    Friend WithEvents rbSupplier As RadioButton
    Friend WithEvents rbTeam As RadioButton
    Friend WithEvents cboTeam As ComboBox
End Class
