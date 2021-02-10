# TrickyBot - Modular Single-Server Discord Bot Framework
## General
TrickyBot is modular single-server discord bot framework based on [Discord.Net](https://github.com/discord-net/Discord.Net).  
TrickyBot uses monolithic service-oriented architecture. Services are .dll files placed in specified directory. You can create your perfect bot from multiple services and use it on your Discord server.  
All services are configurable and can be disabled if you want.
## Installing
### Linux (tested on Ubuntu 20.04 LTS)
At first, run the following script:
```bash
sudo apt update
sudo apt install -y apt-transport-https
sudo apt -y install screen
sudo apt -y install dotnet-runtime-5.0
```
Then go to [releases page](https://github.com/TrickyBestia/TrickyBot/releases) and download TrickyBot.zip from the latest release.  
Unpack the downloaded archive in the directory you want, e.g. `$HOME/Bot/TrickyBot/`. You will have the following file structure:
```
$HOME/
    Bot/
        TrickyBot/
            TrickyBot.dll
            ...
```
Then create a bot start script, e.g. `run.sh`, in the $HOME/Bot/ with the following content:
```bash
chmod -R 700 *
cd TrickyBot
screen -S TrickyBot -d -m dotnet TrickyBot.Core.dll --data "../TrickyBot" --tokenprovidertype "commandlinearg" --token "your bot token here"
```
After executing `./run.sh` type `screen -r` to get access to the bot console. Type `exit` to stop the bot.  
After the first launch you will have the following file structure:
```
$HOME/
    Bot/
        TrickyBot/
            TrickyBot.dll
            ...
        TrickyBotData/
            Configs/
                ...
            Services/
                ...
        run.sh
```
After that put your services in the $HOME/Bot/TrickyBotData/Services and execute `./run.sh` again.  
Now you have a working bot!