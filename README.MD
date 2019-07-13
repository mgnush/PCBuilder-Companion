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

The program comes with the ability to download and install user-selected software from a shared network location (see [Program FLow](https://github.com/mgnush/Builder-Companion#program-flow). The options displayed to the user are specified in

```
Desktop/AAA Testing/Companion Files/rgbs.xml
```

The scripts to install user-selected software need to be located in

```
Desktop/AAA Testing/Companion Files/RGB Scripts
```

For instructions on how to modify the software options and add installation scripts, see 'Software installation from sharepoint'.

## Program flow

Below is a simplified summary of the flow of execution when using the software to test and prepare a new system.

### Testing (Phase 1)

When launched on a new system, the program will wait for the user to select the software to download and install from the server, and press start.

When the start button is pressed, the program will attempt to download and install the selected software from the server. If the software is found, but an install script is not, the user will be prompted to manually install that piece of software. 



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
