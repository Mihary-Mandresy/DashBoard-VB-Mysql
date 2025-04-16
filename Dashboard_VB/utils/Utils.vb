Module Utils

    Public Class OptionItem
        Public Property Text As String
        Public Property Value As String

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Public Sub DrawTextToCenter(text as String, font As Font, brushs As Brush, location  As Point, g As Graphics)
        Dim textSize As SizeF = g.MeasureString(text, font)
        location.X -= textSize.Width / 2
        location.Y -= textSize.Height / 2
        g.DrawString(text, font, brushs, location)
    End Sub

    Public Class DoubleBufferedPanel
        Inherits Panel

        Public Sub New()
            Me.DoubleBuffered = True
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
            Me.UpdateStyles()
        End Sub
    End Class

    Public Function DegreToRadian(degree as Decimal) As Decimal
        Return degree * Math.PI / 180
    End Function

    Public Function RadianToDegree(radian as Decimal) As Decimal
        Return radian * 180 / Math.PI
    End Function

    Public Function KilometrePerHourToMeterPerSeconde(distance as Decimal) as Decimal
        Return distance * 1000/ 3600
    End Function

    Public Structure Input
        Public k_Acc as Boolean
        Public k_Brake as Boolean
        Public rapport as Integer
        Public dejaInsert as Boolean
    End Structure
    
End Module




