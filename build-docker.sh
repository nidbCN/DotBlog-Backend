#！/bin/bash

echo "Start Build Project."
./build.sh

echo "Copy Dockerfile."
cp Dockerfile Release/Dockerfile
 
echo "Build Docker Image."
docker build -t dotblog-server:v1.4 Release/ 
