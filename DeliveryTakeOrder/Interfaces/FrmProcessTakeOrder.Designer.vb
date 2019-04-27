<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessTakeOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProcessTakeOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CmbDelto = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CmbCustomer = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CmbTakeOrderNo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbRequiredDate = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PicLogo = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblCompanyName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnProcessTakeOrder = New System.Windows.Forms.Button()
        Me.BtnPreviewNEditTakeOrder = New System.Windows.Forms.Button()
        Me.LblSeletedRow = New System.Windows.Forms.Label()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.Tick = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TakeOrderNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PONumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DelToId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DelTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateOrd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateRequired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Size = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QtyPCase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QtyPPack = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsFree = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PackOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CTNOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalPcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogInName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PromotionMachanic = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saleman = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimerCustomerLoading = New System.Windows.Forms.Timer(Me.components)
        Me.TimerTakeOrderLoading = New System.Windows.Forms.Timer(Me.components)
        Me.TimerDisplayLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Popmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuModifyTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChangeCustomer = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChangeDelto = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuChangeQtyOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuDeleteTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeltoLoading = New System.Windows.Forms.Timer(Me.components)
        Me.RequiredDateLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel44.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Popmenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.PanelHeader)
        Me.Panel1.Controls.Add(Me.PicLogo)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.LblCompanyName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(818, 103)
        Me.Panel1.TabIndex = 107
        '
        'PanelHeader
        '
        Me.PanelHeader.Controls.Add(Me.Panel5)
        Me.PanelHeader.Controls.Add(Me.Panel3)
        Me.PanelHeader.Controls.Add(Me.Panel2)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelHeader.Location = New System.Drawing.Point(382, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(436, 101)
        Me.PanelHeader.TabIndex = 8
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.CmbDelto)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 64)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel5.Size = New System.Drawing.Size(436, 32)
        Me.Panel5.TabIndex = 3
        '
        'CmbDelto
        '
        Me.CmbDelto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDelto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbDelto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDelto.FormattingEnabled = True
        Me.CmbDelto.Location = New System.Drawing.Point(96, 2)
        Me.CmbDelto.Name = "CmbDelto"
        Me.CmbDelto.Size = New System.Drawing.Size(340, 27)
        Me.CmbDelto.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 28)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Delto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CmbCustomer)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 32)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel3.Size = New System.Drawing.Size(436, 32)
        Me.Panel3.TabIndex = 1
        '
        'CmbCustomer
        '
        Me.CmbCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCustomer.FormattingEnabled = True
        Me.CmbCustomer.Location = New System.Drawing.Point(96, 2)
        Me.CmbCustomer.Name = "CmbCustomer"
        Me.CmbCustomer.Size = New System.Drawing.Size(340, 27)
        Me.CmbCustomer.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 28)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Customer"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CmbTakeOrderNo)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.CmbRequiredDate)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel2.Size = New System.Drawing.Size(436, 32)
        Me.Panel2.TabIndex = 2
        '
        'CmbTakeOrderNo
        '
        Me.CmbTakeOrderNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbTakeOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbTakeOrderNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTakeOrderNo.FormattingEnabled = True
        Me.CmbTakeOrderNo.Location = New System.Drawing.Point(307, 2)
        Me.CmbTakeOrderNo.Name = "CmbTakeOrderNo"
        Me.CmbTakeOrderNo.Size = New System.Drawing.Size(129, 27)
        Me.CmbTakeOrderNo.TabIndex = 1
        Me.CmbTakeOrderNo.Visible = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(225, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 28)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Take Order #"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Visible = False
        '
        'CmbRequiredDate
        '
        Me.CmbRequiredDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbRequiredDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.CmbRequiredDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbRequiredDate.FormattingEnabled = True
        Me.CmbRequiredDate.Location = New System.Drawing.Point(96, 2)
        Me.CmbRequiredDate.Name = "CmbRequiredDate"
        Me.CmbRequiredDate.Size = New System.Drawing.Size(129, 27)
        Me.CmbRequiredDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(0, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 28)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Required Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.Panel4.Size = New System.Drawing.Size(818, 2)
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
        Me.Panel44.Controls.Add(Me.BtnProcessTakeOrder)
        Me.Panel44.Controls.Add(Me.BtnPreviewNEditTakeOrder)
        Me.Panel44.Controls.Add(Me.LblSeletedRow)
        Me.Panel44.Controls.Add(Me.BtnExportToExcel)
        Me.Panel44.Controls.Add(Me.LblCountRow)
        Me.Panel44.Controls.Add(Me.BtnClose)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(5, 467)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel44.Size = New System.Drawing.Size(818, 35)
        Me.Panel44.TabIndex = 110
        '
        'BtnProcessTakeOrder
        '
        Me.BtnProcessTakeOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnProcessTakeOrder.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnProcessTakeOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnProcessTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.BtnProcessTakeOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnProcessTakeOrder.Location = New System.Drawing.Point(283, 2)
        Me.BtnProcessTakeOrder.Name = "BtnProcessTakeOrder"
        Me.BtnProcessTakeOrder.Size = New System.Drawing.Size(167, 31)
        Me.BtnProcessTakeOrder.TabIndex = 10
        Me.BtnProcessTakeOrder.Text = "&Process Take Order"
        Me.BtnProcessTakeOrder.UseVisualStyleBackColor = True
        '
        'BtnPreviewNEditTakeOrder
        '
        Me.BtnPreviewNEditTakeOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPreviewNEditTakeOrder.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnPreviewNEditTakeOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPreviewNEditTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Search16
        Me.BtnPreviewNEditTakeOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnPreviewNEditTakeOrder.Location = New System.Drawing.Point(450, 2)
        Me.BtnPreviewNEditTakeOrder.Name = "BtnPreviewNEditTakeOrder"
        Me.BtnPreviewNEditTakeOrder.Size = New System.Drawing.Size(137, 31)
        Me.BtnPreviewNEditTakeOrder.TabIndex = 14
        Me.BtnPreviewNEditTakeOrder.Text = "Preview && &Edit"
        Me.BtnPreviewNEditTakeOrder.UseVisualStyleBackColor = True
        '
        'LblSeletedRow
        '
        Me.LblSeletedRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblSeletedRow.Location = New System.Drawing.Point(149, 2)
        Me.LblSeletedRow.Name = "LblSeletedRow"
        Me.LblSeletedRow.Size = New System.Drawing.Size(147, 31)
        Me.LblSeletedRow.TabIndex = 13
        Me.LblSeletedRow.Text = "||  Selected Row : 0"
        Me.LblSeletedRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(587, 2)
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
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(724, 2)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(92, 31)
        Me.BtnClose.TabIndex = 9
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Tick, Me.Id, Me.TakeOrderNumber, Me.PONumber, Me.CusNum, Me.CusName, Me.DelToId, Me.DelTo, Me.DateOrd, Me.DateRequired, Me.UnitNumber, Me.Barcode, Me.CusCode, Me.ProName, Me.Size, Me.QtyPCase, Me.QtyPPack, Me.Category, Me.PcsFree, Me.PcsOrder, Me.PackOrder, Me.CTNOrder, Me.TotalPcsOrder, Me.LogInName, Me.PromotionMachanic, Me.ItemDiscount, Me.Remark, Me.Saleman, Me.CreatedDate})
        Me.DgvShow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 108)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(818, 359)
        Me.DgvShow.TabIndex = 111
        '
        'Tick
        '
        Me.Tick.HeaderText = ""
        Me.Tick.Name = "Tick"
        Me.Tick.ReadOnly = True
        Me.Tick.Width = 25
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
        'TakeOrderNumber
        '
        Me.TakeOrderNumber.DataPropertyName = "TakeOrderNumber"
        Me.TakeOrderNumber.HeaderText = "TakeOrderNumber"
        Me.TakeOrderNumber.Name = "TakeOrderNumber"
        Me.TakeOrderNumber.ReadOnly = True
        Me.TakeOrderNumber.Visible = False
        Me.TakeOrderNumber.Width = 135
        '
        'PONumber
        '
        Me.PONumber.DataPropertyName = "PONumber"
        Me.PONumber.HeaderText = "PONumber"
        Me.PONumber.Name = "PONumber"
        Me.PONumber.ReadOnly = True
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "CusNum"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Width = 79
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "CusName"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        '
        'DelToId
        '
        Me.DelToId.DataPropertyName = "DelToId"
        Me.DelToId.HeaderText = "DelToId"
        Me.DelToId.Name = "DelToId"
        Me.DelToId.ReadOnly = True
        Me.DelToId.Visible = False
        Me.DelToId.Width = 60
        '
        'DelTo
        '
        Me.DelTo.DataPropertyName = "DelTo"
        Me.DelTo.HeaderText = "DelTo"
        Me.DelTo.Name = "DelTo"
        Me.DelTo.ReadOnly = True
        '
        'DateOrd
        '
        Me.DateOrd.DataPropertyName = "DateOrd"
        Me.DateOrd.HeaderText = "DateOrd"
        Me.DateOrd.Name = "DateOrd"
        Me.DateOrd.ReadOnly = True
        Me.DateOrd.Width = 90
        '
        'DateRequired
        '
        Me.DateRequired.DataPropertyName = "DateRequired"
        Me.DateRequired.HeaderText = "DateRequired"
        Me.DateRequired.Name = "DateRequired"
        Me.DateRequired.ReadOnly = True
        Me.DateRequired.Width = 90
        '
        'UnitNumber
        '
        Me.UnitNumber.DataPropertyName = "UnitNumber"
        Me.UnitNumber.HeaderText = "UnitNumber"
        Me.UnitNumber.Name = "UnitNumber"
        Me.UnitNumber.ReadOnly = True
        Me.UnitNumber.Visible = False
        Me.UnitNumber.Width = 98
        '
        'Barcode
        '
        Me.Barcode.DataPropertyName = "Barcode"
        Me.Barcode.HeaderText = "Barcode"
        Me.Barcode.Name = "Barcode"
        Me.Barcode.ReadOnly = True
        Me.Barcode.Width = 81
        '
        'CusCode
        '
        Me.CusCode.DataPropertyName = "CusCode"
        Me.CusCode.HeaderText = "Items Code"
        Me.CusCode.Name = "CusCode"
        Me.CusCode.ReadOnly = True
        '
        'ProName
        '
        Me.ProName.DataPropertyName = "ProName"
        Me.ProName.HeaderText = "ProName"
        Me.ProName.Name = "ProName"
        Me.ProName.ReadOnly = True
        Me.ProName.Width = 83
        '
        'Size
        '
        Me.Size.DataPropertyName = "Size"
        Me.Size.HeaderText = "Size"
        Me.Size.Name = "Size"
        Me.Size.ReadOnly = True
        Me.Size.Width = 58
        '
        'QtyPCase
        '
        Me.QtyPCase.DataPropertyName = "QtyPCase"
        Me.QtyPCase.HeaderText = "Q/C"
        Me.QtyPCase.Name = "QtyPCase"
        Me.QtyPCase.ReadOnly = True
        Me.QtyPCase.Width = 56
        '
        'QtyPPack
        '
        Me.QtyPPack.DataPropertyName = "QtyPPack"
        Me.QtyPPack.HeaderText = "QtyPPack"
        Me.QtyPPack.Name = "QtyPPack"
        Me.QtyPPack.ReadOnly = True
        Me.QtyPPack.Visible = False
        Me.QtyPPack.Width = 91
        '
        'Category
        '
        Me.Category.DataPropertyName = "Category"
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        Me.Category.Visible = False
        Me.Category.Width = 85
        '
        'PcsFree
        '
        Me.PcsFree.DataPropertyName = "PcsFree"
        Me.PcsFree.HeaderText = "PcsFree"
        Me.PcsFree.Name = "PcsFree"
        Me.PcsFree.ReadOnly = True
        Me.PcsFree.Width = 79
        '
        'PcsOrder
        '
        Me.PcsOrder.DataPropertyName = "PcsOrder"
        Me.PcsOrder.HeaderText = "PcsOrder"
        Me.PcsOrder.Name = "PcsOrder"
        Me.PcsOrder.ReadOnly = True
        Me.PcsOrder.Width = 85
        '
        'PackOrder
        '
        Me.PackOrder.DataPropertyName = "PackOrder"
        Me.PackOrder.HeaderText = "PackOrder"
        Me.PackOrder.Name = "PackOrder"
        Me.PackOrder.ReadOnly = True
        Me.PackOrder.Width = 93
        '
        'CTNOrder
        '
        Me.CTNOrder.DataPropertyName = "CTNOrder"
        Me.CTNOrder.HeaderText = "CTNOrder"
        Me.CTNOrder.Name = "CTNOrder"
        Me.CTNOrder.ReadOnly = True
        Me.CTNOrder.Width = 88
        '
        'TotalPcsOrder
        '
        Me.TotalPcsOrder.DataPropertyName = "TotalPcsOrder"
        Me.TotalPcsOrder.HeaderText = "TotalPcsOrder"
        Me.TotalPcsOrder.Name = "TotalPcsOrder"
        Me.TotalPcsOrder.ReadOnly = True
        Me.TotalPcsOrder.Width = 113
        '
        'LogInName
        '
        Me.LogInName.DataPropertyName = "LogInName"
        Me.LogInName.HeaderText = "LogInName"
        Me.LogInName.Name = "LogInName"
        Me.LogInName.ReadOnly = True
        Me.LogInName.Visible = False
        Me.LogInName.Width = 95
        '
        'PromotionMachanic
        '
        Me.PromotionMachanic.DataPropertyName = "PromotionMachanic"
        Me.PromotionMachanic.HeaderText = "PromotionMachanic"
        Me.PromotionMachanic.Name = "PromotionMachanic"
        Me.PromotionMachanic.ReadOnly = True
        Me.PromotionMachanic.Visible = False
        Me.PromotionMachanic.Width = 143
        '
        'ItemDiscount
        '
        Me.ItemDiscount.DataPropertyName = "ItemDiscount"
        Me.ItemDiscount.HeaderText = "ItemDiscount"
        Me.ItemDiscount.Name = "ItemDiscount"
        Me.ItemDiscount.ReadOnly = True
        Me.ItemDiscount.Visible = False
        Me.ItemDiscount.Width = 106
        '
        'Remark
        '
        Me.Remark.DataPropertyName = "Remark"
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        Me.Remark.Visible = False
        Me.Remark.Width = 76
        '
        'Saleman
        '
        Me.Saleman.DataPropertyName = "Saleman"
        Me.Saleman.HeaderText = "Saleman"
        Me.Saleman.Name = "Saleman"
        Me.Saleman.ReadOnly = True
        Me.Saleman.Visible = False
        Me.Saleman.Width = 81
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
        'TimerCustomerLoading
        '
        Me.TimerCustomerLoading.Interval = 5
        '
        'TimerTakeOrderLoading
        '
        Me.TimerTakeOrderLoading.Interval = 5
        '
        'TimerDisplayLoading
        '
        Me.TimerDisplayLoading.Interval = 5
        '
        'Popmenu
        '
        Me.Popmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuModifyTakeOrder, Me.ToolStripSeparator1, Me.MnuDeleteTakeOrder})
        Me.Popmenu.Name = "Popmenu"
        Me.Popmenu.Size = New System.Drawing.Size(173, 54)
        '
        'MnuModifyTakeOrder
        '
        Me.MnuModifyTakeOrder.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuChangeCustomer, Me.MnuChangeDelto, Me.MnuChangeQtyOrder})
        Me.MnuModifyTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.refresh_16
        Me.MnuModifyTakeOrder.Name = "MnuModifyTakeOrder"
        Me.MnuModifyTakeOrder.Size = New System.Drawing.Size(172, 22)
        Me.MnuModifyTakeOrder.Text = "&Modify Take Order"
        '
        'MnuChangeCustomer
        '
        Me.MnuChangeCustomer.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Customer
        Me.MnuChangeCustomer.Name = "MnuChangeCustomer"
        Me.MnuChangeCustomer.Size = New System.Drawing.Size(170, 22)
        Me.MnuChangeCustomer.Text = "Change &Customer"
        '
        'MnuChangeDelto
        '
        Me.MnuChangeDelto.Image = Global.DeliveryTakeOrder.My.Resources.Resources.delto
        Me.MnuChangeDelto.Name = "MnuChangeDelto"
        Me.MnuChangeDelto.Size = New System.Drawing.Size(170, 22)
        Me.MnuChangeDelto.Text = "Change &Delto"
        '
        'MnuChangeQtyOrder
        '
        Me.MnuChangeQtyOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Separately
        Me.MnuChangeQtyOrder.Name = "MnuChangeQtyOrder"
        Me.MnuChangeQtyOrder.Size = New System.Drawing.Size(170, 22)
        Me.MnuChangeQtyOrder.Text = "Change &Qty Order"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(169, 6)
        '
        'MnuDeleteTakeOrder
        '
        Me.MnuDeleteTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.deleted_16
        Me.MnuDeleteTakeOrder.Name = "MnuDeleteTakeOrder"
        Me.MnuDeleteTakeOrder.Size = New System.Drawing.Size(172, 22)
        Me.MnuDeleteTakeOrder.Text = "&Delete Barcode"
        '
        'DeltoLoading
        '
        Me.DeltoLoading.Interval = 5
        '
        'RequiredDateLoading
        '
        Me.RequiredDateLoading.Interval = 5
        '
        'FrmProcessTakeOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(828, 507)
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel44)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmProcessTakeOrder"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Process Take Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.PanelHeader.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel44.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Popmenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PanelHeader As System.Windows.Forms.Panel
    Friend WithEvents PicLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LblCompanyName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents BtnProcessTakeOrder As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CmbTakeOrderNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents CmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TimerCustomerLoading As System.Windows.Forms.Timer
    Friend WithEvents TimerTakeOrderLoading As System.Windows.Forms.Timer
    Friend WithEvents TimerDisplayLoading As System.Windows.Forms.Timer
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents Popmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MnuModifyTakeOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChangeCustomer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChangeDelto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChangeQtyOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuDeleteTakeOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmbRequiredDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents CmbDelto As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DeltoLoading As System.Windows.Forms.Timer
    Friend WithEvents RequiredDateLoading As System.Windows.Forms.Timer
    Friend WithEvents LblSeletedRow As System.Windows.Forms.Label
    Friend WithEvents BtnPreviewNEditTakeOrder As System.Windows.Forms.Button
    Friend WithEvents Tick As DataGridViewCheckBoxColumn
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents TakeOrderNumber As DataGridViewTextBoxColumn
    Friend WithEvents PONumber As DataGridViewTextBoxColumn
    Friend WithEvents CusNum As DataGridViewTextBoxColumn
    Friend WithEvents CusName As DataGridViewTextBoxColumn
    Friend WithEvents DelToId As DataGridViewTextBoxColumn
    Friend WithEvents DelTo As DataGridViewTextBoxColumn
    Friend WithEvents DateOrd As DataGridViewTextBoxColumn
    Friend WithEvents DateRequired As DataGridViewTextBoxColumn
    Friend WithEvents UnitNumber As DataGridViewTextBoxColumn
    Friend WithEvents Barcode As DataGridViewTextBoxColumn
    Friend WithEvents CusCode As DataGridViewTextBoxColumn
    Friend WithEvents ProName As DataGridViewTextBoxColumn
    Friend WithEvents Size As DataGridViewTextBoxColumn
    Friend WithEvents QtyPCase As DataGridViewTextBoxColumn
    Friend WithEvents QtyPPack As DataGridViewTextBoxColumn
    Friend WithEvents Category As DataGridViewTextBoxColumn
    Friend WithEvents PcsFree As DataGridViewTextBoxColumn
    Friend WithEvents PcsOrder As DataGridViewTextBoxColumn
    Friend WithEvents PackOrder As DataGridViewTextBoxColumn
    Friend WithEvents CTNOrder As DataGridViewTextBoxColumn
    Friend WithEvents TotalPcsOrder As DataGridViewTextBoxColumn
    Friend WithEvents LogInName As DataGridViewTextBoxColumn
    Friend WithEvents PromotionMachanic As DataGridViewTextBoxColumn
    Friend WithEvents ItemDiscount As DataGridViewTextBoxColumn
    Friend WithEvents Remark As DataGridViewTextBoxColumn
    Friend WithEvents Saleman As DataGridViewTextBoxColumn
    Friend WithEvents CreatedDate As DataGridViewTextBoxColumn
End Class
