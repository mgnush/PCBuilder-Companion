# Builder Companion

This program assists builders with testing and system preparation procedures.

## Using the software

To launch the software on a new system, simply run the "Builder Companion.exe" executable. You will need to allow admin rights when prompted.

### File hierarchy & naming requirements

The main program is compiled into a single executable, which must be named "Builder Companion.exe" for all stand-alone scripts to run correctly. All dll-dependencies are embedded in the executable. This executable may be placed anywhere on the local machine. 

The program looks for all external benchmark tools in

```
Desktop/AAA Testing/Benchmark
```

Stand-alone scripts need to be located in

```
Desktop/AAA Testing/Companion Files
```

The program comes with the ability to download and install user-selected software from a shared network location (see [Program Flow](https://github.com/mgnush/Builder-Companion#program-flow). The options displayed to the user are specified in

```
Desktop/AAA Testing/Companion Files/rgbs.xml
```

The scripts to install user-selected software need to be located in

```
Desktop/AAA Testing/Companion Files/RGB Scripts
```

For instructions on how to modify the software options and add installation scripts, see [Adding sharepoint software to the program]((https://github.com/mgnush/Builder-Companion#Adding-sharepoint-software-to-the-program).

## Program flow

Below is a simplified summary of the flow of execution when using the software to test and prepare a new system.

### Testing (Phase 1)

When launched on a new system, the program will wait for the user to select the software to download and install from the server, and press start. Before pressing start, the user should select the duration of the stress testing using the slider. The default duration is the longest setting. 

When the start button is pressed, the program will attempt to download and install the selected software from the server.

When software downloads and installs have been completed by the program, it will automatically launch the stress testing tools, which are currently

* [Furmark](https://geeks3d.com/furmark/downloads/)
* [Prime95](https://www.mersenne.org/download/)

The system monitoring tools are currently:

* [HWMonitor](https://cpuid.com/softwares/hwmonitor.html)
* [CoreTemp](https://www.alcpu.com/CoreTemp/)

If the stress testing tools return no errors, and the system isn't overheating, the program will automatically launch benchmarking software, which is currently

* Heaven

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

In this phase, the program will display the results obtained (temps, scores) in the testing phase.

It is important that the user (builder or QC'er) manually close this program during or after the QC procedure, as this will properly clean up any resources left on the machine, including the program itself.

### Errors

If the software is found, but an install script is not, the user will be prompted to manually install that piece of software.

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Magnus Hjorth** - [mgnush](https://github.com/mgnush)

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
