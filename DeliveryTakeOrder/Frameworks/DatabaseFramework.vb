Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports DeliveryTakeOrder.ApplicationFrameworks
Imports MySql.Data
Imports MySql.Data.MySqlClient

Namespace DatabaseFrameworks

    Public Class DatabaseFramework_MySQL
        Inherits Configurations
        Implements IDisposable
        Private strconnection As String = "host=192.168.1.22;port=3306;user id=root;password=!$%2m2O8mkn6iG5v2or!@#$#@678;database=untapp;pooling=false;"
        Private App As New ApplicationFramework
        Private Connection As MySqlConnection
        Private Command As MySqlCommand

        Public Property MysqlConnectionString() As String
            Set(value As String)
                strconnection = value
            End Set
            Get
                Return strconnection
            End Get
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal Type As ConnectionType)
            Try
                Connection = New MySqlConnection(strconnection)
                Connection.Open()
                Connection.Close()
                Connection = Nothing
            Catch e As SqlException
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(0)
            Catch ex As Exception
                MessageBox.Show("The database connection fail! The application will be closed." & vbCrLf & " Please check with your support!", "Error database connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Environment.Exit(0)
            End Try
        End Sub

        Public Sub New(ByVal Type As ConnectionType, ByVal IsPrefixDatabase As Boolean)
            Try
                Connection = New MySqlConnection(strconnection)
                Connection.Open()
                Connection.Close()
                Connection = Nothing
            Catch e As SqlException
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(0)
            Catch ex As Exception
                MessageBox.Show("The database connection fail! The application will be closed." & vbCrLf & " Please check with your support!", "Error database connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Environment.Exit(0)
            End Try
        End Sub

        Public Function Get_CURRENT_DATE() As DateTime
            Dim R_CurrentDate As DateTime
            Dim DtData As DataTable
            Try
                Connection = New MySqlConnection(strconnection)
                Command = New MySqlCommand("select NOW() as `date`;", Connection)
                Connection.Open()
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                If DtData.Rows.Count > 0 Then
                    R_CurrentDate = CDate(IIf(IsDBNull(DtData.Rows(0).Item(0)) = True, Now(), DtData.Rows(0).Item(0)))
                Else
                    R_CurrentDate = Now()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return R_CurrentDate
        End Function

        Public Function Execute(ByVal CommandText As String) As Object
            Dim DtData As New DataTable()
            Try
                Connection = New MySqlConnection(strconnection)
                Connection.Open()
                Command = New MySqlCommand(CommandText, Connection)
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                Return True
            Catch ex As Exception
                Return "Sql: " & ex.Message.ToString
            End Try
        End Function

        Public Function Selects(ByVal CommandText As String) As Object
            Dim DtData As New DataTable()
            Try
                Connection = New MySqlConnection(strconnection)
                Connection.Open()
                Command = New MySqlCommand(CommandText, Connection)
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader())
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                Return DtData
            Catch ex As Exception
                Return "Sql: " & ex.Message.ToString
            End Try
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Class DatabaseFramework
        Inherits Configurations
        Implements IDisposable
        Private App As New ApplicationFramework
        Private Connection As SqlConnection
        Private Command As SqlCommand

        Public Sub New()

        End Sub

        Public Sub New(ByVal Type As ConnectionType)
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Connection.Close()
                Connection = Nothing
            Catch e As SqlException
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(0)
            Catch ex As Exception
                MessageBox.Show("The database connection fail! The application will be closed." & vbCrLf & " Please check with your support!", "Error database connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Environment.Exit(0)
            End Try
        End Sub

        Public Sub New(ByVal Type As ConnectionType, ByVal IsPrefixDatabase As Boolean)
            Try
                Connection = New SqlConnection(ConnectionString(Type, IsPrefixDatabase))
                Connection.Open()
                Connection.Close()
                Connection = Nothing
            Catch e As SqlException
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Environment.Exit(0)
            Catch ex As Exception
                MessageBox.Show("The database connection fail! The application will be closed." & vbCrLf & " Please check with your support!", "Error database connection", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Environment.Exit(0)
            End Try
        End Sub

        Public Function Get_CURRENT_DATE(Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As DateTime
            Dim R_CurrentDate As DateTime
            Dim DtData As DataTable
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Command = New SqlCommand("SELECT GETDATE()", Connection)
                Connection.Open()
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                If DtData.Rows.Count > 0 Then
                    R_CurrentDate = CDate(IIf(IsDBNull(DtData.Rows(0).Item(0)) = True, Now(), DtData.Rows(0).Item(0)))
                Else
                    R_CurrentDate = Now()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return R_CurrentDate
        End Function

        Public Function Get_IDENT_CURRENT(ByVal TableName As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Long
            Dim R_Existed As Boolean = False
            Dim R_IDENTCURRENT As Long = 1
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim CommandText = String.Format("SELECT  IDENT_CURRENT('{0}')", TableName)
            Dim DtData As DataTable
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Command = New SqlCommand(String.Format("SELECT * FROM {0}", TableName), Connection)
                Connection.Open()
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Command = Nothing
                If DtData.Rows.Count > 0 Then
                    R_Existed = True
                Else
                    R_Existed = False
                End If
                DtData = Nothing

                Command = New SqlCommand(CommandText, Connection)
                Connection.Open()
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                If DtData.Rows.Count > 0 Then
                    If R_Existed = False And CLng(IIf(IsDBNull(DtData.Rows(0).Item(0)) = True, 0, DtData.Rows(0).Item(0))) = 1 Then
                        R_IDENTCURRENT = 1
                    Else
                        R_IDENTCURRENT = CLng(IIf(IsDBNull(DtData.Rows(0).Item(0)) = True, 0, DtData.Rows(0).Item(0))) + 1
                    End If

                Else
                    R_IDENTCURRENT = 1
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return R_IDENTCURRENT
        End Function

        Public Function ExecuteCommand(ByVal CommandText As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Object
            Dim DtData As New DataTable()
            'If InStr(LTrim(CommandText).ToUpper(), "SELECT", CompareMethod.Text) > 0 Then
            '    Try
            '        Connection = New SqlConnection(ConnectionString(Type))
            '        Connection.Open()
            '        Command = New SqlCommand(CommandText, Connection)
            '        DtData = New DataTable
            '        DtData.Load(Command.ExecuteReader())
            '        Connection.Close()
            '        Connection = Nothing
            '        Command = Nothing
            '        Return DtData
            '    Catch ex As Exception
            '        Return "Sql: " & ex.Message.ToString
            '    End Try
            'Else
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(CommandText, Connection)
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                Return True
            Catch ex As Exception
                Return "Sql: " & ex.Message.ToString
            End Try
            'Else
            '    Return "Incorrect Sql Command String. You can use with command SELECT, INSERT, or UPDATE only."
            'End If
        End Function

        Public Function ExecuteStoredProc(ByVal StoreProcName As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Object
            Dim DtData As New DataTable()
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand()
                Command.Connection = Connection
                Command.CommandType = CommandType.StoredProcedure
                Command.CommandText = StoreProcName
                DtData = New DataTable
                DtData.Load(Command.ExecuteReader())
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                Return DtData
            Catch ex As Exception
                Return "Sql: " & ex.Message.ToString
            End Try
        End Function

        Enum SeparatorList
            Is_And
            Is_Or
        End Enum
        Public Function Selects(ByVal SQLÇommand As String, ByVal Type As ConnectionType) As Object
            Dim DtData As New DataTable()
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(SQLÇommand, Connection)
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
            Catch ex As Exception
                DtData = Nothing
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return DtData
        End Function

        Public Function Selects(ByVal TableName As String, Optional ByVal FieldLists As ArrayList = Nothing, Optional ByVal WhereLists As Dictionary(Of String, Object) = Nothing, Optional ByVal IsConditionIN As Boolean = False, Optional ByVal Separator As SeparatorList = SeparatorList.Is_And, Optional ByVal GroupByLists As ArrayList = Nothing, Optional ByVal OrderByLists As ArrayList = Nothing, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Object
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim DtData As New DataTable()
            Dim ConditionWhere As String = ""
            Dim FieldList As String = ""
            If Not (FieldLists Is Nothing) Then
                For Each GroupBy As Object In FieldLists
                    FieldList &= String.Format("{0},", GroupBy)
                Next
                If Trim(FieldList) <> "" Then FieldList = String.Format("{0}", Mid(FieldList, 1, FieldList.Length - 1))
            Else
                FieldList = "*"
            End If
            Dim CommandText As String = String.Format("SELECT {0} FROM {1}", FieldList, TableName)
            Dim RSeparator As String = IIf(Separator = SeparatorList.Is_And, "AND", "OR ")
            If Not (WhereLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In WhereLists
                    If IsConditionIN = False Then
                        If StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                            ConditionWhere &= String.Format("[{0}] = {1}", field.Key, field.Value) & " " & RSeparator & " "
                        ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Then
                            ConditionWhere &= String.Format("[{0}] = '{1}'", field.Key, field.Value) & " " & RSeparator & " "
                        Else
                            ConditionWhere &= String.Format("[{0}] = {1}", field.Key, field.Value) & " " & RSeparator & " "
                        End If
                    Else
                        ConditionWhere &= String.Format("[{0}] IN ({1})", field.Key, field.Value) & " " & RSeparator & " "
                    End If
                Next
                If Trim(ConditionWhere) <> "" Then ConditionWhere = Mid(ConditionWhere, 1, ConditionWhere.Length - 4)
                CommandText = String.Format("{0} WHERE {1}", CommandText, ConditionWhere)
            End If
            Dim GroupByList As String = ""
            If Not (GroupByLists Is Nothing) Then
                For Each GroupBy As Object In GroupByLists
                    GroupByList &= String.Format("{0},", GroupBy)
                Next
                If Trim(GroupByList) <> "" Then GroupByList = String.Format("GROUP BY {0}", Mid(GroupByList, 1, GroupByList.Length - 1))
            End If

            Dim OrderByList As String = ""
            If Not (OrderByLists Is Nothing) Then
                For Each Orderby As Object In OrderByLists
                    OrderByList &= String.Format("{0},", Orderby)
                Next
                If Trim(OrderByList) <> "" Then OrderByList = String.Format("ORDER BY {0}", Mid(OrderByList, 1, OrderByList.Length - 1))
            End If
            CommandText = String.Format("{0} {1} {2}", CommandText, GroupByList, OrderByList)
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(CommandText, Connection)
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
            Catch ex As Exception
                DtData = Nothing
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return DtData
        End Function

        Public Function Inserts(ByVal TableName As String, ByVal FieldLists_ValueLists As Dictionary(Of String, Object), Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim IsCompleted As Boolean = False
            Dim FieldList As String = ""
            Dim ValueList As String = ""
            Dim ImageList As New Dictionary(Of String, Object)
            If Not (FieldLists_ValueLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In FieldLists_ValueLists
                    FieldList &= String.Format("[{0}],", field.Key)
                    If TypeOf field.Value Is Byte() Then
                        ValueList &= String.Format("@{0},", field.Key)
                        ImageList.Add(field.Key, field.Value)
                    ElseIf StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                        ValueList &= String.Format("{0},", field.Value)
                    ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Or field.Value Is Nothing Then
                        If field.Value Is Nothing Then
                            ValueList &= String.Format("N'{0}',", "")
                        Else
                            ValueList &= String.Format("N'{0}',", field.Value)
                        End If
                    Else
                        ValueList &= String.Format("{0},", field.Value)
                    End If
                Next
                If Trim(FieldList) <> "" Then FieldList = String.Format("{0}", Mid(FieldList, 1, FieldList.Length - 1))
                If Trim(ValueList) <> "" Then ValueList = String.Format("{0}", Mid(ValueList, 1, ValueList.Length - 1))
            End If
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, FieldList, ValueList), Connection)
                If ImageList.Count > 0 Then
                    For Each field As KeyValuePair(Of String, Object) In ImageList
                        If TypeOf field.Value Is Byte() Then
                            Dim ImageBytes(0) As Byte
                            ImageBytes = field.Value
                            With Command.Parameters.Add(New SqlParameter(String.Format("@{0}", field.Key), SqlDbType.Image))
                                .Value = ImageBytes
                                .Size = ImageBytes.Length
                            End With
                        End If
                    Next
                End If
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As SqlException
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return IsCompleted
        End Function

        Public Function Updates(ByVal TableName As String, ByVal FieldLists_ValueLists As Dictionary(Of String, Object), ByVal WhereLists As Dictionary(Of String, Object), Optional ByVal Separator As SeparatorList = SeparatorList.Is_And, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim IsCompleted As Boolean = False
            Dim FieldList_ValueList As String = ""
            Dim ImageList As New Dictionary(Of String, Object)
            If Not (FieldLists_ValueLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In FieldLists_ValueLists
                    If TypeOf field.Value Is Byte() Then
                        FieldList_ValueList &= String.Format("[{0}] = @{1},", field.Key, field.Key)
                        ImageList.Add(field.Key, field.Value)
                    ElseIf StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                        FieldList_ValueList &= String.Format("[{0}] = {1},", field.Key, field.Value)
                    ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Or field.Value Is Nothing Then
                        If field.Value Is Nothing Then
                            FieldList_ValueList &= String.Format("[{0}] = N'{1}',", field.Key, "")
                        Else
                            FieldList_ValueList &= String.Format("[{0}] = N'{1}',", field.Key, field.Value)
                        End If
                    Else
                        FieldList_ValueList &= String.Format("[{0}] = {1},", field.Key, field.Value)
                    End If
                Next
                If Trim(FieldList_ValueList) <> "" Then FieldList_ValueList = String.Format("{0}", Mid(FieldList_ValueList, 1, FieldList_ValueList.Length - 1))
            End If
            Dim SelectedSeparator As String = IIf(Separator = SeparatorList.Is_And, "AND", "OR")
            Dim WhereList As String = ""
            If Not (WhereLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In WhereLists
                    If TypeOf field.Value Is Byte() Then
                        WhereList &= String.Format("[{0}] = {1} {2}", field.Key, field.Value, SelectedSeparator)
                    ElseIf StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                        WhereList &= String.Format("[{0}] = {1} {2}", field.Key, field.Value, SelectedSeparator)
                    ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Or field.Value Is Nothing Then
                        If field.Value Is Nothing Then
                            WhereList &= String.Format("[{0}] = '{1}' {2}", field.Key, "", SelectedSeparator)
                        Else
                            WhereList &= String.Format("[{0}] = '{1}' {2}", field.Key, field.Value, SelectedSeparator)
                        End If
                    Else
                        WhereList &= String.Format("[{0}] = {1} {2}", field.Key, field.Value, SelectedSeparator)
                    End If
                Next
                If Trim(WhereList) <> "" Then WhereList = String.Format("{0}", Mid(WhereList, 1, WhereList.Length - SelectedSeparator.Length))
            End If
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("UPDATE {0} SET {1} WHERE {2}", TableName, FieldList_ValueList, WhereList), Connection)
                If ImageList.Count > 0 Then
                    For Each field As KeyValuePair(Of String, Object) In ImageList
                        If TypeOf field.Value Is Byte() Then
                            Dim ImageBytes(0) As Byte
                            ImageBytes = field.Value
                            With Command.Parameters.Add(New SqlParameter(String.Format("@{0}", field.Key), SqlDbType.Image))
                                .Value = ImageBytes
                                .Size = ImageBytes.Length
                            End With
                        End If
                    Next
                End If
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return IsCompleted
        End Function

        Public Function Updates(ByVal TableName As String, ByVal FieldLists_ValueLists As Dictionary(Of String, Object), ByVal WhereLists As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim IsCompleted As Boolean = False
            Dim FieldList_ValueList As String = ""
            Dim ImageList As New Dictionary(Of String, Object)
            If Not (FieldLists_ValueLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In FieldLists_ValueLists
                    If TypeOf field.Value Is Byte() Then
                        FieldList_ValueList &= String.Format("[{0}] = @{1},", field.Key, field.Key)
                        ImageList.Add(field.Key, field.Value)
                    ElseIf StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                        FieldList_ValueList &= String.Format("[{0}] = {1},", field.Key, field.Value)
                    ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Or field.Value Is Nothing Then
                        If field.Value Is Nothing Then
                            FieldList_ValueList &= String.Format("[{0}] = N'{1}',", field.Key, "")
                        Else
                            FieldList_ValueList &= String.Format("[{0}] = N'{1}',", field.Key, field.Value)
                        End If
                    Else
                        FieldList_ValueList &= String.Format("[{0}] = {1},", field.Key, field.Value)
                    End If
                Next
                If Trim(FieldList_ValueList) <> "" Then FieldList_ValueList = String.Format("{0}", Mid(FieldList_ValueList, 1, FieldList_ValueList.Length - 1))
            End If
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("UPDATE {0} SET {1} WHERE {2}", TableName, FieldList_ValueList, WhereLists), Connection)
                If ImageList.Count > 0 Then
                    For Each field As KeyValuePair(Of String, Object) In ImageList
                        If TypeOf field.Value Is Byte() Then
                            Dim ImageBytes(0) As Byte
                            ImageBytes = field.Value
                            With Command.Parameters.Add(New SqlParameter(String.Format("@{0}", field.Key), SqlDbType.Image))
                                .Value = ImageBytes
                                .Size = ImageBytes.Length
                            End With
                        End If
                    Next
                End If
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return IsCompleted
        End Function

        Public Function Deletes(ByVal TableName As String, ByVal WhereLists As Dictionary(Of String, Object), Optional ByVal Separator As SeparatorList = SeparatorList.Is_And, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim IsCompleted As Boolean = False
            Dim SelectedSeparator As String = IIf(Separator = SeparatorList.Is_And, "AND", "OR")
            Dim WhereList As String = ""
            If Not (WhereLists Is Nothing) Then
                For Each field As KeyValuePair(Of String, Object) In WhereLists
                    If StrComp(field.Value, "GETDATE()", CompareMethod.Text) = 0 Then
                        WhereList &= String.Format("[{0}] = {1} {2}", field.Key, field.Value, SelectedSeparator)
                    ElseIf TypeOf field.Value Is String Or TypeOf field.Value Is Date Then
                        WhereList &= String.Format("[{0}] = N'{1}' {2}", field.Key, field.Value, SelectedSeparator)
                    Else
                        WhereList &= String.Format("[{0}] = {1} {2}", field.Key, field.Value, SelectedSeparator)
                    End If
                Next
                If Trim(WhereList) <> "" Then WhereList = String.Format("{0}", Mid(WhereList, 1, WhereList.Length - SelectedSeparator.Length))
            End If
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("DELETE FROM {0} WHERE {1}", TableName, WhereList), Connection)
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return IsCompleted
        End Function

        Public Function Deletes(ByVal TableName As String, ByVal WhereLists As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            TableName = String.Format("[{0}{1}].[dbo].[{2}{3}]", PrefixDatabase, DatabaseName, PrefixTable, TableName)
            Dim IsCompleted As Boolean = False
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("DELETE FROM {0} WHERE {1}", TableName, WhereLists), Connection)
                Command.ExecuteNonQuery()
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return IsCompleted
        End Function

        Enum TypeOfCheck
            Database
            Table
            Data
        End Enum
        Public Function CheckInDatabaseIsExistOrNot(ByVal DatabaseName As String, Optional ByVal TableName As String = "", Optional ByVal Checker As TypeOfCheck = TypeOfCheck.Data, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            DatabaseName = String.Format("{0}{1}", PrefixDatabase, App.MergeObject(DatabaseName))
            TableName = String.Format("{0}{1}", PrefixTable, TableName)
            Dim CommandString As String = ""
            If Checker = TypeOfCheck.Database Then
                CommandString = "SELECT * FROM master.dbo.sysdatabases WHERE (name = '{0}')"
            ElseIf Checker = TypeOfCheck.Table Then
                CommandString = "USE {0}; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE (Table_Name IN({1}))"
            ElseIf Checker = TypeOfCheck.Data Then
                CommandString = "USE {0}; SELECT * FROM [dbo].[{1}]"
            End If
            Dim DtData As New DataTable()
            Dim IsExisted As Boolean = False
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format(CommandString, DatabaseName, TableName), Connection)
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                If Not (DtData Is Nothing) Then
                    If DtData.Rows.Count > 0 Then
                        IsExisted = True
                    Else
                        IsExisted = False
                    End If
                Else
                    IsExisted = False
                End If
            Catch ex As SqlException
                DtData = Nothing
                IsExisted = False
            Catch ex As Exception
                DtData = Nothing
                IsExisted = False
            End Try
            Return IsExisted
        End Function

        Public Function CreateDatabase(ByVal DatabaseName As String, Optional ByVal Path As String = "", Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            Dim CurrentDatabaseName As String = DatabaseName
            DatabaseName = String.Format("{0}{1}", PrefixDatabase, App.MergeObject(DatabaseName))
            If Trim(Path) = "" Then Path = GetCurrentPathSQLServer(Type)
            Dim CommandString As String = "USE [master]; "
            CommandString &= "CREATE DATABASE " & DatabaseName & " "
            CommandString &= "ON PRIMARY (NAME = '" & DatabaseName & "_DATA', FILENAME =N'" & IIf(StrComp(Right(Path, 1), "\", CompareMethod.Text) = 0, Path, Path & "\") & DatabaseName & "_DATA.MDF' ,SIZE = 20MB ,MAXSIZE = UNLIMITED ,FILEGROWTH = 10MB) "
            CommandString &= "LOG ON (NAME = '" & DatabaseName & "_LOG', FILENAME = N'" & IIf(StrComp(Right(Path, 1), "\", CompareMethod.Text) = 0, Path, Path & "\") & DatabaseName & "_LOG.LDF' ,SIZE=20MB ,MAXSIZE = UNLIMITED ,FILEGROWTH = 10MB) "
            Dim IsExisted As Boolean = False
            If CheckInDatabaseIsExistOrNot(CurrentDatabaseName, , TypeOfCheck.Database, Type) = False Then
                Try
                    Connection = New SqlConnection(ConnectionString(Type))
                    Connection.Open()
                    Command = New SqlCommand(CommandString, Connection)
                    Command.ExecuteNonQuery()
                    Connection.Close()
                    Connection = Nothing
                    Command = Nothing
                    IsExisted = True
                Catch ex As SqlException
                    IsExisted = False
                Catch ex As Exception
                    IsExisted = False
                End Try
            Else
                IsExisted = False
            End If
            Return IsExisted
        End Function

        Public Function DropDatabase(ByVal DatabaseName As String, Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            Dim CurrentDatabaseName As String = DatabaseName
            DatabaseName = String.Format("{0}{1}", PrefixDatabase, App.MergeObject(DatabaseName))
            Dim CommandString As String = "USE [master]; "
            CommandString &= "DROP DATABASE " & DatabaseName & "; "
            Dim IsExisted As Boolean = False
            If CheckInDatabaseIsExistOrNot(CurrentDatabaseName, , TypeOfCheck.Database, Type) = True Then
                Try
                    Connection = New SqlConnection(ConnectionString(Type))
                    Connection.Open()
                    Command = New SqlCommand(CommandString, Connection)
                    Command.ExecuteNonQuery()
                    Connection.Close()
                    Connection = Nothing
                    Command = Nothing
                    IsExisted = True
                Catch ex As SqlException
                    IsExisted = False
                Catch ex As Exception
                    IsExisted = False
                End Try
            Else
                IsExisted = False
            End If
            Return IsExisted
        End Function

        Public Function GetCurrentPathSQLServer(Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As String
            Dim Path As String = ""
            Dim CommandString As String = "USE master; exec sp_helpfile;"
            Dim DtData As New DataTable()
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(CommandString, Connection)
                DtData.Load(Command.ExecuteReader)
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                If DtData IsNot Nothing Then
                    If DtData.Rows.Count > 0 Then
                        Path = Trim(IIf(IsDBNull(DtData.Rows(0).Item(2)) = True, "C:\Microsoft SQL Server\Data\master.mdf", DtData.Rows(0).Item(2)))
                        Path = Trim(Mid(Path, 1, Len(Path) - 10))
                    Else
                        Path = "C:\Microsoft SQL Server\Data"
                    End If
                Else
                    Path = "C:\Microsoft SQL Server\Data"
                End If
                DtData = Nothing
            Catch ex As SqlException
                DtData = Nothing
                Path = ""
            Catch ex As Exception
                DtData = Nothing
                Path = ""
            End Try
            Return Path
        End Function

        Public Function GetAllDatabaseNames(Optional FieldName As String = "*", Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As DataTable
            If Trim(FieldName) = "" Then FieldName = "*"
            Dim DatabaseNameList As DataTable = Nothing
            Dim IsCompleted As Boolean = False
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("select {0} from sysdatabases", FieldName), Connection)
                DatabaseNameList.Load(Command.ExecuteReader())
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                DatabaseNameList = Nothing
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return DatabaseNameList
        End Function

        Public Function GetAllSQLServerNames(Optional FieldName As String = "*", Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As DataTable
            If Trim(FieldName) = "" Then FieldName = "*"
            Dim SQLServerNameList As DataTable = Nothing
            Dim IsCompleted As Boolean = False
            Try
                Connection = New SqlConnection(ConnectionString(Type))
                Connection.Open()
                Command = New SqlCommand(String.Format("select {0} from sysservers  where srvproduct='SQL Server'", FieldName), Connection)
                SQLServerNameList.Load(Command.ExecuteReader())
                Connection.Close()
                Connection = Nothing
                Command = Nothing
                IsCompleted = True
            Catch ex As Exception
                IsCompleted = False
                SQLServerNameList = Nothing
                MessageBox.Show(ex.Message.ToString, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return SQLServerNameList
        End Function

        Public Function BackupDatabase(ByVal DatabaseName As String, Optional ByVal PathName As String = "", Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            Dim CurrentDatabaseName As String = DatabaseName
            DatabaseName = String.Format("{0}{1}", PrefixDatabase, App.MergeObject(DatabaseName))
            If Trim(PathName) = "" Then PathName = GetCurrentPathSQLServer(Type)
            If Right(PathName, 1) = "\" Then
                PathName &= DatabaseName & ".BAK"
            Else
                If Mid(PathName, PathName.Length - 3, 1) <> "." Then PathName &= "\" & DatabaseName & ".BAK"
            End If
            Dim IsCompleted As Boolean = False
            If CheckInDatabaseIsExistOrNot(CurrentDatabaseName, , TypeOfCheck.Database, Type) = True Then
                Try
                    Connection = New SqlConnection(ConnectionString(Type))
                    Connection.Open()
                    Command = New SqlCommand(String.Format("BACKUP DATABASE {0} TO DISK='{1}'", DatabaseName, PathName), Connection)
                    Command.ExecuteNonQuery()
                    Connection.Close()
                    Connection = Nothing
                    Command = Nothing
                    IsCompleted = True
                Catch ex As SqlException
                    IsCompleted = False
                Catch ex As Exception
                    IsCompleted = False
                End Try
            Else
                IsCompleted = False
            End If
            Return IsCompleted
        End Function

        Public Function RestoreDatabase(ByVal DatabaseName As String, Optional ByVal PathName As String = "", Optional ByVal Type As ConnectionType = ConnectionType.NETWORK) As Boolean
            Dim CurrentDatabaseName As String = DatabaseName
            DatabaseName = String.Format("{0}{1}", PrefixDatabase, App.MergeObject(DatabaseName))
            If Trim(PathName) = "" Then PathName = GetCurrentPathSQLServer(Type)
            If Right(PathName, 1) = "\" Then
                PathName &= DatabaseName & ".BAK"
            Else
                If Mid(PathName, PathName.Length - 3, 1) <> "." Then PathName &= "\" & DatabaseName & ".BAK"
            End If
            Dim IsCompleted As Boolean = False
            If CheckInDatabaseIsExistOrNot(CurrentDatabaseName, , TypeOfCheck.Database, Type) = True Then
                Try
                    Connection = New SqlConnection(ConnectionString(Type))
                    Connection.Open()
                    Command = New SqlCommand(String.Format("RESTORE DATABASE {0} FROM DISK='{1}'", DatabaseName, PathName), Connection)
                    Command.ExecuteNonQuery()
                    Connection.Close()
                    Connection = Nothing
                    Command = Nothing
                    IsCompleted = True
                Catch ex As SqlException
                    IsCompleted = False
                Catch ex As Exception
                    IsCompleted = False
                End Try
            Else
                IsCompleted = False
            End If
            Return IsCompleted
        End Function
#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
