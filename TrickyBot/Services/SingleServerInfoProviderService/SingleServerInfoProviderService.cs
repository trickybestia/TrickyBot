using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrickyBot.API.Abstract;
using TrickyBot.API.Interfaces;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    public class SingleServerInfoProviderService : ServiceBase<SingleServerInfoProviderServiceConfig>
    {
        public override string Name { get; } = "SingleServerInfoProvider";
        public override List<ICommand> Commands { get; } = new List<ICommand>();
        public override string Author { get; } = "TrickyBot Team";
        public override Version Version { get; } = Bot.Instance.Version;
        public SocketGuild Guild => Bot.Instance.Client.GetGuild(Config.GuildId);

        protected override Task OnStart()
        {
            return Task.CompletedTask;
        }
        protected override Task OnStop() => Task.CompletedTask;
    }
}
