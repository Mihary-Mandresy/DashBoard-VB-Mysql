Imports MySql.Data.MySqlClient

Module Connexion

    ' Déclare la chaîne de connexion
    Dim connectionString As String = "Server=localhost;Database=dashboard;Uid=root;Pwd=;"

    Function UtilDb() As MySqlConnection
        Try
            Dim connexe as MySqlConnection = New MySqlConnection(connectionString)
            connexe.Open()
            Return connexe
        Catch ex As MySqlException
            Console.WriteLine("Erreur de connexion : " & ex.Message)
            Throw New Exception("Tena olana")
        End Try
    End Function
    

    Sub DbConnect()
        ' Créer une instance de la connexion
        Using connection As New MySqlConnection(connectionString)
            Try
                ' Ouvrir la connexion
                connection.Open()
                Console.WriteLine("Connexion réussie à la base de données MySQL.")

                ' Créer une commande SQL
                Dim command As New MySqlCommand("SELECT * FROM voiture", connection)

                ' Exécuter la commande et lire les résultats
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Console.WriteLine(reader.GetInt32(0)) ' Affiche la première colonne de chaque ligne
                    End While
                End Using

            Catch ex As MySqlException
                ' Gérer les erreurs de connexion
                Console.WriteLine("Erreur de connexion : " & ex.Message)
            Finally
                ' Fermer la connexion
                connection.Close()
            End Try
        End Using
    End Sub

End Module