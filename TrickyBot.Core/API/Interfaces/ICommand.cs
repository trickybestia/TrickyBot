// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;
using TrickyBot.API.Features;

namespace TrickyBot.API.Interfaces
{
    /// <summary>
    /// A command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets a name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a <see cref="CommandRunMode"/> of the command.
        /// </summary>
        CommandRunMode RunMode { get; }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="message">The message that invoked the command.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A task that represents the asynchronous execution operation.</returns>
        Task ExecuteAsync(IMessage message, string parameter);
    }
}