﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDutchmillTakeOrderCustomer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDutchmillTakeOrderCustomer))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Panel42 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TxtCusName = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TxtCusNum = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CmbBillTo = New System.Windows.Forms.ComboBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BillToLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel44.SuspendLayout()
        Me.Panel42.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnUpdate)
        Me.Panel44.Controls.Add(Me.BtnCancel)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(6, 119)
        Me.Panel44.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel44.Size = New System.Drawing.Size(333, 36)
        Me.Panel44.TabIndex = 110
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnUpdate.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnUpdate.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdate.Location = New System.Drawing.Point(109, 3)
        Me.BtnUpdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(129, 30)
        Me.BtnUpdate.TabIndex = 10
        Me.BtnUpdate.Text = "&Update"
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
        Me.BtnCancel.Location = New System.Drawing.Point(238, 3)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(93, 30)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Panel42
        '
        Me.Panel42.Controls.Add(Me.Panel5)
        Me.Panel42.Controls.Add(Me.Panel3)
        Me.Panel42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel42.Location = New System.Drawing.Point(6, 7)
        Me.Panel42.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel42.Name = "Panel42"
        Me.Panel42.Size = New System.Drawing.Size(333, 54)
        Me.Panel42.TabIndex = 111
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.TxtCusName)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(109, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel5.Size = New System.Drawing.Size(224, 54)
        Me.Panel5.TabIndex = 114
        '
        'TxtCusName
        '
        Me.TxtCusName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCusName.Location = New System.Drawing.Point(2, 25)
        Me.TxtCusName.Name = "TxtCusName"
        Me.TxtCusName.ReadOnly = True
        Me.TxtCusName.Size = New System.Drawing.Size(220, 28)
        Me.TxtCusName.TabIndex = 115
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(2, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(220, 25)
        Me.Panel6.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(0, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 19)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Customer Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TxtCusNum)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel3.Size = New System.Drawing.Size(109, 54)
        Me.Panel3.TabIndex = 113
        '
        'TxtCusNum
        '
        Me.TxtCusNum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCusNum.Location = New System.Drawing.Point(2, 25)
        Me.TxtCusNum.Name = "TxtCusNum"
        Me.TxtCusNum.ReadOnly = True
        Me.TxtCusNum.Size = New System.Drawing.Size(105, 28)
        Me.TxtCusNum.TabIndex = 115
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(2, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(105, 25)
        Me.Panel4.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Customer #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(6, 61)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(333, 54)
        Me.Panel1.TabIndex = 112
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CmbBillTo)
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel2.Size = New System.Drawing.Size(333, 54)
        Me.Panel2.TabIndex = 114
        '
        'CmbBillTo
        '
        Me.CmbBillTo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbBillTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmbBillTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBillTo.FormattingEnabled = True
        Me.CmbBillTo.Location = New System.Drawing.Point(2, 25)
        Me.CmbBillTo.Name = "CmbBillTo"
        Me.CmbBillTo.Size = New System.Drawing.Size(329, 27)
        Me.CmbBillTo.TabIndex = 6
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(2, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel7.Size = New System.Drawing.Size(329, 25)
        Me.Panel7.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(329, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BillToLoading
        '
        Me.BillToLoading.Interval = 5
        '
        'FrmDutchmillTakeOrderCustomer
        '
        Me.AcceptButton = Me.BtnUpdate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(345, 162)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel42)
        Me.Controls.Add(Me.Panel44)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmDutchmillTakeOrderCustomer"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Customer"
        Me.Panel44.ResumeLayout(False)
        Me.Panel42.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents Panel42 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents TxtCusName As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtCusNum As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbBillTo As System.Windows.Forms.ComboBox
    Friend WithEvents BillToLoading As System.Windows.Forms.Timer
End Class
