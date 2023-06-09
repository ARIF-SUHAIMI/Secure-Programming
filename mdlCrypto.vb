﻿Imports System.Security.Cryptography
Imports System.Text
Imports System.Net
Imports System.Net.Http
Imports System.Web

Module mdlCrypto
    Public Function GetHash(ByVal input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            ' Convert the input string to a byte array
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)

            ' Compute the hash value of the input byte array
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)

            ' Convert the hash byte array to a string
            Dim hashString As New StringBuilder()

            For i As Integer = 0 To hashBytes.Length - 1
                hashString.Append(hashBytes(i).ToString("X2"))
            Next

            Return hashString.ToString()
        End Using
    End Function

    Public Function GenerateSalt() As String
        ' Define the pool of characters to choose from
        Dim characters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-={}|::<>?"

        ' Generate a random 6-digit phrase by choosing characters from the pool
        Dim phrase As String = ""
        Dim rand As New Random()
        For i As Integer = 1 To 6
            phrase &= characters(rand.Next(0, characters.Length))
        Next

        Return phrase
    End Function

    Private Async Sub XSRF()
        Dim client As New HttpClient()

        ' Retrieve the anti-XSRF token from the server and include it in the request
        Dim antiCsrfToken As String = GetAntiXsrfTokenFromServer()
        Dim requestUri As String = "https://api.stripe.com"

        Dim content As New StringContent("sk_test_51KNBEbGa2z685qyKm9ieOvexY37LeNIPdu1SgeGmL0nB9RLwbgXH8kJRw1nWEY3MzSOA2aBBvRxILNsFFEFkM0jZ00IZNrPosl")
        content.Headers.Add("AntiCsrfToken", antiCsrfToken)

        Dim response As HttpResponseMessage = Await client.PostAsync(requestUri, content)

        ' Process the response
        ' ...

        ' Continue with your code logic here
    End Sub

    Private Function GetAntiXsrfTokenFromServer() As String
        ' Make a request to the server to retrieve the anti-CSRF token
        Dim requestUri As String = "https://api.stripe.com"

        Using client As New HttpClient()
            Dim response As HttpResponseMessage = client.GetAsync(requestUri).Result

            ' Check if the request was successful
            If response.IsSuccessStatusCode Then
                ' Extract the anti-CSRF token from the response
                Dim token As String = response.Content.ReadAsStringAsync().Result

                ' Return the token
                Return token
            Else
                ' Handle the case when the request fails
                ' You can throw an exception or return an empty string as per your application's requirements
                Return String.Empty
            End If
        End Using
    End Function

    Public Function EncodeHtml(ByVal input As String) As String
        ' Use the HttpUtility.HtmlEncode method to encode the input
        Return WebUtility.HtmlEncode(input)
    End Function
End Module
