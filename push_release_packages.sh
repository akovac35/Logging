# This script will push all Release packages to nuget.org.
#
# Set the api key first: nuget setApiKey <key>
#
# Default push source also needs to be added to %AppData%/NuGet/NuGet.Config:
# <?xml version="1.0" encoding="utf-8"?>
# <configuration>
#    ...
#	<config>
#        <add key="defaultPushSource" value="https://api.nuget.org/v3/index.json" />
#	</config>
#	...
# </configuration>
#
# https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-push
# https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file

find . -wholename '*/Release/*.nupkg' -execdir rm {} \;
find . -wholename '*/Release/*.snupkg' -execdir rm {} \;
dotnet clean
# dotnet pack -c Release --version-suffix rc1
dotnet pack -c Release
find . -wholename '*/Release/*.nupkg' -execdir dotnet nuget push --skip-duplicate {} \;