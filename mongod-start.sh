#!/bin/bash

docker run --name mongod -p 27017:27017 -v mongod:/data/db -d mongo