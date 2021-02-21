// -----------------------------------------------------------------------
// <copyright file="SetCommandPrefix.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;

namespace TrickyBot.Services.DiscordCommandService.DiscordCommands
{
    internal class SetCommandPrefix : ConditionDiscordCommand
    {
        public SetCommandPrefix()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("commands.prefix.set"));
        }

        public override string Name { get; } = "commands prefix set";

        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            if (!string.IsNullOrEmpty(parameter) && !parameter.Contains('\n') && !parameter.Contains('\r'))
            {
                var service = Bot.Instance.ServiceManager.GetService<DiscordCommandService>();
                service.Config.CommandPrefix = parameter;
                await message.Channel.SendMessageAsync($"{message.Author.Mention} prefix set!");
            }
            else
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid command prefix!");
            }
        }
    }
}