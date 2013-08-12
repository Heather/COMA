@echo off
cls
SET EnableNuGetPackageRestore=true
if %PROCESSOR_ARCHITECTURE%==x86 (
	set MSBuild="%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
) else (
	set MSBUILD=%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe
)

%MSBuild% COMA.nproj /p:Configuration=Release
