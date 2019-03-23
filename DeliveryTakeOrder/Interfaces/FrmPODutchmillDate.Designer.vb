<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPODutchmillDate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPODutchmillDate))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CmbRequiredDate = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ChkLock = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CmbPlanningOrder = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.planningloading = New System.Windows.Forms.Timer(Me.components)
        Me.requireddateloading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel44.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CmbRequiredDate)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 70)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(244, 60)
        Me.Panel2.TabIndex = 3
        '
        'CmbRequiredDate
        '
        Me.CmbRequiredDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbRequiredDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbRequiredDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbRequiredDate.FormattingEnabled = True
        Me.CmbRequiredDate.Location = New System.Drawing.Point(0, 30)
        Me.CmbRequiredDate.Name = "CmbRequiredDate"
        Me.CmbRequiredDate.Size = New System.Drawing.Size(244, 27)
        Me.CmbRequiredDate.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(0, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(244, 28)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Required Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnExportToExcel)
        Me.Panel44.Controls.Add(Me.BtnCancel)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel44.Location = New System.Drawing.Point(10, 159)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel44.Size = New System.Drawing.Size(244, 35)
        Me.Panel44.TabIndex = 111
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(2, 2)
        Me.BtnExportToExcel.Name = "BtnExportToExcel"
        Me.BtnExportToExcel.Size = New System.Drawing.Size(158, 31)
        Me.BtnExportToExcel.TabIndex = 12
        Me.BtnExportToExcel.Text = "&Export To Excel"
        Me.BtnExportToExcel.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCancel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.Location = New System.Drawing.Point(160, 2)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(82, 31)
        Me.BtnCancel.TabIndex = 9
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ChkLock)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 130)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(244, 29)
        Me.Panel1.TabIndex = 112
        '
        'ChkLock
        '
        Me.ChkLock.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkLock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChkLock.ForeColor = System.Drawing.Color.Maroon
        Me.ChkLock.Location = New System.Drawing.Point(2, 2)
        Me.ChkLock.Name = "ChkLock"
        Me.ChkLock.Size = New System.Drawing.Size(240, 25)
        Me.ChkLock.TabIndex = 0
        Me.ChkLock.Text = "Lock Take Order"
        Me.ChkLock.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CmbPlanningOrder)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 10)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel3.Size = New System.Drawing.Size(244, 60)
        Me.Panel3.TabIndex = 113
        '
        'CmbPlanningOrder
        '
        Me.CmbPlanningOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbPlanningOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbPlanningOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPlanningOrder.FormattingEnabled = True
        Me.CmbPlanningOrder.Location = New System.Drawing.Point(0, 30)
        Me.CmbPlanningOrder.Name = "CmbPlanningOrder"
        Me.CmbPlanningOrder.Size = New System.Drawing.Size(244, 27)
        Me.CmbPlanningOrder.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(244, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Planning Order"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'planningloading
        '
        Me.planningloading.Interval = 5
        '
        'requireddateloading
        '
        Me.requireddateloading.Interval = 5
        '
        'FrmPODutchmillDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 205)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel44)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmPODutchmillDate"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Date"
        Me.Panel2.ResumeLayout(False)
        Me.Panel44.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CmbRequiredDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ChkLock As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents CmbPlanningOrder As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents planningloading As Timer
    Friend WithEvents requireddateloading As Timer
End Class
