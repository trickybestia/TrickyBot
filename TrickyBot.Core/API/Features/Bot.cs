// -----------------------------------------------------------------------
// <copyright file="Bot.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Основной класс бота.
    /// </summary>
    public class Bot
    {
        private readonly ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(true);

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Bot"/>.
        /// </summary>
        public Bot()
        {
            Instance = this;
            this.Client = new DiscordSocketClient();
            this.ServiceManager = new ServiceManager();
        }

        /// <summary>
        /// Получает экземпляр <see cref="Bot"/>.
        /// </summary>
        public static Bot Instance { get; private set; }

        /// <summary>
        /// Получает экземпляр <see cref="DiscordSocketClient"/>, с которым работает бот.
        /// </summary>
        public DiscordSocketClient Client { get; }

        /// <summary>
        /// Получает экземпляр <see cref="TrickyBot.API.Features.ServiceManager"/>, с которым работает бот.
        /// </summary>
        public ServiceManager ServiceManager { get; }

        /// <summary>
        /// Получает версию сборки бота.
        /// </summary>
        public Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Запускает бота асинхронно.
        /// </summary>
        /// <param name="token">Токен бота.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task StartAsync(string token)
        {
            this.manualResetEvent.Reset();
            Log.Info(this, "Запуск бота...");
            await this.Client.LoginAsync(TokenType.Bot, token);
            await this.Client.StartAsync();
            await this.ServiceManager.StartAsync();
            Log.Info(this, "Бот запущен.");
        }

        /// <summary>
        /// Останавливает бота асинхронно.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task StopAsync()
        {
            Log.Info(this, "Остановка бота...");
            await this.ServiceManager.StopAsync();
            await this.Client.LogoutAsync();
            await this.Client.StopAsync();
            Log.Info(this, "Бот остановлен.");
            this.manualResetEvent.Set();
        }

        /// <summary>
        /// Асинхронно ожидает остановки бота.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal async Task WaitToStopAsync()
        {
            await Task.Run(this.manualResetEvent.Wait);
        }
    }
}