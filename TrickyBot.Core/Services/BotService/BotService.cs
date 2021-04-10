// -----------------------------------------------------------------------
// <copyright file="BotService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Discord.WebSocket;
using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.BotService
{
    /// <summary>
    /// Основной сервис бота, инкапсулирующий в себе инициализацию Discord.NET и работу с ним.
    /// </summary>
    public class BotService : ServiceBase<BotServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.BotService;

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = Array.Empty<IConsoleCommand>();

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = Array.Empty<IDiscordCommand>();

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(BotService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <summary>
        /// Получает экземпляр <see cref="DiscordSocketClient"/>, с которым работает бот.
        /// </summary>
        public DiscordSocketClient Client { get; private set; }

        /// <inheritdoc/>
        protected override async Task OnStart()
        {
            this.Client = new DiscordSocketClient();
            await this.Client.LoginAsync(this.Config.TokenType, this.Config.Token);
            await this.Client.StartAsync();
        }

        /// <inheritdoc/>
        protected override async Task OnStop()
        {
            await this.Client.LogoutAsync();
            await this.Client.StopAsync();
        }
    }
}