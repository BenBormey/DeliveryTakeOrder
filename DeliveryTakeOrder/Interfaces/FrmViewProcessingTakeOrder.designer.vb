<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmViewProcessingTakeOrder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewProcessingTakeOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PicLogo = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CmbDate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.CmbDelto = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.CmbCustomer = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LblCompanyName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DelTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateOrd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateRequired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeliveryDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProNumy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProPackSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProQtyPCase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsFree = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CTNOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalPcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PONumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogInName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TakeOrderInvoiceNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintInvoiceNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PromotionMachanic = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProCat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarkExpiry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RelatedKey = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saleman = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.BtnExportToExcel = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.CustomerLoading = New System.Windows.Forms.Timer(Me.components)
        Me.DateLoading = New System.Windows.Forms.Timer(Me.components)
        Me.displayloading = New System.Windows.Forms.Timer(Me.components)
        Me.DeltoLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.PicLogo)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.LblCompanyName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(872, 98)
        Me.Panel1.TabIndex = 108
        '
        'PicLogo
        '
        Me.PicLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicLogo.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicLogo.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Logo
        Me.PicLogo.Location = New System.Drawing.Point(0, 0)
        Me.PicLogo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PicLogo.Name = "PicLogo"
        Me.PicLogo.Size = New System.Drawing.Size(110, 95)
        Me.PicLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PicLogo.TabIndex = 3
        Me.PicLogo.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Panel18)
        Me.Panel2.Controls.Add(Me.Panel8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(457, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(415, 95)
        Me.Panel2.TabIndex = 8
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.CmbDate)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 64)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel3.Size = New System.Drawing.Size(415, 32)
        Me.Panel3.TabIndex = 1
        '
        'CmbDate
        '
        Me.CmbDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDate.FormattingEnabled = True
        Me.CmbDate.Location = New System.Drawing.Point(99, 2)
        Me.CmbDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CmbDate.Name = "CmbDate"
        Me.CmbDate.Size = New System.Drawing.Size(314, 27)
        Me.CmbDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(2, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 28)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Required Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.CmbDelto)
        Me.Panel18.Controls.Add(Me.Label14)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 32)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel18.Size = New System.Drawing.Size(415, 32)
        Me.Panel18.TabIndex = 119
        '
        'CmbDelto
        '
        Me.CmbDelto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDelto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbDelto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDelto.FormattingEnabled = True
        Me.CmbDelto.Items.AddRange(New Object() {"CLAIM", "UNCLAIM"})
        Me.CmbDelto.Location = New System.Drawing.Point(99, 2)
        Me.CmbDelto.Name = "CmbDelto"
        Me.CmbDelto.Size = New System.Drawing.Size(314, 27)
        Me.CmbDelto.TabIndex = 2
        '
        'Label14
        '
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(2, 2)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(97, 28)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Delto"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Controls.Add(Me.CmbCustomer)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel8.Size = New System.Drawing.Size(415, 32)
        Me.Panel8.TabIndex = 0
        '
        'CmbCustomer
        '
        Me.CmbCustomer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCustomer.FormattingEnabled = True
        Me.CmbCustomer.Location = New System.Drawing.Point(99, 2)
        Me.CmbCustomer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CmbCustomer.Name = "CmbCustomer"
        Me.CmbCustomer.Size = New System.Drawing.Size(314, 27)
        Me.CmbCustomer.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(2, 2)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 28)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Customer"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 95)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(872, 3)
        Me.Panel4.TabIndex = 7
        '
        'LblCompanyName
        '
        Me.LblCompanyName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompanyName.ForeColor = System.Drawing.Color.Blue
        Me.LblCompanyName.Location = New System.Drawing.Point(116, 39)
        Me.LblCompanyName.Name = "LblCompanyName"
        Me.LblCompanyName.Size = New System.Drawing.Size(277, 63)
        Me.LblCompanyName.TabIndex = 6
        Me.LblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(117, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(276, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Q's MANAGEMENT SYSTEM"
        '
        'LblCountRow
        '
        Me.LblCountRow.AutoSize = True
        Me.LblCountRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblCountRow.Location = New System.Drawing.Point(0, 0)
        Me.LblCountRow.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblCountRow.Name = "LblCountRow"
        Me.LblCountRow.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.LblCountRow.Size = New System.Drawing.Size(84, 26)
        Me.LblCountRow.TabIndex = 113
        Me.LblCountRow.Text = "Count Row : 0"
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.CusName, Me.CusNum, Me.DelTo, Me.DateOrd, Me.DateRequired, Me.DeliveryDate, Me.ProNumy, Me.ProName, Me.ProPackSize, Me.ProQtyPCase, Me.PcsFree, Me.PcsOrder, Me.CTNOrder, Me.TotalPcsOrder, Me.PONumber, Me.LogInName, Me.TakeOrderInvoiceNumber, Me.PrintInvoiceNumber, Me.PromotionMachanic, Me.ProCat, Me.ItemDiscount, Me.RemarkExpiry, Me.RelatedKey, Me.Saleman, Me.Status})
        Me.DgvShow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(0, 0)
        Me.DgvShow.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(872, 415)
        Me.DgvShow.TabIndex = 114
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "Customer Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        '
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "CusNum"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Visible = False
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
        DataGridViewCellStyle1.Format = "dd-MMM-yyyy"
        Me.DateOrd.DefaultCellStyle = DataGridViewCellStyle1
        Me.DateOrd.HeaderText = "Ord. Date"
        Me.DateOrd.Name = "DateOrd"
        Me.DateOrd.ReadOnly = True
        '
        'DateRequired
        '
        Me.DateRequired.DataPropertyName = "DateRequired"
        DataGridViewCellStyle2.Format = "dd-MMM-yyyy"
        Me.DateRequired.DefaultCellStyle = DataGridViewCellStyle2
        Me.DateRequired.HeaderText = "Required Date"
        Me.DateRequired.Name = "DateRequired"
        Me.DateRequired.ReadOnly = True
        '
        'DeliveryDate
        '
        Me.DeliveryDate.DataPropertyName = "DeliveryDate"
        DataGridViewCellStyle3.Format = "dd-MMM-yyyy"
        Me.DeliveryDate.DefaultCellStyle = DataGridViewCellStyle3
        Me.DeliveryDate.HeaderText = "Delivery Date"
        Me.DeliveryDate.Name = "DeliveryDate"
        Me.DeliveryDate.ReadOnly = True
        '
        'ProNumy
        '
        Me.ProNumy.DataPropertyName = "ProNumy"
        Me.ProNumy.HeaderText = "Barcode"
        Me.ProNumy.Name = "ProNumy"
        Me.ProNumy.ReadOnly = True
        '
        'ProName
        '
        Me.ProName.DataPropertyName = "ProName"
        Me.ProName.HeaderText = "Product Name"
        Me.ProName.Name = "ProName"
        Me.ProName.ReadOnly = True
        '
        'ProPackSize
        '
        Me.ProPackSize.DataPropertyName = "ProPackSize"
        Me.ProPackSize.HeaderText = "Size"
        Me.ProPackSize.Name = "ProPackSize"
        Me.ProPackSize.ReadOnly = True
        '
        'ProQtyPCase
        '
        Me.ProQtyPCase.DataPropertyName = "ProQtyPCase"
        Me.ProQtyPCase.HeaderText = "Q/C"
        Me.ProQtyPCase.Name = "ProQtyPCase"
        Me.ProQtyPCase.ReadOnly = True
        '
        'PcsFree
        '
        Me.PcsFree.DataPropertyName = "PcsFree"
        Me.PcsFree.HeaderText = "Pcs Free"
        Me.PcsFree.Name = "PcsFree"
        Me.PcsFree.ReadOnly = True
        '
        'PcsOrder
        '
        Me.PcsOrder.DataPropertyName = "PcsOrder"
        Me.PcsOrder.HeaderText = "Pcs Order"
        Me.PcsOrder.Name = "PcsOrder"
        Me.PcsOrder.ReadOnly = True
        '
        'CTNOrder
        '
        Me.CTNOrder.DataPropertyName = "CTNOrder"
        Me.CTNOrder.HeaderText = "CTN Order"
        Me.CTNOrder.Name = "CTNOrder"
        Me.CTNOrder.ReadOnly = True
        '
        'TotalPcsOrder
        '
        Me.TotalPcsOrder.DataPropertyName = "TotalPcsOrder"
        Me.TotalPcsOrder.HeaderText = "Total Pcs Order"
        Me.TotalPcsOrder.Name = "TotalPcsOrder"
        Me.TotalPcsOrder.ReadOnly = True
        '
        'PONumber
        '
        Me.PONumber.DataPropertyName = "PONumber"
        Me.PONumber.HeaderText = "PO Number"
        Me.PONumber.Name = "PONumber"
        Me.PONumber.ReadOnly = True
        '
        'LogInName
        '
        Me.LogInName.DataPropertyName = "LogInName"
        Me.LogInName.HeaderText = "LogInName"
        Me.LogInName.Name = "LogInName"
        Me.LogInName.ReadOnly = True
        Me.LogInName.Visible = False
        '
        'TakeOrderInvoiceNumber
        '
        Me.TakeOrderInvoiceNumber.DataPropertyName = "TakeOrderInvoiceNumber"
        Me.TakeOrderInvoiceNumber.HeaderText = "T.O Number"
        Me.TakeOrderInvoiceNumber.Name = "TakeOrderInvoiceNumber"
        Me.TakeOrderInvoiceNumber.ReadOnly = True
        '
        'PrintInvoiceNumber
        '
        Me.PrintInvoiceNumber.DataPropertyName = "PrintInvoiceNumber"
        Me.PrintInvoiceNumber.HeaderText = "Picking Number"
        Me.PrintInvoiceNumber.Name = "PrintInvoiceNumber"
        Me.PrintInvoiceNumber.ReadOnly = True
        '
        'PromotionMachanic
        '
        Me.PromotionMachanic.DataPropertyName = "PromotionMachanic"
        Me.PromotionMachanic.HeaderText = "PromotionMachanic"
        Me.PromotionMachanic.Name = "PromotionMachanic"
        Me.PromotionMachanic.ReadOnly = True
        Me.PromotionMachanic.Visible = False
        '
        'ProCat
        '
        Me.ProCat.DataPropertyName = "ProCat"
        Me.ProCat.HeaderText = "ProCat"
        Me.ProCat.Name = "ProCat"
        Me.ProCat.ReadOnly = True
        Me.ProCat.Visible = False
        '
        'ItemDiscount
        '
        Me.ItemDiscount.DataPropertyName = "ItemDiscount"
        Me.ItemDiscount.HeaderText = "ItemDiscount"
        Me.ItemDiscount.Name = "ItemDiscount"
        Me.ItemDiscount.ReadOnly = True
        Me.ItemDiscount.Visible = False
        '
        'RemarkExpiry
        '
        Me.RemarkExpiry.DataPropertyName = "RemarkExpiry"
        Me.RemarkExpiry.HeaderText = "RemarkExpiry"
        Me.RemarkExpiry.Name = "RemarkExpiry"
        Me.RemarkExpiry.ReadOnly = True
        Me.RemarkExpiry.Visible = False
        '
        'RelatedKey
        '
        Me.RelatedKey.DataPropertyName = "RelatedKey"
        Me.RelatedKey.HeaderText = "RelatedKey"
        Me.RelatedKey.Name = "RelatedKey"
        Me.RelatedKey.ReadOnly = True
        Me.RelatedKey.Visible = False
        '
        'Saleman
        '
        Me.Saleman.DataPropertyName = "Saleman"
        Me.Saleman.HeaderText = "Saleman"
        Me.Saleman.Name = "Saleman"
        Me.Saleman.ReadOnly = True
        Me.Saleman.Visible = False
        '
        'Status
        '
        Me.Status.DataPropertyName = "Status"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel5)
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Controls.Add(Me.LblCountRow)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(6, 520)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(872, 34)
        Me.Panel7.TabIndex = 115
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Panel5.Controls.Add(Me.txtbarcode)
        Me.Panel5.Controls.Add(Me.BtnSearch)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(84, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel5.Size = New System.Drawing.Size(396, 34)
        Me.Panel5.TabIndex = 114
        '
        'txtbarcode
        '
        Me.txtbarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtbarcode.Location = New System.Drawing.Point(132, 2)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(160, 28)
        Me.txtbarcode.TabIndex = 113
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnSearch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnSearch.Image = Global.DeliveryTakeOrder.My.Resources.Resources.view_takeorder
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSearch.Location = New System.Drawing.Point(292, 2)
        Me.BtnSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(102, 30)
        Me.BtnSearch.TabIndex = 112
        Me.BtnSearch.Text = "&Search"
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 30)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Search By Barcode"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.BtnExportToExcel)
        Me.Panel9.Controls.Add(Me.BtnClose)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(588, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel9.Size = New System.Drawing.Size(284, 34)
        Me.Panel9.TabIndex = 0
        '
        'BtnExportToExcel
        '
        Me.BtnExportToExcel.BackColor = System.Drawing.SystemColors.Control
        Me.BtnExportToExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExportToExcel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnExportToExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExportToExcel.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Excel16
        Me.BtnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExportToExcel.Location = New System.Drawing.Point(2, 2)
        Me.BtnExportToExcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnExportToExcel.Name = "BtnExportToExcel"
        Me.BtnExportToExcel.Size = New System.Drawing.Size(178, 30)
        Me.BtnExportToExcel.TabIndex = 110
        Me.BtnExportToExcel.Text = "&Export To Excel"
        Me.BtnExportToExcel.UseVisualStyleBackColor = False
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.SystemColors.Control
        Me.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel_16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(180, 2)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(102, 30)
        Me.BtnClose.TabIndex = 111
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.DgvShow)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(6, 105)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(872, 415)
        Me.Panel10.TabIndex = 116
        '
        'CustomerLoading
        '
        Me.CustomerLoading.Interval = 5
        '
        'DateLoading
        '
        Me.DateLoading.Interval = 5
        '
        'displayloading
        '
        Me.displayloading.Interval = 5
        '
        'DeltoLoading
        '
        Me.DeltoLoading.Interval = 5
        '
        'FrmViewProcessingTakeOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmViewProcessingTakeOrder"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Processing Take Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LblCompanyName As System.Windows.Forms.Label
    Friend WithEvents PicLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents CmbDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents CmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents BtnExportToExcel As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents CustomerLoading As System.Windows.Forms.Timer
    Friend WithEvents DateLoading As System.Windows.Forms.Timer
    Friend WithEvents displayloading As System.Windows.Forms.Timer
    Friend WithEvents Panel18 As Panel
    Friend WithEvents CmbDelto As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents DeltoLoading As Timer
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents CusName As DataGridViewTextBoxColumn
    Friend WithEvents CusNum As DataGridViewTextBoxColumn
    Friend WithEvents DelTo As DataGridViewTextBoxColumn
    Friend WithEvents DateOrd As DataGridViewTextBoxColumn
    Friend WithEvents DateRequired As DataGridViewTextBoxColumn
    Friend WithEvents DeliveryDate As DataGridViewTextBoxColumn
    Friend WithEvents ProNumy As DataGridViewTextBoxColumn
    Friend WithEvents ProName As DataGridViewTextBoxColumn
    Friend WithEvents ProPackSize As DataGridViewTextBoxColumn
    Friend WithEvents ProQtyPCase As DataGridViewTextBoxColumn
    Friend WithEvents PcsFree As DataGridViewTextBoxColumn
    Friend WithEvents PcsOrder As DataGridViewTextBoxColumn
    Friend WithEvents CTNOrder As DataGridViewTextBoxColumn
    Friend WithEvents TotalPcsOrder As DataGridViewTextBoxColumn
    Friend WithEvents PONumber As DataGridViewTextBoxColumn
    Friend WithEvents LogInName As DataGridViewTextBoxColumn
    Friend WithEvents TakeOrderInvoiceNumber As DataGridViewTextBoxColumn
    Friend WithEvents PrintInvoiceNumber As DataGridViewTextBoxColumn
    Friend WithEvents PromotionMachanic As DataGridViewTextBoxColumn
    Friend WithEvents ProCat As DataGridViewTextBoxColumn
    Friend WithEvents ItemDiscount As DataGridViewTextBoxColumn
    Friend WithEvents RemarkExpiry As DataGridViewTextBoxColumn
    Friend WithEvents RelatedKey As DataGridViewTextBoxColumn
    Friend WithEvents Saleman As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnSearch As Button
    Friend WithEvents txtbarcode As TextBox
End Class
