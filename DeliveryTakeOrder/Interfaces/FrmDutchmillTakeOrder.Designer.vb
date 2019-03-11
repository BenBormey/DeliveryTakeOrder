<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDutchmillTakeOrder
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDutchmillTakeOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LblCompanyName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CmbPlanningOrder = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PicLogo = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.CmbProducts = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.CmbBillTo = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.TxtKhmerName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.TxtZone = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.CmbDelto = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LblTeam = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.BtnViewCredit = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.TxtTotalPcsOrder = New System.Windows.Forms.TextBox()
        Me.Panel35 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel32 = New System.Windows.Forms.Panel()
        Me.TxtCTNOrder = New System.Windows.Forms.TextBox()
        Me.Panel33 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.TxtPcsOrder = New System.Windows.Forms.TextBox()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.TxtQtyPerCase = New System.Windows.Forms.TextBox()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.DgvShow = New System.Windows.Forms.DataGridView()
        Me.ManualRenew = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ManualNotAccept = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ManualChangeQty = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Renew = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NotAccept = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ChangeQty = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeltoId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnitNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Size = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QtyPerCase = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CTNOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalPcsOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SupNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PlanningOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IPAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VerifyDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RequiredDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.LblTotalDelto = New System.Windows.Forms.Label()
        Me.LblTotalCustomer = New System.Windows.Forms.Label()
        Me.BtnRetrieveTakeOrder = New System.Windows.Forms.Button()
        Me.LblCountRow = New System.Windows.Forms.Label()
        Me.BtnFinish = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BillToLoading = New System.Windows.Forms.Timer(Me.components)
        Me.DeltoLoading = New System.Windows.Forms.Timer(Me.components)
        Me.ItemLoading = New System.Windows.Forms.Timer(Me.components)
        Me.PlanningOrderLoading = New System.Windows.Forms.Timer(Me.components)
        Me.DisplayLoading = New System.Windows.Forms.Timer(Me.components)
        Me.Popmain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuChangeCustomer = New System.Windows.Forms.ToolStripMenuItem()
        Me.PCustomerRemark = New System.Windows.Forms.Panel()
        Me.TxtCustomerRemark = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.Panel32.SuspendLayout()
        Me.Panel33.SuspendLayout()
        Me.Panel30.SuspendLayout()
        Me.Panel31.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel19.SuspendLayout()
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel44.SuspendLayout()
        Me.Popmain.SuspendLayout()
        Me.PCustomerRemark.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.PanelHeader)
        Me.Panel1.Controls.Add(Me.PicLogo)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(824, 103)
        Me.Panel1.TabIndex = 107
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LblCompanyName)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(94, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Panel2.Size = New System.Drawing.Size(298, 101)
        Me.Panel2.TabIndex = 12
        '
        'LblCompanyName
        '
        Me.LblCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblCompanyName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompanyName.ForeColor = System.Drawing.Color.Blue
        Me.LblCompanyName.Location = New System.Drawing.Point(0, 33)
        Me.LblCompanyName.Name = "LblCompanyName"
        Me.LblCompanyName.Size = New System.Drawing.Size(298, 68)
        Me.LblCompanyName.TabIndex = 13
        Me.LblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(0, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(298, 23)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Q's MANAGEMENT SYSTEM"
        '
        'PanelHeader
        '
        Me.PanelHeader.Controls.Add(Me.Label10)
        Me.PanelHeader.Controls.Add(Me.Panel20)
        Me.PanelHeader.Controls.Add(Me.Panel5)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelHeader.Location = New System.Drawing.Point(543, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.PanelHeader.Size = New System.Drawing.Size(281, 101)
        Me.PanelHeader.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 66)
        Me.Label10.TabIndex = 131
        Me.Label10.Text = "Note"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel20
        '
        Me.Panel20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel20.Controls.Add(Me.Label5)
        Me.Panel20.Controls.Add(Me.Label3)
        Me.Panel20.Controls.Add(Me.Label2)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel20.Location = New System.Drawing.Point(111, 0)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel20.Size = New System.Drawing.Size(170, 66)
        Me.Panel20.TabIndex = 130
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(2, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(164, 20)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "→ C = Change Quantity"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(2, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(164, 26)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "→ N = Not Accept"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(164, 18)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "→ R = Renew"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.CmbPlanningOrder)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 66)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel5.Size = New System.Drawing.Size(281, 30)
        Me.Panel5.TabIndex = 1
        '
        'CmbPlanningOrder
        '
        Me.CmbPlanningOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbPlanningOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmbPlanningOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPlanningOrder.FormattingEnabled = True
        Me.CmbPlanningOrder.Location = New System.Drawing.Point(111, 2)
        Me.CmbPlanningOrder.Name = "CmbPlanningOrder"
        Me.CmbPlanningOrder.Size = New System.Drawing.Size(170, 27)
        Me.CmbPlanningOrder.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 26)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Planning Order"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.Panel4.Size = New System.Drawing.Size(824, 2)
        Me.Panel4.TabIndex = 7
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.Panel3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(5, 108)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel6.Size = New System.Drawing.Size(824, 125)
        Me.Panel6.TabIndex = 108
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel11)
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 2)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.Panel7.Size = New System.Drawing.Size(433, 62)
        Me.Panel7.TabIndex = 2
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.CmbProducts)
        Me.Panel11.Controls.Add(Me.Label7)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 30)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel11.Size = New System.Drawing.Size(431, 30)
        Me.Panel11.TabIndex = 5
        '
        'CmbProducts
        '
        Me.CmbProducts.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbProducts.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmbProducts.FormattingEnabled = True
        Me.CmbProducts.Location = New System.Drawing.Point(94, 2)
        Me.CmbProducts.Name = "CmbProducts"
        Me.CmbProducts.Size = New System.Drawing.Size(337, 27)
        Me.CmbProducts.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 26)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Product Items"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.CmbBillTo)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel8.Size = New System.Drawing.Size(431, 30)
        Me.Panel8.TabIndex = 2
        '
        'CmbBillTo
        '
        Me.CmbBillTo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbBillTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmbBillTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBillTo.FormattingEnabled = True
        Me.CmbBillTo.Location = New System.Drawing.Point(94, 2)
        Me.CmbBillTo.Name = "CmbBillTo"
        Me.CmbBillTo.Size = New System.Drawing.Size(337, 27)
        Me.CmbBillTo.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(0, 2)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 26)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Bill To"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel16)
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(433, 2)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel9.Size = New System.Drawing.Size(391, 62)
        Me.Panel9.TabIndex = 3
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Panel17)
        Me.Panel16.Controls.Add(Me.Panel12)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(2, 30)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(387, 32)
        Me.Panel16.TabIndex = 12
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.TxtKhmerName)
        Me.Panel17.Controls.Add(Me.Label12)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(130, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel17.Size = New System.Drawing.Size(257, 32)
        Me.Panel17.TabIndex = 12
        '
        'TxtKhmerName
        '
        Me.TxtKhmerName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtKhmerName.Font = New System.Drawing.Font("Khmer OS Siemreap", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKhmerName.Location = New System.Drawing.Point(85, 2)
        Me.TxtKhmerName.Name = "TxtKhmerName"
        Me.TxtKhmerName.ReadOnly = True
        Me.TxtKhmerName.Size = New System.Drawing.Size(172, 28)
        Me.TxtKhmerName.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(0, 2)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 28)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Khmer Name"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Panel13)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 2, 2, 0)
        Me.Panel12.Size = New System.Drawing.Size(130, 32)
        Me.Panel12.TabIndex = 11
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.TxtZone)
        Me.Panel13.Controls.Add(Me.Label9)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 2)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel13.Size = New System.Drawing.Size(128, 30)
        Me.Panel13.TabIndex = 1
        '
        'TxtZone
        '
        Me.TxtZone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtZone.Location = New System.Drawing.Point(55, 2)
        Me.TxtZone.Name = "TxtZone"
        Me.TxtZone.ReadOnly = True
        Me.TxtZone.Size = New System.Drawing.Size(73, 28)
        Me.TxtZone.TabIndex = 3
        Me.TxtZone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(0, 2)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 26)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Zone"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.CmbDelto)
        Me.Panel10.Controls.Add(Me.Label6)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(2, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel10.Size = New System.Drawing.Size(387, 30)
        Me.Panel10.TabIndex = 2
        '
        'CmbDelto
        '
        Me.CmbDelto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CmbDelto.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmbDelto.FormattingEnabled = True
        Me.CmbDelto.Location = New System.Drawing.Point(55, 2)
        Me.CmbDelto.Name = "CmbDelto"
        Me.CmbDelto.Size = New System.Drawing.Size(332, 27)
        Me.CmbDelto.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 26)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Delto"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.LblTeam)
        Me.Panel3.Controls.Add(Me.Panel15)
        Me.Panel3.Controls.Add(Me.Panel34)
        Me.Panel3.Controls.Add(Me.Panel32)
        Me.Panel3.Controls.Add(Me.Panel30)
        Me.Panel3.Controls.Add(Me.Panel18)
        Me.Panel3.Controls.Add(Me.Panel14)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 64)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel3.Size = New System.Drawing.Size(824, 59)
        Me.Panel3.TabIndex = 117
        '
        'LblTeam
        '
        Me.LblTeam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTeam.Font = New System.Drawing.Font("Khmer OS Battambang", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTeam.ForeColor = System.Drawing.Color.Blue
        Me.LblTeam.Location = New System.Drawing.Point(650, 0)
        Me.LblTeam.Name = "LblTeam"
        Me.LblTeam.Size = New System.Drawing.Size(172, 59)
        Me.LblTeam.TabIndex = 129
        Me.LblTeam.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.BtnViewCredit)
        Me.Panel15.Controls.Add(Me.BtnAdd)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Location = New System.Drawing.Point(433, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(2, 27, 2, 0)
        Me.Panel15.Size = New System.Drawing.Size(217, 59)
        Me.Panel15.TabIndex = 128
        '
        'BtnViewCredit
        '
        Me.BtnViewCredit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnViewCredit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnViewCredit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnViewCredit.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Search16
        Me.BtnViewCredit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnViewCredit.Location = New System.Drawing.Point(101, 27)
        Me.BtnViewCredit.Name = "BtnViewCredit"
        Me.BtnViewCredit.Size = New System.Drawing.Size(114, 32)
        Me.BtnViewCredit.TabIndex = 128
        Me.BtnViewCredit.Text = "&View Credit"
        Me.BtnViewCredit.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAdd.Image = Global.DeliveryTakeOrder.My.Resources.Resources.add16
        Me.BtnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnAdd.Location = New System.Drawing.Point(2, 27)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(99, 32)
        Me.BtnAdd.TabIndex = 127
        Me.BtnAdd.Text = "&Add"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'Panel34
        '
        Me.Panel34.Controls.Add(Me.TxtTotalPcsOrder)
        Me.Panel34.Controls.Add(Me.Panel35)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel34.Location = New System.Drawing.Point(324, 0)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel34.Size = New System.Drawing.Size(109, 59)
        Me.Panel34.TabIndex = 122
        '
        'TxtTotalPcsOrder
        '
        Me.TxtTotalPcsOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtTotalPcsOrder.Location = New System.Drawing.Point(2, 30)
        Me.TxtTotalPcsOrder.Name = "TxtTotalPcsOrder"
        Me.TxtTotalPcsOrder.ReadOnly = True
        Me.TxtTotalPcsOrder.Size = New System.Drawing.Size(105, 28)
        Me.TxtTotalPcsOrder.TabIndex = 2
        Me.TxtTotalPcsOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel35
        '
        Me.Panel35.Controls.Add(Me.Label21)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel35.Location = New System.Drawing.Point(2, 0)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel35.Size = New System.Drawing.Size(105, 30)
        Me.Panel35.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Location = New System.Drawing.Point(0, 2)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(105, 26)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Total Pcs Order"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel32
        '
        Me.Panel32.Controls.Add(Me.TxtCTNOrder)
        Me.Panel32.Controls.Add(Me.Panel33)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel32.Location = New System.Drawing.Point(239, 0)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel32.Size = New System.Drawing.Size(85, 59)
        Me.Panel32.TabIndex = 125
        '
        'TxtCTNOrder
        '
        Me.TxtCTNOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCTNOrder.Location = New System.Drawing.Point(2, 30)
        Me.TxtCTNOrder.Name = "TxtCTNOrder"
        Me.TxtCTNOrder.Size = New System.Drawing.Size(81, 28)
        Me.TxtCTNOrder.TabIndex = 2
        Me.TxtCTNOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel33
        '
        Me.Panel33.Controls.Add(Me.Label20)
        Me.Panel33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel33.Location = New System.Drawing.Point(2, 0)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel33.Size = New System.Drawing.Size(81, 30)
        Me.Panel33.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label20.Location = New System.Drawing.Point(0, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(81, 26)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "CTN Order"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel30
        '
        Me.Panel30.Controls.Add(Me.TxtPcsOrder)
        Me.Panel30.Controls.Add(Me.Panel31)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel30.Location = New System.Drawing.Point(154, 0)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel30.Size = New System.Drawing.Size(85, 59)
        Me.Panel30.TabIndex = 124
        '
        'TxtPcsOrder
        '
        Me.TxtPcsOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtPcsOrder.Location = New System.Drawing.Point(2, 30)
        Me.TxtPcsOrder.Name = "TxtPcsOrder"
        Me.TxtPcsOrder.Size = New System.Drawing.Size(81, 28)
        Me.TxtPcsOrder.TabIndex = 2
        Me.TxtPcsOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel31
        '
        Me.Panel31.Controls.Add(Me.Label19)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(2, 0)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel31.Size = New System.Drawing.Size(81, 30)
        Me.Panel31.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.Location = New System.Drawing.Point(0, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(81, 26)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Pcs Order"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.TxtQtyPerCase)
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel18.Location = New System.Drawing.Point(94, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel18.Size = New System.Drawing.Size(60, 59)
        Me.Panel18.TabIndex = 123
        '
        'TxtQtyPerCase
        '
        Me.TxtQtyPerCase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtQtyPerCase.Location = New System.Drawing.Point(2, 30)
        Me.TxtQtyPerCase.Name = "TxtQtyPerCase"
        Me.TxtQtyPerCase.ReadOnly = True
        Me.TxtQtyPerCase.Size = New System.Drawing.Size(56, 28)
        Me.TxtQtyPerCase.TabIndex = 2
        Me.TxtQtyPerCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.Label11)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(2, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel19.Size = New System.Drawing.Size(56, 30)
        Me.Panel19.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(0, 2)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 26)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Q/C"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel14
        '
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(2, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel14.Size = New System.Drawing.Size(92, 59)
        Me.Panel14.TabIndex = 127
        '
        'DgvShow
        '
        Me.DgvShow.AllowUserToAddRows = False
        Me.DgvShow.AllowUserToDeleteRows = False
        Me.DgvShow.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.DgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ManualRenew, Me.ManualNotAccept, Me.ManualChangeQty, Me.Renew, Me.NotAccept, Me.ChangeQty, Me.Id, Me.CusNum, Me.CusName, Me.DeltoId, Me.Delto, Me.UnitNumber, Me.Barcode, Me.ProName, Me.Size, Me.QtyPerCase, Me.PcsOrder, Me.CTNOrder, Me.TotalPcsOrder, Me.SupNum, Me.SupName, Me.Department, Me.PlanningOrder, Me.MachineName, Me.IPAddress, Me.CreatedDate, Me.VerifyDate, Me.RequiredDate})
        Me.DgvShow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DgvShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvShow.Location = New System.Drawing.Point(5, 233)
        Me.DgvShow.Name = "DgvShow"
        Me.DgvShow.ReadOnly = True
        Me.DgvShow.RowHeadersWidth = 25
        Me.DgvShow.Size = New System.Drawing.Size(824, 288)
        Me.DgvShow.TabIndex = 111
        '
        'ManualRenew
        '
        Me.ManualRenew.HeaderText = "R"
        Me.ManualRenew.Name = "ManualRenew"
        Me.ManualRenew.ReadOnly = True
        Me.ManualRenew.Width = 23
        '
        'ManualNotAccept
        '
        Me.ManualNotAccept.HeaderText = "N"
        Me.ManualNotAccept.Name = "ManualNotAccept"
        Me.ManualNotAccept.ReadOnly = True
        Me.ManualNotAccept.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ManualNotAccept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ManualNotAccept.Width = 42
        '
        'ManualChangeQty
        '
        Me.ManualChangeQty.HeaderText = "C"
        Me.ManualChangeQty.Name = "ManualChangeQty"
        Me.ManualChangeQty.ReadOnly = True
        Me.ManualChangeQty.Width = 23
        '
        'Renew
        '
        Me.Renew.DataPropertyName = "Renew"
        Me.Renew.HeaderText = "R"
        Me.Renew.Name = "Renew"
        Me.Renew.ReadOnly = True
        Me.Renew.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Renew.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Renew.Visible = False
        Me.Renew.Width = 42
        '
        'NotAccept
        '
        Me.NotAccept.DataPropertyName = "NotAccept"
        Me.NotAccept.HeaderText = "N"
        Me.NotAccept.Name = "NotAccept"
        Me.NotAccept.ReadOnly = True
        Me.NotAccept.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NotAccept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.NotAccept.Visible = False
        Me.NotAccept.Width = 42
        '
        'ChangeQty
        '
        Me.ChangeQty.DataPropertyName = "ChangeQty"
        Me.ChangeQty.HeaderText = "C"
        Me.ChangeQty.Name = "ChangeQty"
        Me.ChangeQty.ReadOnly = True
        Me.ChangeQty.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChangeQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ChangeQty.Visible = False
        Me.ChangeQty.Width = 42
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
        'CusNum
        '
        Me.CusNum.DataPropertyName = "CusNum"
        Me.CusNum.HeaderText = "CusNum"
        Me.CusNum.Name = "CusNum"
        Me.CusNum.ReadOnly = True
        Me.CusNum.Visible = False
        Me.CusNum.Width = 79
        '
        'CusName
        '
        Me.CusName.DataPropertyName = "CusName"
        Me.CusName.HeaderText = "Customer Name"
        Me.CusName.Name = "CusName"
        Me.CusName.ReadOnly = True
        Me.CusName.Width = 120
        '
        'DeltoId
        '
        Me.DeltoId.DataPropertyName = "DeltoId"
        Me.DeltoId.HeaderText = "Delto #"
        Me.DeltoId.Name = "DeltoId"
        Me.DeltoId.ReadOnly = True
        Me.DeltoId.Width = 72
        '
        'Delto
        '
        Me.Delto.DataPropertyName = "Delto"
        Me.Delto.HeaderText = "Delto"
        Me.Delto.Name = "Delto"
        Me.Delto.ReadOnly = True
        Me.Delto.Width = 63
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
        'ProName
        '
        Me.ProName.DataPropertyName = "ProName"
        Me.ProName.HeaderText = "Product Name"
        Me.ProName.Name = "ProName"
        Me.ProName.ReadOnly = True
        Me.ProName.Width = 111
        '
        'Size
        '
        Me.Size.DataPropertyName = "Size"
        Me.Size.HeaderText = "Size"
        Me.Size.Name = "Size"
        Me.Size.ReadOnly = True
        Me.Size.Width = 58
        '
        'QtyPerCase
        '
        Me.QtyPerCase.DataPropertyName = "QtyPerCase"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.QtyPerCase.DefaultCellStyle = DataGridViewCellStyle1
        Me.QtyPerCase.HeaderText = "Q/C"
        Me.QtyPerCase.Name = "QtyPerCase"
        Me.QtyPerCase.ReadOnly = True
        Me.QtyPerCase.Width = 56
        '
        'PcsOrder
        '
        Me.PcsOrder.DataPropertyName = "PcsOrder"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Format = "N0"
        Me.PcsOrder.DefaultCellStyle = DataGridViewCellStyle2
        Me.PcsOrder.HeaderText = "Pcs Order"
        Me.PcsOrder.Name = "PcsOrder"
        Me.PcsOrder.ReadOnly = True
        Me.PcsOrder.Width = 88
        '
        'CTNOrder
        '
        Me.CTNOrder.DataPropertyName = "CTNOrder"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "N2"
        Me.CTNOrder.DefaultCellStyle = DataGridViewCellStyle3
        Me.CTNOrder.HeaderText = "CTN Order"
        Me.CTNOrder.Name = "CTNOrder"
        Me.CTNOrder.ReadOnly = True
        Me.CTNOrder.Width = 91
        '
        'TotalPcsOrder
        '
        Me.TotalPcsOrder.DataPropertyName = "TotalPcsOrder"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Format = "N0"
        Me.TotalPcsOrder.DefaultCellStyle = DataGridViewCellStyle4
        Me.TotalPcsOrder.HeaderText = "Total Pcs Order"
        Me.TotalPcsOrder.Name = "TotalPcsOrder"
        Me.TotalPcsOrder.ReadOnly = True
        Me.TotalPcsOrder.Width = 119
        '
        'SupNum
        '
        Me.SupNum.DataPropertyName = "SupNum"
        Me.SupNum.HeaderText = "SupNum"
        Me.SupNum.Name = "SupNum"
        Me.SupNum.ReadOnly = True
        Me.SupNum.Visible = False
        Me.SupNum.Width = 79
        '
        'SupName
        '
        Me.SupName.DataPropertyName = "SupName"
        Me.SupName.HeaderText = "SupName"
        Me.SupName.Name = "SupName"
        Me.SupName.ReadOnly = True
        Me.SupName.Visible = False
        Me.SupName.Width = 86
        '
        'Department
        '
        Me.Department.DataPropertyName = "Department"
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        Me.Department.Visible = False
        Me.Department.Width = 98
        '
        'PlanningOrder
        '
        Me.PlanningOrder.DataPropertyName = "PlanningOrder"
        Me.PlanningOrder.HeaderText = "PlanningOrder"
        Me.PlanningOrder.Name = "PlanningOrder"
        Me.PlanningOrder.ReadOnly = True
        Me.PlanningOrder.Visible = False
        Me.PlanningOrder.Width = 113
        '
        'MachineName
        '
        Me.MachineName.DataPropertyName = "MachineName"
        Me.MachineName.HeaderText = "MachineName"
        Me.MachineName.Name = "MachineName"
        Me.MachineName.ReadOnly = True
        Me.MachineName.Visible = False
        Me.MachineName.Width = 112
        '
        'IPAddress
        '
        Me.IPAddress.DataPropertyName = "IPAddress"
        Me.IPAddress.HeaderText = "IPAddress"
        Me.IPAddress.Name = "IPAddress"
        Me.IPAddress.ReadOnly = True
        Me.IPAddress.Visible = False
        Me.IPAddress.Width = 90
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
        'VerifyDate
        '
        Me.VerifyDate.DataPropertyName = "VerifyDate"
        Me.VerifyDate.HeaderText = "Verify Date"
        Me.VerifyDate.Name = "VerifyDate"
        Me.VerifyDate.ReadOnly = True
        Me.VerifyDate.Width = 95
        '
        'RequiredDate
        '
        Me.RequiredDate.DataPropertyName = "RequiredDate"
        DataGridViewCellStyle5.Format = "dd-MMM-yyyy"
        Me.RequiredDate.DefaultCellStyle = DataGridViewCellStyle5
        Me.RequiredDate.HeaderText = "Required Date"
        Me.RequiredDate.Name = "RequiredDate"
        Me.RequiredDate.ReadOnly = True
        Me.RequiredDate.Width = 120
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.LblTotalDelto)
        Me.Panel44.Controls.Add(Me.LblTotalCustomer)
        Me.Panel44.Controls.Add(Me.BtnRetrieveTakeOrder)
        Me.Panel44.Controls.Add(Me.LblCountRow)
        Me.Panel44.Controls.Add(Me.BtnFinish)
        Me.Panel44.Controls.Add(Me.BtnClose)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(5, 521)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.Panel44.Size = New System.Drawing.Size(824, 35)
        Me.Panel44.TabIndex = 112
        '
        'LblTotalDelto
        '
        Me.LblTotalDelto.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblTotalDelto.ForeColor = System.Drawing.Color.Blue
        Me.LblTotalDelto.Location = New System.Drawing.Point(275, 2)
        Me.LblTotalDelto.Name = "LblTotalDelto"
        Me.LblTotalDelto.Size = New System.Drawing.Size(118, 31)
        Me.LblTotalDelto.TabIndex = 18
        Me.LblTotalDelto.Text = "♦ Total Delto = 0"
        Me.LblTotalDelto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalCustomer
        '
        Me.LblTotalCustomer.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblTotalCustomer.ForeColor = System.Drawing.Color.Blue
        Me.LblTotalCustomer.Location = New System.Drawing.Point(122, 2)
        Me.LblTotalCustomer.Name = "LblTotalCustomer"
        Me.LblTotalCustomer.Size = New System.Drawing.Size(153, 31)
        Me.LblTotalCustomer.TabIndex = 17
        Me.LblTotalCustomer.Text = "♦ Total Customer = 0"
        Me.LblTotalCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnRetrieveTakeOrder
        '
        Me.BtnRetrieveTakeOrder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnRetrieveTakeOrder.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnRetrieveTakeOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnRetrieveTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.retrieve
        Me.BtnRetrieveTakeOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnRetrieveTakeOrder.Location = New System.Drawing.Point(392, 2)
        Me.BtnRetrieveTakeOrder.Name = "BtnRetrieveTakeOrder"
        Me.BtnRetrieveTakeOrder.Size = New System.Drawing.Size(160, 31)
        Me.BtnRetrieveTakeOrder.TabIndex = 12
        Me.BtnRetrieveTakeOrder.Text = "&Retrieve Take Order"
        Me.BtnRetrieveTakeOrder.UseVisualStyleBackColor = True
        '
        'LblCountRow
        '
        Me.LblCountRow.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblCountRow.Location = New System.Drawing.Point(0, 2)
        Me.LblCountRow.Name = "LblCountRow"
        Me.LblCountRow.Size = New System.Drawing.Size(122, 31)
        Me.LblCountRow.TabIndex = 11
        Me.LblCountRow.Text = "Count Row : 0"
        Me.LblCountRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnFinish
        '
        Me.BtnFinish.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFinish.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnFinish.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFinish.Image = Global.DeliveryTakeOrder.My.Resources.Resources.forward
        Me.BtnFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFinish.Location = New System.Drawing.Point(552, 2)
        Me.BtnFinish.Name = "BtnFinish"
        Me.BtnFinish.Size = New System.Drawing.Size(160, 31)
        Me.BtnFinish.TabIndex = 10
        Me.BtnFinish.Text = "&Forward Take Order"
        Me.BtnFinish.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnClose.Image = Global.DeliveryTakeOrder.My.Resources.Resources.cancel16
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnClose.Location = New System.Drawing.Point(712, 2)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(112, 31)
        Me.BtnClose.TabIndex = 9
        Me.BtnClose.Text = "&Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BillToLoading
        '
        Me.BillToLoading.Interval = 5
        '
        'DeltoLoading
        '
        Me.DeltoLoading.Interval = 5
        '
        'ItemLoading
        '
        Me.ItemLoading.Interval = 5
        '
        'PlanningOrderLoading
        '
        Me.PlanningOrderLoading.Interval = 5
        '
        'DisplayLoading
        '
        Me.DisplayLoading.Interval = 5
        '
        'Popmain
        '
        Me.Popmain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuChangeCustomer})
        Me.Popmain.Name = "ContextMenuStrip1"
        Me.Popmain.Size = New System.Drawing.Size(171, 26)
        '
        'MnuChangeCustomer
        '
        Me.MnuChangeCustomer.Image = Global.DeliveryTakeOrder.My.Resources.Resources.update_blue
        Me.MnuChangeCustomer.Name = "MnuChangeCustomer"
        Me.MnuChangeCustomer.Size = New System.Drawing.Size(170, 22)
        Me.MnuChangeCustomer.Text = "&Change Customer"
        '
        'PCustomerRemark
        '
        Me.PCustomerRemark.BackColor = System.Drawing.Color.Red
        Me.PCustomerRemark.Controls.Add(Me.TxtCustomerRemark)
        Me.PCustomerRemark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PCustomerRemark.Location = New System.Drawing.Point(5, 233)
        Me.PCustomerRemark.Name = "PCustomerRemark"
        Me.PCustomerRemark.Padding = New System.Windows.Forms.Padding(4)
        Me.PCustomerRemark.Size = New System.Drawing.Size(824, 288)
        Me.PCustomerRemark.TabIndex = 13
        Me.PCustomerRemark.Visible = False
        '
        'TxtCustomerRemark
        '
        Me.TxtCustomerRemark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtCustomerRemark.Location = New System.Drawing.Point(4, 4)
        Me.TxtCustomerRemark.Multiline = True
        Me.TxtCustomerRemark.Name = "TxtCustomerRemark"
        Me.TxtCustomerRemark.ReadOnly = True
        Me.TxtCustomerRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtCustomerRemark.Size = New System.Drawing.Size(816, 280)
        Me.TxtCustomerRemark.TabIndex = 11
        '
        'FrmDutchmillTakeOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 561)
        Me.Controls.Add(Me.PCustomerRemark)
        Me.Controls.Add(Me.DgvShow)
        Me.Controls.Add(Me.Panel44)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmDutchmillTakeOrder"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dutchmill Planning Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.PanelHeader.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.PicLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel34.ResumeLayout(False)
        Me.Panel34.PerformLayout()
        Me.Panel35.ResumeLayout(False)
        Me.Panel32.ResumeLayout(False)
        Me.Panel32.PerformLayout()
        Me.Panel33.ResumeLayout(False)
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        Me.Panel31.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        CType(Me.DgvShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel44.ResumeLayout(False)
        Me.Popmain.ResumeLayout(False)
        Me.PCustomerRemark.ResumeLayout(False)
        Me.PCustomerRemark.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents LblCompanyName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PanelHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents CmbPlanningOrder As System.Windows.Forms.ComboBox
    Friend WithEvents PicLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DgvShow As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtKhmerName As System.Windows.Forms.TextBox
    Friend WithEvents TxtZone As System.Windows.Forms.TextBox
    Friend WithEvents CmbDelto As System.Windows.Forms.ComboBox
    Friend WithEvents CmbProducts As System.Windows.Forms.ComboBox
    Friend WithEvents CmbBillTo As System.Windows.Forms.ComboBox
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Friend WithEvents LblCountRow As System.Windows.Forms.Label
    Friend WithEvents BtnFinish As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnRetrieveTakeOrder As System.Windows.Forms.Button
    Friend WithEvents BillToLoading As System.Windows.Forms.Timer
    Friend WithEvents DeltoLoading As System.Windows.Forms.Timer
    Friend WithEvents ItemLoading As System.Windows.Forms.Timer
    Friend WithEvents PlanningOrderLoading As System.Windows.Forms.Timer
    Friend WithEvents DisplayLoading As System.Windows.Forms.Timer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents TxtTotalPcsOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents TxtCTNOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents TxtPcsOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents TxtQtyPerCase As System.Windows.Forms.TextBox
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblTeam As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LblTotalDelto As System.Windows.Forms.Label
    Friend WithEvents LblTotalCustomer As System.Windows.Forms.Label
    Friend WithEvents Popmain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MnuChangeCustomer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualRenew As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ManualNotAccept As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ManualChangeQty As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Renew As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NotAccept As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ChangeQty As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CusName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeltoId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Size As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QtyPerCase As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PcsOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CTNOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalPcsOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SupNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SupName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PlanningOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MachineName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IPAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreatedDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VerifyDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequiredDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnViewCredit As System.Windows.Forms.Button
    Friend WithEvents PCustomerRemark As Panel
    Friend WithEvents TxtCustomerRemark As TextBox
End Class
