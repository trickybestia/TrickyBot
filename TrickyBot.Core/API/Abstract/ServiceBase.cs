// -----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Abstract
{
    /// <summary>
    /// A service base class.
    /// </summary>
    /// <typeparam name="TConfig"><inheritdoc/></typeparam>
    public abstract class ServiceBase<TConfig> : IService<TConfig>
        where TConfig : IConfig, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TConfig}"/> class.
        /// </summary>
        public ServiceBase()
        {
            this.Config = new TConfig();
        }

        /// <inheritdoc/>
        public TConfig Config { get; internal set; }

        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract List<ICommand> Commands { get; }

        /// <inheritdoc/>
        public abstract string Author { get; }

        /// <inheritdoc/>
        public abstract Version Version { get; }

        /// <inheritdoc/>
        public async Task StartAsync()
        {
            Log.Info($"Starting service \"{this.Name}\" v{this.Version} by \"{this.Author}\"...");
            try
            {
                await this.OnStart();
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown while starting service \"{this.Name}\" v{this.Version} by \"{this.Author}\": {ex}");
            }

            Log.Info($"Service \"{this.Name}\" v{this.Version} by \"{this.Author}\" started.");
        }

        /// <inheritdoc/>
        public async Task StopAsync()
        {
            Log.Info($"Stopping service \"{this.Name}\" v{this.Version} by \"{this.Author}\"...");
            try
            {
                await this.OnStop();
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown while stopping service \"{this.Name}\" v{this.Version} by \"{this.Author}\": {ex}");
            }

            Log.Info($"Service \"{this.Name}\" v{this.Version} by \"{this.Author}\" stopped.");
        }

        /// <summary>
        /// A method that contains code that executes when the service starts.
        /// </summary>
        /// <returns>A task that represents the asynchronous start operation.</returns>
        protected abstract Task OnStart();

        /// <summary>
        /// A method that contains code that executes when the service stops.
        /// </summary>
        /// <returns>A task that represents the asynchronous stop operation.</returns>
        protected abstract Task OnStop();
    }
}