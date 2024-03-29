#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Enable CoreTemp logging

#ce ----------------------------------------------------------------------------

WinActivate("Core Temp 1.14")
Send("{F4}")

WinWait("Prime95")

$fhndl = WinActivate("[CLASS:FurMark3DWindow]", "")
If ($fhndl) Then
   WinMove($fhndl, "", 0,0)
Endif

WinActivate("Builder Companion")
WinMove("Builder Companion", "", 320, 180)