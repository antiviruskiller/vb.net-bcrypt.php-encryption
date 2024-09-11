Imports System.Net.Http

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization code, if needed, goes here
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        Dim client As New HttpClient()
        Dim values As New Dictionary(Of String, String) From {
            {"username", username},
            {"password", password}
        }

        Dim content As New FormUrlEncodedContent(values)

        Try
            Dim response As HttpResponseMessage = Await client.PostAsync("https://haxcore.net/login/reg.php", content)
            Dim responseString As String = Await response.Content.ReadAsStringAsync()

            ' Log the response string to help debug
            Console.WriteLine("Response: " & responseString)

            ' Update the response handling
            If responseString.Trim().Contains("Success") Then
                ' Check if Form2 is already open
                Dim form2Open As Boolean = False
                For Each form As Form In Application.OpenForms
                    If TypeOf form Is Form2 Then
                        form2Open = True
                        form.BringToFront()
                        Exit For
                    End If
                Next

                ' Show Form2 if not already open
                If Not form2Open Then
                    Dim form2 As New Form2()
                    form2.Show()
                End If

                MessageBox.Show("Registration successful!")
            Else
                MessageBox.Show("Registration failed: " & responseString)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        Dim client As New HttpClient()
        Dim values As New Dictionary(Of String, String) From {
            {"username", username},
            {"password", password}
        }

        Dim content As New FormUrlEncodedContent(values)

        Try
            Dim response As HttpResponseMessage = Await client.PostAsync("https://haxcore.net/login/login1.php", content)
            Dim responseString As String = Await response.Content.ReadAsStringAsync()

            If responseString.Trim() = "Login successful" Then
                ' Handle successful login
                MessageBox.Show("Login successful!")
                ' Optionally show Form2 or another form
                Dim form2 As New Form2()
                form2.Show()
                Me.Hide() ' Optionally hide Form1
            Else
                TextBox1.Text = ("Login failed: " & responseString & vbNewLine)
            End If
        Catch ex As Exception
            
            TextBox1.Text = ("Error: " & ex.Message)
        End Try

    End Sub
End Class
