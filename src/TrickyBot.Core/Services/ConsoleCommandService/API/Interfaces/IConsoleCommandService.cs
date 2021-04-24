// -----------------------------------------------------------------------
// <copyright file="IConsoleCommandService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService.API.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса, использующего консольные команды.
    /// </summary>
    /// <typeparam name="TConfig"><inheritdoc cref="TrickyBot.API.Interfaces.IService{TConfig}"/></typeparam>
    public interface IConsoleCommandService<out TConfig> : IService<TConfig>
        where TConfig : IConfig
    {
        /// <summary>
        /// Получает список консольных команд, принадлежащих этому сервису.
        /// </summary>
        IReadOnlyList<IConsoleCommand> ConsoleCommands { get; }
    }
}