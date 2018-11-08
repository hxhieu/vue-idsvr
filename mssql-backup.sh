#!/bin/bash

docker run -v mssql:/volume -v /tmp/mssql_backups:/backup --rm loomchild/volume-backup backup mssql_$(date +%Y-%m-%d_%H-%M-%S)
