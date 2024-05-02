rm -Rf Publish
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-windows-x64
dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-linux-x64
cp Lib/linux-x64/* Publish/Roguelitri-linux-x64
dotnet publish -r osx-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-osx-x64
cp Lib/osx-x64/* Publish/Roguelitri-linux-x64
dotnet publish -r osx-arm64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-osx-arm64
cp Lib/osx-arm64/* Publish/Roguelitri-linux-x64
