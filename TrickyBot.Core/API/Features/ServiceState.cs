// -----------------------------------------------------------------------
// <copyright file="ServiceState.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Перечисление значений состояния сервиса.
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// Сервис запущен.
        /// </summary>
        Started,

        /// <summary>
        /// Сервис остановлен.
        /// </summary>
        Stopped,

        /// <summary>
        /// Сервис запускается.
        /// </summary>
        Starting,

        /// <summary>
        /// Сервис останавливается.
        /// </summary>
        Stopping,
    }
}