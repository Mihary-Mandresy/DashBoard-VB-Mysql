Imports MySql.Data.MySqlClient

Public Class MouvementVoitureService
    Private mouvementDao As New MouvementVoitureDAO()

    Public Function GetMouvements(connection As MySqlConnection, idVoiture As Integer) As List(Of MouvementVoiture)
        Return mouvementDao.GetMouvementsByVoiture(connection, idVoiture)
    End Function

    Public Sub CreateMouvement(connection As MySqlConnection, mouvement As MouvementVoiture)
        mouvementDao.InsertMouvement(connection, mouvement)
    End Sub
End Class
