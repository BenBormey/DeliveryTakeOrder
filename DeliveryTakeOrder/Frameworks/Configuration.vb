Imports DeliveryTakeOrder.ApplicationFrameworks

Public MustInherit Class Configuration
    Public Property PrefixDatabase() As String = "DB"
    Public Property PrefixTable() As String = "Tbl"
    Public Property PrefixProcedure() As String = "Pro"
    Public Property PrefixView() As String = "Vie"
    Public Property PrefixFunction() As String = "Fun"
End Class

Public Class Configurations
    Inherits Configuration
    Dim App As New ApplicationFramework

    Private Shared Folder As String = "MAIN_SERVER"
    Public Shared Property FolderName() As String
        Set(value As String)
            Folder = value
        End Set
        Get
            Return Folder
        End Get
    End Property

    Private Shared SelectedDB As String = "BMS"
    Public Shared Property SelectedDatabase() As String
        Set(value As String)
            SelectedDB = value
        End Set
        Get
            Return SelectedDB
        End Get
    End Property

    Public ReadOnly Property GetComputerName() As String
        Get
            Return System.Net.Dns.GetHostName
        End Get
    End Property

    Private DefaultIPAddress As String = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName).AddressList(0).ToString()
    Public ReadOnly Property GetIPAddress() As String
        Get
            Return DefaultIPAddress
        End Get
    End Property

    Private R_DatabaseName As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.DatabaseName, FolderName)
    Public Property DatabaseName() As String
        Set(value As String)
            R_DatabaseName = value
        End Set
        Get
            Return R_DatabaseName
        End Get
    End Property

    Private R_PublicIPAddress As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.PublicIPAddress, FolderName)
    Public Property PublicIPAddress() As String
        Set(value As String)
            R_PublicIPAddress = value
        End Set
        Get
            Return R_PublicIPAddress
        End Get
    End Property

    Private R_IPAddress As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.IPAddress, FolderName)
    Public Property IPAddress() As String
        Set(value As String)
            R_IPAddress = value
        End Set
        Get
            Return R_IPAddress
        End Get
    End Property

    Private R_UserConnection As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.UserConnection, FolderName)
    Public Property UserConnection() As String
        Set(value As String)
            R_UserConnection = value
        End Set
        Get
            Return R_UserConnection
        End Get
    End Property

    Private R_Password As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.PasswordConnection, FolderName)
    Public Property Password() As String
        Set(value As String)
            R_Password = value
        End Set
        Get
            Return R_Password
        End Get
    End Property

    Private R_PortNumber As String = App.GetRegistry(ApplicationFramework.RegistryKeyName.PortNumber, FolderName)
    Public Property PortNumber() As String
        Set(value As String)
            R_PortNumber = value
        End Set
        Get
            Return R_PortNumber
        End Get
    End Property

    Enum ConnectionType
        INTERNET
        NETWORK
    End Enum
    Public Function ConnectionString(Optional ByVal Type As ConnectionType = ConnectionType.NETWORK, Optional ByVal IsPrefixDatabase As Boolean = True) As String
        Dim R_Connection As String = ""
        If Type = ConnectionType.NETWORK Then
            If Trim(DatabaseName) = "" Then
                R_Connection = String.Format("Server={0};uid={1};pwd={2};", IPAddress, UserConnection, Password)
            Else 
                R_Connection = String.Format("Server={0};Initial Catalog={1};uid={2};pwd={3};", IPAddress, IIf(IsPrefixDatabase = True, String.Format("{0}{1}", PrefixDatabase, DatabaseName), DatabaseName), UserConnection, Password)
            End If
        Else
            If Trim(DatabaseName) = "" Then
                R_Connection = String.Format("Network Library=DBMSSOCN;Data Source={0}{1};uid={2};pwd={3};", PublicIPAddress, IIf(Trim(PortNumber) = "", "", "," & PortNumber), UserConnection, Password)
            Else
                R_Connection = String.Format("Network Library=DBMSSOCN;Data Source={0}{1};Initial Catalog={2};uid={3};pwd={4};", PublicIPAddress, IIf(Trim(PortNumber) = "", "", "," & PortNumber), IIf(IsPrefixDatabase = True, String.Format("{0}{1}", PrefixDatabase, DatabaseName), DatabaseName), UserConnection, Password)
            End If
        End If
        Return R_Connection
    End Function
End Class