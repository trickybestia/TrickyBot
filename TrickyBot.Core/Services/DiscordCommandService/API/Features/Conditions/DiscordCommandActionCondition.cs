// -----------------------------------------------------------------------
// <copyright file="DiscordCommandActionCondition.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
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
        /// Делегат, который определяет поведение текущего экземпляра.
        /// </summary>
        private readonly DiscordCommandCondition condition;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DiscordCommandActionCondition"/>.
        /// </summary>
        /// <param name="condition"><inheritdoc cref="condition"/></param>
        public DiscordCommandActionCondition(DiscordCommandCondition condition) => this.condition = condition;

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => this.condition(message, parameter);
    }
}