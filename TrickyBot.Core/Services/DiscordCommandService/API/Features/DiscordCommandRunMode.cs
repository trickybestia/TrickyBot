// -----------------------------------------------------------------------
// <copyright file="DiscordCommandRunMode.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// Перечисление, которое содержит способы выполнения команды.
    /// </summary>
    public enum DiscordCommandRunMode
    {
        /// <summary>
        /// Команда должна выполняться синхронно.
        /// </summary>
        Sync,

        /// <summary>
        /// Команда должна выполняться асинхронно.
        /// </summary>
        Async,
    }
}