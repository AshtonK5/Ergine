@echo off

:: // Constants
set DOTNET_VERSION=net8.0
set TARGET_CONFIG=Debug
set CURRENT_PLATFORM=windows

:: Get project root path
set PROJECT_ROOT_PATH=..\..\

:: Get project source path
set PROJECT_SOURCE_PATH=%PROJECT_ROOT_PATH%src\

:: Get 'BuildTool' project path
set PROJECT_BUILD_TOOL_PATH=%PROJECT_ROOT_PATH%tools\BuildTool

:: Get 'BuildTool' binaries path when it gets built
set PROJECT_BUILD_TOOL_BIN_PATH=%PROJECT_ROOT_PATH%project\binaries\tools\BuildTool\%TARGET_CONFIG%\%DOTNET_VERSION%\publish\BuildTool.exe

:: Constants \\

:: Echo project 'BuildTool' is going to be built at the current path
pushd %PROJECT_BUILD_TOOL_PATH%
echo Building BuildTool Version=%DOTNET_VERSION% Config=%TARGET_CONFIG% at path: %CD%..
popd

:: Publish 'BuildTool' into an executable
dotnet publish %PROJECT_BUILD_TOOL_PATH% -c %TARGET_CONFIG%

:: Invoke the 'Buildtool' and generate project files
%PROJECT_BUILD_TOOL_BIN_PATH% %PROJECT_ROOT_PATH% %PROJECT_SOURCE_PATH% --platform %CURRENT_PLATFORM% -c %TARGET_CONFIG%
pause
