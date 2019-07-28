#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install Coolermaster MasterPlus
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'Setup - MasterPlus'

;$pid = Run($CmdLine[1])

; INSTALL PROCEDURE
WinWait($sInstName, 'Select Destination Location')
WinActivate($sInstName)
Send("!n")

WinWait($sInstName, 'Select Start Menu Folder')
WinActivate($sInstName)
Send("!n")

WinWait($sInstName, 'Select Additional Tasks')
WinActivate($sInstName)
Send("!n")

WinWait($sInstName, 'Ready to Install')
WinActivate($sInstName)
Send("!i")

; FINISH INSTALLATION
WinWait($sInstName, 'Completing the MasterPlus Setup Wizard')
WinActivate($sInstName)
Send("{SPACE}")
Send("!f")

;ProcessWaitClose($pid)
