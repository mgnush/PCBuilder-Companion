#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install AMD Wraith lighting drivers
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'Wraith Prism Setup'

$pid = Run($CmdLine[1])

; INSTALL PROCEDURE
WinWait($sInstName, '')
ControlClick($sInstName, '', 'Button7')
Sleep(200) ;Sleep required as there is no visible text hooks to AU3
ControlClick($sInstName, '', 'Button7')

; CHECK FOR INSTALLATION COMPLETION
; Doesn't work, Button23 is always clickable even when not visible.
;While WinExists($sInstName)
;	Sleep(250)
;	ControlClick($sInstName, '', 'Button23')
;WEnd

; As the installer is actual trash, this is the only way to finish installation.
Sleep(5000) ;5s
ControlClick($sInstName, '', 'Button23')

ProcessWaitClose($pid)