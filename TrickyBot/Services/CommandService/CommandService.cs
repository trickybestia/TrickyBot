using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrickyBot.API.Abstract;
using TrickyBot.API.Interfaces;

namespace TrickyBot.Services.CommandService
{
    public class CommandService : ServiceBase<CommandServiceConfig>
    {
        public override string Name { get; } = "Commands";
        public override List<ICommand> Commands { get; } = new List<ICommand>();
        public override string Author { get; } = "TrickyBot Team";
        public override Version Version { get; } = Bot.Instance.Version;

        public static bool IsCommand(IMessage message)
        {
            var service = Bot.Instance.ServiceManager.GetService<CommandService>();

            if (message.Channel is IDMChannel && !service.Config.AllowDMCommands)
            {
                return false;
            }

            return message is IUserMessage && !message.Author.IsBot && message.Content.StartsWith(service.Config.CommandPrefix);
        }
        protected override Task OnStart()
        {
            Bot.Instance.Client.MessageReceived += OnMessageReceived;
            return Task.CompletedTask;
        }
        protected override Task OnStop()
        {
            Bot.Instance.Client.MessageReceived -= OnMessageReceived;
            return Task.CompletedTask;
        }
        private async Task OnMessageReceived(SocketMessage message)
        {
            if (IsCommand(message))
            {
                var userMessage = (SocketUserMessage)message;
                int argPos = 0;
                userMessage.HasStringPrefix(Config.CommandPrefix, ref argPos);
                await ExecuteCommandAsync(userMessage, userMessage.Content[argPos..]);
            }
        }
        private static Task ExecuteCommandAsync(IMessage message, string parameter)
        {
            foreach (var service in Bot.Instance.ServiceManager.Services)
            {
                if (service.Config.IsEnabled)
                {
                    foreach (var command in service.Commands)
                    {
                        var match = Regex.Match(parameter, @$"{command.Name}\s?(.*)", RegexOptions.Singleline);
                        if (match.Success)
                        {
                            command.ExecuteAsync(message, match.Result("$1"));
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}