// -----------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Перечисление "уровней" логирования.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Информация.
        /// </summary>
        Info,

        /// <summary>
        /// Отладка.
        /// </summary>
        Debug,

        /// <summary>
        /// Предупреждение.
        /// </summary>
        Warn,

        /// <summary>
        /// Ошибка.
        /// </summary>
        Error,
    }
}