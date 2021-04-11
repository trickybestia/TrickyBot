// -----------------------------------------------------------------------
// <copyright file="DiscordCommandService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.DiscordCommands;

namespace TrickyBot.Services.DiscordCommandService
{
    /// <summary>
    /// Сервис для обработки дискорд-команд.
    /// </summary>
    public class DiscordCommandService : ServiceBase<DiscordCommandServiceConfig>
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new IDiscordCommand[]
        {
            new SetCommandPrefix(),
        };

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = Array.Empty<IConsoleCommand>();

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(DiscordCommandService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <inheritdoc/>
        protected override Task OnStart()
        {
            Bot.Client.MessageReceived += this.OnMessageReceived;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task OnStop()
        {
            Bot.Client.MessageReceived -= this.OnMessageReceived;
            return Task.CompletedTask;
        }

        private async Task ExecuteCommandAsync(IMessage message, string parameter)
        {
            await this.semaphore.WaitAsync();
            foreach (var service in ServiceManager.Services)
            {
                if (service.Config.IsEnabled)
                {
                    foreach (var command in service.DiscordCommands)
                    {
                        var match = Regex.Match(parameter, @$"{command.Name}\s?(.*)", RegexOptions.Singleline);
                        if (match.Success)
                        {
                            if (command.RunMode == DiscordCommandRunMode.Sync)
                            {
                                try
                                {
                                    await command.ExecuteAsync(message, match.Result("$1"));
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(this, $"Exception thrown while executing command \"{command.Name}\": {ex}");
                                }
                            }
                            else
                            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                Task.Run(() => command.ExecuteAsync(message, match.Result("$1")));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                            }
                        }
                    }
                }
            }

            this.semaphore.Release();
        }

        private async Task OnMessageReceived(SocketMessage message)
        {
            if (TrickyBot.Services.DiscordCommandService.API.Features.DiscordCommands.IsCommand(message))
            {
                var userMessage = (SocketUserMessage)message;
                int argPos = 0;
                userMessage.HasStringPrefix(this.Config.CommandPrefix, ref argPos);
                await this.ExecuteCommandAsync(userMessage, userMessage.Content[argPos..]);
            }
        }
    }
}