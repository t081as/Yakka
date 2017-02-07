@echo off

echo Yakka - test task
echo.

echo Preparing environment
call "%VS140COMNTOOLS%\vsvars32.bat"
if errorlevel 1 goto error

echo Restoring nuget packages
nuget restore Yakka.sln
if errorlevel 1 goto error

echo Building solution debug
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Debug /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  Yakka.sln
if errorlevel 1 goto error

echo Running unit tests
.\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe .\Yakka\bin\Debug\Yakka.Test\Yakka.test.dll ^
--work=.\Yakka\bin\Debug\Yakka.Test\ ^
--result=Yakka.Test.xml
if errorlevel 1 goto error

echo Running code coverage analysis
.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
  -register:user ^
  "-filter:+[*]* -[Yakka.Test]* -[*]*View -[*]*Control" ^
  -target:".\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe" ^
  -targetargs:".\Yakka\bin\Debug\Yakka.Test\Yakka.test.dll --result=.\Yakka\bin\Debug\Yakka.Test\Yakka.Test.xml" ^
  -output:.\Yakka\bin\Debug\Yakka.Test\Yakka.Coverage.xml
if errorlevel 1 goto error

echo Generating coverage report
packages\ReportGenerator.2.5.2\tools\ReportGenerator.exe ^
  -reports:".\Yakka\bin\Debug\Yakka.Test\Yakka.Coverage.xml" ^
  -targetdir:".\Yakka\bin\Debug\Yakka.Test\Coverage-Report"
if errorlevel 1 goto error

:success
echo.
echo Test successful
exit /b 0

:error
echo.
echo Test failed
exit /b 1