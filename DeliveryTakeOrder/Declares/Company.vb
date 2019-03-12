Public Class Company
    Public Property Id As Decimal
    Public Property ComDateOperate As DateTime
    Public Property ComNumber As String
    Public Property ComName As String
    Public Property ComAddress As String
    Public Property ComCity As String
    Public Property ComCountry As String
    Public Property ComVATNumber As String
    Public Property ComTelephone As String
    Public Property ComMobilePhone As String
    Public Property ComFaxNumber As String
    Public Property ComEmail As String
    Public Property ComWebsite As String
    Public Property ComRemark As String
    Public Property CreatedDate As DateTime
    Public Property CompanyCode As Int32
    Public Property RegistrationNumber As String
    Public Property DatabaseName As String
    Public Property SecondaryDB As String
    Public Property TablePrefix As String
    Public Property IsDeleted As Boolean
    Public Property IsAvailable As Boolean
    Public Property IsSql2000 As Boolean
    Public Property IsStock As Boolean
    Public Property MenuValue As Int64
    Public Property MinBalanceDate As DateTime
    Public Property MinDownloadDate As DateTime


    Private _connectionstring As String
    Private _connectionbuilder As SqlClient.SqlConnectionStringBuilder
    Public Property ConnectionBuilder As SqlClient.SqlConnectionStringBuilder
        Get
            Return _connectionbuilder
        End Get
        Set(value As SqlClient.SqlConnectionStringBuilder)
            _connectionbuilder = value
            _connectionstring = _connectionbuilder.ConnectionString
        End Set
    End Property

    Public Property ConnectionString As String
        Get
            Return _connectionstring
        End Get
        Set(value As String)
            _connectionstring = value
            _connectionbuilder.ConnectionString = _connectionstring
        End Set
    End Property
End Class
