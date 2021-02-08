// -----------------------------------------------------------------------
// <copyright file="ICondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.Services.CommandService.API.Interfaces
{
    /// <summary>
    /// A command execution condition.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Checks the condition.
        /// </summary>
        /// <param name="message">The message that invoked the command.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A value indicating whether the condition is true or not.</returns>
        bool CanExecute(IMessage message, string parameter);
    }
}