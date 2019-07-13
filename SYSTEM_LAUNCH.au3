#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Position all QC windows

#ce ----------------------------------------------------------------------------


Send("{LWINDOWN}{PAUSE}{LWINUP}")
WinWait("System")
WinMove("System", "", 960, 0, 960, 540)
WinMove("System", "", 960, 0, 960, 540)
