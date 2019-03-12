Public Class PreviewControl
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Dock = DockStyle.Fill
    End Sub

    Dim _ds As Object
    Public Property Datasource As Object
        Get
            Return _ds
        End Get
        Set(value As Object)
            _ds = value
        End Set
    End Property
End Class
