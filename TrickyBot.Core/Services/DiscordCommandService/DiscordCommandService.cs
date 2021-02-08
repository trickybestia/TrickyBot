// -----------------------------------------------------------------------
// <copyright file="DiscordCommandService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
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
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.Commands;

namespace TrickyBot.Services.DiscordCommandService
{
    public class DiscordCommandService : ServiceBase<DiscordCommandServiceConfig>
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public override List<IDiscordCommand> DiscordCommands { get; } = new List<IDiscordCommand>()
        {
            new SetCommandPrefix(),
        };

        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Name = "Commands",
            Author = "TrickyBot Team",
            Version = Bot.Instance.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        public static bool IsCommand(IMessage message)
        {
            var service = Bot.Instance.ServiceManager.GetService<DiscordCommandService>();

            if (message.Channel is IDMChannel && !service.Config.AllowDMCommands)
            {
                return false;
            }

            return message is IUserMessage && !message.Author.IsBot && message.Content.StartsWith(service.Config.CommandPrefix);
        }

        protected override Task OnStart()
        {
            Bot.Instance.Client.MessageReceived += this.OnMessageReceived;
            return Task.CompletedTask;
        }

        protected override Task OnStop()
        {
            Bot.Instance.Client.MessageReceived -= this.OnMessageReceived;
            return Task.CompletedTask;
        }

        private async Task ExecuteCommandAsync(IMessage message, string parameter)
        {
            await this.semaphore.WaitAsync();
            foreach (var service in Bot.Instance.ServiceManager.Services)
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
                                    Log.Error($"Exception thrown while executing command \"{command.Name}\": {ex}");
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
            if (IsCommand(message))
            {
                var userMessage = (SocketUserMessage)message;
                int argPos = 0;
                userMessage.HasStringPrefix(this.Config.CommandPrefix, ref argPos);
                await this.ExecuteCommandAsync(userMessage, userMessage.Content[argPos..]);
            }
        }
    }
}