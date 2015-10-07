Imports System.Security.AccessControl
Imports System.IO
Imports System.Management
Imports System.Security
Imports System.Net

Public Class mainForm

    Private Sub mainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Refresh()

        Dim myCoolFileLines() As String = System.IO.File.ReadAllLines(My.Application.Info.DirectoryPath & "\tempfolders.tbl")
        For Each line As String In myCoolFileLines
            Dim lineArray() As String = line.Split("#")
            Dim newItem As New ListViewItem(lineArray(0))
            newItem.SubItems.Add(lineArray(1))
            newItem.SubItems.Add(lineArray(2))
            newItem.SubItems.Add(lineArray(3))
            ListView1.Items.Add(newItem)
        Next

        Timer2.Start()
    End Sub

    Private Sub mainForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim mywriter As New IO.StreamWriter(My.Application.Info.DirectoryPath & "\tempfolders.tbl")
        For Each myItem As ListViewItem In ListView1.Items
            mywriter.WriteLine(myItem.Text & "#" & myItem.SubItems(1).Text & "#" & myItem.SubItems(2).Text & "#" & myItem.SubItems(3).Text)
        Next
        mywriter.Close()
        Application.Exit()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/DeVoresyah/vFolderLocker-Open-Source")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://rully-ardiansyah.blogspot.com")
    End Sub

    Private Sub btnLock_Click(sender As System.Object, e As System.EventArgs) Handles btnLock.Click
        Dim fs As FileSystemSecurity = File.GetAccessControl(TextBox1.Text)
        fs.AddAccessRule(New FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Deny))
        File.SetAccessControl(TextBox1.Text, fs)
        ListView1.SelectedItems(0).SubItems(3).Text = "Locked"
        MessageBox.Show("Your folder is now locked", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnUnlock_Click(sender As System.Object, e As System.EventArgs) Handles btnUnlock.Click
        Dim fs As FileSystemSecurity = File.GetAccessControl(TextBox1.Text)
        fs.RemoveAccessRule(New FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Deny))
        File.SetAccessControl(TextBox1.Text, fs)
        ListView1.SelectedItems(0).SubItems(3).Text = "Unlocked"
        MessageBox.Show("Your folder is now unlocked", "Unlocked", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        AddFolder.ShowDialog()
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        Try
            Dim fs As FileSystemSecurity = File.GetAccessControl(ListView1.SelectedItems(0).SubItems(2).Text)
            fs.RemoveAccessRule(New FileSystemAccessRule(Environment.UserName, FileSystemRights.FullControl, AccessControlType.Deny))
            File.SetAccessControl(ListView1.SelectedItems(0).SubItems(2).Text, fs)
            ListView1.Items.Remove(ListView1.SelectedItems(0))
        Catch ex As Exception
            MessageBox.Show("Select The Folders To Remove!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnHelp_Click(sender As System.Object, e As System.EventArgs) Handles btnHelp.Click
        Try
            System.Diagnostics.Process.Start(My.Application.Info.DirectoryPath & "/Documentations/index.html")
        Catch ex As Exception
            MessageBox.Show("Cant't find the documentation file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        If TextBox1.Text = "" Then
            btnLock.Enabled = False
            btnUnlock.Enabled = False
        Else
            btnLock.Enabled = True
            btnUnlock.Enabled = True
        End If
        Try
            TextBox1.Text = ListView1.SelectedItems(0).SubItems(2).Text
            lblStatusFolder.Text = ListView1.SelectedItems(0).SubItems(3).Text
        Catch ex As Exception
            TextBox1.Text = ""
            lblStatusFolder.Text = ""
        End Try
    End Sub
End Class
