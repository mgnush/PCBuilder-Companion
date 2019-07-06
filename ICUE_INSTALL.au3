#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install iCUE

#ce ----------------------------------------------------------------------------
#include <MsgBoxConstants.au3>

#RequireAdmin
$search = FileFindFirstFile("iCUESetup_*.msi")
If $search = -1 Then
        MsgBox($MB_SYSTEMMODAL, "", "Error: No files/directories matched the search pattern.")
		Exit
EndIf

$filename = FileFindNextFile($search)

FileClose($search)

ShellExecute($filename)

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

Sleep(15000)
