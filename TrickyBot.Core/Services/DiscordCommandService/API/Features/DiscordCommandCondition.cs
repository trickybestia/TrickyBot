// -----------------------------------------------------------------------
// <copyright file="DiscordCommandCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// A delegate that represents a condition.
    /// </summary>
    /// <param name="message">The message that invoked the command.</param>
    /// <param name="parameter">Command parameter.</param>
    /// <returns>A value indicating whether the condition is true or not.</returns>
    public delegate bool DiscordCommandCondition(IMessage message, string parameter);
}