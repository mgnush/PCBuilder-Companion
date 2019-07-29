#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install Dragon Center, old version
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = 'Select Setup Language'
$sInstName = 'Setup - Dragon Center'

$pid = Run($CmdLine[1])

; SELECT LANGUAGE SCREEN
WinWait($sPreInstName, '')
WinActivate($sPreInstName)
Send('{ENTER}')

; WELCOME SCREEN
WinWait($sInstName, 'Welcome to the Dragon Center Setup Wizard')
WinActivate($sInstName)
Send('!a')
Send('!n')

; PASS EULA
WinWaitActive($sInstName, 'License Agreement')
Send('!a')
Sleep(500) ; required to ensure 'Next' button is not greyed out
Send('!n')

; SETUP PROCESS
WinWaitActive($sInstName, 'Select Destination Location')
Send('!n')

WinWaitActive($sInstName, 'Select Additional Tasks', 3000) ;3s timeout
Send('!n')

WinWaitActive($sInstName, 'Ready to Install')
Send('!i')


; INSTALL OCCURS HERE


; CONCLUSION OF INSTALL
WinWait($sInstName, 'Completing the Dragon Center Setup Wizard')
WinActivate($sInstName) ; Cannot get consistent ControlID handle on checkbox
Send("{SPACE}")
Send("!f")


ProcessWaitClose($pid)
