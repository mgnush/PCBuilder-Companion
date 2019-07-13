#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Reposition test windows

#ce ----------------------------------------------------------------------------

#RequireAdmin

$prime = WinWait("Prime95")
WinMove($prime, "", 0, 0)

$furmark = WinWait("[CLASS:FurMark3DWindow]")
WinMove($furmark, "", 640, 360)

$hwm = WinWait("CPUID HWMonitor")
WinMove($hwm, "", 0, 0, 530, 1080)

$program = WinWait("PCCG Companion")
WinMove("PCCG Companion", "", 491, 299)
WinActivate("PCCG Companion")