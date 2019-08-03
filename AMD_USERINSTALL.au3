#cs ----------------------------------------------------------------------------
 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth & Haohan Liu
 Date: 2019/07/30
 Script Function:
	Install MysticLight
#ce ----------------------------------------------------------------------------

#include <Array.au3>
#include <File.au3>
#include <MsgBoxConstants.au3>

;#RequireAdmin

$pid = Run($CmdLine[1])

#include <ButtonConstants.au3>
#include <GUIConstantsEx.au3>
#include <StaticConstants.au3>
#include <WindowsConstants.au3>
#Region ### START Koda GUI section ### Form=C:\Users\haoha\Google Drive\WORK\_Work-Scripts\Magware\InstallWindow.kxf
$InstallWindow = GUICreate("Manual User Install", 411, 98, 192, 124, -1, BitOR($WS_EX_TOPMOST,$WS_EX_WINDOWEDGE))
$Label1 = GUICtrlCreateLabel("This installation cannot be automatically installed. Please complete the installation", 8, 8, 386, 17)
$Label2 = GUICtrlCreateLabel("and click to 'Finished Installation' to exit and continue to the next step.", 8, 24, 333, 17)
$Finish = GUICtrlCreateButton("Finished Installation", 8, 48, 395, 25)
GUISetState(@SW_SHOW)
WinSetOnTop("Manual User Install", "", 1)
#EndRegion ### END Koda GUI section ###

While 1
	$nMsg = GUIGetMsg()
	Switch $nMsg
		Case $GUI_EVENT_CLOSE
			Exit
		Case $Finish
			Exit

	EndSwitch
WEnd

ProcessWaitClose($pid)
