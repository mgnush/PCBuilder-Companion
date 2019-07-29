#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/30
 Script Function:
	Run Kraken Reprogrammer and update Kraken X52, X62, X72
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = '[CLASS:ConsoleWindowClass]'

$pid = Run($CmdLine[1])

; INITIALISE KRAKEN REPROGRAMMER
WinWait($sInstName)
WinActivate($sInstName)
Send('{ENTER}')

; WAIT FOR REPROGRAMMER TO FINISH
Sleep(10000) ;10s

While WinExists($sInstName)
	WinActivate($sInstName)
	Send('{ENTER}')
	Sleep(1000) ;1s
WEnd

ProcessWaitClose($pid)
