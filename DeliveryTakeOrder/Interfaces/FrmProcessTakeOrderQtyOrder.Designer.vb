<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessTakeOrderQtyOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProcessTakeOrderQtyOrder))
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Panel42 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TxtProName = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.TxtSize = New System.Windows.Forms.TextBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.TxtQtyPerCase = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TxtBarcode = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.TxtNewPcsOrder = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtOldPcsOrder = New System.Windows.Forms.TextBox()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TxtNewPackOrder = New System.Windows.Forms.TextBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtOldPackOrder = New System.Windows.Forms.TextBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.TxtNewCTNOrder = New System.Windows.Forms.TextBox()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtOldCTNOrder = New System.Windows.Forms.TextBox()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel44.SuspendLayout()
        Me.Panel42.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.BtnUpdate)
        Me.Panel44.Controls.Add(Me.BtnCancel)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel44.Location = New System.Drawing.Point(6, 208)
        Me.Panel44.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel44.Size = New System.Drawing.Size(516, 36)
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
        Me.BtnUpdate.Location = New System.Drawing.Point(331, 3)
        Me.BtnUpdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(99, 30)
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
        Me.BtnCancel.Location = New System.Drawing.Point(430, 3)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 30)
        Me.BtnCancel.TabIndex = 11
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Panel42
        '
        Me.Panel42.Controls.Add(Me.Panel5)
        Me.Panel42.Controls.Add(Me.Panel9)
        Me.Panel42.Controls.Add(Me.Panel7)
        Me.Panel42.Controls.Add(Me.Panel3)
        Me.Panel42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel42.Location = New System.Drawing.Point(6, 7)
        Me.Panel42.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel42.Name = "Panel42"
        Me.Panel42.Size = New System.Drawing.Size(516, 54)
        Me.Panel42.TabIndex = 111
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.TxtProName)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(101, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel5.Size = New System.Drawing.Size(306, 54)
        Me.Panel5.TabIndex = 114
        '
        'TxtProName
        '
        Me.TxtProName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtProName.Location = New System.Drawing.Point(2, 25)
        Me.TxtProName.Name = "TxtProName"
        Me.TxtProName.ReadOnly = True
        Me.TxtProName.Size = New System.Drawing.Size(302, 28)
        Me.TxtProName.TabIndex = 115
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(2, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(302, 25)
        Me.Panel6.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(0, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(302, 19)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Product Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.TxtSize)
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel9.Location = New System.Drawing.Point(407, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel9.Size = New System.Drawing.Size(61, 54)
        Me.Panel9.TabIndex = 116
        '
        'TxtSize
        '
        Me.TxtSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtSize.Location = New System.Drawing.Point(2, 25)
        Me.TxtSize.Name = "TxtSize"
        Me.TxtSize.ReadOnly = True
        Me.TxtSize.Size = New System.Drawing.Size(57, 28)
        Me.TxtSize.TabIndex = 115
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Label5)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(2, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(57, 25)
        Me.Panel10.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(0, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 19)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Size"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.TxtQtyPerCase)
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(468, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel7.Size = New System.Drawing.Size(48, 54)
        Me.Panel7.TabIndex = 115
        '
        'TxtQtyPerCase
        '
        Me.TxtQtyPerCase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtQtyPerCase.Location = New System.Drawing.Point(2, 25)
        Me.TxtQtyPerCase.Name = "TxtQtyPerCase"
        Me.TxtQtyPerCase.ReadOnly = True
        Me.TxtQtyPerCase.Size = New System.Drawing.Size(44, 28)
        Me.TxtQtyPerCase.TabIndex = 115
        Me.TxtQtyPerCase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(2, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel8.Size = New System.Drawing.Size(44, 25)
        Me.Panel8.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(0, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 19)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Q/C"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TxtBarcode)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel3.Size = New System.Drawing.Size(101, 54)
        Me.Panel3.TabIndex = 113
        '
        'TxtBarcode
        '
        Me.TxtBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtBarcode.Location = New System.Drawing.Point(2, 25)
        Me.TxtBarcode.Name = "TxtBarcode"
        Me.TxtBarcode.ReadOnly = True
        Me.TxtBarcode.Size = New System.Drawing.Size(97, 28)
        Me.TxtBarcode.TabIndex = 115
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(2, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(97, 25)
        Me.Panel4.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Barcode"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.TxtNewPcsOrder)
        Me.Panel16.Controls.Add(Me.Panel1)
        Me.Panel16.Controls.Add(Me.TxtOldPcsOrder)
        Me.Panel16.Controls.Add(Me.Panel17)
        Me.Panel16.Location = New System.Drawing.Point(91, 82)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel16.Size = New System.Drawing.Size(346, 30)
        Me.Panel16.TabIndex = 114
        '
        'TxtNewPcsOrder
        '
        Me.TxtNewPcsOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtNewPcsOrder.Location = New System.Drawing.Point(278, 0)
        Me.TxtNewPcsOrder.Name = "TxtNewPcsOrder"
        Me.TxtNewPcsOrder.Size = New System.Drawing.Size(66, 28)
        Me.TxtNewPcsOrder.TabIndex = 117
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(171, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(107, 30)
        Me.Panel1.TabIndex = 116
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "New Pcs Order"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtOldPcsOrder
        '
        Me.TxtOldPcsOrder.Dock = System.Windows.Forms.DockStyle.Left
        Me.TxtOldPcsOrder.Location = New System.Drawing.Point(101, 0)
        Me.TxtOldPcsOrder.Name = "TxtOldPcsOrder"
        Me.TxtOldPcsOrder.ReadOnly = True
        Me.TxtOldPcsOrder.Size = New System.Drawing.Size(70, 28)
        Me.TxtOldPcsOrder.TabIndex = 115
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.Label8)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel17.Location = New System.Drawing.Point(2, 0)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel17.Size = New System.Drawing.Size(99, 30)
        Me.Panel17.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(0, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 24)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Old Pcs Order"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TxtNewPackOrder)
        Me.Panel2.Controls.Add(Me.Panel11)
        Me.Panel2.Controls.Add(Me.TxtOldPackOrder)
        Me.Panel2.Controls.Add(Me.Panel12)
        Me.Panel2.Location = New System.Drawing.Point(91, 120)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel2.Size = New System.Drawing.Size(346, 30)
        Me.Panel2.TabIndex = 115
        '
        'TxtNewPackOrder
        '
        Me.TxtNewPackOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtNewPackOrder.Location = New System.Drawing.Point(278, 0)
        Me.TxtNewPackOrder.Name = "TxtNewPackOrder"
        Me.TxtNewPackOrder.Size = New System.Drawing.Size(66, 28)
        Me.TxtNewPackOrder.TabIndex = 117
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label6)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel11.Location = New System.Drawing.Point(171, 0)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(107, 30)
        Me.Panel11.TabIndex = 116
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(0, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "New Pack Order"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtOldPackOrder
        '
        Me.TxtOldPackOrder.Dock = System.Windows.Forms.DockStyle.Left
        Me.TxtOldPackOrder.Location = New System.Drawing.Point(101, 0)
        Me.TxtOldPackOrder.Name = "TxtOldPackOrder"
        Me.TxtOldPackOrder.ReadOnly = True
        Me.TxtOldPackOrder.Size = New System.Drawing.Size(70, 28)
        Me.TxtOldPackOrder.TabIndex = 115
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Label7)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel12.Location = New System.Drawing.Point(2, 0)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel12.Size = New System.Drawing.Size(99, 30)
        Me.Panel12.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(0, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 24)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Old Pack Order"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.TxtNewCTNOrder)
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Controls.Add(Me.TxtOldCTNOrder)
        Me.Panel13.Controls.Add(Me.Panel15)
        Me.Panel13.Location = New System.Drawing.Point(91, 158)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Panel13.Size = New System.Drawing.Size(346, 30)
        Me.Panel13.TabIndex = 116
        '
        'TxtNewCTNOrder
        '
        Me.TxtNewCTNOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtNewCTNOrder.Location = New System.Drawing.Point(278, 0)
        Me.TxtNewCTNOrder.Name = "TxtNewCTNOrder"
        Me.TxtNewCTNOrder.Size = New System.Drawing.Size(66, 28)
        Me.TxtNewCTNOrder.TabIndex = 117
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label9)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(171, 0)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel14.Size = New System.Drawing.Size(107, 30)
        Me.Panel14.TabIndex = 116
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(107, 24)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "New CTN Order"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtOldCTNOrder
        '
        Me.TxtOldCTNOrder.Dock = System.Windows.Forms.DockStyle.Left
        Me.TxtOldCTNOrder.Location = New System.Drawing.Point(101, 0)
        Me.TxtOldCTNOrder.Name = "TxtOldCTNOrder"
        Me.TxtOldCTNOrder.ReadOnly = True
        Me.TxtOldCTNOrder.Size = New System.Drawing.Size(70, 28)
        Me.TxtOldCTNOrder.TabIndex = 115
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Label10)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Location = New System.Drawing.Point(2, 0)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(99, 30)
        Me.Panel15.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Location = New System.Drawing.Point(0, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 24)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Old CTN Order"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmProcessTakeOrderQtyOrder
        '
        Me.AcceptButton = Me.BtnUpdate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(528, 251)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel16)
        Me.Controls.Add(Me.Panel42)
        Me.Controls.Add(Me.Panel44)
        Me.Font = New System.Drawing.Font("Khmer OS Battambang", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmProcessTakeOrderQtyOrder"
        Me.Padding = New System.Windows.Forms.Padding(6, 7, 6, 7)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Qty Order"
        Me.Panel44.ResumeLayout(False)
        Me.Panel42.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
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
    Friend WithEvents TxtProName As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents TxtSize As System.Windows.Forms.TextBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents TxtQtyPerCase As System.Windows.Forms.TextBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents TxtNewPcsOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtOldPcsOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TxtNewPackOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtOldPackOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents TxtNewCTNOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtOldCTNOrder As System.Windows.Forms.TextBox
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
