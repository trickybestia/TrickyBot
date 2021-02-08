// -----------------------------------------------------------------------
// <copyright file="IDiscordCommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Features;

namespace TrickyBot.Services.DiscordCommandService.API.Interfaces
{
    /// <summary>
    /// A command.
    /// </summary>
    public interface IDiscordCommand
    {
        /// <summary>
        /// Gets a name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a <see cref="DiscordCommandRunMode"/> of the command.
        /// </summary>
        DiscordCommandRunMode RunMode { get; }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="message">The message that invoked the command.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A task that represents the asynchronous execution operation.</returns>
        Task ExecuteAsync(IMessage message, string parameter);
    }
}