Imports System.Net.Sockets
Imports System.Text
Public Class Form1
    Dim tcp As New TcpClient
    Dim ns As NetworkStream
    Public Sub comand()
        Try
            Dim rbyte As Byte() = New Byte(1000) {}
            ns.Read(rbyte, 0, rbyte.Length)
            ns.Flush()
            Dim w As String = Encoding.ASCII.GetString(rbyte)
            Dim tamanho As Integer = Convert.ToInt16(w)
            Dim r As Byte() = New Byte(tamanho) {}
            ns.Read(r, 0, r.Length)
            ns.Flush()
            Dim show As String = Encoding.ASCII.GetString(r)
            RichTextBox1.Text = show
            ns.Flush()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            tcp.Connect(TextBox1.Text, TextBox2.Text)
            ns = tcp.GetStream
            MessageBox.Show("Connect Successfly...")
        Catch ex As Exception
            MessageBox.Show("Connect Fali...")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If tcp.Connected = True Then
                Dim bytes As Byte() = Encoding.ASCII.GetBytes(TextBox3.Text)
                ns.Write(bytes, 0, bytes.Length)
                ns.Flush()
                comand()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
