#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Enable CoreTemp logging

#ce ----------------------------------------------------------------------------

WinActivate("Core Temp 1.14")
Sleep(300)
Send("{F4}")

WinWaitActive("Prime95")

WinActivate("PCCG Tester")