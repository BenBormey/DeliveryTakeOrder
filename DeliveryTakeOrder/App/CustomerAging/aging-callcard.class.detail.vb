Public Class AgingCallcardDetail
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
    Public Property Zone As String
    Public Property Street As String
    Public Property HouseNumber As String
    Public Property Tel1 As String
    Public Property Tel2 As String
    Public Property City As String
    Public Property SalesmanNumber As String
    Public Property SalesmanName As String
    Public Property MobileNumber As String
    Public Property DaysOver As Int32
    Public Property GrandTotal As Decimal
    Public Property Division As String

    Public ReadOnly Property Tel As String
        Get
            Dim result As String = Me.Tel1
            If Not Me.Tel2.Equals(String.Empty) Then result &= String.Format(" / {0}", Me.Tel2)
            Return result
        End Get
    End Property


    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone
    End Function
End Class
