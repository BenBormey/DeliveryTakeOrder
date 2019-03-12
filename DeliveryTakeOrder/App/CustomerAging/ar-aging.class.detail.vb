Public Class ARAgingDetail
    Implements ICloneable

    Public Property IsChecked As Boolean = True
    Public Property InvNumber As Decimal
    Public Property PONumber As String
    Public Property ShipDate As DateTime
    Public Property DueDate As DateTime
    Public Property CusNum As String
    Public Property CusName As String
    Public Property DeltoId As Decimal
    Public Property DelTo As String
    Public Property DaysOver As Int32
    Public Property GrandTotal As Decimal
    Public Property Division As String

    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone
    End Function
End Class
