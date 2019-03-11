<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPODutchmillSelected
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPODutchmillSelected))
        Me.RdbAllUnpaid = New System.Windows.Forms.RadioButton()
        Me.RdbAllTransaction = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DTPFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DTPTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RdbAllUnpaid
        '
        Me.RdbAllUnpaid.AutoSize = True
        Me.RdbAllUnpaid.Checked = True
        Me.RdbAllUnpaid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbAllUnpaid.Location = New System.Drawing.Point(41, 13)
        Me.RdbAllUnpaid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RdbAllUnpaid.Name = "RdbAllUnpaid"
        Me.RdbAllUnpaid.Size = New System.Drawing.Size(132, 23)
        Me.RdbAllUnpaid.TabIndex = 0
        Me.RdbAllUnpaid.TabStop = True
        Me.RdbAllUnpaid.Text = "All Not Yet Process"
        Me.RdbAllUnpaid.UseVisualStyleBackColor = True
        '
        'RdbAllTransaction
        '
        Me.RdbAllTransaction.AutoSize = True
        Me.RdbAllTransaction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdbAllTransaction.Location = New System.Drawing.Point(41, 44)
        Me.RdbAllTransaction.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RdbAllTransaction.Name = "RdbAllTransaction"
        Me.RdbAllTransaction.Size = New System.Drawing.Size(111, 23)
        Me.RdbAllTransaction.TabIndex = 1
        Me.RdbAllTransaction.Text = "All Transaction"
        Me.RdbAllTransaction.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.DTPFrom)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(59, 75)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(139, 50)
        Me.Panel3.TabIndex = 108
        '
        'DTPFrom
        '
        Me.DTPFrom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DTPFrom.CustomFormat = "dd-MMM-yyyy"
        Me.DTPFrom.Dock = System.Windows.Forms.DockStyle.Top
        Me.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPFrom.Location = New System.Drawing.Point(0, 19)
        Me.DTPFrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DTPFrom.MinDate = New Date(2017, 1, 1, 0, 0, 0, 0)
        Me.DTPFrom.Name = "DTPFrom"
        Me.DTPFrom.Size = New System.Drawing.Size(139, 28)
        Me.DTPFrom.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(139, 19)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "From :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DTPTo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(205, 75)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(139, 50)
        Me.Panel1.TabIndex = 109
        '
        'DTPTo
        '
        Me.DTPTo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DTPTo.CustomFormat = "dd-MMM-yyyy"
        Me.DTPTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPTo.Location = New System.Drawing.Point(0, 19)
        Me.DTPTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DTPTo.Name = "DTPTo"
        Me.DTPTo.Size = New System.Drawing.Size(139, 28)
        Me.DTPTo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "To :"
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCancel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.Location = New System.Drawing.Point(205, 130)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(139, 38)
        Me.BtnCancel.TabIndex = 115
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnPreview
        '
        Me.BtnPreview.BackColor = System.Drawing.SystemColors.Control
        Me.BtnPreview.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPreview.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnPreview.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPreview.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.BtnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnPreview.Location = New System.Drawing.Point(59, 130)
        Me.BtnPreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(139, 38)
        Me.BtnPreview.TabIndex = 116
        Me.BtnPreview.Text = "&Preview"
        Me.BtnPreview.UseVisualStyleBackColor = False
        '
        'FrmPODutchmillSelected
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 195)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnPreview)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.RdbAllTransaction)
        Me.Controls.Add(Me.RdbAllUnpaid)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "FrmPODutchmillSelected"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview"
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RdbAllUnpaid As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAllTransaction As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents DTPFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DTPTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnPreview As System.Windows.Forms.Button
End Class
