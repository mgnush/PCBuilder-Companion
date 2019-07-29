#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Run Kraken Reprogrammer and update Kraken X52, X62, X72
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'Setup - CONNECT'

;$pid = Run($CmdLine[1])

; INSTALLATION PROCEDURE
WinWait($sInstName, 'Welcome to the CONNECT Setup Wizard')
WinActivate($sInstName)
Send('!n')

WinWaitActive($sInstName, 'Select Destination Location')
Send('!n')

WinWaitActive($sInstName, 'Select Start Menu Folder')
Send('!n')

WinWaitActive($sInstName, 'Ready to Install')
Send('!i')

; FINISH INSTALLATION
WinWait($sInstName, 'Completing the CONNECT Setup Wizard')
WinActivate($sInstName)
Send('{SPACE}')
Send('!f')

;ProcessWaitClose($pid)
