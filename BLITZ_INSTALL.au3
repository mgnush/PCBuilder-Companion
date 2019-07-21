#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install Blitz

#ce ----------------------------------------------------------------------------

#RequireAdmin

$pid = Run($CmdLine[1])

WinWaitActive("InstallShield Wizard", "The InstallShield® Wizard will install Blitz")
Send("!n")

WinWaitActive("InstallShield Wizard", "Setup will install Blitz in the following folder.")
Send("!n")

WinWaitActive("Restarting Windows", "Setup has finished copying files to your computer.")
Send("{DOWN}")
Send("{ENTER}")

ProcessWaitClose($pid)