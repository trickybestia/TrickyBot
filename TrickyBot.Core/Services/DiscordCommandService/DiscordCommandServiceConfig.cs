// -----------------------------------------------------------------------
// <copyright file="DiscordCommandServiceConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Features;

namespace TrickyBot.Services.DiscordCommandService
{
    public class DiscordCommandServiceConfig : AlwaysEnabledConfig
    {
        public string CommandPrefix { get; set; } = "!";

        public bool AllowDMCommands { get; set; } = false;
    }
}