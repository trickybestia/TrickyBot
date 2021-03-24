# TrickyBot - модульный фреймворк для односерверных дискорд-ботов
TrickyBot - это обёртка на [Discord.Net](https://github.com/discord-net/Discord.Net).  
Этот фреймворк построен по принципам архитектуры модульного монолита. Т.е. есть ядро бота (этот репозиторий) и подключаемые сервисы. Сервисы компилируются в динамические библиотеки и помещаются в специальную директорию, из которой ядро их и подгружает. Плюс такой архитектуры в том, что можно реконфигурировать основные части бота без полной пересборки всего проекта, а также в уменьшении количества boilerplate-кода.
## Установка
### Linux (проверено на Ubuntu 20.04 LTS)
1. Устанавливаем .NET.
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
2. Устанавливаем ядро бота. Для этого идём на [страницу релизов](https://github.com/TrickyBestia/TrickyBot/releases) и скачиваем TrickyBot.zip, который приложен к последнему релизу. Распаковываем его туда, куда нужно, например в `$HOME/Bot/TrickyBot/`. После этих действий струтура файлов $HOME будет выглядеть так:
```
$HOME/
    Bot/
        TrickyBot/
            TrickyBot.dll
            ...
```
Потом создаём скрипт запуска бота, например `run.sh` в `$HOME/Bot/`, со следующим содержимым:
```bash
chmod -R 700 *
cd TrickyBot
screen -S TrickyBot -d -m dotnet TrickyBot.Core.dll --data "../TrickyBotData" --tokenprovidertype "commandlinearg" --token "токен бота"
```
3. Запускаем бота. Переходим в `$HOME/Bot/`, вводим `./run.sh`, потом `screen -r`, чтобы получить доступ к консоли бота. Чтобы остановить бота, введите `exit`.
После первого запуска, бот автоматически создаст некоторые файлы, и дерево файлов будет выглядеть так:
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
После этого положите .dll файлы необходимых сервисов в `$HOME/Bot/TrickyBotData/Services` и опять запустите бота.  
Теперь у вас есть работающий бот.