#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Position QC windows

#ce ----------------------------------------------------------------------------
#RequireAdmin

; Launch system info - moving it twice is more stable?!
Send("{LWINDOWN}{PAUSE}{LWINUP}")
WinWait("System")
WinMove("System", "", 960, 0, 960, 540)
WinMove("System", "", 960, 0, 960, 540)

; Explorer
Run("explorer.exe")
$expl = WinWait("[CLASS:CabinetWClass]", "Ribbon")
WinMove($expl, "", 0, 0, 960, 540)

; Move update window (give up after 6 seconds)
$wup = WinActivate("Settings", "CN=Microsoft")
If $wup Then
   WinMove($wup, "", 960, 0, 960, 540)
Else
   $wup = WinWait("Settings", "CN=Microsoft", 6)
   WinMove($wup, "", 960, 0, 960, 540)
EndIf

WinActivate("Builder Companion")
WinMove("Builder Companion", "", 491, 299)