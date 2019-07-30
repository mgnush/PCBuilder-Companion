#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/30
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
WinWaitActive($sPreInstName)
Send("{ENTER}")

WinWaitActive($sInstName, "Welcome to the MysticLight Setup Wizard")
Send("!n")

WinWaitActive($sInstName, "Select Destination Location")
Send("!n")

WinWaitActive($sInstName, 'Select Additional Tasks', 1000) ;3s timeout
If WinExists($sInstName, 'Select Additional Tasks') Then
	Send("!n")
EndIf

WinWaitActive($sInstName, "Ready to Install")
Send("!i")


; CONCLUSION OF INSTALL
WinWaitActive($sInstName, 'Completing the MysticLight Setup Wizard')
Send("{DOWN}")
Send("{SPACE}")
Send("{ENTER}")


ProcessWaitClose($pid)
