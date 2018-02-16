@ECHO OFF

XCOPY "Mup\bin\Release" "Mup\bin\Package\lib" /S /I /Y /V
XCOPY "Mup\Mup.nuspec" "Mup\bin\Package" /Y /V

PUSHD "Mup\bin\Package"
nuget pack Mup.nuspec -Exclude *.* -Exclude lib\*.* -Exclude lib\*\*.pdb
POPD