#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install CAM

#ce ----------------------------------------------------------------------------
#include <MsgBoxConstants.au3>

#RequireAdmin
$search = FileFindFirstFile("CAM_Installer *.exe")
If $search = -1 Then
        MsgBox($MB_SYSTEMMODAL, "", "Error: No files/directories matched the search pattern.")
		Exit
EndIf

$filename = FileFindNextFile($search)

FileClose($search)

Run($filename)

WinWaitActive("CAM Setup", "Please select a language:")
Send("{ENTER}")

WinWaitActive("CAM Setup", "Welcome to the CAM Setup Wizard")
Send("!n")

WinWaitActive("CAM Setup", "END USER LICENSE AGREEMENT")
Send("!a")
Send("!n")

WinWaitActive("CAM Setup", "Select NZXT component drivers.")
Send("!n")

WinWaitActive("CAM Setup", "Select Installation Folder")
Send("!n")

WinWaitActive("CAM Setup", "Click ")
Send("!i")

WinWaitActive("CAM Setup", "Completing the CAM Setup Wizard")
Send("{ENTER}")

Sleep(2000)
WinActivate("Windows Security Alert")
Send("!a")