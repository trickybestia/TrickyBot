// -----------------------------------------------------------------------
// <copyright file="ConsoleCommandService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.ConsoleCommandService.ConsoleCommands;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService
{
    public class ConsoleCommandService : ServiceBase<ConsoleCommandServiceConfig>
    {
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new List<IDiscordCommand>();

        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = new List<IConsoleCommand>()
        {
            new Exit(),
        };

        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Name = nameof(ConsoleCommandService),
            Author = "TrickyBot Team",
            Version = Bot.Instance.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        protected override Task OnStart()
        {
            Task.Run(this.ParseCommandsAsync);
            return Task.CompletedTask;
        }

        protected override Task OnStop() => Task.CompletedTask;

        private async Task ParseCommandsAsync()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Log.Info(this, $"Handling command \"{input}\"...");
                    bool commandHandled = false;
                    foreach (var service in Bot.Instance.ServiceManager.Services)
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
                        Log.Error(this, $"Unrecognized command \"{input}\"!");
                    }
                }
            }
        }
    }
}