Public Class ARAging
    Public Property CusNum As String
    Public Property CusName As String
    Public Property Terms As Decimal
    Public Property CreditLimit As Decimal
    Public Property Current As Decimal
    Public Property AA As Decimal
    Public Property AB As Decimal
    Public Property A As Decimal
    Public Property B As Decimal
    Public Property C As Decimal
    Public Property D As Decimal
    Public Property E As Decimal
    Public Property Total As Decimal
    Public Property AA_Max As Int32
    Public Property AB_Max As Int32
    Public Property A_Max As Int32
    Public Property B_Max As Int32
    Public Property C_Max As Int32
    Public Property D_Max As Int32
    Public Property E_Max As Int32
    Public Property Total_Max As Int32

    Public ReadOnly Property AB_Color As Color
        Get
            If AB_Max >= Terms And AB > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If AB_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If AB > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property

    Public ReadOnly Property AA_Color As Color
        Get
            If AA_Max >= Terms And AA > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If AA_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If AA > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property

    Public ReadOnly Property A_Color As Color
        Get
            If A_Max >= Terms And A > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If A_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If A > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property

    Public ReadOnly Property B_Color As Color
        Get
            If B_Max >= Terms And B > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If B_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If B > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property
    Public ReadOnly Property C_Color As Color
        Get
            If C_Max >= Terms And C > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If C_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If C > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property
    Public ReadOnly Property D_Color As Color
        Get
            If D_Max >= Terms And D > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If D_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If D > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property
    Public ReadOnly Property E_Color As Color
        Get
            If E_Max >= Terms And E > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If E_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If E > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property
    Public ReadOnly Property Total_Color As Color
        Get
            If Total_Max >= Terms And Total > Me.CreditLimit Then Return ColorTranslator.FromHtml("#9A0089")
            If Total_Max >= Terms Then Return ColorTranslator.FromHtml("#0099BC")
            If Total > Me.CreditLimit Then Return ColorTranslator.FromHtml("#E81123")
            Return Color.Black
        End Get
    End Property

    Public Property Remarks As List(Of ARAgingRemark)
End Class
