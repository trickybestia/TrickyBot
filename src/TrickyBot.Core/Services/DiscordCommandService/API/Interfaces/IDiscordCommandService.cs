// -----------------------------------------------------------------------
// <copyright file="IDiscordCommandService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Interfaces;

namespace TrickyBot.Services.DiscordCommandService.API.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса, использующего дискорд-команды.
    /// </summary>
    /// <typeparam name="TConfig"><inheritdoc cref="TrickyBot.API.Interfaces.IService{TConfig}"/></typeparam>
    public interface IDiscordCommandService<out TConfig> : IService<TConfig>
        where TConfig : IConfig
    {
        /// <summary>
        /// Получает список дискорд-команд, принадлежащих этому сервису.
        /// </summary>
        IReadOnlyList<IDiscordCommand> DiscordCommands { get; }
    }
}