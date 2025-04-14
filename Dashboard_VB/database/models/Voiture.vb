Public Class Voiture
    Public Property IdVoiture As Integer
    Public Property Matricule As String
    Public Property Acceleration As Decimal
    Public Property Freinage As Decimal
    Public Property Reservoir As Integer
    Public Property Consommation As Decimal
    Public Property MaxTableau As Decimal

    Public Overrides Function ToString() as String
        Return IdVoiture & " " & matricule
    End Function
End Class
