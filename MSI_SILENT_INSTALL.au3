#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Haohan Liu

 Script Function:
	Install iCUE

#ce ----------------------------------------------------------------------------
#RequireAdmin
#include <MsgBoxConstants.au3>

ShellExecuteWait($CmdLine[1], "/SILENT /NORESTART /SP-")