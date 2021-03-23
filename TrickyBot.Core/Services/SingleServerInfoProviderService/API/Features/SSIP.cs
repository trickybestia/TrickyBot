// -----------------------------------------------------------------------
// <copyright file="SSIP.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord.WebSocket;

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
        public static SocketGuild Guild => Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService>().Guild;
    }
}