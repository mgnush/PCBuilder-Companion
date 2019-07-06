#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         Magnus Hjorth

 Script Function:
	Install Aura

#ce ----------------------------------------------------------------------------

#RequireAdmin
Run("Setup.exe")
WinWaitActive("InstallShield Wizard", "The InstallShield® Wizard will install AURA on your computer.")
Send("!n")

WinWaitActive("InstallShield Wizard", "Setup will install AURA in the following folder.")
Send("!n")

WinWaitActive("InstallShield Wizard", "The InstallShield Wizard has successfully installed")
Send("{ENTER}")

WinWaitActive("Restarting Windows", "Setup has finished copying files to your computer.")
Send("{DOWN}")
Send("{ENTER}")


