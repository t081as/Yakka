![YAKKA](https://gitlab.com/tobiaskoch/Yakka/raw/master/Media/Yakka-256.png)

# YAKKA

[![pipeline status](https://gitlab.com/tobiaskoch/Yakka/badges/master/pipeline.svg)](https://gitlab.com/tobiaskoch/Yakka/commits/master)
[![maintained: yes](https://tobiaskoch.gitlab.io/badges/maintained-yes.svg)](https://gitlab.com/tobiaskoch/Yakka/commits/master)
[![donate: paypal](https://tobiaskoch.gitlab.io/badges/donate-paypal.svg)](https://www.tk-software.de/donate)

[**yak**-*uh*]

**noun**, *Australian*
1. hard work

---
Yakka is small .NET based system tray application for the windows operating system displaying your working hours.

## Features
* Written in C#
* Using .NET Framework 4.5 (or later) preinstalled on (almost) every windows computer
* No installation necessary (xcopy deployment)
* Single executable file
* Just creates a single configuration file in the application data directory

## Installation
After "installation" (i.e. xcopy deployment) there are no furthur steps necessary - but you might want to copy the executable or a link to the executable to your autostart folder.

### Option 1: Binary
Stable versions can be downloaded [here](https://gitlab.com/tobiaskoch/Yakka/pipelines?scope=tags).

### Option 2: Source
#### Requirements
The following applications must be available and included in you *PATH* environment variable:

* [Git](https://git-scm.com/)
* [Nuget.exe](https://www.nuget.org/)
* MSBuild (.NET Framework / Mono; [Visual Studio](https://www.visualstudio.com) recommended for development)

#### Source code
Get the source code using the following command:

    > git clone https://gitlab.com/tobiaskoch/Yakka.git

#### Build
    > .\Build.cmd

The executable will be located in the directory *.\Build\Release* if the build succeeds.

#### Test
    > .\Test.cmd

The script will report if the unit tests succeeds, the coverage report will be placed in the directory *.\Build\Debug\Coverage*.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/Yakka/blob/master/CONTRIBUTING.md)

## Contributors
see [AUTHORS.txt](https://gitlab.com/tobiaskoch/Yakka/blob/master/AUTHORS.txt)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**Yakka** © 2017-2018  [Tobias Koch](https://www.tk-software.de). Released under the [GPL](https://gitlab.com/tobiaskoch/Yakka/blob/master/LICENSE.md).