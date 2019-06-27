#include <Constants.au3>

;
; Author:         Magnus Hjorth
;
; Script Function:
;   UAC Control
;
#RequireAdmin

;If NOT isadmin() Then
 ;       RunAs("Administrator","Mgnus", "", 0, @C:\ & " on", "", @SW_SHOWMINIMIZED)
  ;      Exit
;EndIf

Run("Benchmark\Heaven\heaven.bat")


WinWaitActive("Unigine Heaven Benchmark 4.0 (Basic Edition)")
MouseClick($MOUSE_CLICK_LEFT, 1240, 676)
Sleep(14000)
Send("{F9}")
Sleep(14000)
Send("{F9}")

Sleep(300000)
Send("{ENTER}")
Sleep(1000)
Send("{ENTER}")
Sleep(1000)
Send("!{F4}")

;Heaven benchmark should now be in Users/User