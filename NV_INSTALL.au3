#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/28
 Script Function:
	Install NVIDIA Graphics drivers
#ce ----------------------------------------------------------------------------

; NOTE: IF NEW VERSIONS BREAK THE CLASSID METHOD OF COMMANDCLICK/COMMANDCONTROL
; CHANGE TO CLASSNAMENN VARIANT

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

#RequireAdmin

$sInstName = 'NVIDIA Installer'
$iTimeout = 120000 ; 12s

$pid = Run($CmdLine[1])

; EULA PAGE WAIT
$bTimeoutState = WinWait($sInstName, 'GeForce Experience requires data', $iTimeout)
if $bTimeoutState == 0 Then
	Exit
EndIf

; CHECK GRAPHICS DRIVER ONLY, AND CONTINUE
ControlCommand($sInstName, 'NVIDIA Graphics Driver', 1016, 'Check')
ControlClick($sInstName, '&AGREE AND CONTINUE', 1027)

; CHECK CLEAN INSTALLATION
; Not needed when the -clean switch is passed
; WinWait($sInstName, 'Installation options')
; ControlCommand($sInstName, 'C&ustom (Advanced)', 1017, 'Check')
; ControlClick($sInstName, '&NEXT', 1022)
; WinWait($sInstName, 'Custom installation options')
; ControlCommand($sInstName, '&Perform a clean installation', 1017, 'Check')
; ControlClick($sInstName, '&NEXT', 1022)

; BEGIN INSTALLATION
; For use when no clean install is needed
WinWait($sInstName, 'Installation options')
ControlClick($sInstName, '&NEXT', 1022)

; WAIT FOR INSTALLATION COMPLETION
WinWait($sInstName, 'NVIDIA Installer has finished')
ControlClick($sInstName, '&CLOSE', 1019)


ProcessWaitClose($pid)
