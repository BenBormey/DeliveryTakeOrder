Imports System.Data.SqlClient
Imports System.Reflection
Imports System.IO

Public Class RMDB
    Private _connectionstring As String

    Public Property ConnectionString As String
        Get
            Return _connectionstring
        End Get
        Set(value As String)
            _connectionstring = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(pConnectionString As String)
        _connectionstring = pConnectionString
    End Sub

    Public Sub ExecuteNonQuery(pSql As String, pParam As Dictionary(Of String, Object))
        Using con As New SqlConnection(_connectionstring)
            con.Open()
            Using tran As SqlTransaction = con.BeginTransaction
                Using cmd As SqlCommand = BuildCommand(con, pSql, pParam)
                    Try
                        cmd.Transaction = tran
                        cmd.ExecuteNonQuery()
                        tran.Commit()
                    Catch ex As Exception
                        MessageBox.Show(String.Format("Rollback Exception Type: {0} {1}{1}Message: {2}",
                                                 ex.GetType(),
                                                 Environment.NewLine,
                                                 ex.Message))
                        Try
                            tran.Rollback()
                        Catch ex2 As Exception
                            MessageBox.Show(String.Format("Rollback Exception Type: {0} {1}{1}Message: {2}",
                                                ex2.GetType(),
                                                Environment.NewLine,
                                                ex2.Message))
                        End Try
                    End Try
                End Using
            End Using
        End Using
    End Sub

    Public Sub ExecuteNonQuery(pSql As String)
        ExecuteNonQuery(pSql, Nothing)
    End Sub

    Public Function ExecuteScalar(pSql As String, pParam As Dictionary(Of String, Object)) As Object
        Using con As New SqlConnection(_connectionstring)
            con.Open()
            Using tran As SqlTransaction = con.BeginTransaction
                Using cmd As SqlCommand = BuildCommand(con, pSql, pParam)
                    Try
                        cmd.CommandText = String.Format("{0}; {1}", pSql, "SELECT CAST(scope_identity() AS int);")
                        cmd.Transaction = tran
                        Dim result As Object = cmd.ExecuteScalar
                        tran.Commit()
                        Return result
                    Catch ex As Exception
                        MessageBox.Show(String.Format("Rollback Exception Type: {0} {1}{1}Message: {2}",
                                                 ex.GetType(),
                                                 Environment.NewLine,
                                                 ex.Message))
                        Try
                            tran.Rollback()
                        Catch ex2 As Exception
                            MessageBox.Show(String.Format("Rollback Exception Type: {0} {1}{1}Message: {2}",
                                                ex2.GetType(),
                                                Environment.NewLine,
                                                ex2.Message))
                            Return Nothing
                        End Try
                        Return Nothing
                    End Try
                End Using
            End Using
        End Using
    End Function

    Public Function ExecuteScalar(pSql As String) As Object
        Return ExecuteScalar(pSql, Nothing)
    End Function

    Private Function BuildCommand(pConnection As SqlConnection, pSql As String, pParams As Dictionary(Of String, Object)) As SqlCommand
        Dim cmd As SqlCommand = pConnection.CreateCommand
        With cmd
            .Connection = pConnection
            .CommandType = CommandType.Text
            .CommandTimeout = 0
            .CommandText = pSql
            If pParams IsNot Nothing Then
                For Each param As KeyValuePair(Of String, Object) In pParams
                    .Parameters.AddWithValue(param.Key, param.Value)
                Next
            End If
        End With
        Return cmd
    End Function

    Private Function BuildCommand(pConnection As SqlConnection, pSql As String, ParamArray pParams() As SqlParameter) As SqlCommand
        Dim cmd As SqlCommand = pConnection.CreateCommand
        With cmd
            .Connection = pConnection
            .CommandType = CommandType.Text
            .CommandTimeout = 0
            .CommandText = pSql
            If pParams IsNot Nothing Then
                For Each param As SqlParameter In pParams
                    .Parameters.AddWithValue(param.ParameterName, param.Value)
                Next
            End If
        End With
        Return cmd
    End Function

    Public Function GetDataTable(pSql As String, pParam As Dictionary(Of String, Object)) As DataTable
        Using con As New SqlConnection(_connectionstring)
            con.Open()
            Using adapter As New SqlDataAdapter(BuildCommand(con, pSql, pParam))
                Try
                    Dim ds As New DataSet
                    Dim dt As DataTable
                    adapter.Fill(ds)
                    dt = ds.Tables(0)
                    Return dt
                Catch ex As Exception
                    MessageBox.Show(String.Format("Exception Type: {0} {1}{1}Message: {2}",
                                                 ex.GetType(),
                                                 Environment.NewLine,
                                                 ex.Message))
                    Return Nothing
                End Try
            End Using
        End Using
    End Function

    Public Function GetDataTable(pSql As String, ParamArray pParams() As SqlParameter) As DataTable
        Using con As New SqlConnection(_connectionstring)
            con.Open()
            Using adapter As New SqlDataAdapter(BuildCommand(con, pSql, pParams))
                Try
                    Dim ds As New DataSet
                    Dim dt As DataTable
                    adapter.Fill(ds)
                    dt = ds.Tables(0)
                    Return dt
                Catch ex As Exception
                    MessageBox.Show(String.Format("Exception Type: {0} {1}{1}Message: {2}",
                                                 ex.GetType(),
                                                 Environment.NewLine,
                                                 ex.Message))
                    Return Nothing
                End Try
            End Using
        End Using
    End Function

    Public Function GetDataTable(pSql As String) As DataTable
        Return GetDataTable(pSql, New Dictionary(Of String, Object))
    End Function

    Public Function GetDataTableToObject(Of T)(pDataTable As DataTable) As List(Of T)
        Dim ls As New List(Of T)
        Dim o As T

        For Each dr As DataRow In pDataTable.Rows
            o = Activator.CreateInstance(GetType(T))
            Dim value
            For Each p As PropertyInfo In GetType(T).GetProperties
                Try
                    If p.CanWrite Then
                        If pDataTable.Columns.Contains(p.Name) Then
                            Dim propType As Type = p.PropertyType
                            value = If(IsDBNull(dr(p.Name)), Nothing, Convert.ChangeType(dr(p.Name), propType))
                            p.SetValue(o, value, Nothing)
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            ls.Add(o)
        Next
        Return ls
    End Function

    Public Function GetDataTableToObject(Of T)(pSqlQuery As String) As List(Of T)
        Return Me.GetDataTableToObject(Of T)(Me.GetDataTable(pSqlQuery))
    End Function

    Public Shared Function SqlStringNull(ByVal pString As String) As String

        If pString = "" Then
            Return "NULL"
        Else
            Return String.Format(" N'{0}' ", (If(pString, "")).Replace("'", "''"))
        End If
    End Function

    Public Shared Function SqlString(ByVal str As String) As String
        If IsDBNull(str) Then Return "NULL"
        Return String.Format(" N'{0}' ", (If(str, "")).Replace("'", "''"))
    End Function

    Public Shared Function SqlBit(ByVal b As Boolean?) As String
        If IsDBNull(b) Then Return "NULL"
        If b = False Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    Public Shared Function SqlDateNull(pDate As Date) As String
        If pDate.Year = 1 Then Return "NULL" Else Return String.Format("'{0:yyyy-MM-dd}'", pDate)
    End Function

    Public Shared Function SqlDate(ByVal dt As Date) As String
        Return String.Format("'{0:yyyy-MM-dd}'", dt)
    End Function

    Public Shared Function SqlDateTimeNull(pDate As Date?) As String
        If IsDBNull(pDate) Then Return "NULL"
        If pDate.Value.Year = 1 Then Return "NULL" Else Return String.Format("'{0:yyyy-MM-dd hh:mm:ss}'", pDate)
    End Function

    Public Shared Function SqlDateTime(ByVal dt As DateTime) As String
        If IsDBNull(dt) Then Return "NULL"
        Return String.Format("'{0:yyyy-MM-dd HH:mm:ss:fff}'", dt)
    End Function

    Public Shared Function SqlInt(ByVal num As String) As String
        Try
            Dim nums As Integer = CInt(num)
            Return num
        Catch ex As Exception
            Return "NULL"
        End Try
    End Function

    Public Sub GeneratePropertiesFromQuery(pQuery As String)
        Dim text As String = String.Empty
        Dim tmp As String = "Public Property {0} As {1}"
        Dim dt As DataTable = Me.GetDataTable(pQuery)
        If dt Is Nothing Then Return
        For Each col As DataColumn In dt.Columns
            text &= (String.Format(tmp, col.ColumnName, col.DataType.Name)) & Environment.NewLine
        Next

        Try
            Using sw As New StreamWriter(String.Format("{0}\TempText.txt", My.Computer.FileSystem.SpecialDirectories.Temp), False)
                sw.Write(text)
                Process.Start(String.Format("{0}\TempText.txt", My.Computer.FileSystem.SpecialDirectories.Temp))
            End Using
        Catch ex As Exception
        End Try
    End Sub
End Class