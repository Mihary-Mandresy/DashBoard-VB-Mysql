Public Class MouvementVoiture
    Public Property IdMouvement As Integer
    Public Property VitesseInitial As Decimal
    Public Property Acceleration As Decimal
    Public Property Rapport As Integer
    Public Property Duration As Decimal
    Public Property Daty As DateTime
    Public Property IdVoiture As Integer

    Public Overrides Function ToString() As String
        Return $"v : {VitesseInitial}, a : {Acceleration}, r : {Rapport}, duration : {Duration}, D : {Daty}, Idvoiture: {IdVoiture} "
    End Function
End Class
