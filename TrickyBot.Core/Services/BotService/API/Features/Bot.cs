// -----------------------------------------------------------------------
// <copyright file="Bot.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

using Discord.WebSocket;
using TrickyBot.API.Features;

namespace TrickyBot.Services.BotService.API.Features
{
    /// <summary>
    /// API для <see cref="TrickyBot.Services.BotService.BotService"/>.
    /// </summary>
    public static class Bot
    {
        /// <summary>
        /// Получает версию сборки бота.
        /// </summary>
        public static Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        /// <inheritdoc cref="TrickyBot.Services.BotService.BotService.Client"/>
        public static DiscordSocketClient Client => ServiceManager.GetService<BotService>().Client;
    }
}