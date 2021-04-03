#！/bin/bash

echo "[I]  Start Build Project."
./build.sh

echo "[II] Copy Dockerfile."
cp Dockerfile Release/Dockerfile
 
echo "[III]Build Docker Image."
docker build -t dotblog-server:v1.4.2 Release/ 
