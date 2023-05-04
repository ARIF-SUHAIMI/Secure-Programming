﻿Imports System.Net
Imports System.Net.Mail

Public Class frmRegister
    Dim clsda As New DataAccess

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim strError As String = checkEmpty()
        If strError.Trim <> "" Then
            MsgBox(strError)
            Exit Sub
        Else
            insertIntoTale()
            sendEmail()
        End If

    End Sub

    Private Function checkEmpty() As String
        Dim strName = txtName.Text
        Dim strEmail = txtEmail.Text
        Dim strPass1 = txtPass1.Text
        Dim strPass2 = txtPass2.Text
        Dim strError As String = ""

        If strName.Trim = "" Then
            strError += "Please insert name" & vbCrLf
        End If
        If strEmail.Trim = "" Then
            strError += "Please insert email" & vbCrLf
        End If
        If (strPass1.Trim = "" OrElse strPass2.Trim = "") OrElse strPass1 <> strPass2 Then
            strError += "Please key in password" & vbCrLf
        End If

        Return strError
    End Function

    Private Sub insertIntoTale()
        Dim strName = txtName.Text
        Dim strEmail = txtEmail.Text
        Dim strPass1 = txtPass1.Text
        Dim strPass2 = txtPass2.Text
        Dim strError As String = ""

        Dim strSql As String = "INSERT INTO User_tb (UserName, Password) VALUES ('" & strName & "','" & strPass1 & "')"
        strError = clsda.ExecuteNonQuery(strSql)
        If strError <> "" Then
            MsgBox(strError)
        Else
            MsgBox("Succesfully Register")
        End If
    End Sub

    Private Sub sendEmail()
        Dim strName = txtName.Text
        Dim strEmail = txtEmail.Text
        Dim strPass1 = txtPass1.Text
        Dim strPass2 = txtPass2.Text
        Dim strError As String = ""
        Try

            ' Set up the email message
            Dim fromAddress As New MailAddress("testingaustralia4@gmail.com", "Developer")
            Dim toAddress As New MailAddress(strEmail.Trim, "Recipient")
            Dim subject As String = "New user registered"
            Dim body As String = "A new user has registered on our website."
            Dim message As New MailMessage(fromAddress, toAddress)
            message.Subject = subject
            message.Body = body

            ' Set up the Gmail SMTP client
            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            smtpClient.EnableSsl = True
            smtpClient.Credentials = New NetworkCredential("testingaustralia4@gmail.com", "agwvimydczrjqist")

            ' Send the email
            smtpClient.Send(message)
        Catch ex As Exception
            strError = ex.Message
        Finally
            If strError.Trim = "" Then
                ' Display a message to the user indicating that the email has been sent
                MessageBox.Show("Email sent successfully.")
            Else
                MsgBox(strError)
            End If
        End Try


    End Sub
End Class