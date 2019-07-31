#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install Polychrome
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'Setup - ASRock RGB LED'

$pid = Run($CmdLine[1])

; WELCOME SCREEN
WinWait($sInstName, 'Select Destination Location')
ControlClick($sInstName, '&Next >', 'TNewButton2')

; SETUP PROCESS
WinWait($sInstName, 'Select Start Menu Folder')
ControlClick($sInstName, '&Next >', 'TNewButton4')
WinWait($sInstName, 'Ready to Install')
ControlClick($sInstName, '&Install', 'TNewButton4')

; FINISH INSTALLATION
WinWait($sInstName, 'Setup has finished installing ASRock RGB LED')
ControlClick($sInstName, '&Finish', 'TNewButton4')



ProcessWaitClose($pid)
