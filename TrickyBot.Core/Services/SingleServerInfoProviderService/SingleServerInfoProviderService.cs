// -----------------------------------------------------------------------
// <copyright file="SingleServerInfoProviderService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.WebSocket;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    /// <summary>
    /// Сервис, предоставляющий доступ к информации о "привязанном" к боту сервере.
    /// </summary>
    public class SingleServerInfoProviderService : ServiceBase<SingleServerInfoProviderServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = Array.Empty<IDiscordCommand>();

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = Array.Empty<IConsoleCommand>();

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(SingleServerInfoProviderService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <inheritdoc/>
        protected override Task OnStart()
        {
            Bot.Client.GuildAvailable += this.OnGuildAvailable;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task OnStop()
        {
            Bot.Client.GuildAvailable -= this.OnGuildAvailable;
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