#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install iCUE

#ce ----------------------------------------------------------------------------
#include <MsgBoxConstants.au3>

$pid = ShellExecute($CmdLine[1])

WinWaitActive("SELECT SETUP LANGUAGE")
Send("{ENTER}")

WinWaitActive("INSTALL CORSAIR iCUE Software")
Send("{RIGHT}")
Send("{RIGHT}")
Send("{SPACE}")

Sleep(200)

WinWaitActive("INSTALL CORSAIR iCUE Software")
Send("{TAB}")
Send("{TAB}")
Send("{SPACE}")
Send("{TAB}")
Send("{TAB}")
Send("{TAB}")
Send("{TAB}")
Send("{SPACE}")


Sleep(200)

WinWaitActive("INSTALL CORSAIR iCUE Software")
Send("{SPACE}")

Sleep(25000)

ProcessWaitClose($pid)