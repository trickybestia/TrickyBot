// -----------------------------------------------------------------------
// <copyright file="SSIP.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord.WebSocket;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;

namespace TrickyBot.Services.SingleServerInfoProviderService.API.Features
{
    /// <summary>
    /// API для <see cref="SingleServerInfoProviderService"/>.
    /// </summary>
    public static class SSIP
    {
        /// <summary>
        /// Получает "привязанный" к боту сервер.
        /// </summary>
        public static SocketGuild Guild
        {
            get
            {
                return Bot.Client.GetGuild(ServiceManager.GetService<SingleServerInfoProviderService>().Config.GuildId);
            }
        }
    }
}