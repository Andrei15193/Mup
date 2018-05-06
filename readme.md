Building the MUP solution
-------------------------

Vistual Studio 2017 Build Tools need to be installed. The targeted frameworks are:
* .NET Framework 2.0
* .NET Framework 4.5
* .NET Core 1.0
* .NET Standard 1.0

In order to run tests, the solution must be built for any change made on the source code.
The project references are made by referencing assembly files to ensure the desired target is loaded when running the tests.

Refer to .vscode/tasks.json for more details about the build process.