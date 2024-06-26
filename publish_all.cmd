if exist Publish ( rmdir /s /q Publish )
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-windows-x64
dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-linux-x64
dotnet publish -r osx-x64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-osx-x64
dotnet publish -r osx-arm64 -p:PublishSingleFile=true --self-contained true -o Publish/Roguelitri-osx-arm64
