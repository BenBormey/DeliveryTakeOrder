Public Class AgingCallcardFilterForm
    Private bs As BindingSource
    Public Property IsCancel As Boolean = True
    Dim ls As Object
    Public Function GetDatasource(Of T)() As List(Of T)
        Return CType(ls, SortableBindingList(Of T)).AsEnumerable.Select(Function(x) CType(x, ICloneable).Clone).Cast(Of T).ToList
    End Function
    Public Sub New()
        InitializeComponent()
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Public Sub FillSortableList(Of T)(lsBs As Object)
        Dim lsTmp As Object = lsBs.DataSource
        Dim lsSource As List(Of T) = DirectCast(lsTmp, List(Of T))
        ls = New SortableBindingList(Of T)
        lsSource.ForEach(Sub(x) ls.Add(DirectCast(x, ICloneable).Clone))

        bs = New BindingSource(ls, Nothing)
        dgvData.DataSource = bs
        DesignDgv()
    End Sub

    Private Sub DesignDgv()
        With dgvData
            For Each col As DataGridViewColumn In dgvData.Columns
                If col.Name <> "IsChecked" Then col.ReadOnly = True
                col.Visible = False
            Next
            .Columns("IsChecked").Visible = True
            .Columns("InvNumber").Visible = True
            .Columns("PONumber").Visible = True
            .Columns("ShipDate").Visible = True
            .Columns("DueDate").Visible = True
            .Columns("GrandTotal").Visible = True
            .Columns("Division").Visible = True
            If .Columns.Contains("Delto") Then .Columns("Delto").Visible = True
            .Columns("CusName").Visible = True
            .Columns("DaysOver").Visible = True
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each o In bs.DataSource
            o.IsChecked = True
        Next
        dgvData.Refresh()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each o In bs.DataSource
            o.IsChecked = False
        Next
        dgvData.Refresh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.IsCancel = False
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each dr As DataGridViewRow In dgvData.SelectedRows
            Dim o = dr.DataBoundItem
            o.IsChecked = True
        Next
        dgvData.Refresh()
    End Sub
End Class