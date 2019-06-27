#include <Constants.au3>

;
; Author:         Magnus Hjorth
;
; Script Function:
;   UAC Control
;
#RequireAdmin
Run("PCCG_Tester_1.2.exe")

Sleep(10000)
WinActivate("PCCG Tester")
WinWaitActive("PCCG Tester", "Prime OK")
Run("AAA Testing\Benchmark\Heaven\heaven.bat")


WinWaitActive("Unigine Heaven Benchmark 4.0 (Basic Edition)")
MouseClick($MOUSE_CLICK_LEFT, 1240, 676)
Sleep(14000)
Send("{F9}")
Sleep(14000)
Send("{F9}")