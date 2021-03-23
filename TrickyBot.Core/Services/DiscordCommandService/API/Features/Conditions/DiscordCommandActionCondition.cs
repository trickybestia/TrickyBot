﻿// -----------------------------------------------------------------------
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
    /// Условие, поведение которого зависит от предоставленного <see cref="Features.DiscordCommandCondition"/>.
    /// </summary>
    public class DiscordCommandActionCondition : IDiscordCommandCondition
    {
        /// <summary>
        /// Получает или задает делегат, который определяет поведение текущего экземпляра.
        /// </summary>
        public DiscordCommandCondition Condition { get; set; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => this.Condition(message, parameter);
    }
}