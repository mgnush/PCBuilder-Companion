#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install Mystic Light

#ce ----------------------------------------------------------------------------

#RequireAdmin


Run("MysticLight.exe")

WinWaitActive("Select Setup Language")
Send("{ENTER}")

WinWaitActive("Setup - MysticLight", "Welcome to the MysticLight Setup Wizard")
Send("!n")

WinWaitActive("Setup - MysticLight", "Select Destination Location")
Send("!n")

WinWaitActive("Setup - MysticLight", "Ready to Install")
Send("!i")

WinWaitActive("Setup - MysticLight", "Completing the MysticLight Setup Wizard")
Send("{DOWN}")
Send("{SPACE}")
Send("{ENTER}")