// -----------------------------------------------------------------------
// <copyright file="ConsoleCommandRunMode.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.ConsoleCommandService.API.Features
{
    /// <summary>
    /// An enum that contains console command run modes.
    /// </summary>
    public enum ConsoleCommandRunMode
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