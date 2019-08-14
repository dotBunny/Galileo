#!/bin/bash

# Features
OBSFUCATE=1
BUILD_CONFIG=Release

# Handle Parameters
while [ "$1" != "" ]; do
    case $1 in
        --debug )                               BUILD_CONFIG=Debug
                                                echo "> Debug Build"
                                                ;;
    esac
    shift
done


# Get our local path
LOCAL=$(pwd)
NUGET="mono /usr/local/bin/nuget.exe"
SOLUTION="$LOCAL/../Galileo.Client.Mac.sln"
PLIST="$LOCAL/../projects/Galileo.Client.Mac/Info.plist"
PKG_PROJECT="$LOCAL/../installers/Galileo.PKG.Installer/Galileo.PKG.$BUILD_CONFIG.pkgproj"
PKG="$LOCAL/../installers/Galileo.PKG.Installer/Galileo.pkg"
VERSION=`cat $LOCAL/version.txt`
LATEST_DIR="$LOCAL/temp"
LATEST="$LOCAL/temp/Galileo-$VERSION.pkg"


MONO_BUNDLE="$LOCAL/../projects/Galileo.Client.Mac/bin/$BUILD_CONFIG/Galileo.app/Contents/MonoBundle/"

# Make sure our pathing starts from the local directory
cd "$(dirname "$0")"

# Make dir
mkdir -p $LATEST_DIR/

# Clean Workspace
rm -rf $LOCAL/../Galileo.Client.Mac/bin/$BUILD_CONFIG
rm -rf $LOCAL/../Galileo.Client.Mac/obj/$BUILD_CONFIG

msbuild $SOLUTION /nologo /p:Configuration=$BUILD_CONFIG /t:Clean
if [ ! $? -eq 0 ]; then
    echo "[FAIL] Cleaning Solution"
    exit 1;
fi
echo "[PASS] Cleaning Solution"

# NuGet Restore
nuget restore $SOLUTION
if [ ! $? -eq 0 ]; then
    echo "[FAIL] NuGet Package Restore"
    exit 1;
fi
echo "[PASS] NuGet Package Restore"

# Copy Version Text To Plist
chmod +w $PLIST
plutil -replace CFBundleShortVersionString -string $VERSION $PLIST

# Build Galileo Mac Client
msbuild $SOLUTION /detailedsummary /nologo /t:Rebuild /p:Configuration=$BUILD_CONFIG;BUILD_DEFINE="__PACKAGE__;TRACE"
if [ ! $? -eq 0 ]; then
    echo "[FAIL] Soluton Build"
    exit 1;
fi
echo "[PASS] Soluton Build"

# Reset Version
plutil -replace CFBundleShortVersionString -string "0.0.0.0" $PLIST

# Package PKG
/usr/local/bin/packagesbuild --package-version "$VERSION" $PKG_PROJECT
if [ ! $? -eq 0 ]; then
    echo "[FAIL] Packaging"
    exit 1;
fi
echo "[PASS] Packaging"

# Move to local folder
mv -f $PKG $LATEST
if [ ! $? -eq 0 ]; then
    echo "[FAIL] Move Package To Temp"
    exit 1;
fi    
echo "[PASS] Move Package To Temp"


echo "[macOS] $VERSION ($BUILD_CONFIG) Package Created"
