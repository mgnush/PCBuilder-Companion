#include <Constants.au3>

;
; Author:         Magnus Hjorth
;
; Script Function:
;   UAC Control for Heaven
;
#RequireAdmin

Run("Benchmark\Heaven\heaven.bat")


WinWaitActive("Unigine Heaven Benchmark 4.0 (Basic Edition)")
MouseClick($MOUSE_CLICK_LEFT, 1240, 676)
Sleep(14000)
Send("{F9}")
Sleep(14000)
Send("{F9}")

Sleep(300000)   ; Wait for at least 3 minutes for heaven to finish
; Then spam enter for 3 minutes
;For $m = 1 To 90 Step 1
   Send("{ENTER}")
   Sleep(1000)
   Send("{ENTER}")
   Sleep(1000)
;Next
; Shouldn't take this long to finish, but just in case
Send("!{F4}")

;Heaven benchmark should now be in Users/User