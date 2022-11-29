Imports WindowsApplication1.frmMain

Public Class UserControl1
    Private Sub UserControl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenWindow()

    End Sub
    Private m_WindowList As New List(Of frmMain)
    Private Sub OpenWindow()
        Dim NewWindow As New frmMain
        m_WindowList.Add(NewWindow)

        NewWindow.Show()
    End Sub

End Class
