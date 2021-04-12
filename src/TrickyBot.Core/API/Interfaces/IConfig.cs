// -----------------------------------------------------------------------
// <copyright file="IConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Interfaces
{
    /// <summary>
    /// Конфиг сервиса.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Получает или задает значение, показывающее, включён ли сервис, владеющий данным конфигом.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}