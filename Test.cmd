@echo off

echo Yakka - test task
echo.

echo Preparing environment
call "%VS140COMNTOOLS%\vsvars32.bat"
if errorlevel 1 goto error

echo Restoring nuget packages
nuget restore Yakka.sln
if errorlevel 1 goto error

echo Building solution (debug)
msbuild.exe /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo ^
  /property:Configuration=Debug /property:Platform="Any CPU" ^
  /verbosity:quiet ^
  Yakka.sln
if errorlevel 1 goto error

echo Running unit tests
.\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe .\Build\Debug\Yakka.Test.dll ^
--work=.\Build\Debug\ ^
--result=Yakka.TestReport.xml
if errorlevel 1 goto error

echo Running code coverage analysis
.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
  -register:user ^
  "-filter:+[*]* -[Yakka.Test]* -[*]*View -[*]*Control -[*]*.Properties.* -[*]*.Program -[*]*Label" ^
  -target:".\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe" ^
  -targetargs:".\Build\Debug\Yakka.Test.dll --result=.\Build\Debug\Yakka.TestReport.xml" ^
  -output:.\Build\Debug\Yakka.Coverage.xml
if errorlevel 1 goto error

echo Generating coverage report
packages\ReportGenerator.2.5.2\tools\ReportGenerator.exe ^
  -reports:".\Build\Debug\Yakka.Coverage.xml" ^
  -targetdir:".\Build\Debug\Coverage"
if errorlevel 1 goto error

:success
echo.
echo Test successful
exit /b 0

:error
echo.
echo Test failed
exit /b 1