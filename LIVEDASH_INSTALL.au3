#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
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
ControlClick($sInstName, '&Next >', 'Button1')

; SETUP PROCESS
WinWait($sInstName, 'Choose Destination Location')
WinActivate($sInstName)
ControlClick($sInstName, '&Next >', 'Button1')

; FINISH INSTALLATION
WinWait($sInstName, 'InstallShield Wizard Complete')
WinActivate($sInstName)
ControlCommand($sInstName, 'No, I will restart my computer later.', 'Button2', 'Check')
ControlClick($sInstName, 'Finish', 'Button4')



ProcessWaitClose($pid)
