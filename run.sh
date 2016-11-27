# sudo apt-get install psmisc
sudo lsof -t -i tcp:80 -s tcp:listen | sudo xargs kill
SCRIPT=`realpath $0`
ABSOLUTE_PATH=`dirname $SCRIPT`
clear
cd "$ABSOLUTE_PATH/MSiteFramework/bin/Debug/"
sudo mono MSiteFramework.exe
