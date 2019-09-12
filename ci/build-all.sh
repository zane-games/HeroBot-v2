#!/bin/sh
for i in $(ls -d */)
do 
	FILE=${i%%/}
	if [[ ${FILE} = HeroBot.Plugins.* ]]
	then
		cd ${FILE}
		dotnet restore
		dotnet build -c Release -o ../build/Modules
		cd ../
	fi
done
cd HeroBot.Core
dotnet restore
dotnet build -c Release -o ../build
cd ../
