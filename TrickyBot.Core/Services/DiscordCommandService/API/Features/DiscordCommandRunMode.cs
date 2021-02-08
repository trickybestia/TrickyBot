// -----------------------------------------------------------------------
// <copyright file="DiscordCommandRunMode.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// An enum that contains command run modes.
    /// </summary>
    public enum DiscordCommandRunMode
    {
        /// <summary>
        /// A command can be run synchronously.
        /// </summary>
        Sync,

        /// <summary>
        /// A command should be run asynchronously.
        /// </summary>
        Async,
    }
}