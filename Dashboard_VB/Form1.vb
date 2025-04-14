Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports MySql.Data.MySqlClient

Public Class Form1
        <DllImport("kernel32.dll", SetLastError:=True)>
        Private Shared Function AllocConsole() As Boolean
        End Function

        Private Sub PrintATableau(tabs as String())

            For Each tab As String In tabs
                Console.WriteLine(tab)
            Next
            
        End Sub

        Private  Sub Affiche()
            Console.WriteLine("Kaiza daoly ry lerony")
        End Sub

        Private Function Addition(one as Integer, two as Integer) As Integer
            Return  one + two
        End Function
        

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AllocConsole()

            Utils.Akory()

            Dim a as New Utils.Aire(6)
            a.PrintKely()

            a.Koto()
            a.Kotobe()

            Dim begin As Date = Date.Now

            Console.WriteLine(a.Rayon)

            Dim cpt as Integer = 0

            Affiche()

            Dim tst as New List(Of String)

            tst.Add("Yrahim")
            tst.Add("Yserdnam")


            For Each t As String In tst
                Console.WriteLine(t)
            Next
            

            Dim tabs As String() = {"Ranjatoson", "Herindray", "Mihary", "Mandresy"}

            Console.WriteLine("Length : " & tabs.Length)

            PrintATableau(tabs)

            Console.WriteLine("Value addition : " & Addition(3, 5))

            While cpt < 10
                Console.WriteLine(cpt)
                cpt += 1
            End While

            Dim farany As Date = Date.Now

            Console.WriteLine("Farany ito lesy eh " & GetDiffMIlliSecond(farany, begin) & " s")

            ' For a As Integer  = 1 To 6
            '     Console.WriteLine(a)
            ' Next

            Dim connexe as MySqlConnection = UtilDb()

            Dim voitureService as VoitureService = New VoitureService()

            Dim voiture as Voiture = voitureService.GetVoitureById(connexe, 1)
            voiture.Matricule = "Baba"

            voitureService.CreateVoiture(connexe, voiture)

            Console.WriteLine("vita")

            Console.WriteLine(voiture)

        End Sub
End Class
