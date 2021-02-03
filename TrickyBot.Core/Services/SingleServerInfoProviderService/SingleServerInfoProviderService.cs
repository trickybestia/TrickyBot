// -----------------------------------------------------------------------
// <copyright file="SingleServerInfoProviderService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.WebSocket;

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

        public SocketGuild Guild => Bot.Instance.Client.GetGuild(this.Config.GuildId);

        protected override Task OnStart()
        {
            return Task.CompletedTask;
        }

        protected override Task OnStop() => Task.CompletedTask;
    }
}