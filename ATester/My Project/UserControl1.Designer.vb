<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControl1
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Sender1 = New System.Windows.Forms.GroupBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblSimStatus = New System.Windows.Forms.Label()
        Me.btn_Restart = New System.Windows.Forms.Button()
        Me.Close = New System.Windows.Forms.Button()
        Me.txtCallNo = New System.Windows.Forms.TextBox()
        Me.txtSMS = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtRX = New System.Windows.Forms.RichTextBox()
        Me.txtRX2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.Sender1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Sender1
        '
        Me.Sender1.Controls.Add(Me.lblStatus)
        Me.Sender1.Controls.Add(Me.lblSimStatus)
        Me.Sender1.Controls.Add(Me.btn_Restart)
        Me.Sender1.Controls.Add(Me.Close)
        Me.Sender1.Controls.Add(Me.txtCallNo)
        Me.Sender1.Controls.Add(Me.txtSMS)
        Me.Sender1.Controls.Add(Me.GroupBox1)
        Me.Sender1.Controls.Add(Me.GroupBox2)
        Me.Sender1.Location = New System.Drawing.Point(143, 3)
        Me.Sender1.Name = "Sender1"
        Me.Sender1.Size = New System.Drawing.Size(388, 564)
        Me.Sender1.TabIndex = 75
        Me.Sender1.TabStop = False
        Me.Sender1.Text = "Sender1"
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(271, 168)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(108, 31)
        Me.lblStatus.TabIndex = 76
        '
        'lblSimStatus
        '
        Me.lblSimStatus.Location = New System.Drawing.Point(274, 124)
        Me.lblSimStatus.Name = "lblSimStatus"
        Me.lblSimStatus.Size = New System.Drawing.Size(108, 31)
        Me.lblSimStatus.TabIndex = 0
        '
        'btn_Restart
        '
        Me.btn_Restart.Location = New System.Drawing.Point(274, 57)
        Me.btn_Restart.Name = "btn_Restart"
        Me.btn_Restart.Size = New System.Drawing.Size(75, 23)
        Me.btn_Restart.TabIndex = 75
        Me.btn_Restart.Text = "Restart"
        Me.btn_Restart.UseVisualStyleBackColor = True
        '
        'Close
        '
        Me.Close.Location = New System.Drawing.Point(274, 28)
        Me.Close.Name = "Close"
        Me.Close.Size = New System.Drawing.Size(75, 23)
        Me.Close.TabIndex = 74
        Me.Close.Text = "Close"
        Me.Close.UseVisualStyleBackColor = True
        '
        'txtCallNo
        '
        Me.txtCallNo.Location = New System.Drawing.Point(129, 124)
        Me.txtCallNo.Name = "txtCallNo"
        Me.txtCallNo.Size = New System.Drawing.Size(121, 20)
        Me.txtCallNo.TabIndex = 69
        Me.txtCallNo.Text = "+959"
        '
        'txtSMS
        '
        Me.txtSMS.Location = New System.Drawing.Point(129, 168)
        Me.txtSMS.Multiline = True
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSMS.Size = New System.Drawing.Size(121, 42)
        Me.txtSMS.TabIndex = 73
        Me.txtSMS.Text = "Hello! SMS Test"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRX)
        Me.GroupBox1.Controls.Add(Me.txtRX2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 235)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 315)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Modem Respond"
        '
        'txtRX
        '
        Me.txtRX.Location = New System.Drawing.Point(18, 42)
        Me.txtRX.Name = "txtRX"
        Me.txtRX.Size = New System.Drawing.Size(294, 139)
        Me.txtRX.TabIndex = 75
        Me.txtRX.Text = ""
        '
        'txtRX2
        '
        Me.txtRX2.Location = New System.Drawing.Point(16, 209)
        Me.txtRX2.Multiline = True
        Me.txtRX2.Name = "txtRX2"
        Me.txtRX2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRX2.Size = New System.Drawing.Size(296, 87)
        Me.txtRX2.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Received"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 193)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Received (HEX)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbPort)
        Me.GroupBox2.Controls.Add(Me.btnDisconnect)
        Me.GroupBox2.Controls.Add(Me.btnConnect)
        Me.GroupBox2.Location = New System.Drawing.Point(27, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(223, 85)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "COM Port"
        '
        'cmbPort
        '
        Me.cmbPort.FormattingEnabled = True
        Me.cmbPort.Location = New System.Drawing.Point(99, 25)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.Size = New System.Drawing.Size(111, 21)
        Me.cmbPort.TabIndex = 7
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(18, 52)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btnDisconnect.TabIndex = 6
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(18, 24)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 5
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'UserControl1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Sender1)
        Me.Name = "UserControl1"
        Me.Size = New System.Drawing.Size(978, 595)
        Me.Sender1.ResumeLayout(False)
        Me.Sender1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Sender1 As GroupBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblSimStatus As Label
    Friend WithEvents btn_Restart As Button
    Friend WithEvents Close As Button
    Friend WithEvents txtCallNo As TextBox
    Friend WithEvents txtSMS As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtRX As RichTextBox
    Friend WithEvents txtRX2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cmbPort As ComboBox
    Friend WithEvents btnDisconnect As Button
    Friend WithEvents btnConnect As Button
End Class
