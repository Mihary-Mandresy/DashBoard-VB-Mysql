Imports MySql.Data.MySqlClient

Public Class VoitureDAO
    Public Function GetVoitures(connection As MySqlConnection) As List(Of Voiture)
        Dim voitures As New List(Of Voiture)()
        Dim query As String = "SELECT * FROM voiture"
        
        Using command As New MySqlCommand(query, connection)
            Using reader As MySqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim voiture As New Voiture With {
                        .IdVoiture = reader.GetInt32("id_voiture"),
                        .Matricule = reader.GetString("matricule"),
                        .Acceleration = reader.GetDecimal("acceleration"),
                        .Freinage = reader.GetDecimal("freinage"),
                        .Reservoir = reader.GetInt32("reservoire"),
                        .Consommation = reader.GetDecimal("consommation"),
                        .MaxTableau = reader.GetDecimal("max_tableau")
                    }
                    voitures.Add(voiture)
                End While
            End Using
        End Using
        
        Return voitures
    End Function

    Public Function GetVoitureById(connection As MySqlConnection, id As Integer) As Voiture
        Dim query As String = "SELECT * FROM voiture WHERE id_voiture = @id"
        
        Using command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@id", id)
            
            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    Return New Voiture With {
                        .IdVoiture = reader.GetInt32("id_voiture"),
                        .Matricule = reader.GetString("matricule"),
                        .Acceleration = reader.GetDecimal("acceleration"),
                        .Freinage = reader.GetDecimal("freinage"),
                        .Reservoir = reader.GetInt32("reservoire"),
                        .Consommation = reader.GetDecimal("consommation"),
                        .MaxTableau = reader.GetDecimal("max_tableau")
                    }
                End If
            End Using
        End Using
        
        Return Nothing
    End Function

    Public Sub InsertVoiture(connection As MySqlConnection, voiture As Voiture)
        Dim query As String = "INSERT INTO voiture (matricule, acceleration, freinage, reservoire, consommation, max_tableau) 
                               VALUES (@matricule, @acceleration, @freinage, @reservoire, @consommation, @max_tableau)"
        
        Using command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@matricule", voiture.Matricule)
            command.Parameters.AddWithValue("@acceleration", voiture.Acceleration)
            command.Parameters.AddWithValue("@freinage", voiture.Freinage)
            command.Parameters.AddWithValue("@reservoire", voiture.Reservoir)
            command.Parameters.AddWithValue("@consommation", voiture.Consommation)
            command.Parameters.AddWithValue("@max_tableau", voiture.MaxTableau)
            
            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
