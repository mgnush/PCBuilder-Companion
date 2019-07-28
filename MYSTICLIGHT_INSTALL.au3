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

; SELECT LANGUAGE SCREEN
WinWait($sPreInstName, '')
ControlClick($sPreInstName, 'OK', 'TNewButton1')

; SETUP SELECTION
WinWait($sInstName, 'Welcome to the MysticLight Setup Wizard')
ControlClick($sInstName, '&Next >', 'TNewButton1')

WinWait($sInstName, 'Select Destination Location')
ControlClick($sInstName, '&Next >', 'TNewButton3')

WinWait($sInstName, 'Select Additional Tasks', 500) ;500ms timeout
If WinExists($sInstName, 'Select Additional Tasks') Then
	ControlClick($sInstName, '&Next >', 'TNewButton3')
EndIf

WinWait($sInstName, 'Ready to Install')
ControlClick($sInstName, '&Install', 'TNewButton3')

; INSTALL OCCURS HERE


; CONCLUSION OF INSTALL
WinWait($sInstName, 'Completing the MysticLight Setup Wizard')
WinActivate($sInstName) ; Cannot get consistent ControlID handle on checkbox
Send("{DOWN}")
Send("{SPACE}")
Send("{ENTER}")



ProcessWaitClose($pid)
