Imports DeliveryTakeOrder.DatabaseFrameworks
Imports DeliveryTakeOrder.ApplicationFrameworks

Module Initialized
    Public R_KeyPassword As String = "" '"2606MSR15866307"
    Public R_Logo As Image = My.Resources.Logo
    Public R_CompanyCode As Long = 10001
    Public R_CompanyName As String
    Public R_CompanyAddress As String
    Public R_CompanyVATNumber As String
    Public R_FullCompanyName As String
    Public R_FullCompanyAddress As String
    Public R_FullCompanyAddress_No_VAT As String
    Public R_CompanyKhmerName As String
    Public R_CompanyKhmAddress As String
    Public R_CompanyVATTin As String
    Public R_CompanyTelephone As String

    Public R_PrefixDatabase As String = "DB"
    Public R_PublicIPAddress As String
    Public R_IPAddress As String = "192.168.1.111"
    Public R_IPAddress_Temp As String = "192.168.100.49"
    Public R_UserConnection As String = "UserConnection"
    Public R_PasswordConnection As String = "123"
    Public R_DatabaseName As String
    Public R_MainDatabaseName As String = ""
    Public R_PortConnection As String
    Public vIsNestleOnly As Boolean = False

    Public R_CorrectPassword As Boolean
    Public R_PermissionPassword As String
    Public R_IsCancel As Boolean
    Public R_SearchCustomerId As Boolean
    Public R_SearchValue As String
    Public R_AllUnpaid As Boolean
    Public R_DateFrom As Date
    Public R_DateTo As Date
    Public R_IndexString As String
    Public R_SelectedAmount As Double
    Public R_IsFullPayment As Boolean
    Public R_CollectorName As String
    Public R_SpecialOfferAmount As Double
    Public R_Barcode As String
    Public R_StampNumberSelected As String
    Public R_MessageAlert As String
    Public R_DocumentNumber As String
    Public R_LineCode As String
    Public R_DeptCode As String

    Public Enum ViewJournalReport
        All_Journal
        All_Journal_Completed
        All_Journal_Not_Completed
    End Enum
    Public R_JournalSelected As ViewJournalReport

    '*******
    Private DTable As DataTable

    Public Function CheckCompaniesExistOrNot(ByVal Data As DatabaseFramework, ByVal App As ApplicationFramework) As Boolean
        Dim RExisted As Boolean = False
        Dim Dic As New Dictionary(Of String, Object)
        Dic.Add("CompanyCode", R_CompanyCode)
        Data.DatabaseName = "CompanySetup"
        DTable = Data.Selects("Companies", , Dic, , , , , GetConnectionType(Data, App))
        If Not (DTable Is Nothing) Then
            If DTable.Rows.Count > 0 Then
                Initialized.R_CompanyKhmerName = Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComKhmerName")) = True, "", DTable.Rows(0).Item("ComKhmerName")))
                Initialized.R_CompanyName = Replace(StrConv(IIf(IsDBNull(DTable.Rows(0).Item("ComName")) = True, "", DTable.Rows(0).Item("ComName")), VbStrConv.Uppercase), "&", "&&").Trim().Replace("'S", "'s").Trim()
                Initialized.R_CompanyAddress = Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComAddress")) = True, "", DTable.Rows(0).Item("ComAddress")))
                Initialized.R_CompanyAddress &= vbCrLf & IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) = "", "", "Tel:" & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))))
                Initialized.R_CompanyKhmAddress = Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComKhmerAddress")) = True, "", DTable.Rows(0).Item("ComKhmerAddress")))

                Initialized.R_CompanyVATTin = Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComVATNumber")) = True, "", DTable.Rows(0).Item("ComVATNumber")))
                Initialized.R_CompanyTelephone = IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) = "", "", Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))))

                R_FullCompanyName = Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComKhmerName")) = True, "", DTable.Rows(0).Item("ComKhmerName"))).Trim()
                R_FullCompanyName &= vbCrLf & Replace(StrConv(IIf(IsDBNull(DTable.Rows(0).Item("ComName")) = True, "", DTable.Rows(0).Item("ComName")), VbStrConv.Uppercase), "&", "&&").Trim().Replace("'S", "'s").Trim()

                R_FullCompanyAddress = "លេខអត្តសញ្ញាណកម្ម អតប (VAT TIN)៖ " & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComVATNumber")) = True, "", DTable.Rows(0).Item("ComVATNumber"))).Trim()
                R_FullCompanyAddress &= vbCrLf & "អាសយដ្ឋាន៖ " & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComKhmerAddress")) = True, "", DTable.Rows(0).Item("ComKhmerAddress")))
                R_FullCompanyAddress &= vbCrLf & "Address៖ " & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComAddress")) = True, "", DTable.Rows(0).Item("ComAddress")))
                R_FullCompanyAddress &= vbCrLf & "ទូរស័ព្ទលេខ / Telephone Nº៖ " & IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) = "", Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone"))), Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) & IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone"))) = "", "", "/" & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone")))))

                R_FullCompanyAddress_No_VAT = "អាសយដ្ឋាន៖ " & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComKhmerAddress")) = True, "", DTable.Rows(0).Item("ComKhmerAddress")))
                R_FullCompanyAddress_No_VAT &= vbCrLf & "Address៖ " & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComAddress")) = True, "", DTable.Rows(0).Item("ComAddress")))
                R_FullCompanyAddress_No_VAT &= vbCrLf & "ទូរស័ព្ទលេខ / Telephone Nº៖ " & IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) = "", Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone"))), Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComTelephone")) = True, "", DTable.Rows(0).Item("ComTelephone"))) & IIf(Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone"))) = "", "", "/" & Trim(IIf(IsDBNull(DTable.Rows(0).Item("ComMobilePhone")) = True, "", DTable.Rows(0).Item("ComMobilePhone")))))
                RExisted = True
            Else
                GoTo Err_Skip
            End If
        Else
Err_Skip:
            Initialized.R_CompanyKhmerName = ""
            Initialized.R_CompanyName = ""
            Initialized.R_CompanyAddress = ""
            Initialized.R_FullCompanyAddress_No_VAT = ""
            Initialized.R_CompanyVATTin = ""
            Initialized.R_CompanyTelephone = ""
            RExisted = False
        End If
        Initialized.R_DatabaseName = Initialized.R_CompanyName
        Data.DatabaseName = App.MergeObject(Initialized.R_CompanyName)
        Return RExisted
    End Function

    Public Function GetConnectionType(ByVal Data As DatabaseFramework, ByVal App As ApplicationFramework) As DeliveryTakeOrder.Configurations.ConnectionType
        If App.CheckConnectionByPing(Data.PublicIPAddress) = True Then
            Return DeliveryTakeOrder.Configurations.ConnectionType.INTERNET
        Else
            If App.CheckConnectionByPing(Data.IPAddress) = False Then
                If App.CheckConnectionByPing(Initialized.R_IPAddress_Temp) = False Then
                    Data.IPAddress = Initialized.R_IPAddress
                Else
                    Data.IPAddress = Initialized.R_IPAddress_Temp
                End If
            End If
            Return DeliveryTakeOrder.Configurations.ConnectionType.NETWORK
        End If
    End Function

    Public Sub LoadingInitialized(ByVal Data As DatabaseFramework, ByVal App As ApplicationFramework)
        Data.PrefixDatabase = Initialized.R_PrefixDatabase
        Data.PublicIPAddress = Initialized.R_PublicIPAddress
        Data.IPAddress = Initialized.R_IPAddress
        Data.UserConnection = Initialized.R_UserConnection
        Data.Password = Initialized.R_PasswordConnection
        Data.DatabaseName = App.MergeObject(Initialized.R_CompanyName)
        Data.PortNumber = Initialized.R_PortConnection
    End Sub
End Module
