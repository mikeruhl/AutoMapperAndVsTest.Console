# AutomapperAndVSTest.Console

## Description

There's an issue running VSTest.Console with certain settings that results in an exception for any test using Automapper.  Running it under certain conditions results in the following exception:
```
Initialization method Core.MS.Tests.UnitTest1.SetUp threw exception. System.Security.VerificationException: Operation could destabilize the runtime..
Stack Trace:
    at AutoMapper.Mapper.Initialize(Action`1 config) in C:\projects\automapper\src\AutoMapper\Mapper.cs:line 46
```

## Project Structure

- AutomapperReproduce.sln (solution file)
  - Core (core library representing the application logic)
  - Core.MS.Tests (Tests for Automapper using MSTest Framework)
  - Core.Tests (Tests for Automapper using NUnit Framework)
  - CoreConsole (Console application for manually testing Extruder class)

## Setup

You will need Vstest.console installed.  To install:
1. Download the nuget.exe if you don't have it: [Nuget Client Tools](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools)
2. In a directory of your choosing, run `nuget install Microsoft.TestPlatform` to pull vstest.
3. You'll now see a new directory "Microsoft.TestPlatform.x.x.x".  Navigate into there and rename the nupkg to a zip.
4. Now extract the contents of the zip to a directory of your choosing.
5. You now have the vstest.console tool available in `\tools\net451\Common7\IDE\Extensions\TestPlatform`.  You can add this path to your PATH system variable for access from anywhere on the command line.

## Reproduce the error

For reproduction, I'm assuming VStest.console is available anywhere.  If it is not on your machine, you'll have to reference the exe directly.

This reproduction has a solution for the issue as well, first let's reproduce it:

From a command line in the root of the project:
`vstest.console ".\Core.Tests\bin\Debug\Core.Tests.dll" /Settings:".\.runsettings"`

For consistency, I included tests in MSTest to ensure it's not NUnit.  I found the solution on NUnit issues github so I at first thought it could be their test adapter.
`vstest.console ".\Core.MS.Tests\bin\Debug\Core.MS.Tests.dll" /Settings:".\.runsettings"`

At this point you should receive the same errors for all 3 tests that's mentioned at the top of the README.

Now to fix the issue, refer to the `InProc.runsettings` at the base of the project.  This is an identical file to .runsettings, only changing the DataCollection stuff to InProcData...

This:

```
DataCollectionRunSettings>
    <DataCollectors>
      <DataCollector>
```

To This:

```
<InProcDataCollectionRunSettings>
    <InProcDataCollectors>
      <InProcDataCollector>
```
---
now run vstest.console with these settings:

`vstest.console ".\Core.Tests\bin\Debug\Core.Tests.dll" /Settings:".\InProc.runsettings"`

`vstest.console ".\Core.MS.Tests\bin\Debug\Core.MS.Tests.dll" /Settings:".\InProc.runsettings"`

You should get no errors now.   So there is something going on with the way each of those work and their relationship to Automapper.  That's as far as I got but at least there is a solution.  I'm not familiar with the difference between the 2 data collection methods, so I do not know what impact changing this has.
