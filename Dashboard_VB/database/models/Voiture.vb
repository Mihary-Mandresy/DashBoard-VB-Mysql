Public Class Voiture
    Public Property IdVoiture As Integer
    Public Property Matricule As String
    Public Property Acceleration As Decimal
    Public Property Freinage As Decimal
    Public Property Reservoir As Integer
    Public Property Consommation As Decimal
    Public Property MaxTableau As Decimal
    Public _velocity as Decimal = 0

    Public LastVelocity as Decimal = 0

    Public Property Velocity as Decimal
        Get
            Return _velocity
        End Get
        Set(value As Decimal)
            If value <= 0 Then
                _velocity = 0
            ElseIf value >= MaxTableau Then
                _velocity = MaxTableau
            Else
                _velocity = value
            End If
        End Set
        
    End Property

    Public Overrides Function ToString() as String
        Return IdVoiture & " " & matricule
    End Function
End Class
