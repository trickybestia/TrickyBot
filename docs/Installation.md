# Установка
## Docker (Linux)
Наиболее простым способом установки бота является использование [Docker-образа](https://hub.docker.com/repository/docker/trickybestia/trickybot).
1. Создаём директорию в /var/, в которой будут находиться файлы конфигурации бота.
```bash
mkdir /var/TrickyBotData
```
2. Создаём и запускаем Docker-контейнер.
```bash
docker run docker run -it --name trickybot --mount type=bind,source=/var/TrickyBotData,target=/appdata trickybestia/trickybot:latest
```
3. В /var/TrickyBotData/Configs/BotService.json задаём токен бота.
4. Запускаем Docker-контейнер.
```bash
docker start trickybot
```
## Ubuntu 20.04 LTS
1. Устанавливаем .NET 5.0.
```bash
cd ~
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt update
sudo apt -y install apt-transport-https
sudo apt -y install screen
sudo apt -y install dotnet-runtime-5.0
```
2. Устанавливаем ядро бота. Для этого идём на [страницу релизов](https://github.com/TrickyBestia/TrickyBot/releases) и скачиваем TrickyBot.zip, который приложен к последнему релизу. Распаковываем его туда, куда нужно, например в `$HOME/Bot/`.
3. Создаём скрипт запуска бота, например `run.sh` в `$HOME/Bot/`, со следующим содержимым:
```bash
screen -S TrickyBot -d -m dotnet TrickyBot.Core.dll
```
4. При первой установке запустите скрипт `run.sh` один раз. После этого автоматически создадутся все файлы и директории бота.  
Откройте `$HOME/Bot/Data/Configs/BotService.json`, в поле `BotToken` впишите токен бота.
5. После первоначальной настройки, вам опять нужно запустить скрипт `run.sh`, потом команду `screen -r`. Если всё было сделано правильно, вы увидите перед собой консоль бота.
6. Для установки сервисов необходимо положить нужные .dll-файлы в `$HOME/Bot/Data/Services/` и перезапустить бота.
## Windows
1. Устанавливаем .NET 5.0. Переходим на [страницу загрузки](https://dotnet.microsoft.com/download/dotnet/5.0). Справа будет блок с заголовком `.NET Runtime 5.x.x`. В этом блоке есть строка `Windows`. Находим в ней Installer под нужную платформу, скачиваем и запускаем его.
2. Устанавливаем ядро бота. Для этого идём на [страницу релизов](https://github.com/TrickyBestia/TrickyBot/releases) и скачиваем TrickyBot.zip, который приложен к последнему релизу. Распаковываем его туда, куда нужно, например в `%USERPROFILE%\Desktop\Bot\`.
3. Создаём скрипт запуска бота, например `run.cmd` в `%USERPROFILE%\Desktop\Bot\`, со следующим содержимым:
```bash
dotnet TrickyBot.Core.dll
```
4. При первой установке запустите скрипт `run.cmd` один раз. После этого автоматически создадутся все файлы и директории бота.  
Откройте `%USERPROFILE%\Desktop\Bot\Data\Configs\BotService.json`, в поле `BotToken` впишите токен бота.
5. После первоначальной настройки, вам опять нужно запустить скрипт `run.cmd`. Если всё было сделано правильно, вы увидите перед собой консоль бота. При желании, ярлык на этот скрипт можно добавить в Автозагрузку, чтобы бот запускался при запуске компьютера.
6. Для установки сервисов необходимо положить нужные .dll-файлы в `%USERPROFILE%\Desktop\Bot\Data\Services\` и перезапустить бота.
