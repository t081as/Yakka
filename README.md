<p align="center">
  <img src="https://gitlab.com/tobiaskoch/Yakka/raw/master/assets/Yakka-256.png">
</p>

# YAKKA

[**yak**-*uh*]

**noun**, *Australian*
1. hard work

---
Yakka is a .NET based system tray application for the windows operating system displaying your working hours.

## Features
* No installation necessary
* Single executable file
* No framework required to run
* Just creates a single configuration file in the user data directory

## Installation
After "installation" (i.e. xcopy deployment) there are no further steps necessary - but you might want to copy the executable or a link to the executable to your autostart folder.

### Option 0: Binary
Stable versions can be downloaded here: [https://gitlab.com/tobiaskoch/Yakka/-/releases](https://gitlab.com/tobiaskoch/Yakka/-/releases).

### Option 1: Source
#### Requirements
The following tools must be available to build the software:

* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download)

The following tools are recommended for development:

* [Visual Studio 2019](https://visualstudio.microsoft.com/vs/)
* [Visual Studio Code](https://code.visualstudio.com/)

#### Source code
Get the source code using the following command:

    > git clone https://gitlab.com/tobiaskoch/Yakka.git

#### Test
    > ./build

The script will report if the unit tests succeeds, the coverage report will be located in the directory *./output/coverage*.

#### Build
The build script can be executed using the following command:

    > ./build --configuration Release

The build will produce several zip archives in the root directory or the repository if it succeeds.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/Yakka/blob/master/CONTRIBUTING.md)

## Contributors
see [AUTHORS.txt](https://gitlab.com/tobiaskoch/Yakka/blob/master/AUTHORS.txt)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**Yakka** © 2017-2020  [Tobias Koch](https://www.tk-software.de). Released under the [GPL](https://gitlab.com/tobiaskoch/Yakka/blob/master/LICENSE.md).