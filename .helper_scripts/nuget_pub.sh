#!/usr/bin/env bash

source ~/.nuget_bionic_key

DIR=$(dirname "$(readlink -f "$BASH_SOURCE")")

if [ -z "$1" ]
  then
    echo "No version supplied"
    exit 1
fi

if [ -z "${NUGET_BIONIC_KEY}" ]
  then
    echo "No NuGet key found for Bionic"
    exit 1
fi

${DIR}/build_release.sh

dotnet pack -c Release /p:SourceLinkCreate=true /p:VersionSuffix= /p:OfficialBuild=true

echo "Pushing BionicCapacitorPlugin.$1.nupkg to NuGet..."
dotnet nuget push ./BionicCapacitorPlugin/nupkg/BionicCapacitorPlugin.$1.nupkg --source https://api.nuget.org/v3/index.json --api-key ${NUGET_BIONIC_KEY}
if [ $? = 0 ]
  then
    echo "Completed pushing BionicCapacitorPlugin.$1.nupkg to NuGet. Should be available in a few minutes."
fi

echo "Pushing BionicCapacitorTemplate.$1.nupkg to NuGet..."
dotnet nuget push ./BionicCapacitorTemplate/nupkg/BionicCapacitorTemplate.$1.nupkg --source https://api.nuget.org/v3/index.json --api-key ${NUGET_BIONIC_KEY}
if [ $? = 0 ]
  then
    echo "Completed pushing BionicCapacitorTemplate.$1.nupkg to NuGet. Should be available in a few minutes."
fi
