#！/bin/bash
if [ ! -d "/Release" ]; then
    echo "\t[0]New Release Directory."
    mkdir /Release
else
    echo "\t[0]Clean Release Directory."
    rm -rf /Release/*
fi

echo "\t[1]Update database."
rm -f DotBlog.Server/DotBlog.db
$HOME/.dotnet/tools/dotnet-ef database update --project DotBlog.Server

echo "\t[2]Build Application."
dotnet publish -c Release -p:PublishReadyToRun=true --no-self-contained -r ubuntu.20.04-x64 -o ./Release

echo "\t[3]Copy Files."
cp DotBlog.Server/DotBlog.db Release/DotBlog.db

