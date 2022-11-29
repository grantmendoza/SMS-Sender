Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Security
Imports System.Threading
Imports System.Threading.Tasks
Imports MySqlConnector

Public Class frmMain
    Inherits System.Windows.Forms.Form
    Dim rxd As Char

    Dim myPort As Array                                     'var to store all COM ports
    Delegate Sub SetTextCallback(ByVal [text] As String)    'Added to prevent threading errors during receiveing of data

    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    'MessageID from database
    Dim msgid As String = ""

    'Number of ports in use - retrieved from database
    Dim port_Count

    'Sim number
    Dim simNo

    Dim conn As MySqlConnection

    'For CheckQueue
    Dim istrue

    'Stops timer when true
    Dim endtimer As Boolean = False


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'lists all serial ports 
        myPort = IO.Ports.SerialPort.GetPortNames()     ' ports list

        Try
            For i = 2 To UBound(myPort)
                cmbPort.Items.Add(myPort(i))
            Next
            cmbPort.Text = cmbPort.Items.Item(0)    'COM no 1

        Catch
            'lblMessage.Text = "No Ports Found"
            MessageBox.Show("No Ports Found")
            Application.Exit()

        End Try

        btnDisconnect.Enabled = False
        btn_ResetPort.Enabled = False
        portCount()

    End Sub

    'Connect to Port
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        lblMin.Text = ""
        Dim Clicks As String

        Clicks = DirectCast(sender, Button).Name

        If Clicks = "btnConnect" Then

            SerialPort1.Close()
            SerialPort1.PortName = cmbPort.Text       'Set COM port
            SerialPort1.BaudRate = "19200"        'Set Baud rate 

            'Other Serial Port Property
            SerialPort1.Parity = IO.Ports.Parity.None
            SerialPort1.StopBits = IO.Ports.StopBits.One
            SerialPort1.DataBits = 8                    'Open serial port
            SerialPort1.ReadTimeout = 2000
            SerialPort1.WriteTimeout = 2000
            Try
                SerialPort1.Open()

                btnConnect.Enabled = False
                btnDisconnect.Enabled = True

            Catch
                lblMin.Text = "Port in use"
                'MessageBox.Show("PORT IN USE")

                'update_PortCount()
                btnConnect.Enabled = True
                btnDisconnect.Enabled = False
            End Try
            endtimer = 0
            lblMin.Text = ""
            lblColon.Text = ""
            lblTenSecs.Text = ""
            lblSeconds.Text = ""

            FindBaud()

        End If
    End Sub

    'Disconnects from port
    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click

        Dim Clicks As String

        Clicks = DirectCast(sender, Button).Name

        If Clicks = "btnDisconnect" Then

            istrue = False

            lbl_portcount.Text = "Ports in use: " & portCount()
            SerialPort1.Close()                         'Close Serial Port

            lblStatus.ForeColor = Color.Red
            lblStatus.Text = "Disconnected"
            lblSimStatus.Text = ""

            txtRX.Clear()


            btnConnect.Enabled = True
            btnDisconnect.Enabled = False
            SimReset()

            msgid = ""

            endtimer = True

            'reset timer labels
            lblMin.Text = ""
            lblColon.Text = ""
            lblTenSecs.Text = ""
            lblSeconds.Text = ""

        End If
    End Sub
    ' # debug test

    'Counts number of ports in use - Counts how many sim cards in use from database
    Private Function portCount()
        Dim count
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        Dim sqlquery = "SELECT COUNT(*) from sim_card where sim_status = 'In-Use' "
        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                port_Count = dr("COUNT(*)")
            End While
            dr.Close()
        End If
        Return port_Count

        GetCon.Close()

    End Function

    'Automatically finds responsive Baud Rate and finds the sim card in use
    Private Function FindBaud()
        Try
            Do
                SerialPort1.Write("AT+CGMM" & vbCr)
                Delay(0.04)
                'MessageBox.Show("1 returns: " & GetRX())

                If GetRX() <> "OK" Then
                    SerialPort1.Close()
                    SerialPort1.BaudRate = "57600"
                    SerialPort1.Open()
                    SerialPort1.Write("AT+CGMM" & vbCr)
                    Delay(0.04)
                    'MessageBox.Show("1 at 57600 returns: " & GetRX())
                End If

                If GetRX() <> "OK" Then
                    SerialPort1.Close()
                    SerialPort1.BaudRate = "38400"
                    SerialPort1.Open()
                    SerialPort1.Write("AT+CGMM" & vbCr)
                    Delay(0.04)
                    'MessageBox.Show("1 at 38400 returns: " & GetRX())
                End If

                If GetRX() <> "OK" Then
                    SerialPort1.Close()
                    SerialPort1.BaudRate = "115200"
                    SerialPort1.Open()
                    SerialPort1.Write("AT+CGMM" & vbCr)
                    Delay(0.04)
                    'MessageBox.Show("1 at 115200 returns: " & GetRX())
                End If

                If GetRX() <> "OK" Then
                    SerialPort1.Close()
                    SerialPort1.BaudRate = "9600"
                    '115200
                    '9600
                    SerialPort1.Open()
                    SerialPort1.Write("AT+CGMM" & vbCr)
                    Delay(0.04)
                    'MessageBox.Show("1 at 9600 returns: " & GetRX())
                End If
                'Catch Error
                If GetRX() <> "OK" Then
                    lblStatus.ForeColor = Color.Red
                    lblStatus.Text = "Error"

                ElseIf GetRX().Contains("ERROR: 304") Then
                ElseIf GetRX() = "OK" Then
                    lblStatus.ForeColor = Color.Green
                    lblStatus.Text = "Connected"
                    Exit Do
                    'CheckQueue()
                End If

            Loop Until GetRX() = "OK"

            If GetRX() = "OK" Then
                'Check Sim number-  end if no sim
                SerialPort1.Write("AT+CNUM" & vbCr)
                Delay(0.04)

                If GetRX() = "OK" Then

                    FindSim()
                    Try
                        Do

                            SelectChecking()
                            'MessageBox.Show(simNo)

                            If txtRX.Text.Contains(simNo) Then
                                SimInUse()
                                lblSimStatus.ForeColor = Color.Green
                                lblSimStatus.Text = SimName() & " IN USE"
                                'port_Count = portCount() + 1
                                'port_Count = portCount() + 1
                                'update_PortCount()
                                lbl_portcount.Text = "Ports in use: " & portCount()
                                'ResetChecking()
                                btn_ResetPort.Enabled = True
                                Exit Do
                            Else
                                SimNotMatch()
                            End If

                            If NotMatchVSTotal() = True Then
                                lblSimStatus.ForeColor = Color.Red
                                lblSimStatus.Text = "SIM NOT RECOGNIZED"
                                Exit Do
                            End If
                            FindSim()
                        Loop Until SimCheckCount() = 0

                        ResetNotMatch()
                        ResetChecking()

                    Catch
                        ResetChecking()
                        ResetNotMatch()
                    End Try

                Else
                    lblSimStatus.ForeColor = Color.Red
                    lblSimStatus.Text = "NO SIM CARD"
                    btnDisconnect.Enabled = False
                    btnConnect.Enabled = True

                End If

            End If
            SendProcess()

        Catch

            'update_PortCount()
            btnConnect.Enabled = True
            btnDisconnect.Enabled = False
            'MessageBox.Show("PORT IN USE")
        End Try

    End Function

    'Find sim card that is available
    Private Function FindSim()
        Dim count
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        If AvailableCount() > 0 Then
            Dim sqlquery = "SELECT * FROM sim_card WHERE sim_status = 'Available' "

            myCommand.Connection = GetCon()
            myCommand.CommandText = sqlquery
            Dim dr = myCommand.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    '1
                    simNo = dr("simNo").ToString
                End While
                dr.Close()
            End If
        End If
        SetToChecking()
        GetCon.Close()

    End Function

    'Counts number of sim card that is Available
    Private Function AvailableCount()
        Dim count
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        Dim sqlquery = "SELECT Count(*) FROM sim_card WHERE sim_status = 'Available';"

        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                count = dr("Count(*)")
            End While
            dr.Close()
        End If
        Return count
        GetCon.Close()

    End Function

    'Sets sim card status to checking
    Private Function SetToChecking()

        Dim sqlquery = "UPDATE sim_card set sim_status = 'Checking' WHERE simNo = '" & simNo & "'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'Number of sim cards currently being checked
    Private Function SimCheckCount()

        Dim check_count
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        Dim sqlquery = "SELECT Count(*) FROM sim_card WHERE sim_status = 'Checking';"

        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                check_count = dr("Count(*)")
            End While
            dr.Close()
        End If

        Return check_count
        GetCon.Close()

    End Function

    'Select single sim card with 'Checking' status
    Private Function SelectChecking()
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        Dim sqlquery = "SELECT simNo FROM sim_card WHERE sim_status = 'Checking';"

        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                simNo = dr("simNo")
            End While
            dr.Close()
        End If

        Return simNo
        GetCon.Close()

    End Function

    'Sets the number of confirmed sim card to In-Use status in database
    Private Function SimInUse()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'In-Use' WHERE simNo = '" & simNo & "'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'Sets status of sim card that didn't match to 'not-match' to prevent being picked up again until the correct sim card is chosen from the database.
    Private Function SimNotMatch()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'not-match' WHERE simNo = '" & simNo & "'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'checks if all the sim cards are are marked not-match. This means sim card in modem is not registered in database.
    Private Function NotMatchVSTotal()
        Dim not_match
        Dim all_sim
        Dim isequal = False
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()

        Dim sqlquery = "SELECT Count(*) FROM sim_card WHERE sim_status = 'not-match';"

        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                not_match = dr("Count(*)")
            End While
            dr.Close()
        End If


        sqlquery = "SELECT Count(*) FROM sim_card;"

        myCommand.CommandText = sqlquery

        dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                all_sim = dr("Count(*)")
            End While
            dr.Close()
        End If

        If not_match = all_sim Then
            isequal = True
        End If
        Return isequal
        GetCon.Close()

    End Function

    'reset all the sim cards with status 'not-match' to Available
    Private Function ResetNotMatch()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'Available' WHERE sim_status = 'not-match'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'Resets current sim picked up using msgid thus making port available.
    Private Function SimReset()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'Available' WHERE simNo = '" & simNo & "'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'resets all sim card with 'Checking' status back to 'Available'  
    Private Function ResetChecking()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'Available' WHERE sim_status = 'Checking'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function


    'Resets all the status of all sim cards in database thus, resetting the number of port in use to 0
    Private Function ResetAllSim()
        Dim sqlquery = "UPDATE sim_card set sim_status = 'Available' "

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function

    'Get Sim name of sim card in use from database
    Private Function SimName()

        Dim sqlquery As String
        Dim sim_name As String
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        sqlquery = "SELECT sim_name FROM sim_card WHERE simNo = '" & simNo & "'"
        myCommand.CommandText = sqlquery
        Dim dr = myCommand.ExecuteReader

        If dr.HasRows Then
            While dr.Read
                '1
                sim_name = dr("sim_name").ToString
                'SettoProcessing()
            End While
            dr.Close()
        End If
        Return sim_name
        GetCon.Close()
    End Function

    'Timer with interval of 5 seconds
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        'add delayed code here
        Timer1.Interval = 5
        Timer1.Start()

    End Sub

    'Converts hex to string which is shown in txtRX
    Private Function ByteArrayToHexString(ByVal data() As Byte) As String

        Dim sb As New System.Text.StringBuilder(data.Length * 3)
        For Each b As Byte In data
            sb.Append(Convert.ToString(b, 16).PadLeft(2, "0"c).PadRight(3, " "c))
        Next
        Return sb.ToString.ToUpper

    End Function

    'From http://stackoverflow.com/questions/10504034/how-do-you-convert-a-string-into-hexadecimal-in-vb-net

    Public Function StrToHex(ByRef Data As String) As String
        Dim sVal As String
        Dim sHex As String = ""

        While Data.Length > 0
            sVal = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()))
            Data = Data.Substring(1, Data.Length - 1)
            sHex = sHex & " " & sVal
        End While
        Return sHex
    End Function


    'Executes AT Commands
    Private Function SendProcess()
        Try

            If GetRX() = "OK" Then
                btnRefresh.PerformClick()
                'MessageBox.Show("Inside the try")
                'Do Loop until pending is 0
                Do

                    portCount()
                    lbl_portcount.Text = "Ports in use: " & port_Count
                    CheckQueue()
                    'MessageBox.Show("Inside the pending do loop")
                    'Check if there are any pending and processing - Boolean
                    GetID()

                    If CheckQueue() = True Then
                        Exit Do
                    End If
                    'MessageBox.Show("CheckQueue is False")

                    'Do loop until Processing is 0
                    Do
                        If CheckQueue() = True Then
                            Exit Do
                        End If
                        'MessageBox.Show("Inside the processing do loop")

                        ProcessCount()
                        'Pre Processing 
                        'MessageBox.Show("After Process Count")

                        'Check if there are any pending and processing
                        If CheckQueue() = True Then
                            'MessageBox.Show("CheckQueue is 0")
                            Exit Do
                        End If
                        'MessageBox.Show("CheckQueue is not 0")

                        'Make sure textbox is clear
                        txtRX.Clear()

                        'set status and button settings during sending process
                        lblStatus.ForeColor = Color.Black
                        lblStatus.Text = "Sending..."
                        btnConnect.Enabled = False
                        btnDisconnect.Enabled = False
                        Close.Enabled = False
                        Reset_sim.Enabled = False

                        'exit when message is too long
                        If txtRX.Text.Contains("NO CARRIER") Then
                            UpdateToPending()
                            MessageBox.Show("Message Too Long")
                            Close.Enabled = True
                            Exit Do
                        End If

                        'Check if there are pending and processing
                        If PendingCount() = 0 Then
                            'get message ID based on status
                            GetID()
                        ElseIf PendingCount() = 0 And ProcessCount() > 0 Then
                            'Get Message and number based on status
                            GetMessage_No()
                        ElseIf PendingCount() = 0 And ProcessCount() = 0 Then
                            Exit Do
                        End If

                        'Do Loop for Sending Commands
                        'Delays are necessary to ensure the proper response is being caught
                        Do
                            'execute 2nd Command
                            SerialPort1.Write("AT+CGMI" & vbCr)
                            Delay(0.03)
                            'MessageBox.Show("2nd command exec")

                            'Check 2nd Command response and execute 3rd Command
                            If GetRX() = "OK" Then

                                'execute 3rd Command
                                SerialPort1.Write("AT+CREG?" & vbCr)
                                Delay(0.0288)

                                'check 3rd command and execute 4th command
                                If GetRX() = "OK" Then

                                    'execute 4th command
                                    SerialPort1.Write("AT+CGREG?" & vbCr)
                                    Delay(0.0283)

                                    'check 4th command and execute 5th command
                                    If GetRX() = "OK" Then

                                        'execute 5th command
                                        SerialPort1.Write("AT+CMGF?" & vbCr)
                                        Delay(0.036)

                                        'check 5th command and execute 6th command
                                        If GetRX() = "OK" Then

                                            'execute 6th command
                                            SerialPort1.Write("AT + CMGF = 1 " & vbCr)
                                            Delay(0.036)

                                            'check for 6th command and execute 7th command

                                            'check 6th command and execute 7th command- number and message command
                                            If GetRX() = "OK" Then
                                                '7th and 8th command
                                                'Command for number
                                                GetMessage_No()
                                                SerialPort1.Write("AT+CMGS=" & Chr(34) & "0" & txtCallNo.Text & Chr(34) & vbCr)
                                                Delay(0.04)

                                                'Command for message
                                                SerialPort1.Write(txtSMS.Text & Chr(26) & vbCr)
                                                Delay(6)

                                                'check every 5 seconds if message takes time to send
                                                GetRX()
                                                '5 seconds
                                                Timer1.Start()
                                                GetRX()

                                                '10 secs
                                                Timer1.Start()
                                                GetRX()

                                                '15 secs
                                                Timer1.Start()
                                                GetRX()

                                                '20 secs
                                                Timer1.Start()
                                                GetRX()

                                                '25 secs
                                                Timer1.Start()
                                                GetRX()

                                                '30 secs
                                                Timer1.Start()
                                                GetRX()

                                                '35 secs
                                                Timer1.Start()
                                                GetRX()

                                                '40 secs
                                                Timer1.Start()
                                                GetRX()

                                                '45 secs
                                                Timer1.Start()
                                                GetRX()

                                                '50 secs
                                                Timer1.Start()
                                                GetRX()

                                                '55 secs
                                                Timer1.Start()
                                                GetRX()

                                                '60 secs
                                                Timer1.Start()
                                                GetRX()

                                                '65 secs
                                                Timer1.Start()
                                                Timer1.Stop()

                                                'check for 7th and 8th command
                                                'Check if message is sent 

                                                If txtRX.Text.Contains("ERROR") = False And Not txtRX.Text.Contains("ERROR: 304") Then
                                                    UpSent()
                                                    Exit Do
                                                Else
                                                    UpdateToPending()
                                                    Exit Do
                                                End If
                                            Else 'for 6th command
                                                UpdateToPending()
                                                Exit Do
                                            End If
                                        Else 'for 5th command
                                            UpdateToPending()
                                            Exit Do
                                        End If
                                    Else 'for 4th command
                                        UpdateToPending()
                                        Exit Do
                                    End If
                                Else 'for 3rd command
                                    UpdateToPending()
                                    Exit Do
                                End If
                            Else 'for 2nd command
                                UpdateToPending()
                                Exit Do
                            End If
                            CheckQueue()
                            portCount()
                            lbl_portcount.Text = "Ports in use: " & portCount()

                        Loop Until txtRX.Text.Contains("ERROR")
                        CheckQueue()
                    Loop Until ProcessCount() = 0
                    CheckQueue()
                Loop Until PendingCount() = 0
                CheckQueue()
            Else
                UpdateToPending()
            End If
        Catch
            UpdateToPending()
        End Try
    End Function

    'Checks if there are any pending and processing status
    Private Function CheckQueue()
        istrue = False
        lblMin.Text = ""
        lblColon.Text = ""
        lblTenSecs.Text = ""
        lblSeconds.Text = ""

        If PendingCount() = 0 And ProcessCount() = 0 Then
            lblStatus.ForeColor = Color.Green
            lblStatus.Text = "Queue Finished"
            istrue = True
            lblSimStatus.Text = "Will restart in 5 minutes"
            'Will rerun code after 5 mins
            ' Delay(300)

            lblMin.Text = 5
            lblColon.Text = ":"

            lblTenSecs.Text = 0

            lblSeconds.Text = 0
            mins_count()

            lblSimStatus.Text = ""
            endtimer = True

            If SerialPort1.IsOpen Then
                btnDisconnect.PerformClick()
                SimReset()
                SerialPort1.Close()

                msgid = ""

                btnConnect.PerformClick()
            Else

            End If


            lblMin.Text = ""
            lblColon.Text = ""
            lblTenSecs.Text = ""
            lblSeconds.Text = ""

        End If

        Return istrue

    End Function

    Private Sub ReceivedText(ByVal [text] As String)
        'compares the ID of the creating Thread to the ID of the calling Thread
        If Me.txtRX.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            'Me.txtRX.Text &= [text]
            txtRX.AppendText(text & vbCrLf)
            'txtRX2.AppendText(Convert.ToInt32(text, 16) & vbCrLf)
            'txtRX2.AppendText(Hex(text) & vbCrLf)
            txtRX2.AppendText(StrToHex(text) & vbCrLf)
            rxd = text
            If rxd = "#" Then
                MsgBox("This is a test")
            End If
        End If
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())            'event call for data receving, read until data is completed
        'ReceivedText(SerialPort1.ReadLine())               ' read until line end

    End Sub

    'From http://stackoverflow.com/questions/10504034/how-do-you-convert-a-string-into-hexadecimal-in-vb-net


    'Used for delaying code
    Sub Delay(ByVal dblSecs As Double)
        'From http://pastebin.com/2bSWZ16p

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()                      'Allow windows messages to be processed
        Loop

    End Sub

    Private Sub cmbPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SerialPort1.IsOpen = False Then
            SerialPort1.PortName = cmbPort.Text
        Else
            'MessageBox.Show("PORT IN USE!")
        End If
    End Sub


    'Close Button - with 2 second delay to allow setting to pending
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Close.Click

        lblStatus.Text = "CLOSING... PLEASE WAIT"
        Delay(1)
        If SerialPort1.IsOpen Then
            UpdateToPending()
        End If

        SimReset()
        Application.Exit()


    End Sub

    'Set to show bottom of textbox
    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtRX.TextChanged
        txtRX.ScrollToCaret()
    End Sub


    'reads the last line of AT Command response
    Private Function GetRX()
        Dim response = ""
        For i As Integer = Me.txtRX.Lines.GetUpperBound(0) To 0 Step -1
            If Me.txtRX.Lines(i).Trim <> "" Then
                response = txtRX.Lines(i)
                Exit For
            End If
        Next

        Return response
    End Function

    'Database Connection
    Private Function GetCon()
        Dim conn As MySqlConnection

        'Connect to the database using these credentials
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; user id=root; password=; database=sms_sender"

        'Try and connect (conn.open)
        Try
            conn.Open()

        Catch myerror As MySqlException 'If it fails do this... (i.e. no internet connection, etc.)
            'MsgBox("Error connecting to database. Check your internet connection.", MsgBoxStyle.Critical)

            'UpdateToPending()
            'SendProcess()
        End Try
        Return conn
    End Function

    'get message ID based on status
    Private Function GetID()

        Dim Lim As Integer = 0

        'Dim myAdapter As New MySqlDataAdapter
        'Tell where to find the file with the emails/passes stored
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        Dim sqlquery As String = ""

        If ProcessCount() > 0 Then

            If ProcessCount() / portCount() >= 5 Then
                Lim = 5
            Else
                Lim = ProcessCount() / portCount()
            End If

            sqlquery = "SELECT msg_id FROM message_table WHERE status = 'processing-" & simNo & "' Limit " & Lim

            myCommand.Connection = GetCon()
            myCommand.CommandText = sqlquery
            Dim dr = myCommand.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    '1
                    msgid = dr("msg_id").ToString
                    SettoProcessing()
                End While
                dr.Close()
            End If

        ElseIf ProcessCount() = 0 Then

            myCommand.Connection = GetCon()
            myCommand.CommandText = sqlquery

            If PendingCount() > 0 Then

                If PendingCount() / portCount() >= 5 Then
                    Lim = 5
                ElseIf PendingCount() / portCount() < 5 And PendingCount() / portCount() >= 1 Then
                    'MessageBox.Show("Lim is less than 5 but Greater than 1")
                    Lim = PendingCount() / portCount()
                ElseIf PendingCount() / portCount() < 1 And PendingCount() / portCount() >= 0 Then
                    'MessageBox.Show("Lim is less than 1 or 0")
                    Lim = 1
                    'ElseIf PendingCount() < portCount Then
                    'Lim = 1
                End If

                'MessageBox.Show(PendingCount() & "/" & portCount & "=" & Lim)
                sqlquery = "SELECT msg_id FROM message_table WHERE status = 'pending' Limit " & Lim

                myCommand.Connection = GetCon()
                myCommand.CommandText = sqlquery
                Dim dr = myCommand.ExecuteReader
                If dr.HasRows Then
                    While dr.Read
                        '1
                        msgid = dr("msg_id").ToString
                        SettoProcessing()
                    End While
                    dr.Close()
                End If
            Else
                Dim count As Integer = 0
                sqlquery = "SELECT COUNT(*) FROM message_table WHERE status = 'failed'"

                myCommand.Connection = GetCon()
                myCommand.CommandText = sqlquery

                Dim dr = myCommand.ExecuteReader

                If dr.HasRows Then
                    While dr.Read
                        count = dr("Count(*)")
                    End While
                    dr.Close()
                End If

                If count > 0 Then

                    If count / portCount() >= 5 Then
                        Lim = 5
                    Else
                        Lim = count / portCount()
                    End If

                    sqlquery = "SELECT msg_id FROM message_table WHERE status = 'failed' Limit " & Lim

                    myCommand.Connection = GetCon()
                    myCommand.CommandText = sqlquery
                    dr = myCommand.ExecuteReader
                    If dr.HasRows Then
                        While dr.Read
                            '1
                            msgid = dr("msg_id").ToString
                            SettoProcessing()
                        End While
                        dr.Close()
                    End If
                Else
                    'lblMessage.Text = "No Data Found"
                    'MessageBox.Show("No data found")
                End If
            End If
        End If
        GetCon.Close()
    End Function

    'Set the status of selected messages to processing-(Sim card in use) -based on ID
    Private Function SettoProcessing()

        Dim sqlquery As String = "UPDATE message_table SET status = 'processing-" & SimName() & "' , sender = '" & SimName() & "', date = NOW() WHERE msg_id = '" & msgid & "'"

        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        myCommand.ExecuteNonQuery()
        GetCon.Close()

    End Function


    'gets message and recipient number of the message based on ID
    Private Function GetMessage_No()

        'Dim myAdapter As New MySqlDataAdapter

        'Tell where to find the file with the emails/passes stored
        Dim sqlquery As String = "SELECT msg_id, recipientNo, message from message_table WHERE status = 'processing-" & SimName() & "'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        If myCommand.ExecuteNonQuery = 0 Then

            sqlquery = "SELECT msg_id, recipientNo, message from message_table WHERE status = 'failed'  "

            myCommand.Connection = GetCon()
            myCommand.CommandText = sqlquery

            If myCommand.ExecuteNonQuery = 0 Then
                sqlquery = "SELECT msg_id, recipientNo, message from message_table WHERE status = 'pending'  "

                myCommand.Connection = GetCon()
                myCommand.CommandText = sqlquery
            End If
        End If

        Dim dr = myCommand.ExecuteReader
        If dr.HasRows Then
            While dr.Read
                msgid = dr("msg_id").ToString
                txtCallNo.Text = dr("recipientNo").ToString
                txtSMS.Text = dr("message").ToString
                'msgid = GetID()

            End While
            dr.Close()
        End If
        GetCon.Close()

    End Function


    'sets the status of failed messages to pending so it could be picked up again
    Private Function UpdateToPending()

        'Tell where to find the file with the emails/passes stored
        Dim sqlquery As String = "UPDATE message_table SET status = 'pending' , sender = '" & SimName() & "', date = NOW() WHERE msg_id = '" & msgid & "'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery
        myCommand.ExecuteNonQuery()


        lblStatus.ForeColor = Color.Red
        lblStatus.Text = "Message Not Sent"
        GetCon.Close()

        'invalidate msgid to avoid being used
        msgid = ""

        btnDisconnect.PerformClick()
        'btnConnect.PerformClick()
    End Function

    'sets the status of message to sent 
    Private Function UpSent()

        'Tell where to find the file with the emails/passes stored
        Dim sqlquery As String = "UPDATE message_table SET status = 'Sent' , sender = '" & SimName() & "', date = NOW() WHERE msg_id = '" & msgid & "'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery
        myCommand.ExecuteNonQuery()

        lblStatus.ForeColor = Color.Green
        lblStatus.Text = "Message Sent"

        btnConnect.Enabled = False
        btnDisconnect.Enabled = True
        GetCon.Close()

        'invalidate msgid to avoid being used
        msgid = ""

    End Function


    'Counts the number of messages that has the status of  processing-(Sim in use)
    Private Function ProcessCount()

        Dim sqlquery As String = "SELECT COUNT(*) FROM message_table WHERE status = 'processing-" & SimName() & "'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader
        Dim count As String = ""
        If dr.HasRows Then
            While dr.Read
                count = dr("Count(*)")
            End While
            dr.Close()
        End If
        Return count
        GetCon.Close()

    End Function


    'counts the number of messages with pending status
    Private Function PendingCount()

        Dim sqlquery As String = "SELECT COUNT(*) FROM message_table WHERE status = 'pending'"
        Dim myCommand As New MySqlCommand
        myCommand.Connection = GetCon()
        myCommand.CommandText = sqlquery

        Dim dr = myCommand.ExecuteReader
        Dim count As String = ""
        If dr.HasRows Then
            While dr.Read
                count = dr("Count(*)")
            End While
            dr.Close()
        End If
        Return count
        GetCon.Close()

    End Function

    'Resets all sim cards to available
    'Used to troubleshoot port problems since the ports are based on the sim card in use
    'Also disconnects the form
    Private Sub Reset_sim_Click(sender As Object, e As EventArgs) Handles Reset_sim.Click
        ResetAllSim()
        lbl_resetSim.Text = "Reset Done. You may try again."
        lbl_portcount.Text = "Ports in use: " & portCount()

        btnDisconnect.PerformClick()

    End Sub

    'resets the port. sets message to pending and restarts the application - with 2 second delay to allow setting to pending
    Private Sub btn_ResetPort_Click(sender As Object, e As EventArgs) Handles btn_ResetPort.Click
        SerialPort1.Close()
        UpdateToPending()

        btn_ResetPort.Enabled = True
        'port_Count = portCount() - 1
        'update_PortCount()
        lbl_portcount.Text = "Ports in use: " & portCount()
        'Close Serial Port

        lblStatus.ForeColor = Color.Red
        lblStatus.Text = "RESTARTING PORT..."
        txtRX.Clear()

        btnConnect.Enabled = True
        btnDisconnect.Enabled = False
        SimReset()
        Delay(2)
        Application.Restart()


    End Sub

    'Click to see the updated number of ports in use
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        lbl_portcount.Text = "Ports in use: " & portCount()

    End Sub


    'Timer for seconds
    Private Function secs_count()
        Do
            btnRefresh.PerformClick()

            'MessageBox.Show(endtimer)
            If endtimer = True Then
                lblSeconds.Text = 0
            Else
                'MessageBox.Show(endtimer)
                If endtimer = True Then
                    Exit Do
                End If
                lblSeconds.Text = 9
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 8
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 7
                Delay(1)

                If endtimer = True Then
                End If
                endtimer = endtimer
                lblSeconds.Text = 6
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 5
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 4
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 3
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 2
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 1
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                endtimer = endtimer
                lblSeconds.Text = 0
                Delay(1)
                Exit Do
            End If

            If endtimer = True Then
                'MessageBox.Show("Endtime catch in secs")
                Exit Do
            End If
        Loop Until endtimer = True
        Return lblSeconds.Text
    End Function

    'Timer for tenth seconds
    Private Function tens_count()
        Do
            If endtimer = True Then
                lblTenSecs.Text = 0
            Else
                'MessageBox.Show(endtimer)
                'MessageBox.Show("tens else executed")
                If endtimer = True Then
                    Exit Do
                End If

                lblTenSecs.Text = 5
                secs_count()
                If endtimer = True Then
                    Exit Do
                End If
                lblTenSecs.Text = 4

                secs_count()
                lblTenSecs.Text = 3
                If endtimer = True Then
                    Exit Do
                End If
                secs_count()
                lblTenSecs.Text = 2
                If endtimer = True Then
                    Exit Do
                End If
                secs_count()
                lblTenSecs.Text = 1

                If endtimer = True Then
                    Exit Do
                End If
                secs_count()
                lblTenSecs.Text = 0
                    secs_count()
                    Exit Do

                End If

                If endtimer = True Then
                'MessageBox.Show("End time Tens")
            End If

        Loop Until endtimer = True
        Return lblTenSecs.Text
    End Function

    'timer for minutes - set to 5 minutes
    Private Function mins_count()
        Do
            'MessageBox.Show(endtimer)
            If endtimer = True Then
                lblMin.Text = 0

            Else
                'MessageBox.Show("Mins else executed")
                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 5
                Delay(1)

                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 4
                tens_count()
                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 3
                tens_count()
                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 2
                tens_count()
                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 1
                tens_count()
                If endtimer = True Then
                    Exit Do
                End If
                lblMin.Text = 0
                tens_count()
                Exit Do
            End If
            If endtimer = True Then
                'MessageBox.Show("Endtime catch in mins")
            End If
        Loop Until endtimer = True
        Return lblMin.Text

    End Function
End Class