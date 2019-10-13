#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Haohan Liu

 Script Function:
	Literally nothing

#ce ----------------------------------------------------------------------------

#RequireAdmin

$pid = Run($CmdLine[1])


ProcessWaitClose($pid)
