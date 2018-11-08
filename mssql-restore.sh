#!/bin/bash

docker run -v mssql:/volume -v /tmp/mssql_backups:/backup --rm loomchild/volume-backup restore $1
