#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install RGB Fusion

#ce ----------------------------------------------------------------------------

#RequireAdmin

$pid = Run($CmdLine[1])

ProcessWaitClose($pid)