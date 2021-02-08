// -----------------------------------------------------------------------
// <copyright file="ActionCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;
using TrickyBot.Services.CommandService.API.Interfaces;

namespace TrickyBot.Services.CommandService.API.Features.Conditions
{
    /// <summary>
    /// A condition based on <see cref="Features.Condition"/> delegate.
    /// </summary>
    public class ActionCondition : ICondition
    {
        /// <summary>
        /// Gets or sets a <see cref="Features.Condition"/> delegate that called on every <see cref="CanExecute(IMessage, string)"/> call.
        /// </summary>
        public Condition Condition { get; set; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => this.Condition(message, parameter);
    }
}