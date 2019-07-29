#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install MysticLight
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = 'Select Setup Language'
$sInstName = 'Setup - MysticLight'

$pid = Run($CmdLine[1])

; INSTALL PROCEDURE
WinWait($sPreInstName)
WinActivate($sPreInstName)
Send("{ENTER}")

WinWait("Setup - MysticLight", "Welcome to the MysticLight Setup Wizard")
WinActivate($sInstName)
Send("!n")

WinWaitActive("Setup - MysticLight", "Select Destination Location")
Send("!n")

WinWaitActive($sInstName, 'Select Additional Tasks', 3000) ;3s timeout
If WinExists($sInstName, 'Select Additional Tasks') Then
	Send("!n")
EndIf

WinWaitActive("Setup - MysticLight", "Ready to Install")
Send("!i")


; CONCLUSION OF INSTALL
WinWait($sInstName, 'Completing the MysticLight Setup Wizard')
WinActivate($sInstName) ; Cannot get consistent ControlID handle on checkbox
Send("{DOWN}")
Send("{SPACE}")
Send("{ENTER}")


ProcessWaitClose($pid)
