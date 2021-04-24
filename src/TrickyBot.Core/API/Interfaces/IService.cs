// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.API.Features;

namespace TrickyBot.API.Interfaces
{
    /// <summary>
    /// Интерфейс, который должен быть реализован сервисом бота.
    /// </summary>
    /// <typeparam name="TConfig">Конфиг сервиса.</typeparam>
    public interface IService<out TConfig>
        where TConfig : IConfig
    {
        /// <summary>
        /// Получает состояние сервиса.
        /// </summary>
        ServiceState State { get; }

        /// <summary>
        /// Получает приоритет загрузки сервиса.
        /// </summary>
        Priority Priority { get; }

        /// <summary>
        /// Получает конфиг сервиса.
        /// </summary>
        TConfig Config { get; }

        /// <summary>
        /// Получает информацию о сервисе.
        /// </summary>
        ServiceInfo Info { get; }

        /// <summary>
        /// Запускает сервис асинхронно.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task StartAsync();

        /// <summary>
        /// Останавливает сервис асинхронно.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task StopAsync();
    }
}