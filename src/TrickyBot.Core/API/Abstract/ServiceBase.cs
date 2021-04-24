// -----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

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
            this.State = ServiceState.Stopped;
            this.Config = new TConfig();
        }

        /// <inheritdoc/>
        public virtual Priority Priority => Priorities.DynamicService;

        /// <inheritdoc/>
        public ServiceState State { get; private set; }

        /// <inheritdoc/>
        public TConfig Config { get; internal set; }

        /// <inheritdoc/>
        public abstract ServiceInfo Info { get; }

        /// <inheritdoc/>
        public async Task StartAsync()
        {
            if (this.State == ServiceState.Stopped)
            {
                this.State = ServiceState.Starting;
                Log.Info(this, $"Запуск сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\"...");
                try
                {
                    await this.OnStart();
                }
                catch (Exception ex)
                {
                    this.State = ServiceState.Stopped;
                    throw new Exception($"Ошибка во время запуска сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\".", ex);
                }

                this.State = ServiceState.Started;
                Log.Info(this, $"Сервис \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\" запущен.");
            }
        }

        /// <inheritdoc/>
        public async Task StopAsync()
        {
            if (this.State == ServiceState.Started)
            {
                this.State = ServiceState.Stopping;
                Log.Info(this, $"Остановка сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\"...");
                try
                {
                    await this.OnStop();
                }
                catch (Exception ex)
                {
                    Log.Error(this, $"Ошибка во время остановки сервиса \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\": {ex}");
                }

                this.State = ServiceState.Stopped;
                Log.Info(this, $"Сервис \"{this.Info.Name}\" v{this.Info.Version.ToString(3)} от \"{this.Info.Author}\" остановлен.");
            }
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