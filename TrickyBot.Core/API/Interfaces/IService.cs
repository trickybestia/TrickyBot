// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.API.Interfaces
{
    /// <summary>
    /// A service.
    /// </summary>
    /// <typeparam name="TConfig">A service config.</typeparam>
    public interface IService<out TConfig>
        where TConfig : IConfig
    {
        /// <summary>
        /// Gets a list of commands associated with this service.
        /// </summary>
        IReadOnlyList<IDiscordCommand> DiscordCommands { get; }

        /// <summary>
        /// Gets a service config.
        /// </summary>
        TConfig Config { get; }

        /// <summary>
        /// Gets an info of the service.
        /// </summary>
        ServiceInfo Info { get; }

        /// <summary>
        /// Starts a service asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous start operation.</returns>
        Task StartAsync();

        /// <summary>
        /// Stops a service asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous stop operation.</returns>
        Task StopAsync();
    }
}