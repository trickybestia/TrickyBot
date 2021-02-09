// -----------------------------------------------------------------------
// <copyright file="IConsoleCommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.Services.ConsoleCommandService.API.Features;

namespace TrickyBot.Services.ConsoleCommandService.API.Interfaces
{
    /// <summary>
    /// A server console command.
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Gets a name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a command run mode.
        /// </summary>
        ConsoleCommandRunMode RunMode { get; }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="parameter">A command parameter.</param>
        /// <returns>A task that represents the asynchronous execution operation.</returns>
        Task ExecuteAsync(string parameter);
    }
}