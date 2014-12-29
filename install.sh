#!/bin/bash
SERVER_HOME=/home/paalgyula/wspace/UnturnedServer
echo "Cleaning up data directory"
rm $SERVER_HOME/*.dll -f
echo "Moving Assembly DLL-s"
cp bin/Debug/*.dll $SERVER_HOME/Unturned_Data/Managed
echo "Cleaning up server mods"
rm $SERVER_HOME/Managed/mods/Server\ mods/* -Rf
echo "Moving AdminCommands DLL-s"
cp bin/Debug/mods/Server\ mods/AdminCommands.dll $SERVER_HOME/Unturned_Data/Managed/mods/Server\ mods/
echo