Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.AccessControl
Imports System.Management

Public Class AddFolder

    Private Sub AddFolder_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If TextBox1.Text = "" Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
                Dim fs As FileSystemSecurity = File.GetAccessControl(FolderBrowserDialog1.SelectedPath)
                fs.RemoveAccessRule(New FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Deny))
                File.SetAccessControl(TextBox1.Text, fs)

                Dim last As String
                last = IO.Path.GetFileName(FolderBrowserDialog1.SelectedPath)

                Dim counterF = My.Computer.FileSystem.GetFiles(TextBox1.Text)
                Dim fileCount As Integer = IO.Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Count()
                Dim nyimpen As ListViewItem
                nyimpen = mainForm.ListView1.Items.Add(last)
                nyimpen.SubItems.Add(fileCount)
                nyimpen.SubItems.Add(TextBox1.Text)
                nyimpen.SubItems.Add("Unlocked")
            Catch ex As Exception
                Dim pilfolder As String = FolderBrowserDialog1.SelectedPath
                Dim fs As FileSystemSecurity = File.GetAccessControl(pilfolder)
                fs.RemoveAccessRule(New FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Deny))
                File.SetAccessControl(pilfolder, fs)

                Dim last As String
                last = IO.Path.GetFileName(FolderBrowserDialog1.SelectedPath)

                Dim counterF = My.Computer.FileSystem.GetFiles(TextBox1.Text)
                Dim fileCount As Integer = IO.Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Count()
                Dim nyimpen As ListViewItem
                nyimpen = mainForm.ListView1.Items.Add(last)
                nyimpen.SubItems.Add(fileCount)
                nyimpen.SubItems.Add(TextBox1.Text)
                nyimpen.SubItems.Add("Unlocked")
            End Try
        
        Me.Close()
    End Sub
End Class