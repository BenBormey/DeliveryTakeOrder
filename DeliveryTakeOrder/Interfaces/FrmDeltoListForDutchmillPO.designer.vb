<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDeltoListForDutchmillPO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDeltoListForDutchmillPO))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PicLogo = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblCompanyName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.CmbDelto = New System.Windows.Forms.ComboBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtId = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ChkTuesday = New System.Windows.Forms.CheckBox()
        Me.ChkMonday = New System.Windows.Forms.CheckBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.ChkThursday = New System.Windows.Forms.CheckBox()
        Me.ChkWednesday = New System.Windows.Forms.CheckBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.ChkSaturday = New System.Windows.Forms.CheckBox()
        Me.ChkFriday = New System.Windows.Forms.CheckBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.ChkSunday = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.TimerDeltoLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeltoId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Tuesday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Wednesday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Thursday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Friday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Saturday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Sunday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CreatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimerDisplayLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel44.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.PicLogo)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.LblCompanyName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(803, 103)
        Me.Panel1.TabIndex = 108
        '
        'PicLogo
        '
        Me.PicLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicLogo.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Logo
        Me.PicLogo.Location = New System.Drawing.Point(0, 0)
        Me.PicLogo.Name = "PicLogo"
        Me.PicLogo.Size = New System.Drawing.Size(94, 101)
        Me.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicLogo.TabIndex = 3
        Me.PicLogo.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 101)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(803, 2)
        Me.Panel4.TabIndex = 7
        '
        'LblCompanyName
        '
        Me.LblCompanyName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompanyName.ForeColor = System.Drawing.Color.Blue
        Me.LblCompanyName.Location = New System.Drawing.Point(104, 46)
        Me.LblCompanyName.Name = "LblCompanyName"
        Me.LblCompanyName.Size = New System.Drawing.Size(272, 43)
        Me.LblCompanyName.TabIndex = 6
        Me.LblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(100, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Q's MANAGEMENT SYSTEM"
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnExportToExcel)
        Me.Panel44.Controls.Add(Me.LblCountRow)
        Me.Panel44.Controls.Add(Me.BtnClose)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(5, 466)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel44.Size = New System.Drawing.Size(803, 35)
        Me.Panel44.TabIndex = 111
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(572, 2)
        Me.BtnExportToExcel.Name = "BtnExportToExcel"
        Me.BtnExportToExcel.Size = New System.Drawing.Size(137, 31)
        Me.BtnExportToExcel.TabIndex = 12
        Me.BtnExportToExcel.Text = "&Export To Excel"
        Me.BtnExportToExcel.UseVisualStyleBackColor = True
        '
        'LblCountRow
        '
        Me.LblCountRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblCountRow.Location = New System.Drawing.Point(2, 2)
        Me.LblCountRow.Name = "LblCountRow"
        Me.LblCountRow.Size = New System.Drawing.Size(147, 31)
        Me.LblCountRow.TabIndex = 11
        Me.LblCountRow.Text = "Count Row : 0"
        Me.LblCountRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnClose
        '
        Me.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(709, 2)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(92, 31)
        Me.BtnClose.TabIndex = 9
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Controls.Add(Me.Panel8)
        Me.Panel5.Controls.Add(Me.Panel9)
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(5, 108)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel5.Size = New System.Drawing.Size(803, 60)
        Me.Panel5.TabIndex = 112
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.CmbDelto)
        Me.Panel6.Controls.Add(Me.Panel3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 2, 2, 2)
        Me.Panel6.Size = New System.Drawing.Size(316, 56)
        Me.Panel6.TabIndex = 3
        '
        'CmbDelto
        '
        Me.CmbDelto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDelto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbDelto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDelto.FormattingEnabled = True
        Me.CmbDelto.Location = New System.Drawing.Point(0, 27)
        Me.CmbDelto.Name = "CmbDelto"
        Me.CmbDelto.Size = New System.Drawing.Size(314, 27)
        Me.CmbDelto.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.TxtId)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(314, 25)
        Me.Panel3.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(228, 25)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Delto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtId
        '
        Me.TxtId.Dock = System.Windows.Forms.DockStyle.Right
        Me.TxtId.Location = New System.Drawing.Point(228, 0)
        Me.TxtId.Name = "TxtId"
        Me.TxtId.Size = New System.Drawing.Size(86, 28)
        Me.TxtId.TabIndex = 2
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.ChkTuesday)
        Me.Panel7.Controls.Add(Me.ChkMonday)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(316, 2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel7.Size = New System.Drawing.Size(100, 56)
        Me.Panel7.TabIndex = 4
        '
        'ChkTuesday
        '
        Me.ChkTuesday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkTuesday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkTuesday.ForeColor = System.Drawing.Color.Blue
        Me.ChkTuesday.Location = New System.Drawing.Point(2, 27)
        Me.ChkTuesday.Name = "ChkTuesday"
        Me.ChkTuesday.Size = New System.Drawing.Size(96, 25)
        Me.ChkTuesday.TabIndex = 1
        Me.ChkTuesday.Text = "Tuesday"
        Me.ChkTuesday.UseVisualStyleBackColor = True
        '
        'ChkMonday
        '
        Me.ChkMonday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkMonday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkMonday.ForeColor = System.Drawing.Color.Blue
        Me.ChkMonday.Location = New System.Drawing.Point(2, 2)
        Me.ChkMonday.Name = "ChkMonday"
        Me.ChkMonday.Size = New System.Drawing.Size(96, 25)
        Me.ChkMonday.TabIndex = 0
        Me.ChkMonday.Text = "Monday"
        Me.ChkMonday.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.ChkThursday)
        Me.Panel8.Controls.Add(Me.ChkWednesday)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(416, 2)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel8.Size = New System.Drawing.Size(100, 56)
        Me.Panel8.TabIndex = 5
        '
        'ChkThursday
        '
        Me.ChkThursday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkThursday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkThursday.ForeColor = System.Drawing.Color.Blue
        Me.ChkThursday.Location = New System.Drawing.Point(2, 27)
        Me.ChkThursday.Name = "ChkThursday"
        Me.ChkThursday.Size = New System.Drawing.Size(96, 25)
        Me.ChkThursday.TabIndex = 1
        Me.ChkThursday.Text = "Thursday"
        Me.ChkThursday.UseVisualStyleBackColor = True
        '
        'ChkWednesday
        '
        Me.ChkWednesday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkWednesday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkWednesday.ForeColor = System.Drawing.Color.Blue
        Me.ChkWednesday.Location = New System.Drawing.Point(2, 2)
        Me.ChkWednesday.Name = "ChkWednesday"
        Me.ChkWednesday.Size = New System.Drawing.Size(96, 25)
        Me.ChkWednesday.TabIndex = 0
        Me.ChkWednesday.Text = "Wednesday"
        Me.ChkWednesday.UseVisualStyleBackColor = True
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.ChkSaturday)
        Me.Panel9.Controls.Add(Me.ChkFriday)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(516, 2)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel9.Size = New System.Drawing.Size(100, 56)
        Me.Panel9.TabIndex = 6
        '
        'ChkSaturday
        '
        Me.ChkSaturday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkSaturday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkSaturday.ForeColor = System.Drawing.Color.Blue
        Me.ChkSaturday.Location = New System.Drawing.Point(2, 27)
        Me.ChkSaturday.Name = "ChkSaturday"
        Me.ChkSaturday.Size = New System.Drawing.Size(96, 25)
        Me.ChkSaturday.TabIndex = 1
        Me.ChkSaturday.Text = "Saturday"
        Me.ChkSaturday.UseVisualStyleBackColor = True
        '
        'ChkFriday
        '
        Me.ChkFriday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkFriday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkFriday.ForeColor = System.Drawing.Color.Blue
        Me.ChkFriday.Location = New System.Drawing.Point(2, 2)
        Me.ChkFriday.Name = "ChkFriday"
        Me.ChkFriday.Size = New System.Drawing.Size(96, 25)
        Me.ChkFriday.TabIndex = 0
        Me.ChkFriday.Text = "Friday"
        Me.ChkFriday.UseVisualStyleBackColor = True
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.ChkSunday)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel10.Location = New System.Drawing.Point(616, 2)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel10.Size = New System.Drawing.Size(100, 56)
        Me.Panel10.TabIndex = 7
        '
        'ChkSunday
        '
        Me.ChkSunday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkSunday.Dock = System.Windows.Forms.DockStyle.Top
        Me.ChkSunday.ForeColor = System.Drawing.Color.Blue
        Me.ChkSunday.Location = New System.Drawing.Point(2, 2)
        Me.ChkSunday.Name = "ChkSunday"
        Me.ChkSunday.Size = New System.Drawing.Size(96, 25)
        Me.ChkSunday.TabIndex = 0
        Me.ChkSunday.Text = "Sunday"
        Me.ChkSunday.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnAdd)
        Me.Panel2.Controls.Add(Me.BtnCancel)
        Me.Panel2.Controls.Add(Me.BtnUpdate)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(716, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel2.Size = New System.Drawing.Size(87, 56)
        Me.Panel2.TabIndex = 8
        '
        'BtnAdd
        '
        Me.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAdd.Image = Global.DeliveryTakeOrder.My.Resources.Resources.add16
        Me.BtnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnAdd.Location = New System.Drawing.Point(2, 10)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(83, 37)
        Me.BtnAdd.TabIndex = 12
        Me.BtnAdd.Text = "&Add"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCancel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.update_16
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.Location = New System.Drawing.Point(2, 30)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(83, 28)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.Text = "C&ancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.Visible = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnUpdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnUpdate.Image = Global.DeliveryTakeOrder.My.Resources.Resources.update_16
        Me.BtnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdate.Location = New System.Drawing.Point(2, 2)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(83, 28)
        Me.BtnUpdate.TabIndex = 10
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        Me.BtnUpdate.Visible = False
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DgvShow.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.DeltoId, Me.Delto, Me.Monday, Me.Tuesday, Me.Wednesday, Me.Thursday, Me.Friday, Me.Saturday, Me.Sunday, Me.CreatedDate})
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 168)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(803, 298)
        Me.DgvShow.TabIndex = 113
        '
        'TimerDeltoLoading
        '
        Me.TimerDeltoLoading.Interval = 5
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        Me.Id.Width = 44
        '
        'DeltoId
        '
        Me.DeltoId.DataPropertyName = "DeltoId"
        Me.DeltoId.HeaderText = "#"
        Me.DeltoId.Name = "DeltoId"
        Me.DeltoId.ReadOnly = True
        Me.DeltoId.Width = 40
        '
        'Delto
        '
        Me.Delto.DataPropertyName = "Delto"
        Me.Delto.HeaderText = "Delto"
        Me.Delto.Name = "Delto"
        Me.Delto.ReadOnly = True
        Me.Delto.Width = 63
        '
        'Monday
        '
        Me.Monday.DataPropertyName = "Monday"
        Me.Monday.HeaderText = "Monday"
        Me.Monday.Name = "Monday"
        Me.Monday.ReadOnly = True
        Me.Monday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Monday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Monday.Width = 78
        '
        'Tuesday
        '
        Me.Tuesday.DataPropertyName = "Tuesday"
        Me.Tuesday.HeaderText = "Tuesday"
        Me.Tuesday.Name = "Tuesday"
        Me.Tuesday.ReadOnly = True
        Me.Tuesday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Tuesday.Width = 82
        '
        'Wednesday
        '
        Me.Wednesday.DataPropertyName = "Wednesday"
        Me.Wednesday.HeaderText = "Wednesday"
        Me.Wednesday.Name = "Wednesday"
        Me.Wednesday.ReadOnly = True
        Me.Wednesday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Wednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Wednesday.Width = 99
        '
        'Thursday
        '
        Me.Thursday.DataPropertyName = "Thursday"
        Me.Thursday.HeaderText = "Thursday"
        Me.Thursday.Name = "Thursday"
        Me.Thursday.ReadOnly = True
        Me.Thursday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Thursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Thursday.Width = 86
        '
        'Friday
        '
        Me.Friday.DataPropertyName = "Friday"
        Me.Friday.HeaderText = "Friday"
        Me.Friday.Name = "Friday"
        Me.Friday.ReadOnly = True
        Me.Friday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Friday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Friday.Width = 69
        '
        'Saturday
        '
        Me.Saturday.DataPropertyName = "Saturday"
        Me.Saturday.HeaderText = "Saturday"
        Me.Saturday.Name = "Saturday"
        Me.Saturday.ReadOnly = True
        Me.Saturday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Saturday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Saturday.Width = 84
        '
        'Sunday
        '
        Me.Sunday.DataPropertyName = "Sunday"
        Me.Sunday.HeaderText = "Sunday"
        Me.Sunday.Name = "Sunday"
        Me.Sunday.ReadOnly = True
        Me.Sunday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Sunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Sunday.Width = 76
        '
        'CreatedDate
        '
        Me.CreatedDate.DataPropertyName = "CreatedDate"
        Me.CreatedDate.HeaderText = "CreatedDate"
        Me.CreatedDate.Name = "CreatedDate"
        Me.CreatedDate.ReadOnly = True
        Me.CreatedDate.Visible = False
        Me.CreatedDate.Width = 104
        '
        'TimerDisplayLoading
        '
        Me.TimerDisplayLoading.Interval = 5
        '
        'FrmDeltoListForDutchmillPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnClose
        Me.ClientSize = New System.Drawing.Size(813, 506)
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel44)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "FrmDeltoListForDutchmillPO"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delto List For Dutchmill P.O"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel44.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PicLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LblCompanyName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents CmbDelto As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents ChkTuesday As System.Windows.Forms.CheckBox
    Friend WithEvents ChkMonday As System.Windows.Forms.CheckBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents ChkThursday As System.Windows.Forms.CheckBox
    Friend WithEvents ChkWednesday As System.Windows.Forms.CheckBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents ChkSaturday As System.Windows.Forms.CheckBox
    Friend WithEvents ChkFriday As System.Windows.Forms.CheckBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents ChkSunday As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtId As System.Windows.Forms.TextBox
    Friend WithEvents TimerDeltoLoading As System.Windows.Forms.Timer
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeltoId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Tuesday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Wednesday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Thursday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Friday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Saturday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Sunday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents CreatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimerDisplayLoading As System.Windows.Forms.Timer
End Class
