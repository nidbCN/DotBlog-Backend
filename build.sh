#！/bin/bash
if [ ! -d "/Release" ]; then
    echo "[0]New Release Directory."
    mkdir /Release
else
    echo "[0]Clean Release Directory."
    rm -rf /Release/*
fi

echo "[1]Update database."
rm -f DotBlog.Server/DotBlog.db
$HOME/.dotnet/tools/dotnet-ef database update --project DotBlog.Server

echo "[2]Build Application."
dotnet publish -c Release -r ubuntu.20.04-x64 -o ./Release

echo "[3]Copy Files."
cp DotBlog.Server/DotBlog.db Release/DotBlog.db

echo "Ok."
