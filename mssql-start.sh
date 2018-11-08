#!/bin/bash

docker run --name mssql -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Dev_123' -p 1433:1433 -v mssql:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
