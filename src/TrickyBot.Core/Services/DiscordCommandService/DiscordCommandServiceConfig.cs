// -----------------------------------------------------------------------
// <copyright file="DiscordCommandServiceConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Features;

namespace TrickyBot.Services.DiscordCommandService
{
    /// <summary>
    /// Конфиг <see cref="TrickyBot.Services.DiscordCommandService.DiscordCommandService"/>.
    /// </summary>
    public class DiscordCommandServiceConfig : AlwaysEnabledConfig
    {
        /// <summary>
        /// Получает или задает префикс дискорд-команд.
        /// </summary>
        public string CommandPrefix { get; set; } = "!";

        /// <summary>
        /// Получает или задает значение, показывающее, должен ли бот обрабатывать команды в личных сообщениях.
        /// </summary>
        public bool AllowDMCommands { get; set; } = false;
    }
}