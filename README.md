![YAKKA](https://gitlab.com/tobiaskoch/Yakka/raw/master/Media/Yakka-256.png)

# YAKKA

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
* Just creates a single configuration file in the isolated storage (later)

## Installation
After installation there are no furthur steps necessary - but you might want to copy the executable or a link to the executable to your autostart folder.

### Option 1: Binary
There's no compiled version available yet.

### Option 2: Source
#### Requirements
The following applictions must be available and included in you *PATH* environment variable:

* [Git](https://git-scm.com/)
* [Nuget.exe](https://www.nuget.org/)
* MSBuild ([Visual Studio](https://www.visualstudio.com) recommended for development)

#### Source code
Get the source code using the following command:
    > git clone https://gitlab.com/tobiaskoch/Yakka.git

#### Build
    > .\Build.cmd

The executable will be located in the directory *.\Yakka\bin\Release\Yakka* if the build succeeds.

#### Test
    > .\Test.cmd

The script will report if the unit tests succeeds, the coverage report will be placed in the directory *.\Yakka\bin\Debug\Yakka.Test\Coverage-Report*.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/Yakka/blob/master/CONTRIBUTING.md)

## Contributors
see [AUHORS.txt](https://gitlab.com/tobiaskoch/Yakka/blob/master/AUTHORS.txt)

## License
**Yakka** © 2017  Tobias Koch. Released under the [GPL License](https://gitlab.com/tobiaskoch/Yakka/blob/master/LICENSE.md)