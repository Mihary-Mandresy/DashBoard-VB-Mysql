Imports MySql.Data.MySqlClient

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        AllocConsole()
        connexion = UtilDb()

        input.k_Acc = False
        input.k_Brake = False
        input.rapport = 0
        input.replayActivate = False

        dateInitial = Date.Now

        components = New System.ComponentModel.Container()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(WINDOW_WIDHT, WINDOW_HEIGHT)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "DashBoard"
        Me.KeyPreview = True

        ' ! Panel Header

        p_header = New Panel() With {
            .Location = new Point(0 , 0),
            .Size = New Size(WINDOW_WIDHT, 60)
        }

        Dim combobox As New ComboBox() With {
            .Location = New Point(50, 10),
            .Size = New Size(300, 60),
            .DropDownStyle = ComboBoxStyle.DropDownList
        }

        AddHandler combobox.SelectedIndexChanged, AddressOf H_Select

        Dim voitureService as New VoitureService()
        listVoiture = voitureService.GetAllVoitures(connexion)

        For Each voiture As Voiture In listVoiture
            combobox.Items.Add(voiture.Matricule)
        Next

        combobox.SelectedIndex = 0
        
        voiture = listVoiture(0)

        restReservoir = voiture.Reservoir
        p_header.Controls.Add(combobox)

        ' $ btn replay

        Dim b_replay As New Button() With {
            .Text = "Replay",
            .Location = New Point(500, 10),
            .Size = New Size(200, 40),
            .Font = New Font("Arial", 20)
        }

        AddHandler b_replay.Click , AddressOf H_ClickReplay
        
        p_header.Controls.Add(b_replay)

        ' ! end panel header

        p_Body = New DoubleBufferedPanel With {
            .BackColor = Color.Gray,
            .Size = new Size(WINDOW_WIDHT, WINDOW_HEIGHT),
            .Location = new Point(0, 60)
        }

        AddHandler p_Body.Paint, AddressOf H_Paint

        AddHandler Me.KeyDown, AddressOf H_KeyDown
        AddHandler Me.KeyUp , AddressOf H_KeyUp


        timer.Interval = 10
        AddHandler timer.Tick, AddressOf H_Timer
        lastDate = Date.Now
        timer.Start()

        Me.Controls.Add(p_header)
        Me.Controls.Add(p_Body)

    End Sub

    ' ! Replay mode
    Private replayMode as Boolean = False
    ' ! <== End Replay ==>

    Private dateInitial as Date

    Public mouvementService As New MouvementVoitureService()

    Public listVoiture as List(of Voiture)

    Public ReadOnly WINDOW_WIDHT As Integer = 1200
    Public ReadOnly WINDOW_HEIGHT As Integer = 800

    Public ReadOnly BODY_HEIGHT As Integer = 740

    Private input as New Input()

    Private timer as New Timer()

    Private pause as Boolean = False

    Private connexion as MySqlConnection

    Private p_header as Panel
    Private p_Body as DoubleBufferedPanel

    Private voiture as Voiture

    Private temps as Decimal = 0
    Private restReservoir as Decimal
    Private distance as Decimal = 0
    
    Private lastDate as Date

End Class
