Imports System.Windows.Forms
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Drawing2D

Namespace ApplicationFrameworks
    Public Class ApplicationFramework
        Public Function IsNotAllowBlank(ByVal ControlName() As Object, ByVal ProviderName As ErrorProvider, Optional ByVal Message As String = "Cannot Zero!", Optional ByVal Alignment As ErrorIconAlignment = ErrorIconAlignment.MiddleRight, Optional ByVal Style As ErrorBlinkStyle = ErrorBlinkStyle.NeverBlink, Optional ByVal ConditionIsZero As Boolean = False) As Boolean
            Dim R_Return As Boolean = False
            Dim R_IsBlank As Boolean = False
            For Each Item As Control In ControlName
                If TypeOf Item Is TextBox Or TypeOf Item Is ComboBox Or TypeOf Item Is MaskedTextBox Then
                    If ConditionIsZero = True Then
                        If Trim(Item.Text) = 0 Then
                            R_IsBlank = True
                        Else
                            R_IsBlank = False
                        End If
                    Else
                        If String.IsNullOrEmpty(Item.Text) = True Then
                            R_IsBlank = True
                        Else
                            R_IsBlank = False
                        End If
                    End If
                ElseIf TypeOf Item Is CheckBox Then
                    If CType(Item, CheckBox).Checked = False Then
                        R_IsBlank = True
                    Else
                        R_IsBlank = False
                    End If
                ElseIf TypeOf Item Is RadioButton Then
                    If CType(Item, RadioButton).Checked = False Then
                        R_IsBlank = True
                    Else
                        R_IsBlank = False
                    End If
                ElseIf TypeOf Item Is PictureBox Then
                    If CType(Item, PictureBox).Image Is Nothing Then
                        R_IsBlank = True
                    Else
                        R_IsBlank = False
                    End If
                End If
                If R_IsBlank = True Then
                    ProviderName.SetError(Item, Message)
                    ProviderName.SetIconAlignment(Item, Alignment)
                    ProviderName.BlinkStyle = Style
                    R_Return = True
                Else
                    ProviderName.SetError(Item, Nothing)
                End If
            Next
            Return R_Return
        End Function

        Private Key As KeyPressEventArgs
        Enum TypeKeyPress
            Format_Number
            Format_Float
            Format_Amount
            Format_UppercaseText
            Format_LowercaseText
            Normal
        End Enum
        Public Sub KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs, Optional ByVal Type As TypeKeyPress = TypeKeyPress.Format_Number, Optional ByVal AllowCharacter As String = "", Optional ByVal Length As Integer = 100)
            If sender.Text.Length >= Length And e.KeyChar <> ChrW(Keys.Back) Then e.Handled = True
            If Type = TypeKeyPress.Normal Then
                If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) Or (e.KeyChar >= "a" And e.KeyChar <= "z") Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
                    Exit Sub
                Else
                    If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
                End If
            ElseIf Type = TypeKeyPress.Format_UppercaseText Then
                If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) Or (e.KeyChar >= "a" And e.KeyChar <= "z") Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
                    If (e.KeyChar >= "a" And e.KeyChar <= "z") And e.KeyChar <> ChrW(Keys.Return) And e.KeyChar <> ChrW(Keys.Back) Then e.KeyChar = e.KeyChar.ToString.ToUpper
                    Exit Sub
                Else
                    If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
                End If
            ElseIf Type = TypeKeyPress.Format_LowercaseText Then
                If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) Or (e.KeyChar >= "a" And e.KeyChar <= "z") Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
                    If (e.KeyChar >= ChrW(Keys.A) And e.KeyChar <= ChrW(Keys.Z)) And e.KeyChar <> ChrW(Keys.Return) And e.KeyChar <> ChrW(Keys.Back) Then e.KeyChar = e.KeyChar.ToString.ToLower
                    Exit Sub
                Else
                    If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
                End If
            ElseIf Type = TypeKeyPress.Format_Number Then
                If (e.KeyChar >= ChrW(Keys.D0) And e.KeyChar <= ChrW(Keys.D9)) Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Then
                    Exit Sub
                Else
                    If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
                End If
            ElseIf Type = TypeKeyPress.Format_Float Then
                If (e.KeyChar >= ChrW(Keys.D0) And e.KeyChar <= ChrW(Keys.D9)) Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                    If e.KeyChar = "." And InStr(sender.text, ".", CompareMethod.Text) > 0 Then e.Handled = True
                    Exit Sub
                Else
                    If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
                End If
            ElseIf Type = TypeKeyPress.Format_Amount Then
                If (e.KeyChar >= ChrW(Keys.D0) And e.KeyChar <= ChrW(Keys.D9)) Or e.KeyChar = ChrW(Keys.Return) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                    If e.KeyChar = "." And InStr(sender.text, ".", CompareMethod.Text) > 0 Then
                        e.Handled = True
                        Exit Sub
                    End If
                    If InStr(sender.text, ".", CompareMethod.Text) > 0 Then Exit Sub
                    Key = e
                    KP = True
                    AddHandler CType(sender, TextBox).TextChanged, AddressOf KeyTextChanged
                    Exit Sub
            Else
                If AllowCharacter.Contains(e.KeyChar) = False Then e.Handled = True
            End If
            End If
        End Sub

        Private Index As Long
        Private R_Text As String
        Private KP As Boolean
        Private Sub KeyTextChanged(sender As Object, e As EventArgs)
            If InStr(sender.text, ".", CompareMethod.Text) > 0 Then Exit Sub
            Try
                If KP = True Then
                    Index = IIf(CType(sender, TextBox).SelectionStart = 0, Index, CType(sender, TextBox).SelectionStart)
                    R_Text = String.Format("{0:#,###,###,###,###,###,###,###,##0}", Convert.ToDecimal(CType(sender, TextBox).Text))
                    'Dim R_More As Integer = R_Text.Count(Function(c As Char) c = ",")
                    If Key.KeyChar = ChrW(Keys.Back) Then
                        Index -= 1
                    Else
                        Index += 1
                    End If
                    KP = False
                    CType(sender, TextBox).Text = R_Text
                End If
                KP = False
                CType(sender, TextBox).SelectionStart = Index
            Catch ex As Exception

            End Try
        End Sub

        Public Function ImagetoByte(ByVal img As Image) As Byte()
            Dim ms As New MemoryStream()
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            Return ms.ToArray()
        End Function

        Public Function BytetoImage(ByVal byt As Byte()) As Image
            If byt Is Nothing Then Return Nothing
            Dim ms = New MemoryStream(byt)
            Dim returnImage As Image = Image.FromStream(ms)
            Return returnImage
        End Function

        Public Function ResizeImage(ByVal Img As Image) As Image
            Dim BitSource As New Bitmap(Img)
            Dim BitDesign As New Bitmap(BitSource.Width * 0.7, BitSource.Height * 0.7, Imaging.PixelFormat.Format24bppRgb) 'Format24bppR
            BitDesign.MakeTransparent(BitDesign.GetPixel(1, 1))
            Dim GraDesign As Graphics = Graphics.FromImage(BitDesign)
            GraDesign.DrawImage(BitSource, 0, 0, BitDesign.Width, BitDesign.Height)
            Img = BitDesign
            Return Img
        End Function

        Public Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
            Dim newWidth As Integer
            Dim newHeight As Integer
            If preserveAspectRatio Then
                Dim originalWidth As Integer = image.Width
                Dim originalHeight As Integer = image.Height
                Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
                Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
                Dim percent As Single = If(percentHeight < percentWidth,
                        percentHeight, percentWidth)
                newWidth = CInt(originalWidth * percent)
                newHeight = CInt(originalHeight * percent)
            Else
                newWidth = size.Width
                newHeight = size.Height
            End If
            Dim newImage As Image = New Bitmap(newWidth, newHeight)
            Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
            End Using
            Return newImage
        End Function

        Public Function GetDayPerMonth(ByVal Dates As Date) As Integer
            Select Case Month(Dates)
                Case 1 : Return 31
                Case 2
                    If Year(Dates) Mod 4 = 0 Then
                        Return 29
                    Else
                        Return 28
                    End If
                Case 3 : Return 31
                Case 4 : Return 30
                Case 5 : Return 31
                Case 6 : Return 30
                Case 7 : Return 31
                Case 8 : Return 31
                Case 9 : Return 30
                Case 10 : Return 31
                Case 11 : Return 30
                Case 12 : Return 31
                Case Else : Return 0
            End Select
        End Function

        Public Function ConvertTextToPassword(ByVal Password As String, Optional ByVal Key As String = "") As String
            Dim EncryptionKey As String = IIf(Trim(Key) = "", "26M0S6R1I9T8H91", Key)
            Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(Password)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(clearBytes, 0, clearBytes.Length)
                        cs.Close()
                    End Using
                    Password = Convert.ToBase64String(ms.ToArray())
                End Using
            End Using
            Return Password
        End Function

        Public Function ConvertPasswordToText(ByVal Password As String, Optional ByVal Key As String = "") As String
            Dim EncryptionKey As String = IIf(Trim(Key) = "", "26M0S6R1I9T8H91", Key)
            Dim cipherBytes As Byte() = Convert.FromBase64String(Password)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                        cs.Write(cipherBytes, 0, cipherBytes.Length)
                        cs.Close()
                    End Using
                    Password = Encoding.Unicode.GetString(ms.ToArray())
                End Using
            End Using
            Return Password
        End Function

        Public Sub SetEnableController(ByVal StatusValue As Boolean, ByVal ParamArray ControlName() As Control)
            For c As Integer = LBound(ControlName) To UBound(ControlName)
                ControlName(c).Enabled = StatusValue
            Next
        End Sub

        Public Sub SetVisibleController(ByVal StatusValue As Boolean, ByVal ParamArray ControlName() As Control)
            For c As Integer = LBound(ControlName) To UBound(ControlName)
                ControlName(c).Visible = StatusValue
            Next
        End Sub

        Public Sub SetReadOnlyController(ByVal StatusValue As Boolean, ByVal ParamArray ControlName() As TextBox)
            For c As Integer = LBound(ControlName) To UBound(ControlName)
                ControlName(c).ReadOnly = StatusValue
            Next
        End Sub

        Public Sub ClearController(ByVal FormName As Form, ByVal ParamArray ExceptionControlName() As Control)
            For Each c As Control In FormName.Controls
                If UBound(ExceptionControlName) > 0 Then
                    For e As Integer = LBound(ExceptionControlName) To UBound(ExceptionControlName)
                        If StrComp(c.Name, ExceptionControlName(e).Name, CompareMethod.Text) = 0 Then
                            GoTo OverStep
                            Exit For
                        End If
                    Next
                End If
                If TypeOf c Is TextBox Then
                    CType(c, TextBox).Text = ""
                Else
                    If TypeOf c Is ComboBox Then
                        If CType(c, ComboBox).DropDownStyle = 1 Then
                            CType(c, ComboBox).Text = ""
                        ElseIf CType(c, ComboBox).DropDownStyle = 2 Then
                            CType(c, ComboBox).SelectedIndex = -1
                        End If
                    End If
                End If
OverStep:
            Next
        End Sub

        Public Sub ClearController(ByVal ParamArray ControlName() As Control)
            For Each c As Control In ControlName
                If TypeOf c Is TextBox Then
                    CType(c, TextBox).Text = ""
                ElseIf TypeOf c Is MaskedTextBox Then
                    CType(c, MaskedTextBox).Text = ""
                ElseIf TypeOf c Is ComboBox Then
                    If CType(c, ComboBox).DropDownStyle = 1 Then
                        CType(c, ComboBox).Text = ""
                    ElseIf CType(c, ComboBox).DropDownStyle = 2 Then
                        CType(c, ComboBox).SelectedIndex = -1
                    End If
                Else
                    If TypeOf c Is CheckBox Then
                        CType(c, CheckBox).Checked = False
                    ElseIf TypeOf c Is RadioButton Then
                        CType(c, RadioButton).Checked = False
                    End If
                End If
            Next
        End Sub

        Public Sub ReleaseObject(ByVal o As Object)
            Try
                While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
                End While
            Catch
            Finally
                o = Nothing
            End Try
        End Sub

        Enum LocationRegistry
            CurrentConfig
            CurrentUser
            ClassesRoot
        End Enum
        Enum RegistryKeyName
            PublicIPAddress
            PortNumber
            IPAddress
            UserConnection
            PasswordConnection
            DatabaseName
        End Enum
        Public Sub SetRegistry(ByVal Name As RegistryKeyName, ByVal Value As String, Optional ByVal FolderName As String = "", Optional ByVal Location As LocationRegistry = LocationRegistry.CurrentUser)
            If Trim(FolderName) = "" Then FolderName = Configurations.FolderName
            Dim KeyName As String = ""
            Dim Master_Key As String = "HKEY_"
            If Location = LocationRegistry.CurrentConfig Then
                If Trim(FolderName) <> "" Then
                    My.Computer.Registry.CurrentConfig.CreateSubKey(FolderName)
                    KeyName = My.Computer.Registry.CurrentConfig.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.CurrentConfig.ToString
                End If
            ElseIf Location = LocationRegistry.CurrentUser Then
                If Trim(FolderName) <> "" Then
                    My.Computer.Registry.CurrentUser.CreateSubKey(FolderName)
                    KeyName = My.Computer.Registry.CurrentUser.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.CurrentUser.ToString
                End If
            ElseIf Location = LocationRegistry.ClassesRoot Then
                If Trim(FolderName) <> "" Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey(FolderName)
                    KeyName = My.Computer.Registry.ClassesRoot.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.ClassesRoot.ToString
                End If
            End If
            My.Computer.Registry.SetValue(KeyName, Master_Key & "" & Name.ToString, Value)
        End Sub

        Public Function GetRegistry(ByVal Name As RegistryKeyName, Optional ByVal FolderName As String = "", Optional ByVal Location As LocationRegistry = LocationRegistry.CurrentUser) As String
            If Trim(FolderName) = "" Then FolderName = Configurations.FolderName
            Dim KeyName As String = ""
            Dim Master_Key As String = "HKEY_"
            If Location = LocationRegistry.CurrentConfig Then
                If Trim(FolderName) <> "" Then
                    KeyName = My.Computer.Registry.CurrentConfig.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.CurrentConfig.ToString
                End If
            ElseIf Location = LocationRegistry.CurrentUser Then
                If Trim(FolderName) <> "" Then
                    KeyName = My.Computer.Registry.CurrentUser.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.CurrentUser.ToString
                End If
            ElseIf Location = LocationRegistry.ClassesRoot Then
                If Trim(FolderName) <> "" Then
                    KeyName = My.Computer.Registry.ClassesRoot.ToString & "\" & FolderName
                Else
                    KeyName = My.Computer.Registry.ClassesRoot.ToString
                End If
            End If
            Return My.Computer.Registry.GetValue(KeyName, Master_Key & "" & Name.ToString, "")
        End Function

        Public Shared Function CheckInternetConnection() As Boolean
            Try
                'My.Computer.Network.IsAvailable
                Using client = New System.Net.WebClient()
                    Using stream = client.OpenRead("http://www.google.com")
                        Return True
                    End Using
                End Using
            Catch
                Return False
            End Try
        End Function

        Public Function CheckConnectionByPing(ByVal IPAddress As String) As Boolean
            Try
                Return My.Computer.Network.Ping(IPAddress)
            Catch
                Return False
            End Try
        End Function

        Public Function MergeObject(ByVal Data As String) As String
            Dim Remove As String = ".,;/\|'{}[]-+*&^%$#@!`~_=?<>(): """
            For Each c As Char In Remove.ToCharArray
                Data = Replace(Data, c, "")
            Next
            Return Data
        End Function
    End Class

    Public Class SwitchKeyboardLanguage
        Public Declare Function GetKeyboardLayoutName Lib "user32" Alias "GetKeyboardLayoutNameA" (ByVal pwszKLID As String) As Long
        Public Declare Function LoadKeyboardLayout Lib "user32" Alias "LoadKeyboardLayoutA" (ByVal pwszKLID As String, ByVal flags As Long) As Long
        Const KLF_ACTIVATE = &H1

        ' some languages code
        'Public Const LANG_ENGLISH As String = "00000409"
        'Public Const LANG_FRENCH As String = "0000040C"
        'Public Const LANG_ARABIC As String = "00000401"
        'Public Const LANG_GREEK As String = "00000408"
        'Public Const LANG_KHMER As String = "a0000403"
        'Public Const LANG_ITALIAN As String = "00000400"
        'Public Const LANG_GERMAN As String = "00000407"

        Enum Type_Of_Language
            LANG_ENGLISH
            LANG_FRENCH
            LANG_ARABIC
            LANG_GREEK
            LANG_KHMER
            LANG_ITALIAN
            LANG_GERMAN
        End Enum

        Public Function SwitchKeyboardLang(ByVal Language As Type_Of_Language) As Boolean
            Dim KeyboardLanguage As String = ""
            If Language = Type_Of_Language.LANG_ARABIC Then
                KeyboardLanguage = "00000401"
            ElseIf Language = Type_Of_Language.LANG_ENGLISH Then
                KeyboardLanguage = "00000409"
            ElseIf Language = Type_Of_Language.LANG_FRENCH Then
                KeyboardLanguage = "0000040C"
            ElseIf Language = Type_Of_Language.LANG_GERMAN Then
                KeyboardLanguage = "00000407"
            ElseIf Language = Type_Of_Language.LANG_GREEK Then
                KeyboardLanguage = "00000408"
            ElseIf Language = Type_Of_Language.LANG_ITALIAN Then
                KeyboardLanguage = "00000400"
            ElseIf Language = Type_Of_Language.LANG_KHMER Then
                KeyboardLanguage = "a0000403"
            End If
            Dim strRet As String
            On Error Resume Next
            strRet = New String("0", 9)
            GetKeyboardLayoutName(strRet)
            If strRet = (KeyboardLanguage & Chr(0)) Then
                SwitchKeyboardLang = True
                Exit Function
            Else
                strRet = New String("0", 9)
                strRet = LoadKeyboardLayout((KeyboardLanguage & Chr(0)), KLF_ACTIVATE)
            End If

            GetKeyboardLayoutName(strRet) ' Test if switch successed
            If strRet = (KeyboardLanguage) Then
                SwitchKeyboardLang = True
            End If
        End Function
    End Class

    Public Class PrintToPrinter
        Implements IDisposable
        Private R_CurrentPageIndex As Integer
        Private R_Streams As IList(Of Stream)
        Private R_Printdoc As PrintDocument
        Private R_PrintDialogs As PrintDialog

        Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
            Dim stream As Stream = New MemoryStream()
            R_Streams.Add(stream)
            Return stream
        End Function

        Private Sub Export(ByVal Report As LocalReport, ByVal Width As Integer, ByVal Height As Integer)
            Dim deviceInfo As String = "<DeviceInfo>" & _
                "<OutputFormat>EMF</OutputFormat>" & _
                "<PageWidth>" & Width / 100 & "in</PageWidth>" & _
                "<PageHeight>" & Height / 100 & "in</PageHeight>" & _
                "<MarginTop>0.0in</MarginTop>" & _
                "<MarginLeft>0.0in</MarginLeft>" & _
                "<MarginRight>0.0in</MarginRight>" & _
                "<MarginBottom>0.0in</MarginBottom>" & _
                "</DeviceInfo>"
            Dim warnings As Warning()
            R_Streams = New List(Of Stream)()
            Report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream As Stream In R_Streams
                stream.Position = 0
            Next
        End Sub

        Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
            Dim pageImage As New Metafile(R_Streams(R_CurrentPageIndex))
            Dim adjustedRect As New Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX),
                                              ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), _
                                              ev.PageBounds.Width, _
                                              ev.PageBounds.Height)

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect)
            ev.Graphics.DrawImage(pageImage, adjustedRect)
            R_CurrentPageIndex += 1
            ev.HasMorePages = (R_CurrentPageIndex < R_Streams.Count)
        End Sub

        Private Sub Print(Optional ByVal Copies As Integer = 1)
            If R_Streams Is Nothing OrElse R_Streams.Count = 0 Then
                Throw New Exception("Error: no stream to print.")
            End If
            If Not R_Printdoc.PrinterSettings.IsValid Then
                Throw New Exception("Error: cannot find the default printer.")
            Else
                AddHandler R_Printdoc.PrintPage, AddressOf PrintPage
                R_CurrentPageIndex = 0
                R_Printdoc.PrinterSettings.Copies = Copies
                R_Printdoc.Print()
            End If
        End Sub

        Public Sub PrintReport(ByRef Report As LocalReport, Optional ByVal Copies As Integer = 1, Optional ByVal PaperKind As PaperKind = Printing.PaperKind.A4, Optional ByVal IsLandscap As Boolean = False, Optional ByVal PrinterName As String = Nothing, Optional ByVal PrintDialogs As Boolean = False)
            Dim w As Integer
            Dim h As Integer
            R_Printdoc = New PrintDocument()
            If PrinterName <> Nothing Or Trim(PrinterName) <> "" Then R_Printdoc.PrinterSettings.PrinterName = PrinterName
            If Not R_Printdoc.PrinterSettings.IsValid Then
                Throw New Exception("Cannot find the specified printer")
            Else
                Dim Paper As New PaperSize
                If PaperKind = Printing.PaperKind.A4 Then
                    'Paper = New PaperSize("A4", 827, 1169)
                    Paper = New PaperSize("A4", R_Printdoc.DefaultPageSettings.PaperSize.Width, R_Printdoc.DefaultPageSettings.PaperSize.Height)
                    Paper.RawKind = Printing.PaperKind.A4
                    R_Printdoc.DefaultPageSettings.PaperSize = Paper
                Else
                    Dim Pagekind_found As Boolean = False
                    For i As Integer = 0 To R_Printdoc.PrinterSettings.PaperSizes.Count - 1
                        If StrComp(R_Printdoc.PrinterSettings.PaperSizes.Item(i).Kind.ToString, PaperKind.ToString, CompareMethod.Text) = 0 Then
                            Paper = R_Printdoc.PrinterSettings.PaperSizes.Item(i)
                            R_Printdoc.DefaultPageSettings.PaperSize = Paper
                            Pagekind_found = True
                            Exit For
                        End If
                    Next
                    If Not Pagekind_found Then
                        Throw New Exception("Paper size is invalid!")
                    End If
                End If
                R_Printdoc.DefaultPageSettings.Margins.Top = 100
                R_Printdoc.DefaultPageSettings.Margins.Bottom = 100
                R_Printdoc.DefaultPageSettings.Margins.Left = 100
                R_Printdoc.DefaultPageSettings.Margins.Right = 100
                If R_Printdoc.DefaultPageSettings.Landscape = True Then
                    w = R_Printdoc.DefaultPageSettings.PaperSize.Height
                    h = R_Printdoc.DefaultPageSettings.PaperSize.Width
                Else
                    w = R_Printdoc.DefaultPageSettings.PaperSize.Width
                    h = R_Printdoc.DefaultPageSettings.PaperSize.Height
                End If
                R_Printdoc.DefaultPageSettings.Landscape = IsLandscap
                Export(Report, w, h)
                If PrintDialogs = True Then
                    R_PrintDialogs = New PrintDialog With {.Document = R_Printdoc}
                    If R_PrintDialogs.ShowDialog() = DialogResult.OK Then Print(Copies)
                Else
                    Print(Copies)
                End If
            End If
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                End If
            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If R_Streams IsNot Nothing Then
                For Each stream As Stream In R_Streams
                    stream.Close()
                Next
                R_Streams = Nothing
            End If

            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Class Printers
        Private m_currentPageIndex As Integer
        Private m_streams As IList(Of Stream)
        Private printdoc As PrintDocument
        Private PrintDialogs As PrintDialog

        Public Sub Print_To_Printer_Default(ByRef report As LocalReport, ByVal page_width As Integer, ByVal page_height As Integer, Optional ByVal islandscap As Boolean = False, Optional ByVal printer_name As String = Nothing)
            printdoc = New PrintDocument()
            If printer_name <> Nothing Then printdoc.PrinterSettings.PrinterName = printer_name
            If Not printdoc.PrinterSettings.IsValid Then ' detecate is the printer is exist
                Throw New Exception("Cannot find the specified printer")
            Else
                Dim ps As New PaperSize("Custom", page_width, page_height)
                printdoc.DefaultPageSettings.PaperSize = ps
                printdoc.DefaultPageSettings.Landscape = islandscap
                Export(report)
                Print()
            End If
        End Sub

        Public Sub Print_To_Printer_PrintDialg(ByRef report As LocalReport, ByVal page_width As Integer, ByVal page_height As Integer, Optional ByVal islandscap As Boolean = False, Optional ByVal printer_name As String = Nothing)
            printdoc = New PrintDocument()
            PrintDialogs = New PrintDialog()
            If printer_name <> Nothing Then printdoc.PrinterSettings.PrinterName = printer_name
            If Not printdoc.PrinterSettings.IsValid Then ' detecate is the printer is exist
                Throw New Exception("Cannot find the specified printer")
            Else
                Dim ps As New PaperSize("Custom", page_width, page_height)
                printdoc.DefaultPageSettings.PaperSize = ps
                printdoc.DefaultPageSettings.Landscape = islandscap
                Export(report)
                PrintDialogs.Document = printdoc
                If PrintDialogs.ShowDialog() = DialogResult.OK Then
                    Print()
                End If
            End If
        End Sub

        Public Sub Print_To_Printer_Default(ByVal report As LocalReport, Optional PrintCopies As Integer = 1, Optional ByVal paperkind As String = "A4", Optional ByVal islandscap As Boolean = False, Optional ByVal printer_name As String = Nothing)
            printdoc = New PrintDocument()
            If printer_name <> Nothing Then printdoc.PrinterSettings.PrinterName = printer_name
            If Not printdoc.PrinterSettings.IsValid Then ' detecate is the printer is exist
                Throw New Exception("Cannot find the specified printer")
            Else
                Dim ps As PaperSize
                Dim pagekind_found As Boolean = False
                For i = 0 To printdoc.PrinterSettings.PaperSizes.Count - 1
                    If printdoc.PrinterSettings.PaperSizes.Item(i).Kind.ToString = paperkind Then
                        ps = printdoc.PrinterSettings.PaperSizes.Item(i)
                        printdoc.DefaultPageSettings.PaperSize = ps
                        pagekind_found = True
                        Exit For
                    End If
                Next
                If Not pagekind_found Then Throw New Exception("paper size is invalid")
                printdoc.DefaultPageSettings.Landscape = islandscap
                Export(report)
                Print(PrintCopies)
            End If
        End Sub

        Public Sub Print_To_Printer_PrintDialg(ByVal report As LocalReport, Optional ByVal paperkind As String = "A4", Optional ByVal islandscap As Boolean = False, Optional ByVal printer_name As String = Nothing)
            printdoc = New PrintDocument()
            PrintDialogs = New PrintDialog()
            If printer_name <> Nothing Then printdoc.PrinterSettings.PrinterName = printer_name
            If Not printdoc.PrinterSettings.IsValid Then ' detecate is the printer is exist
                Throw New Exception("Cannot find the specified printer")
            Else
                Dim ps As PaperSize
                Dim pagekind_found As Boolean = False
                For i = 0 To printdoc.PrinterSettings.PaperSizes.Count - 1
                    If printdoc.PrinterSettings.PaperSizes.Item(i).Kind.ToString = paperkind Then
                        ps = printdoc.PrinterSettings.PaperSizes.Item(i)
                        printdoc.DefaultPageSettings.PaperSize = ps
                        pagekind_found = True
                        Exit For
                    End If
                Next
                If Not pagekind_found Then Throw New Exception("paper size is invalid")
                printdoc.DefaultPageSettings.Landscape = islandscap
                Export(report)
                PrintDialogs.Document = printdoc
                If PrintDialogs.ShowDialog() = DialogResult.OK Then
                    Print()
                End If
            End If
        End Sub

        ' Routine to provide to the report renderer, in order to
        ' save an image for each page of the report.
        Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
            Dim stream As Stream = New MemoryStream()
            m_streams.Add(stream)
            Return stream
        End Function

        ' Export the given report as an EMF (Enhanced Metafile) file.
        Private Sub Export(ByVal report As LocalReport)
            Dim w As Integer
            Dim h As Integer
            If printdoc.DefaultPageSettings.Landscape = True Then
                w = printdoc.DefaultPageSettings.PaperSize.Height
                h = printdoc.DefaultPageSettings.PaperSize.Width
            Else
                w = printdoc.DefaultPageSettings.PaperSize.Width
                h = printdoc.DefaultPageSettings.PaperSize.Height
            End If
            Dim deviceInfo As String = "<DeviceInfo>" & _
                "<OutputFormat>EMF</OutputFormat>" & _
                "<PageWidth>" & w / 100 & "in</PageWidth>" & _
                "<PageHeight>" & h / 100 & "in</PageHeight>" & _
                "<MarginTop>0.0in</MarginTop>" & _
                "<MarginLeft>0.0in</MarginLeft>" & _
                "<MarginRight>0.0in</MarginRight>" & _
                "<MarginBottom>0.0in</MarginBottom>" & _
                "</DeviceInfo>"
            Dim warnings As Warning() = Nothing
            m_streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream As Stream In m_streams
                stream.Position = 0
            Next
        End Sub

        ' Handler for PrintPageEvents
        Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
            Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

            ' Adjust rectangular area with printer margins.
            Dim adjustedRect As New Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX),
                                              ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), _
                                              ev.PageBounds.Width, _
                                              ev.PageBounds.Height)

            ' Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect)

            ' Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect)

            ' Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex += 1
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
        End Sub

        Private Sub Print(Optional PrintCopies As Integer = 1)
            If m_streams Is Nothing OrElse m_streams.Count = 0 Then
                Throw New Exception("Error: no stream to print.")
            End If
            AddHandler printdoc.PrintPage, AddressOf PrintPage
            m_currentPageIndex = 0
            printdoc.PrinterSettings.Copies = PrintCopies
            printdoc.Print()
        End Sub
    End Class

    Public Class Properties
        Private R_PageWidth As Single
        Property PageWidth As Single
            Set(value As Single)
                R_PageWidth = value
            End Set
            Get
                Return R_PageWidth
            End Get
        End Property

        Private R_PageHeight As Single
        Property PageHeight As Single
            Set(value As Single)
                R_PageHeight = value
            End Set
            Get
                Return R_PageHeight
            End Get
        End Property

        Private R_MarginTop As Single
        Property MarginTop As Single
            Set(value As Single)
                R_MarginTop = value
            End Set
            Get
                Return R_MarginTop
            End Get
        End Property

        Private R_MarginLeft As Single
        Property MarginLeft As Single
            Set(value As Single)
                R_MarginLeft = value
            End Set
            Get
                Return R_MarginLeft
            End Get
        End Property

        Private R_MarginRight As Single
        Property MarginRight As Single
            Set(value As Single)
                R_MarginRight = value
            End Set
            Get
                Return R_MarginRight
            End Get
        End Property

        Private R_MarginBottom As Single
        Property MarginBottom As Single
            Set(value As Single)
                R_MarginBottom = value
            End Set
            Get
                Return R_MarginBottom
            End Get
        End Property
    End Class

    Public Class ConvertReport
        Inherits Properties

        Enum Type_Converter
            PDF
            Excel
        End Enum

        Public Sub Save(ByVal viewer As ReportViewer, Optional ByVal SavePath As String = "", Optional ByVal Convert As Type_Converter = Type_Converter.PDF, Optional ByVal IsOpen As Boolean = False)
            If Trim(SavePath) = "" Then
                Dim SaveDialog As New SaveFileDialog()
                If Convert = Type_Converter.PDF Then
                    SaveDialog.Filter = "*PDF files (*.pdf)|*.pdf"
                Else
                    SaveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls"
                End If
                SaveDialog.FilterIndex = 2
                SaveDialog.RestoreDirectory = True
                If SaveDialog.ShowDialog() = DialogResult.Cancel Then Exit Sub
                SavePath = SaveDialog.FileName
            End If
            Dim deviceInfo As String = Nothing
            If Convert = Type_Converter.PDF Then deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>" & PageWidth & "in</PageWidth><PageHeight>" & PageHeight & "in</PageHeight><MarginTop>" & MarginTop & "in</MarginTop><MarginLeft>" & MarginLeft & "in</MarginLeft><MarginRight>" & MarginRight & "in</MarginRight><MarginBottom>" & MarginBottom & "in</MarginBottom></DeviceInfo>"
            Dim warn As Warning() = Nothing
            Dim streamids As String() = Nothing
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty
            Dim Bytes() As Byte = viewer.LocalReport.Render(Convert.ToString, deviceInfo, mimeType, encoding, extension, streamids, warn)
            Using Stream As New FileStream(SavePath, FileMode.Create)
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
            End Using
            If IsOpen = True Then System.Diagnostics.Process.Start(SavePath)
        End Sub

        Enum TypeLocationReport
            ReportEmbeddedResource
            ReportPath
        End Enum

        Public Sub Save(ByVal Type As TypeLocationReport, ByVal FullPathReport As String, Optional ByVal SavePath As String = "", Optional ByVal Convert As Type_Converter = Type_Converter.PDF, Optional ByVal IsOpen As Boolean = False)
            Dim viewer As New ReportViewer
            If Type = TypeLocationReport.ReportEmbeddedResource Then
                viewer.LocalReport.ReportEmbeddedResource = FullPathReport
            Else
                viewer.LocalReport.ReportPath = FullPathReport
            End If
            viewer.RefreshReport()

            If Trim(SavePath) = "" Then
                Dim SaveDialog As New SaveFileDialog()
                If Convert = Type_Converter.PDF Then
                    SaveDialog.Filter = "*PDF files (*.pdf)|*.pdf"
                Else
                    SaveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls"
                End If
                SaveDialog.FilterIndex = 2
                SaveDialog.RestoreDirectory = True
                If SaveDialog.ShowDialog() = DialogResult.Cancel Then Exit Sub
                SavePath = SaveDialog.FileName
            End If
            Dim deviceInfo As String = Nothing
            If Convert = Type_Converter.PDF Then deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>" & PageWidth & "in</PageWidth><PageHeight>" & PageHeight & "in</PageHeight><MarginTop>" & MarginTop & "in</MarginTop><MarginLeft>" & MarginLeft & "in</MarginLeft><MarginRight>" & MarginRight & "in</MarginRight><MarginBottom>" & MarginBottom & "in</MarginBottom></DeviceInfo>"
            Dim warn As Warning() = Nothing
            Dim streamids As String() = Nothing
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty
            Dim Bytes() As Byte = viewer.LocalReport.Render(Convert.ToString, deviceInfo, mimeType, encoding, extension, streamids, warn)
            Using Stream As New FileStream(SavePath, FileMode.Create)
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
            End Using
            If IsOpen = True Then System.Diagnostics.Process.Start(SavePath)
        End Sub

    End Class

    Public Class ConvertNumberToWord
        Dim mOnesArray(8) As String
        Dim mOneTensArray(9) As String
        Dim mTensArray(7) As String
        Dim mPlaceValues(4) As String


        Public Sub New()

            mOnesArray(0) = "one"
            mOnesArray(1) = "two"
            mOnesArray(2) = "three"
            mOnesArray(3) = "four"
            mOnesArray(4) = "five"
            mOnesArray(5) = "six"
            mOnesArray(6) = "seven"
            mOnesArray(7) = "eight"
            mOnesArray(8) = "nine"

            mOneTensArray(0) = "ten"
            mOneTensArray(1) = "eleven"
            mOneTensArray(2) = "twelve"
            mOneTensArray(3) = "thirteen"
            mOneTensArray(4) = "fourteen"
            mOneTensArray(5) = "fifteen"
            mOneTensArray(6) = "sixteen"
            mOneTensArray(7) = "seventeen"
            mOneTensArray(8) = "eighteen"
            mOneTensArray(9) = "nineteen"

            mTensArray(0) = "twenty"
            mTensArray(1) = "thirty"
            mTensArray(2) = "forty"
            mTensArray(3) = "fifty"
            mTensArray(4) = "sixty"
            mTensArray(5) = "seventy"
            mTensArray(6) = "eighty"
            mTensArray(7) = "ninety"

            mPlaceValues(0) = "hundred"
            mPlaceValues(1) = "thousand"
            mPlaceValues(2) = "million"
            mPlaceValues(3) = "billion"
            mPlaceValues(4) = "trillion"

        End Sub


        Protected Function GetOnes(ByVal OneDigit As Integer) As String

            GetOnes = ""

            If OneDigit = 0 Then
                Exit Function
            End If

            GetOnes = mOnesArray(OneDigit - 1)

        End Function


        Protected Function GetTens(ByVal TensDigit As Integer) As String

            GetTens = ""

            If TensDigit = 0 Or TensDigit = 1 Then
                Exit Function
            End If

            GetTens = mTensArray(TensDigit - 2)

        End Function

        Enum Currencies
            RIEL
            USD
        End Enum
        Public Function ConvertNumberToWords(ByVal NumberValue As String, Optional ByVal Currency As Currencies = Currencies.USD) As String
            Dim mNumberValue As String = ""
            Dim mNumbers As String = ""
            Dim mFraction As String = ""
            Dim j As Integer = 0
            Dim mNumWord As String = ""
            ConvertNumberToWords = ""
            ' validate input
            Try
                j = CDbl(NumberValue)
            Catch ex As Exception
                ConvertNumberToWords = "Invalid input."
                Exit Function
            End Try

            ' get fractional part {if any}
            If InStr(NumberValue, ".", CompareMethod.Binary) = 0 Then
                ' no fraction
                mNumberValue = NumberValue
            Else
                mNumberValue = Microsoft.VisualBasic.Left(NumberValue, InStr(NumberValue, ".", CompareMethod.Binary) - 1)
                mFraction = Mid(NumberValue, InStr(NumberValue, ".", CompareMethod.Binary)) ' + 1)
                If StrComp(mFraction, ".", CompareMethod.Binary) = 0 Then mFraction = "0"
                mFraction = Math.Round(CSng(mFraction), 2) * 100

                If CInt(mFraction) = 0 Then
                    mFraction = ""
                Else
                    mFraction = Currency.ToString & " And Cent " & GetWord(mFraction.ToCharArray)
                End If
            End If
            mNumWord = GetWord(mNumberValue.ToCharArray)
            Return StrConv((Trim(mNumWord) & " " & Trim(IIf(Trim(mFraction) = "", Currency.ToString, mFraction)) & " ONLY"), VbStrConv.Uppercase)
        End Function

        Private Function GetWord(ByVal mNumbers As String) As String
            Dim Delimiter As String = " "
            Dim TensDelimiter As String = " "
            Dim mNumberStack() As String
            Dim i As Integer = 0
            Dim mOneTens As Boolean = False
            Dim mNumWord As String = ""
            ' move numbers to stack/array backwards
            For j = mNumbers.Length - 1 To 0 Step -1
                ReDim Preserve mNumberStack(i)

                mNumberStack(i) = mNumbers(j)
                i += 1
            Next

            For j = mNumbers.Length - 1 To 0 Step -1
                Select Case j
                    Case 0, 3, 6, 9, 12
                        ' ones  value
                        If Not mOneTens Then
                            mNumWord &= GetOnes(Val(mNumberStack(j))) & Delimiter
                        End If

                        Select Case j
                            Case 3
                                ' thousands
                                mNumWord &= mPlaceValues(1) & Delimiter

                            Case 6
                                ' millions
                                mNumWord &= mPlaceValues(2) & Delimiter

                            Case 9
                                ' billions
                                mNumWord &= mPlaceValues(3) & Delimiter

                            Case 12
                                ' trillions
                                mNumWord &= mPlaceValues(4) & Delimiter
                        End Select


                    Case Is = 1, 4, 7, 10, 13
                        ' tens value
                        If Val(mNumberStack(j)) = 0 Then
                            mNumWord &= GetOnes(Val(mNumberStack(j - 1))) & Delimiter
                            mOneTens = True
                            Exit Select
                        End If

                        If Val(mNumberStack(j)) = 1 Then
                            mNumWord &= mOneTensArray(Val(mNumberStack(j - 1))) & Delimiter
                            mOneTens = True
                            Exit Select
                        End If

                        mNumWord &= GetTens(Val(mNumberStack(j)))

                        ' this places the tensdelimiter; check for succeeding 0
                        If Val(mNumberStack(j - 1)) <> 0 Then
                            mNumWord &= TensDelimiter
                        End If
                        mOneTens = False

                    Case Else
                        ' hundreds value 
                        mNumWord &= GetOnes(Val(mNumberStack(j))) & Delimiter

                        If Val(mNumberStack(j)) <> 0 Then
                            mNumWord &= mPlaceValues(0) & Delimiter
                        End If
                End Select
            Next
            Return mNumWord
        End Function
    End Class

End Namespace
