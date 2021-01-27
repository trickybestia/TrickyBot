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
    public abstract class ServiceBase<TConfig> : IService<TConfig>
        where TConfig : IConfig, new()
    {
        public ServiceBase()
        {
            this.Config = new TConfig();
        }

        public TConfig Config { get; internal set; }

        public abstract string Name { get; }

        public abstract List<ICommand> Commands { get; }

        public abstract string Author { get; }

        public abstract Version Version { get; }

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

        protected abstract Task OnStart();

        protected abstract Task OnStop();
    }
}