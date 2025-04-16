Imports System.Runtime.InteropServices
' Imports System.Collections.Generic
' Imports MySql.Data.MySqlClient

Public Class Form1
        <DllImport("kernel32.dll", SetLastError:=True)>
        Private Shared Function AllocConsole() As Boolean
        End Function


        ' ! Handler

        Private Sub H_Select(sender As Object, e As EventArgs)
            Dim cb As ComboBox = CType(sender, ComboBox)
            Dim selected As String = cb.SelectedItem.ToString()

            For Each voiture As Voiture In listVoiture
                If selected = voiture.Matricule Then
                    Me.voiture = voiture
                    restReservoir = voiture.Reservoir
                    Exit For
                End If
            Next

            If Not isNothing(p_Body)  Then
                p_Body.Focus()
            End If
        End Sub

        Private Sub H_KeyDown(sender as Object, e as KeyEventArgs)
            If e.KeyCode >= 49 And e.KeyCode <= 58 Then
                input.rapport = e.KeyCode - 48
            End If

            Select Case e.KeyCode
                Case Keys.Up
                    If Not input.k_Acc Then
                        input.k_Acc = True
                        temps = 0

                        If input.rapport <> 0 Then
                            Dim now as Date= Date.Now
                            Dim diff As TimeSpan = now - dateInitial
                            Dim secondes As Double = diff.TotalSeconds
                            InsertMouvement(voiture.Velocity, voiture.Acceleration, input.rapport, secondes, now, voiture.IdVoiture)
                        End If
                    End If

                Case Keys.Down
                    If Not input.k_Brake Then 
                        input.k_Brake = True
                        temps = 0

                        If input.rapport <> 0 Then
                            Dim now as Date= Date.Now
                            Dim diff As TimeSpan = now - dateInitial
                            Dim secondes As Double = diff.TotalSeconds
                            InsertMouvement(voiture.Velocity, -voiture.Freinage, input.rapport, secondes, now, voiture.IdVoiture)
                        End If
                    End If

                Case Keys.Space
                    input.rapport = 10
                Case Keys.P
                    pause = True
                Case Keys.C
                    showConsomationMoyenne()
            End Select
        End Sub

        Private Sub H_KeyUp(sender as Object, e as KeyEventArgs)
            If e.KeyCode >= 49 And e.KeyCode <= 58 Then
                input.rapport = 0
            End If

            Select Case e.KeyCode
                Case Keys.Up
                    input.k_Acc = False

                    Dim now as Date= Date.Now
                    Dim diff As TimeSpan = now - dateInitial
                    Dim secondes As Double = diff.TotalSeconds
                    InsertMouvement(voiture.Velocity, 0, 0, secondes, now, voiture.IdVoiture)
                Case Keys.Down
                    input.k_Brake = False

                    Dim now as Date= Date.Now
                    Dim diff As TimeSpan = now - dateInitial
                    Dim secondes As Double = diff.TotalSeconds
                    InsertMouvement(voiture.Velocity, 0, 0, secondes, now, voiture.IdVoiture)
                Case Keys.Space
                    input.rapport = 0
            End Select
        End Sub

        Private Sub H_Timer(sender as Object, e as EventArgs)
            Dim now As Date = Date.Now
            Dim temps_en_milliseconde as Long = GetDiffMIlliSecond(now, lastDate)
            Dim temps_en_seconde as Decimal = temps_en_milliseconde / 1000
            Dim tempAcc As Decimal = 0
            If input.k_Acc Then
                temps += temps_en_seconde
                voiture.Velocity += WithRapport(voiture.Acceleration) * temps_en_seconde
                restReservoir -= temps_en_seconde * WithRapport(voiture.Consommation)
                tempAcc = voiture.Acceleration()
            End If

            If  input.k_Brake Then                
                temps += temps_en_seconde
                voiture.Velocity -= WithRapport(voiture.Freinage) * temps_en_seconde
            End If
            
            If Not pause Then
                distance += (WithRapport(tempAcc) / 2) * temps_en_seconde * temps_en_seconde + KilometrePerHourToMeterPerSeconde(voiture.Velocity) * temps_en_seconde
            End If
            
            p_Body.Invalidate()
            lastDate = now
        End Sub

        Private Sub H_Paint(sender As Object, e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            g.Clear(Color.Gray)

            DrawReservoir(g)
            DrawDashBoard(g)
            DrawAllText(g)
        End Sub

        ' ! <======== EndHandler ========>

        ' ! Update Windows        
        Private Sub showConsomationMoyenne()
            Dim consommation As Decimal = voiture.Reservoir - restReservoir
            Dim enMoyenne As Decimal =  consommation * 100000 / distance

            Console.WriteLine($"Consommation moyenne en 100 km : {enMoyenne} L")
        End Sub


        Private Sub DrawAllText(g As Graphics)
        ' $ Vitesse
            Dim vitesseFont As New Font("Arial", 40)
            DrawTextToCenter(voiture.Velocity.ToString("F2") & " KM/H", vitesseFont, Brushes.Black, New Point(WINDOW_WIDHT / 2, BODY_HEIGHT - 40), g)

            Dim font As New Font("Arial", 30)

            ' $ Rapport
            DrawTextToCenter((input.rapport * 10) & " %", font, Brushes.Black, New Point(WINDOW_WIDHT - 100, BODY_HEIGHT / 2), g)

            ' $ Distance
            DrawTextToCenter(distance.ToString("F2") & " m", font, Brushes.Black, New Point(WINDOW_WIDHT - 200, BODY_HEIGHT - 50), g)
            ' $ Temps
            DrawTextToCenter(temps.ToString("F2") & " s", font, Brushes.Black, New Point(200, BODY_HEIGHT - 50), g)

        End Sub
        

        Private Sub DrawDashBoard(g As Graphics)
            Const angleSupplementaire As Integer = 40
            Dim restAngle as Integer = 360 - angleSupplementaire * 2
            Dim nbTrait as Single = (voiture.MaxTableau / 20)

            Dim stepAngle as Single = restAngle / nbTrait

            Const DASHBOARD_RADIUS As Integer = 260
            Dim DIAMETRE as Integer = DASHBOARD_RADIUS * 2

            Dim penDashBoard As New Pen(Color.Black, 5)
            Dim penTrait As New Pen(Color.Black, 3)
            Dim penAiguille As New Pen(Color.Red, 4)

            Dim centerX as Single = WINDOW_WIDHT / 2
            Dim centerY as Single = BODY_HEIGHT / 2

            Dim x AS Single = centerX - DASHBOARD_RADIUS
            Dim y As Single = centerY - DASHBOARD_RADIUS

            g.DrawEllipse(penDashBoard, x, y, DIAMETRE, DIAMETRE)

            Dim taille As Integer = 10

            For a As Integer = 0 To nbTrait - 1
                Dim ang As Single = a * stepAngle + (180 - angleSupplementaire)
                Dim value As String = (20 * a).ToString()

                Dim radianAngle As Single = DegreToRadian(ang)

                Dim p1 As New PointF(Math.Cos(radianAngle) * (DASHBOARD_RADIUS + taille) + centerX,
                                    Math.Sin(radianAngle) * (DASHBOARD_RADIUS + taille) + centerY)

                Dim p2 As New PointF(Math.Cos(radianAngle) * (DASHBOARD_RADIUS - taille) + centerX,
                                    Math.Sin(radianAngle) * (DASHBOARD_RADIUS - taille) + centerY)

                Dim pText As New PointF(Math.Cos(radianAngle) * (DASHBOARD_RADIUS + taille * 2.5) + centerX,
                                        Math.Sin(radianAngle) * (DASHBOARD_RADIUS + taille * 2.5) + centerY)

                Dim font As New Font("Arial", 10)
                Dim pCentered As New Point(pText.X, pText.Y)

                g.DrawLine(penTrait, p1, p2)
                DrawTextToCenter(value, font, Brushes.Black, pCentered, g)
            Next

            Dim angleAiguille As Single = (voiture.Velocity * restAngle / voiture.MaxTableau) + (180 - angleSupplementaire)
            Dim angleToRad As Single = DegreToRadian(angleAiguille)
            
            Dim pAiguille as New Point(Math.Cos(angleToRad) * (DASHBOARD_RADIUS - taille * 3) + centerX,
                                    Math.Sin(angleToRad) * (DASHBOARD_RADIUS - taille * 3) + centerY)

            g.DrawLine(penAiguille, pAiguille, New Point(centerX, centerY))
        End Sub

        Private Sub DrawReservoir(g AS Graphics)
            Const  RESERVOIR_WIDTH = 60
            Const  RESERVOIR_HEIGHT = 250

            Dim penBordure As New Pen(Color.Black, 4)

            Dim font As New Font("Arial", 15)
            Dim value As String = restReservoir.ToString("F2") & " L"

            DrawTextToCenter(value, font, Brushes.Black, New Point(130, 170), g)

            Dim restantHeight As Single = RESERVOIR_HEIGHT * (1 -  restReservoir / voiture.Reservoir)

            g.DrawRectangle(penBordure, 100, 200, RESERVOIR_WIDTH, RESERVOIR_HEIGHT)
            g.FillRectangle(Brushes.Green, 100, 200, RESERVOIR_WIDTH, RESERVOIR_HEIGHT)
            g.FillRectangle(Brushes.Gray, 100, 200, RESERVOIR_WIDTH, restantHeight)
        End Sub
        ' ! <======== EndUpdate ========>

        ' ! <======== Other Sub ========>
        
        Private Function WithRapport(nb as Decimal) As Decimal
            Return  nb * input.rapport / 10
        End Function

        Private  Sub InsertMouvement(vitesse_initial as Single, acceleration as Single, rapport as Integer, duration as Single, daty as Date, id_voiture As Integer)
            Dim mouvementVoiture as MouvementVoiture = New MouvementVoiture() With {
                .VitesseInitial = vitesse_initial,
                .Acceleration = acceleration, 
                .Rapport = rapport,
                .Duration = duration,
                .Daty = daty,
                .IdVoiture = id_voiture
            }
            mouvementService.CreateMouvement(connexion, mouvementVoiture)

            Console.WriteLine("Insertion termine")
        End Sub
        
        

End Class
