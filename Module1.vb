Module Module1
    Dim path As String = ""
    Dim versionFile As String = ""
    Dim classTemp As String = ""
    Dim classFile As String = ""
    Dim fail As Boolean = False
    Dim counter As Integer = 0
    '
    Dim currentVersion As String = ""
    Dim outPutVersion As String = ""
    Dim outPutVersionRev As String = ""
    Sub Main()
        Dim myArg() As String, iCount As Integer
        myArg = System.Environment.GetCommandLineArgs
        For iCount = 0 To UBound(myArg)
            val(myArg(iCount).ToString)
        Next
        If path = "" Or versionFile = "" Or classTemp = "" Then
            fail = True
            Console.WriteLine("没有参数")
        End If
        If fail = False Then
            Dim clr As Date = Now
            FileOpen(1, versionFile, OpenMode.Input)
            currentVersion = LineInput(1)
            FileClose(1)

            Dim ver As New Version(Split(currentVersion, ":")(1))
            outPutVersion = ver.Major & "." & ver.Minor & "." & Mid(clr.Year, 3, 4) & clr.DayOfYear
            outPutVersionRev = outPutVersion & "." & ver.MinorRevision + 1

            FileOpen(2, versionFile, OpenMode.Output)
            Print(2, "Version:" & outPutVersionRev)
            FileClose(2)

            Dim fileContent As String = ""
            FileOpen(3, classTemp, OpenMode.Input)
            Do Until (EOF(3))
                fileContent &= LineInput(3) & vbCrLf
            Loop
            FileClose(3)

            fileContent = fileContent.Replace("!VersionString", outPutVersion).Replace("!VersionRevString", outPutVersionRev)

            FileOpen(4, classFile, OpenMode.Output)
            Print(4, fileContent)
            FileClose(4)
            Console.WriteLine("Done")
        End If
        Console.WriteLine("End")
    End Sub

    Private Sub val(ByVal str As String)
        If counter = 1 Then
            path = str
            Console.WriteLine("工程路径：" & path)
            If path.EndsWith("\") = False Then path &= "\"
            If FileIO.FileSystem.DirectoryExists(path) = False Then
                fail = True
                Console.WriteLine("工程路径无效")
            End If
        End If
        If counter = 2 Then
            versionFile = str
            versionFile = path & versionFile
            If FileIO.FileSystem.FileExists(versionFile) = False Then
                fail = True
                Console.WriteLine("版本号文件不存在")
            End If
        End If
        If counter = 3 Then
            classTemp = str
            classTemp = path & classTemp
            If FileIO.FileSystem.FileExists(classTemp) = False Then
                fail = True
                Console.WriteLine("文件模板不存在")
            End If
        End If
        If counter = 4 Then
            classFile = str
            classFile = path & classFile
            If FileIO.FileSystem.FileExists(classFile) = False Then
                fail = True
                Console.WriteLine("输出文件不存在")
            End If
        End If
        counter += 1
    End Sub
End Module
