#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/30
 Script Function:
	Install LiveDash for ASUS products
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'InstallShield Wizard'

$pid = Run($CmdLine[1])

; WELCOME SCREEN
WinWait($sInstName, 'Welcome to the InstallShield Wizard for LiveDash')
WinActivate($sInstName)
Send('!n')

; SETUP PROCESS
WinWait($sInstName, 'Choose Destination Location')
WinActivate($sInstName)
Send('!n')

; FINISH INSTALLATION
WinWait($sInstName, 'InstallShield Wizard Complete')
WinActivate($sInstName)
Send('{TAB}')
Send('{TAB}')
Send('{UP}')
Send('{ENTER}')


ProcessWaitClose($pid)
