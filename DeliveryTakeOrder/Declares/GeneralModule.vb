Imports System.Net.NetworkInformation
Imports System.Reflection
Imports System.IO

Module GeneralModule
    Public Function getMacAddress() As List(Of String)
        Dim lsMac As New List(Of String)
        Dim str As String = ""
        Dim nics() As NetworkInterface = _
              NetworkInterface.GetAllNetworkInterfaces
        For Each s As NetworkInterface In nics
            ' Dim st As String = "'{0}:{1}:{2}:{3}:{4}:{5}'"
            Dim st As String = "{0}:{1}:{2}:{3}:{4}:{5}"
            Dim st2 = s.GetPhysicalAddress.ToString
            Try
                If st2 <> "" Then
                    str = String.Format(st, st2.Substring(0, 2), st2.Substring(2, 2), st2.Substring(4, 2), st2.Substring(6, 2), st2.Substring(8, 2), st2.Substring(10, 2))
                    lsMac.Add(str)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next
        'If str <> "" Then
        '    str = str.Replace(",'00:00:00:00:00:00'", "")
        '    str = str.Remove(0, 1)

        'End If

        If lsMac.Count = 0 Then
            lsMac.Add("00:00:00:00:00:00")
        End If
        Return lsMac
    End Function
    Public Function GetIPv4Address() As String
        GetIPv4Address = ""
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next
    End Function

    Public Function GetDataTableToObject(Of T)(pDataTable As DataTable) As List(Of T)
        Dim ls As New List(Of T)
        Dim o As T

        For Each dr As DataRow In pDataTable.Rows
            o = Activator.CreateInstance(GetType(T))
            Dim value
            For Each p As PropertyInfo In GetType(T).GetProperties
                Try
                    If pDataTable.Columns.Contains(p.Name) Then
                        Dim propType As Type = p.PropertyType
                        value = If(IsDBNull(dr(p.Name)), Nothing, Convert.ChangeType(dr(p.Name), propType))
                        p.SetValue(o, value, Nothing)
                    End If
                Catch ex As Exception
                End Try
            Next
            ls.Add(o)
        Next
        Return ls
    End Function

    Public Sub GeneratePropertiesFromDataTable(pDatatable As DataTable)
        Dim text As String = String.Empty
        Dim tmp As String = "Public Property {0} As {1}"
        Dim dt As DataTable = pDatatable
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

    ''' <summary>
    ''' Binding combobox with datasource
    ''' </summary>
    ''' <param name="pCombo"></param>
    ''' <param name="pDataSource"></param>
    ''' <param name="pValueMember"></param>
    ''' <param name="pDisplayMember"></param>
    ''' <param name="pIsAutoComplete">Optional parameter by default False</param>
    ''' <remarks></remarks>
    Public Sub BindCombo(pCombo As ComboBox, pDataSource As BindingSource, pValueMember As String, pDisplayMember As String, Optional pIsAutoComplete As Boolean = False)
        With pCombo
            .DataSource = pDataSource
            .ValueMember = pValueMember
            .DisplayMember = pDisplayMember
            If pIsAutoComplete Then
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End If
        End With
    End Sub
End Module
