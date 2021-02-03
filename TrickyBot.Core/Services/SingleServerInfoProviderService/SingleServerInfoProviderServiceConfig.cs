// -----------------------------------------------------------------------
// <copyright file="SingleServerInfoProviderServiceConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Features;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    public class SingleServerInfoProviderServiceConfig : AlwaysEnabledConfig
    {
        public ulong GuildId { get; set; }
    }
}