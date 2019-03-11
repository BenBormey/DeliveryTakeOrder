<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessTakeOrderPreviewNEdit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProcessTakeOrderPreviewNEdit))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.DisplayLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel44.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.LblCountRow)
        Me.Panel44.Controls.Add(Me.BtnClose)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(6, 518)
        Me.Panel44.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel44.Size = New System.Drawing.Size(772, 36)
        Me.Panel44.TabIndex = 110
        '
        'LblCountRow
        '
        Me.LblCountRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblCountRow.Location = New System.Drawing.Point(2, 3)
        Me.LblCountRow.Name = "LblCountRow"
        Me.LblCountRow.Size = New System.Drawing.Size(147, 30)
        Me.LblCountRow.TabIndex = 12
        Me.LblCountRow.Text = "Count Row : 0"
        Me.LblCountRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnClose
        '
        Me.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(678, 3)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(92, 30)
        Me.BtnClose.TabIndex = 10
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DgvShow.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DgvShow.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgvShow.ColumnHeadersHeight = 150
        Me.DgvShow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(6, 7)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(772, 511)
        Me.DgvShow.TabIndex = 112
        '
        'DisplayLoading
        '
        Me.DisplayLoading.Interval = 5
        '
        'FrmProcessTakeOrderPreviewNEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel44)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmProcessTakeOrderPreviewNEdit"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview & Edit Take Order"
        Me.Panel44.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents DisplayLoading As System.Windows.Forms.Timer
End Class
