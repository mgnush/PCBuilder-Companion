#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/30
 Script Function:
	Install Thermaltake TT RGB Plus
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sPreInstName = ''
$sInstName = 'TT RGB Plus'

$pid = Run($CmdLine[1])

; INSTALLATION PROCEDURE
WinWait($sInstName, 'Welcome to TT RGB Plus')
WinActivate($sInstName)
Send('!n')

WinWaitActive($sInstName, 'Choose Install Location')
Send('!i')

; INSTALL C++ REDISTRIBUTABLES
$sMSVC = 'Microsoft Visual C++ 2015 Redistributable (x64)'
WinWait($sMSVC, '', 3000)
If WinExists($sMSVC) Then
	Sleep(500) ;500ms ensure window is fully loaded
	Send('!a')
	Send('!i')
	While WinExists($sMSVC)
		Sleep(1000) ;1s
		WinActivate($sMSVC)
		Send('!c')
	WEnd
EndIf

; FINISH INSTALLATION
WinWait($sInstName, 'Completing TT RGB Plus')
WinActivate($sInstName)
Send('{TAB}')
Send('{TAB}')
Send('{SPACE}')
Send('!f')

ProcessWaitClose($pid)
