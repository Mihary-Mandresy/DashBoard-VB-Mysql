Module DateUtils
    Function GetTimestamp(d As DateTime) As Long
        Return DateDiff(DateInterval.Second, New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), d.ToUniversalTime())
    End Function

    Function GetDateByTimestamp(t as Long) As Date
        Return  New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(t)
    End Function

    Function GetDiffMIlliSecond(final as Date, initial as Date) As Long
        Return CLng((final - initial).TotalMilliseconds)
    End Function
    
End Module
