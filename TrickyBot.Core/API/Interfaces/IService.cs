// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        List<ICommand> Commands { get; }

        /// <summary>
        /// Gets a service config.
        /// </summary>
        TConfig Config { get; }

        /// <summary>
        /// Gets a service name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a service author.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets a service version.
        /// </summary>
        Version Version { get; }

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