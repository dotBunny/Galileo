@echo off

set BUILD_CONFIG=Release

:parse
IF "%~1"=="" GOTO endparse
IF "%~1"=="--debug" set BUILD_CONFIG=Debug
SHIFT
GOTO parse
:endparse

set SOLUTION=%cd%\..\Galileo.Client.Win.sln
set BUILD_FOLDER=%cd%\..\projects\Galileo.Client.Win\bin\%BUILD_CONFIG%
set PACKAGE=%cd%\..\installers\Galileo.MSI.Installer\Galileo.MSI.Installer.wixproj
set PKG=%cd%\..\installers\Galileo.MSI.Installer\bin\%BUILD_CONFIG%\Galileo.msi
set /p VERSION=<%cd%\version.txt
set SHORTVER=%VERSION:~2%
set TEMP_FOLDER=%cd%\temp
set TEMP=%TEMP_FOLDER%\Galileo-%VERSION%.msi 
set MSBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"

if not exist "%TEMP_FOLDER%\" mkdir "%TEMP_FOLDER%\"
rmdir /S /Q %cd%\..\projects\Galileo.Client.Win\bin\%BUILD_CONFIG%
rmdir /S /Q %cd%\..\projects\Galileo.Client.Win\obj\%BUILD_CONFIG%

%MSBUILD% %SOLUTION% /p:Configuration=%BUILD_CONFIG% /t:Clean

IF ERRORLEVEL 0 GOTO cleaned
echo [FAIL] Cleaning Solution
exit 1
:cleaned
echo [PASS] Cleaning Solution

dotnet restore %SOLUTION%
IF ERRORLEVEL 0 GOTO restored
echo [FAIL] NuGet Package Restore
exit 1
:restored
echo [PASS] NuGet Package Restore

%MSBUILD% %SOLUTION% /p:Configuration=%BUILD_CONFIG%;BUILD_DEFINE=__PACKAGE__ /t:Rebuild
IF ERRORLEVEL 0 GOTO built
echo [FAIL] Soluton Build
exit 1
:built
echo [PASS] Soluton Build


%MSBUILD% /p:Configuration=%BUILD_CONFIG%;PackageVersion=%SHORTVER%;BuildProjectReferences=false;BUILD_DEFINE=__PACKAGE__ /t:Rebuild %PACKAGE%
IF ERRORLEVEL 0 GOTO package
echo [FAIL] Packaging
exit 1
:package
echo [PASS] Packaging

move /Y %PKG% %TEMP%
IF ERRORLEVEL 0 GOTO moved
echo [FAIL] Move Installer To Temp
exit 1
:moved
echo [PASS] Move Installer To Temp

echo [Windows] %VERSION% (%BUILD_CONFIG%) Installer Created (%SHORTVER%)