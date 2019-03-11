<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDI))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDeliveryTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuUnlockPrinterTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSetPiecesPerTray = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuDutchmillOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTakeOrderForDutchmill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDownloadTakeOrderFromSaleTeam = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuProcessTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuDeltoListForDutchmillPO = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTakeOrderProcessing = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuChangePassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Loading = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuViewProcessingTakeOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(632, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuDeliveryTakeOrder, Me.MnuUnlockPrinterTakeOrder, Me.ToolStripMenuItem2, Me.ToolStripSeparator2, Me.MnuViewProcessingTakeOrder, Me.ToolStripMenuItem1, Me.MnuChangePassword, Me.ToolStripMenuItem3, Me.MnuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.FileToolStripMenuItem.Text = "&File     "
        '
        'MnuDeliveryTakeOrder
        '
        Me.MnuDeliveryTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.po
        Me.MnuDeliveryTakeOrder.Name = "MnuDeliveryTakeOrder"
        Me.MnuDeliveryTakeOrder.Size = New System.Drawing.Size(214, 22)
        Me.MnuDeliveryTakeOrder.Text = "&Delivery Take Order"
        '
        'MnuUnlockPrinterTakeOrder
        '
        Me.MnuUnlockPrinterTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Unlock_Printer
        Me.MnuUnlockPrinterTakeOrder.Name = "MnuUnlockPrinterTakeOrder"
        Me.MnuUnlockPrinterTakeOrder.Size = New System.Drawing.Size(214, 22)
        Me.MnuUnlockPrinterTakeOrder.Text = "&Unlock Printer Take Order"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSetPiecesPerTray, Me.ToolStripSeparator1, Me.MnuDutchmillOrder, Me.MnuTakeOrderForDutchmill, Me.MnuDownloadTakeOrderFromSaleTeam, Me.MnuProcessTakeOrder, Me.ToolStripMenuItem4, Me.MnuDeltoListForDutchmillPO, Me.MnuTakeOrderProcessing})
        Me.ToolStripMenuItem2.Image = Global.DeliveryTakeOrder.My.Resources.Resources._to
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(214, 22)
        Me.ToolStripMenuItem2.Text = "&Dutchmill"
        '
        'MnuSetPiecesPerTray
        '
        Me.MnuSetPiecesPerTray.Image = Global.DeliveryTakeOrder.My.Resources.Resources.tray
        Me.MnuSetPiecesPerTray.Name = "MnuSetPiecesPerTray"
        Me.MnuSetPiecesPerTray.Size = New System.Drawing.Size(275, 22)
        Me.MnuSetPiecesPerTray.Text = "&Set Pieces Per Tray"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(272, 6)
        '
        'MnuDutchmillOrder
        '
        Me.MnuDutchmillOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Check_Arrival
        Me.MnuDutchmillOrder.Name = "MnuDutchmillOrder"
        Me.MnuDutchmillOrder.Size = New System.Drawing.Size(275, 22)
        Me.MnuDutchmillOrder.Text = "Dutchmill &Order"
        '
        'MnuTakeOrderForDutchmill
        '
        Me.MnuTakeOrderForDutchmill.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MnuTakeOrderForDutchmill.Image = Global.DeliveryTakeOrder.My.Resources.Resources.circle_old_code
        Me.MnuTakeOrderForDutchmill.Name = "MnuTakeOrderForDutchmill"
        Me.MnuTakeOrderForDutchmill.Size = New System.Drawing.Size(275, 22)
        Me.MnuTakeOrderForDutchmill.Text = "&Take Order"
        Me.MnuTakeOrderForDutchmill.Visible = False
        '
        'MnuDownloadTakeOrderFromSaleTeam
        '
        Me.MnuDownloadTakeOrderFromSaleTeam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.MnuDownloadTakeOrderFromSaleTeam.Image = Global.DeliveryTakeOrder.My.Resources.Resources.download
        Me.MnuDownloadTakeOrderFromSaleTeam.Name = "MnuDownloadTakeOrderFromSaleTeam"
        Me.MnuDownloadTakeOrderFromSaleTeam.Size = New System.Drawing.Size(275, 22)
        Me.MnuDownloadTakeOrderFromSaleTeam.Text = "D&ownload Take Order From Sale Team"
        Me.MnuDownloadTakeOrderFromSaleTeam.Visible = False
        '
        'MnuProcessTakeOrder
        '
        Me.MnuProcessTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.update_16
        Me.MnuProcessTakeOrder.Name = "MnuProcessTakeOrder"
        Me.MnuProcessTakeOrder.Size = New System.Drawing.Size(275, 22)
        Me.MnuProcessTakeOrder.Text = "&Process Take Order"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(272, 6)
        '
        'MnuDeltoListForDutchmillPO
        '
        Me.MnuDeltoListForDutchmillPO.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Separately
        Me.MnuDeltoListForDutchmillPO.Name = "MnuDeltoListForDutchmillPO"
        Me.MnuDeltoListForDutchmillPO.Size = New System.Drawing.Size(275, 22)
        Me.MnuDeltoListForDutchmillPO.Text = "&Delto List For Dutchmill P.O"
        '
        'MnuTakeOrderProcessing
        '
        Me.MnuTakeOrderProcessing.Image = Global.DeliveryTakeOrder.My.Resources.Resources.circle_old_code
        Me.MnuTakeOrderProcessing.Name = "MnuTakeOrderProcessing"
        Me.MnuTakeOrderProcessing.Size = New System.Drawing.Size(275, 22)
        Me.MnuTakeOrderProcessing.Text = "&Take Order Processing"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(211, 6)
        '
        'MnuChangePassword
        '
        Me.MnuChangePassword.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Password
        Me.MnuChangePassword.Name = "MnuChangePassword"
        Me.MnuChangePassword.Size = New System.Drawing.Size(214, 22)
        Me.MnuChangePassword.Text = "&Change Password"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(211, 6)
        '
        'MnuExit
        '
        Me.MnuExit.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Close
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Size = New System.Drawing.Size(214, 22)
        Me.MnuExit.Text = "&Exit"
        '
        'Loading
        '
        Me.Loading.Interval = 5
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(211, 6)
        '
        'MnuViewProcessingTakeOrder
        '
        Me.MnuViewProcessingTakeOrder.Image = Global.DeliveryTakeOrder.My.Resources.Resources.Search16
        Me.MnuViewProcessingTakeOrder.Name = "MnuViewProcessingTakeOrder"
        Me.MnuViewProcessingTakeOrder.Size = New System.Drawing.Size(214, 22)
        Me.MnuViewProcessingTakeOrder.Text = "&View Processing Takeorder"
        '
        'MDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MDI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delivery Take Order"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuChangePassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuDeliveryTakeOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTakeOrderForDutchmill As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuProcessTakeOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuDeltoListForDutchmillPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSetPiecesPerTray As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuDownloadTakeOrderFromSaleTeam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDutchmillOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUnlockPrinterTakeOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTakeOrderProcessing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Loading As Timer
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents MnuViewProcessingTakeOrder As ToolStripMenuItem
End Class
