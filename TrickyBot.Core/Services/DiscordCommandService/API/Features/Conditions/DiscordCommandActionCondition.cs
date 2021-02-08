// -----------------------------------------------------------------------
// <copyright file="DiscordCommandActionCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.DiscordCommandService.API.Features.Conditions
{
    /// <summary>
    /// A condition based on <see cref="Features.DiscordCommandCondition"/> delegate.
    /// </summary>
    public class DiscordCommandActionCondition : IDiscordCommandCondition
    {
        /// <summary>
        /// Gets or sets a <see cref="Features.DiscordCommandCondition"/> delegate that called on every <see cref="CanExecute(IMessage, string)"/> call.
        /// </summary>
        public DiscordCommandCondition Condition { get; set; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => this.Condition(message, parameter);
    }
}