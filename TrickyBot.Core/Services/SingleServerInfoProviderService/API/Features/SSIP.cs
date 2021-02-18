// -----------------------------------------------------------------------
// <copyright file="SSIP.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord.WebSocket;

namespace TrickyBot.Services.SingleServerInfoProviderService.API.Features
{
    // I think "SingleServerInfoProvider" is too long so "SSIP".
    public static class SSIP
    {
        public static SocketGuild Guild => Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService>().Guild;
    }
}