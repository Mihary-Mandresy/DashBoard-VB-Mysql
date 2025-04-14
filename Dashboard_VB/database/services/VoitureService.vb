Imports MySql.Data.MySqlClient

Public Class VoitureService
    Private voitureDao As New VoitureDAO()

    Public Function GetAllVoitures(connection As MySqlConnection) As List(Of Voiture)
        Return voitureDao.GetVoitures(connection)
    End Function

    Public Function GetVoitureById(connection As MySqlConnection, id As Integer) As Voiture
        Return voitureDao.GetVoitureById(connection, id)
    End Function

    Public Sub CreateVoiture(connection As MySqlConnection, voiture As Voiture)
        voitureDao.InsertVoiture(connection, voiture)
    End Sub
End Class
