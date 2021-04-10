// -----------------------------------------------------------------------
// <copyright file="ConsoleCommandService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.ConsoleCommandService.ConsoleCommands;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService
{
    /// <summary>
    /// Сервис для обработки консольных команд.
    /// </summary>
    public class ConsoleCommandService : ServiceBase<ConsoleCommandServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = Array.Empty<IDiscordCommand>();

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = new IConsoleCommand[]
        {
            new Exit(),
        };

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(ConsoleCommandService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <inheritdoc/>
        protected override Task OnStart()
        {
            EventConsole.OnReadLine += this.ParseCommandAsync;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task OnStop()
        {
            EventConsole.OnReadLine -= this.ParseCommandAsync;
            return Task.CompletedTask;
        }

        private async void ParseCommandAsync(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                Log.Info(this, $"Обработка команды\n{input}");
                bool commandHandled = false;
                foreach (var service in ServiceManager.Services)
                {
                    if (service.Config.IsEnabled)
                    {
                        foreach (var command in service.ConsoleCommands)
                        {
                            var match = Regex.Match(input, @$"{command.Name}\s?(.*)", RegexOptions.Singleline);
                            if (match.Success)
                            {
                                commandHandled = true;
                                if (command.RunMode == ConsoleCommandRunMode.Sync)
                                {
                                    await command.ExecuteAsync(match.Result("$1"));
                                }
                                else if (command.RunMode == ConsoleCommandRunMode.Async)
                                {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                    Task.Run(() => command.ExecuteAsync(match.Result("$1")));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                }
                            }
                        }
                    }
                }

                if (!commandHandled)
                {
                    Log.Error(this, $"Нераспознанная команда:\n{input}");
                }
            }
        }
    }
}