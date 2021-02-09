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
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    public class SingleServerInfoProviderService : ServiceBase<SingleServerInfoProviderServiceConfig>
    {
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new List<IDiscordCommand>();

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
            return Task.CompletedTask;
        }

        protected override Task OnStop() => Task.CompletedTask;
    }
}