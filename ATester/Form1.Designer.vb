<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtRX2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtRX = New System.Windows.Forms.RichTextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbl_portcount = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.txtSMS = New System.Windows.Forms.TextBox()
        Me.txtCallNo = New System.Windows.Forms.TextBox()
        Me.Sender1 = New System.Windows.Forms.GroupBox()
        Me.lblSeconds = New System.Windows.Forms.Label()
        Me.lblTenSecs = New System.Windows.Forms.Label()
        Me.lblColon = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.lbl_resetSim = New System.Windows.Forms.Label()
        Me.Reset_sim = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblSimStatus = New System.Windows.Forms.Label()
        Me.btn_ResetPort = New System.Windows.Forms.Button()
        Me.Close = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Sender1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SerialPort1
        '
        '
        'Timer1
        '
        Me.Timer1.Interval = 60000
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRX)
        Me.GroupBox1.Controls.Add(Me.txtRX2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 252)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 306)
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_portcount)
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
        'lbl_portcount
        '
        Me.lbl_portcount.Location = New System.Drawing.Point(99, 55)
        Me.lbl_portcount.Name = "lbl_portcount"
        Me.lbl_portcount.Size = New System.Drawing.Size(98, 27)
        Me.lbl_portcount.TabIndex = 81
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
        'txtSMS
        '
        Me.txtSMS.Location = New System.Drawing.Point(136, 160)
        Me.txtSMS.Multiline = True
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSMS.Size = New System.Drawing.Size(121, 42)
        Me.txtSMS.TabIndex = 73
        Me.txtSMS.Text = "Hello! SMS Test"
        '
        'txtCallNo
        '
        Me.txtCallNo.Location = New System.Drawing.Point(136, 124)
        Me.txtCallNo.Name = "txtCallNo"
        Me.txtCallNo.Size = New System.Drawing.Size(121, 20)
        Me.txtCallNo.TabIndex = 69
        Me.txtCallNo.Text = "+959"
        '
        'Sender1
        '
        Me.Sender1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Sender1.Controls.Add(Me.lblSeconds)
        Me.Sender1.Controls.Add(Me.lblTenSecs)
        Me.Sender1.Controls.Add(Me.lblColon)
        Me.Sender1.Controls.Add(Me.btnRefresh)
        Me.Sender1.Controls.Add(Me.Label3)
        Me.Sender1.Controls.Add(Me.lblMin)
        Me.Sender1.Controls.Add(Me.lbl_resetSim)
        Me.Sender1.Controls.Add(Me.Reset_sim)
        Me.Sender1.Controls.Add(Me.lblStatus)
        Me.Sender1.Controls.Add(Me.lblSimStatus)
        Me.Sender1.Controls.Add(Me.btn_ResetPort)
        Me.Sender1.Controls.Add(Me.Close)
        Me.Sender1.Controls.Add(Me.txtCallNo)
        Me.Sender1.Controls.Add(Me.txtSMS)
        Me.Sender1.Controls.Add(Me.GroupBox1)
        Me.Sender1.Controls.Add(Me.GroupBox2)
        Me.Sender1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Sender1.Location = New System.Drawing.Point(0, 0)
        Me.Sender1.MaximumSize = New System.Drawing.Size(379, 680)
        Me.Sender1.MinimumSize = New System.Drawing.Size(379, 680)
        Me.Sender1.Name = "Sender1"
        Me.Sender1.Size = New System.Drawing.Size(379, 680)
        Me.Sender1.TabIndex = 74
        Me.Sender1.TabStop = False
        Me.Sender1.Text = "Sender"
        '
        'lblSeconds
        '
        Me.lblSeconds.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeconds.Location = New System.Drawing.Point(206, 579)
        Me.lblSeconds.Name = "lblSeconds"
        Me.lblSeconds.Size = New System.Drawing.Size(31, 43)
        Me.lblSeconds.TabIndex = 89
        '
        'lblTenSecs
        '
        Me.lblTenSecs.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenSecs.Location = New System.Drawing.Point(166, 579)
        Me.lblTenSecs.Name = "lblTenSecs"
        Me.lblTenSecs.Size = New System.Drawing.Size(34, 43)
        Me.lblTenSecs.TabIndex = 87
        '
        'lblColon
        '
        Me.lblColon.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColon.Location = New System.Drawing.Point(150, 579)
        Me.lblColon.Name = "lblColon"
        Me.lblColon.Size = New System.Drawing.Size(19, 43)
        Me.lblColon.TabIndex = 86
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(274, 81)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 40)
        Me.btnRefresh.TabIndex = 85
        Me.btnRefresh.Text = "Refresh Port Count"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(30, 163)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 59)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "*Click if you encounter port problems or any errors"
        '
        'lblMin
        '
        Me.lblMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMin.ForeColor = System.Drawing.Color.Black
        Me.lblMin.Location = New System.Drawing.Point(121, 579)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(39, 43)
        Me.lblMin.TabIndex = 84
        '
        'lbl_resetSim
        '
        Me.lbl_resetSim.Location = New System.Drawing.Point(123, 222)
        Me.lbl_resetSim.Name = "lbl_resetSim"
        Me.lbl_resetSim.Size = New System.Drawing.Size(98, 27)
        Me.lbl_resetSim.TabIndex = 82
        '
        'Reset_sim
        '
        Me.Reset_sim.Location = New System.Drawing.Point(27, 113)
        Me.Reset_sim.Name = "Reset_sim"
        Me.Reset_sim.Size = New System.Drawing.Size(87, 40)
        Me.Reset_sim.TabIndex = 80
        Me.Reset_sim.Text = "Troubleshoot"
        Me.Reset_sim.UseVisualStyleBackColor = True
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
        'btn_ResetPort
        '
        Me.btn_ResetPort.Location = New System.Drawing.Point(274, 57)
        Me.btn_ResetPort.Name = "btn_ResetPort"
        Me.btn_ResetPort.Size = New System.Drawing.Size(75, 23)
        Me.btn_ResetPort.TabIndex = 75
        Me.btn_ResetPort.Text = "Reset Port"
        Me.btn_ResetPort.UseVisualStyleBackColor = True
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
        'Timer2
        '
        Me.Timer2.Interval = 15
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 648)
        Me.ControlBox = False
        Me.Controls.Add(Me.Sender1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(395, 687)
        Me.MinimumSize = New System.Drawing.Size(395, 687)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SMS Sender"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.Sender1.ResumeLayout(False)
        Me.Sender1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtRX2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents txtSMS As System.Windows.Forms.TextBox
    Friend WithEvents txtCallNo As System.Windows.Forms.TextBox
    Friend WithEvents Sender1 As GroupBox
    Friend WithEvents Close As Button
    Friend WithEvents txtRX As RichTextBox
    Friend WithEvents lblSimStatus As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnDisconnect As Button
    Friend WithEvents Reset_sim As Button
    Friend WithEvents lbl_portcount As Label
    Friend WithEvents lbl_resetSim As Label
    Friend WithEvents btn_ResetPort As Button
    Friend WithEvents lblMin As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents lblTenSecs As Label
    Friend WithEvents lblColon As Label
    Friend WithEvents lblSeconds As Label
End Class
