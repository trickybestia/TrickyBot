// -----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;
using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.API.Abstract
{
    /// <summary>
    /// Базовый класс сервиса.
    /// </summary>
    /// <typeparam name="TConfig"><inheritdoc/></typeparam>
    public abstract class ServiceBase<TConfig> : IService<TConfig>
        where TConfig : IConfig, new()
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceBase{TConfig}"/>.
        /// </summary>
        public ServiceBase()
        {
            this.Config = new TConfig();
            this.DiscordCommands = DiscordCommandLoader.GetCommands(this.GetType().Assembly);
            this.ConsoleCommands = ConsoleCommandLoader.GetCommands(this.GetType().Assembly);
        }

        /// <inheritdoc/>
        public TConfig Config { get; internal set; }

        /// <inheritdoc/>
        public virtual IReadOnlyList<IDiscordCommand> DiscordCommands { get; }

        /// <inheritdoc/>
        public virtual IReadOnlyList<IConsoleCommand> ConsoleCommands { get; }

        /// <inheritdoc/>
        public abstract ServiceInfo Info { get; }

        /// <inheritdoc/>
        public async Task StartAsync()
        {
            Log.Info(this, $"Запуск сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\"...");
            try
            {
                await this.OnStart();
            }
            catch (Exception ex)
            {
                Log.Error(this, $"Exception во время запуска сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\": {ex}");
            }

            Log.Info(this, $"Сервис \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\" запущен.");
        }

        /// <inheritdoc/>
        public async Task StopAsync()
        {
            Log.Info(this, $"Остановка сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\"...");
            try
            {
                await this.OnStop();
            }
            catch (Exception ex)
            {
                Log.Error(this, $"Exception во время остановки сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\": {ex}");
            }

            Log.Info(this, $"Сервис \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\" остановлен.");
        }

        /// <summary>
        /// Метод, который выполняется при запуске сервиса.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        protected abstract Task OnStart();

        /// <summary>
        /// Метод, который выполняется при остановке сервиса.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        protected abstract Task OnStop();
    }
}