# TrickyBot - модульный фреймворк для односерверных дискорд-ботов
![Main CI](https://github.com/TrickyBestia/TrickyBot/workflows/Main%20TrickyBot%20CI/badge.svg)
![Release](https://img.shields.io/github/v/release/TrickyBestia/TrickyBot?include_prereleases&style=flat)
![Total Downloads](https://img.shields.io/github/downloads/TrickyBestia/TrickyBot/total.svg?style=flat)  
TrickyBot - это фреймворк-обёртка, основанный на [Discord.Net](https://github.com/discord-net/Discord.Net).  
Этот фреймворк предназначен для создания дискорд-ботов из модулей, т.е. есть ядро бота (этот репозиторий) и подключаемые сервисы. Сервисы компилируются в динамические библиотеки и помещаются в специальную директорию, из которой ядро их подгружает. Сервисы можно сравнить с плагинами, если вы имели дело с кастомными серверами каких-либо игр.  
Плюс такой архитектуры в том, что можно реконфигурировать основные части бота без полной пересборки всего проекта.
## Документация
Для быстрого старта использования бота, а также для более подробного ознакомления с техническими особенностями вы можете заглянуть в папку [docs](docs). Начать стоит с гайда по установке: [Installation.md](docs/Installation.md)
## Лицензия
Данный репозиторий лицензирован под лицензией Creative Commons Attribution-NoDerivatives 4.0 International.