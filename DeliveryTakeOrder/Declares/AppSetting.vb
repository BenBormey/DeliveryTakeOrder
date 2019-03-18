Public Class AppSetting
    Public Shared Property DTAllPaymentTables As DataTable
    Public Shared Property DTAllStatementTables As DataTable
    Public Shared Property DTAllDivisionTables As DataTable

    Private Shared _connectionstring As String
    Private Shared _connectionstringbuilder As SqlClient.SqlConnectionStringBuilder

    Public Shared ReadOnly Property AppNameSpace As String
        Get
            Return "untwholesale"
        End Get
    End Property

    Public Shared ReadOnly Property AppNumber As Integer
        Get
            Return -1
        End Get
    End Property

    Public Shared ReadOnly Property ApplicationVersion As String
        Get
            Return "2019.02.11"
        End Get
    End Property

    Private Shared _signalrserver As String = "192.168.1.99"
    Public Shared Property SignalRClientServer As String
        Get
            Return _signalrserver
        End Get
        Set(value As String)
            _signalrserver = value
        End Set
    End Property

    Private Shared _db As RMDB
    Public Shared ReadOnly Property DBMain As RMDB
        Get
            If _db Is Nothing Then _db = New RMDB(ConnectionString)
            Return _db
        End Get
    End Property

    Private Shared _db_local As RMDB
    Public Shared ReadOnly Property DBLOCAL As RMDB
        Get
            If _db_local Is Nothing Then
                Dim conBuilder As New SqlClient.SqlConnectionStringBuilder With {.DataSource = "192.168.1.58",
                                                                         .InitialCatalog = "DBUNTWHOLESALECOLTD",
                                                                         .UserID = "UserConnection",
                                                                         .Password = "123"}
                _db_local = New RMDB(conBuilder.ConnectionString)
            End If
            Return _db_local
        End Get
    End Property

    Public Shared Property ConnectionString As String
        Get
            Return _connectionstring
        End Get
        Set(value As String)
            If value Is Nothing Then Return
            _connectionstring = value
            _connectionstringbuilder.ConnectionString = _connectionstring
        End Set
    End Property

    Public Shared Property ConnectionStringBuilder As SqlClient.SqlConnectionStringBuilder
        Get
            Return _connectionstringbuilder
        End Get
        Set(value As SqlClient.SqlConnectionStringBuilder)
            If value Is Nothing Then Return
            _connectionstringbuilder = value
            _connectionstring = _connectionstringbuilder.ConnectionString
        End Set
    End Property

    Private Shared _lscompany As New List(Of Company)
    Public Shared Sub InitialCompany()
        Dim primary_ip As String = "192.168.1.111"
        Dim secondary_ip As String = "192.168.100.49"

        If Not My.Computer.Network.Ping(primary_ip) Then
            primary_ip = secondary_ip
        End If

        Dim conBuilder As New SqlClient.SqlConnectionStringBuilder With {.DataSource = primary_ip,
                                                                         .InitialCatalog = "DBUNTWHOLESALECOLTD",
                                                                         .UserID = "UserConnection",
                                                                         .Password = "123"}

        'Dim conBuilder As New SqlClient.SqlConnectionStringBuilder With {.DataSource = ".",
        '                                                                 .InitialCatalog = "DBCompanySetup",
        '                                                                 .IntegratedSecurity = True}
        ConnectionStringBuilder = conBuilder
        Dim oCompany As New Company
        With oCompany
            .ComName = "UNT WHOLESALE CO., LTD"
            .ComAddress = <String><![CDATA[
Land Lot No. 891, 
Sangkat Chorm Chao, Khan Por Sen Chey, 
Phnom Penh, Cambodia
Tel: (855) 023 995 900 / 012 702 000
Fax: (855) 203 995 589
]]></String>
            .ComCity = "Phnom Penh"
            .ComCountry = "Cambodia"
            .CompanyCode = "10001"
            .ComVATNumber = "L001-100050409"
            .ComTelephone = "(855)012 702 000"
            .DatabaseName = "DBUNTWHOLESALECOLTD"
            .SecondaryDB = "Stock"
            .MinBalanceDate = DateTimePicker.MinimumDateTime 'New Date(2016, 8, 2)
            .MinDownloadDate = New Date(2017, 2, 1) '
            .ConnectionBuilder = conBuilder
            .IsStock = True
        End With

        _lscompany.Add(oCompany)
        BSCompany = New BindingSource(_lscompany, Nothing)
    End Sub

    Public Shared ReadOnly Property Companies As List(Of Company)
        Get
            Return _lscompany
        End Get
    End Property

    Public Shared Property DRCurrentUser As DataRow
    Public Shared Property BSCompany As BindingSource

    Public Shared ReadOnly Property CurrentCompany As Company
        Get
            Dim curCompany As Company = BSCompany.Current
            Return curCompany
        End Get
    End Property

    Public Shared ReadOnly Property GetCurrentServerYear(pConnectionString As String) As Integer
        Get
            Dim db As New RMDB(pConnectionString)
            Dim dt As DataTable = db.GetDataTable("SELECT DATEPART(YEAR, GETDATE()) AS yyyy")
            Return dt.Rows(0)(0)
        End Get
    End Property

    Public Shared ReadOnly Property GetCurrentServerDate(pConnectionString As String) As Date
        Get
            Dim db As New RMDB(pConnectionString)
            Dim dt As DataTable = db.GetDataTable("SELECT GETDATE() AS dddd")
            Return dt.Rows(0)(0)
        End Get
    End Property

    Public Shared ReadOnly Property GetCurrentServerDate As Date
        Get
            Return GetCurrentServerDate(ConnectionString)
        End Get
    End Property

    Public Shared Property SaleManager_Supplier As DataTable
    Public Shared Property DtArCustomer As DataTable

    '    Public Shared ReadOnly Property SqlGetCompany() As String
    '        Get
    '            Dim sqlGet As String = String.Empty

    '            sqlGet = _
    '        <SQL><![CDATA[
    ';
    'WITH    vCompany
    '          AS ( SELECT   0 AS Id ,
    '                        GETDATE() ComDateOperate ,
    '                        N'' ComNumber ,
    '                        N'(Select Company)' ComName ,
    '                        N'' ComAddress ,
    '                        N'' ComCity ,
    '                        N'' ComCountry ,
    '                        N'' ComVATNumber ,
    '                        N'' ComTelephone ,
    '                        N'' ComMobilePhone ,
    '                        N'' ComFaxNumber ,
    '                        N'' ComEmail ,
    '                        N'' ComWebsite ,
    '                        N'' ComRemark ,
    '                        GETDATE() CreatedDate ,
    '                        0 CompanyCode ,
    '                        N'' RegistrationNumber ,
    '                        N'' DatabaseName ,
    '                        N'' ConnectionString ,
    '                        N'' SecondaryDB ,
    '                        N'' TablePrefix ,
    '                        NULL IsDeleted ,
    '                        NULL IsAvailable ,
    '                        NULL IsSql2000 ,
    '                        NULL IsStock ,
    '                        1 MenuValue ,
    '                        GETDATE() MinBalanceDate ,
    '                        GETDATE() MinDownloadDate
    '               UNION ALL
    '               SELECT   Id ,
    '                        ComDateOperate ,
    '                        ComNumber ,
    '                        ComName ,
    '                        ComAddress ,
    '                        ComCity ,
    '                        ComCountry ,
    '                        ComVATNumber ,
    '                        ComTelephone ,
    '                        ComMobilePhone ,
    '                        ComFaxNumber ,
    '                        ComEmail ,
    '                        ComWebsite ,
    '                        ComRemark ,
    '                        CreatedDate ,
    '                        CompanyCode ,
    '                        RegistrationNumber ,
    '                        DatabaseName ,
    '                        ConnectionString ,
    '                        SecondaryDB ,
    '                        TablePrefix ,
    '                        IsDeleted ,
    '                        IsAvailable ,
    '                        IsSql2000 ,
    '                        IsStock ,
    '                        MenuValue ,
    '                        MinBalanceDate ,
    '                        MinDownloadDate
    '               FROM     dbo.TblCompanies
    '               WHERE    IsAvailable = 1
    '                        AND IsDeleted = 0 
    '                        AND IsDeleted = 0 AND (CompanyCode IN ({0}))
    '             )
    '    SELECT  *
    '    FROM    vCompany
    '    ORDER BY vCompany.ComName;
    ']]></SQL>
    '            Return sqlGet
    '        End Get
    '    End Property
End Class
