namespace TrickyBot.API.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TrickyBot.API.Features;
    using TrickyBot.API.Interfaces;

    public abstract class ServiceBase<TConfig> : IService<TConfig> where TConfig : IConfig, new()
    {
        public TConfig Config { get; internal set; }
        public abstract string Name { get; }
        public abstract List<ICommand> Commands { get; }
        public abstract string Author { get; }
        public abstract Version Version { get; }

        public ServiceBase()
        {
            Config = new TConfig();
        }
        public async Task StartAsync()
        {
            Log.Info($"Starting service \"{Name}\" v{Version} by \"{Author}\"...");
            try
            {
                await OnStart();
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown while starting service \"{Name}\" v{Version} by \"{Author}\": {ex}");
            }
            Log.Info($"Service \"{Name}\" v{Version} by \"{Author}\" started.");
        }
        public async Task StopAsync()
        {
            Log.Info($"Stopping service \"{Name}\" v{Version} by \"{Author}\"...");
            try
            {
                await OnStop();
            }
            catch (Exception ex)
            {
                Log.Error($"Exception thrown while stopping service \"{Name}\" v{Version} by \"{Author}\": {ex}");
            }
            Log.Info($"Service \"{Name}\" v{Version} by \"{Author}\" stopped.");
        }
        protected abstract Task OnStart();
        protected abstract Task OnStop();
    }
}