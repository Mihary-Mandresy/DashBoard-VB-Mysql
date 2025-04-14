Imports MySql.Data.MySqlClient

Public Class MouvementVoitureDAO
    Public Function GetMouvementsByVoiture(connection As MySqlConnection, idVoiture As Integer) As List(Of MouvementVoiture)
        Dim mouvements As New List(Of MouvementVoiture)()
        Dim query As String = "SELECT * FROM mouvement_voiture WHERE id_voiture = @id_voiture"
        
        Using command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@id_voiture", idVoiture)
            
            Using reader As MySqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim mouvement As New MouvementVoiture With {
                        .IdMouvement = reader.GetInt32("id_mouvement"),
                        .VitesseInitial = reader.GetDecimal("vitesse_initial"),
                        .Acceleration = reader.GetDecimal("acceleration"),
                        .Rapport = reader.GetInt32("rapport"),
                        .Duration = reader.GetDecimal("duration"),
                        .Daty = reader.GetDateTime("daty"),
                        .IdVoiture = reader.GetInt32("id_voiture")
                    }
                    mouvements.Add(mouvement)
                End While
            End Using
        End Using
        
        Return mouvements
    End Function

    Public Sub InsertMouvement(connection As MySqlConnection, mouvement As MouvementVoiture)
        Dim query As String = "INSERT INTO mouvement_voiture (vitesse_initial, acceleration, rapport, duration, daty, id_voiture) 
                               VALUES (@vitesse_initial, @acceleration, @rapport, @duration, @daty, @id_voiture)"
        
        Using command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@vitesse_initial", mouvement.VitesseInitial)
            command.Parameters.AddWithValue("@acceleration", mouvement.Acceleration)
            command.Parameters.AddWithValue("@rapport", mouvement.Rapport)
            command.Parameters.AddWithValue("@duration", mouvement.Duration)
            command.Parameters.AddWithValue("@daty", mouvement.Daty)
            command.Parameters.AddWithValue("@id_voiture", mouvement.IdVoiture)
            
            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
