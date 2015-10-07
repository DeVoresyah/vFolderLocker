Imports System.IO
Imports System.Management
Imports System.Security
Imports Microsoft.Win32

Public Class start

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 5
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            Timer1.Enabled = False
            Me.Hide()
            mainForm.Show()
        End If
    End Sub

    Private Sub start_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Value = 0
        If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\tempfolders.tbl") Then
            Timer1.Enabled = True
        Else
            Dim objCreate As New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "\tempfolders.tbl")
            objCreate.Close()
            Timer1.Enabled = True
        End If
    End Sub
End Class