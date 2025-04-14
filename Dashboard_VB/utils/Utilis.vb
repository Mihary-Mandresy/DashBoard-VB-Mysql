Namespace Utils
    Public Module Utils
        Sub Akory()
            Console.WriteLine("Akory leh baba")
        End Sub

        Sub Veloma()
            Console.WriteLine("Veloma leh baba")
        End Sub
    End Module

    Public MustInherit Class Babakely
        Public MustOverride  Sub Kotobe()
    End Class

    Public Interface Kotokely
        Sub Koto()
    End Interface

    Public Class Aire 
        Inherits Babakely
        Implements Kotokely

        Public Rayon as Integer

        Public Overrides Sub  Kotobe()
            Console.WriteLine("kotobe a la rescousse")
        End Sub

        Public sub Koto() Implements Kotokely.Koto
            Console.WriteLine("oay zany kotokely")
        end sub

        Public Sub New(rayon as Integer)
            Me.Rayon = rayon
        End Sub

        Public Sub PrintKely()
            Console.WriteLine("De aon kou ary zany : " &  Rayon)
        End Sub
    End Class
    
    
End Namespace
