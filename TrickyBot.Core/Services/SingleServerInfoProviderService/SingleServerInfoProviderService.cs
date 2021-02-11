// -----------------------------------------------------------------------
// <copyright file="SingleServerInfoProviderService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.WebSocket;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    public class SingleServerInfoProviderService : ServiceBase<SingleServerInfoProviderServiceConfig>
    {
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new List<IDiscordCommand>();

        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = new List<IConsoleCommand>();

        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Name = "SingleServerInfoProvider",
            Author = "TrickyBot Team",
            Version = Bot.Instance.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        public SocketGuild Guild => Bot.Instance.Client.GetGuild(this.Config.GuildId);

        protected override Task OnStart()
        {
            Bot.Instance.Client.GuildAvailable += this.OnGuildAvailable;
            return Task.CompletedTask;
        }

        protected override Task OnStop()
        {
            Bot.Instance.Client.GuildAvailable -= this.OnGuildAvailable;
            return Task.CompletedTask;
        }

        private async Task OnGuildAvailable(SocketGuild arg)
        {
            if (this.Config.GuildId == 0)
            {
                this.Config.GuildId = arg.Id;
            }
            else if (this.Config.GuildId != arg.Id)
            {
                await arg.LeaveAsync();
            }
        }
    }
}