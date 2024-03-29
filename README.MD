# Builder Companion

This program assists builders with testing and system preparation procedures.

## Using the software

To launch the software on a new system, simply run the "Builder Companion.exe" executable. You will need to allow admin rights when prompted.

### File hierarchy & naming requirements

The main program is compiled into a single executable, which must be named "Builder Companion.exe" for all stand-alone scripts to run correctly. All dll-dependencies are embedded in the executable. This executable may be placed anywhere on the local machine. If run from the desktop, it is recommended to place the executable away from the upper left corner. This will allow RGB-software to be organised neatly during the [QC-phase](https://github.com/mgnush/Builder-Companion#qc-phase-4). 

The program looks for all external benchmark tools in

```
Desktop/AAA Testing/Benchmark
```

Stand-alone scripts need to be located in

```
Desktop/AAA Testing/Companion Files
```

The program comes with the ability to download and install user-selected software from a shared network location (see [Program Flow](https://github.com/mgnush/Builder-Companion#program-flow)). The options displayed to the user are specified in

```
Desktop/AAA Testing/Companion Files/rgbs.xml
```

The scripts to install user-selected software need to be located in

```
Desktop/AAA Testing/Companion Files/RGB Scripts
```

For instructions on how to modify the software options and add installation scripts, see [Adding sharepoint software to the program](https://github.com/mgnush/Builder-Companion#Adding-sharepoint-software-to-the-program).

## Program flow

Below is a simplified summary of the flow of execution when using the software to test and prepare a new system.

### Testing (Phase 1)

When launched on a new system, the program will wait for the user to select the software to download and install from the server, and press start. Before pressing start, the user should select the duration of the stress testing using the slider. The default duration is the longest setting. 

When the start button is pressed, the program will attempt to download and install the selected software from the server.

When software downloads and installs have been completed by the program, it will automatically launch the stress testing tools, which are currently

* [Furmark](https://geeks3d.com/furmark/downloads/)
* [Prime95](https://www.mersenne.org/download/)

The system monitoring tools are currently

* [HWMonitor](https://cpuid.com/softwares/hwmonitor.html)
* [CoreTemp](https://www.alcpu.com/CoreTemp/)

If the stress testing tools return no errors, and the system isn't overheating, the program will automatically launch benchmarking software, which is currently

* [Heaven](https://benchmark.unigine.com/heaven)

It's important to note that the heaven settings should be configured prior to running this program.

During phase 1, Windows updates will be downloaded and installed in the background.

### Windows update (Phase 2)

Phase 1 finishes when benchmarks have run with no errors and the initial Windows update session is completed. 

During phase 2, the system will reboot until Windows is up to date. Due to the "nature" of Windows updates, this phase may require user interaction, i.e. manual restart, if Windows updater gets stuck.

### System prep (Phase 3)

When Windows is up to date, the program will tell the user to test the audio. It is at this stage that the user is encouraged to configure any software such as RGB software installed at the start. When the user indicates that the audio works, the program will bring up Windows activation.

When the user has activated Windows, the "Restart (QC)" button may be pressed to enter the QC phase.

### QC (Phase 4)

When the system has restarted, it will format drives, delete desktop files and bring up the manual QC information panes

* System Control Panel
* Device Manager
* Disk Management
* Windows Explorer
* Windows Update
* This program

In this phase, the program will display the results obtained (temps, scores) in the testing phase, as well as any relevant system information obtained.

It is important that the user (builder or QC'er) manually close this program during or after the QC procedure, as this will properly clean up any resources left on the machine, including the program itself.

### Errors & flow interruptions

Below is a list of errors or other intentional interruptions the user may encounter

* **Device Manager corn chip** The Device manager status is checked every time the program is launched. If a corn chip is found, the program will launch Device Manager for the user to check.
* **File not found** All files that are being referenced in the program will bring up a prompt if they are not found. Depending on the file, this will halt the program flow. If a software installation script is not found (for rgb software), execution will continue as normal.
* **Overheating** If the CPU or GPU is overheating, a warning message in red will appear. Testing will continue - however, at the end of stress-testing, the user will have to manually press the 'ignore' button to advance to benchmarking.
* **Couldn't connect to sharepoint** This error occurs when the program cannot establish a connection to the NAS.
* **Prime failed** If Prime fails, the program will detect it within 1 minute of the failure. When this is detected, red text will appear and all stress testing programs will exit. The program cannot advance from this event, as it's expected the user start over with new/changed hardware.

## Adding sharepoint software to the program

To add sharepoint software (i.e. stored on NAS), the xml file listed in [File hierarchy](https://github.com/mgnush/Builder-Companion-Readme#file-hierarchy--naming-requirements) needs to be edited. 

Add an entry by adding a record node to the file, such as the example below

```
<record>
	<GUI>SoftwareFolderName</GUI>
	<FILENAME>SoftwareInstaller*.exe</FILENAME>
	<SCRIPTNAME>SOFTWARE_INSTALL.exe</SCRIPTNAME>
</record>
```

Where the *GUI* node specifies the name of the superfolder in which the software to be installed lies in. For Aura, this could be "Aura" (without the quotes). 

The *FILENAME* node specifies the installer file of the software. This should ONLY be the filename, not a path. For example, "Setup.exe" (without quotes). If the superfolder contains multiple subfolders or multiple install files with similar or identical names, the newest version will be launched. The * character denotes wildcard characters if the executable name varies between versions.

The *SCRIPTNAME* node specifies the scriptname of the installation script. If this is left blank, the software will be downloaded to the local machine, but not installed.

### Creating new installation scripts 

To add installation scripts for software found on the NAS, it is recommended to use AutoIt3. [This tutorial](https://www.autoitscript.com/autoit3/docs/tutorials/winzip/winzip.htm) explains well how most software install wizards can be automated with just a few lines of code. 

Create a single script for each program to be automated, and add its name to the software xml file in the procedure described above. 

## Built With

* [Fody Costura](https://github.com/Fody/Costura) - For embedding dll's
* [Html Agility Pack](https://html-agility-pack.net/) - For reading HTML files
* [AutoIt3](https://www.autoitscript.com/site/autoit/) - For writing and compiling stand-alone scripts to go with this software

## Contributing

Please contact Magnus Hjorth on mhjorthen@gmail.com to request becoming a collaborator.

## Licensing

This code is distributed under an MIT license. Please see LICENSE.MD for more details.

## Authors

* **Magnus Hjorth** - [mgnush](https://github.com/mgnush)

## Acknowledgments

* [dahall](https://github.com/dahall/taskscheduler) - Used the Task Scheduler Wrapper to create Windows tasks
* Inspiration & help from the Systems team including Robin, Nathan, Geoffrey, Haohan & Travis
