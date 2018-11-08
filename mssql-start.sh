#!/bin/bash

#sa/!Dev_123!

docker run --name mssql -e ACCEPT_EULA=Y -p 1433:1433 -v mssql:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest