# Development Notes

## Debug Build

```bash
dotnet build
```

## Building release from the command line:

```bash
dotnet build -c Release /p:SourceLinkCreate=true /p:VersionSuffix= /p:OfficialBuild=true
```

## Creating packages from command line:

```bash
dotnet pack -c Release /p:SourceLinkCreate=true /p:VersionSuffix= /p:OfficialBuild=true
```

## Add local package as nuget install sources

```bash
nuget sources add -name BionicCapacitor -source $PWD/BionicCapacitorPlugin/nupkg
nuget sources add -name BionicCapacitorTemplate -source $PWD/BionicCapcitorTemplate/nupkg
```

 ## Install local plugin

In a ***Blazor Standalone (or Hosted Client)*** project execute:

 ```bash
nuget install BionicCapacitorPlugin -DirectDownload -ExcludeVersion -PackageSaveMode nuspec -o .bionic
 ```

The plugin should be now installed. Test it using:

```bash
bionic platform
```

And ***capacitor*** should now be listed as a command.

To re-install in project, remove .bionic, re-build, re-pack and follow the last two steps under [Install local plugin](#install-local-plugin).
