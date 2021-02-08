// -----------------------------------------------------------------------
// <copyright file="RemovePermission.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.CommandService.API.Abstract;
using TrickyBot.Services.CommandService.API.Features;
using TrickyBot.Services.CommandService.API.Features.Conditions;
using TrickyBot.Services.PermissionService.API.Features;

namespace TrickyBot.Services.PermissionService.Commands
{
    internal class RemovePermission : ConditionCommand
    {
        public RemovePermission()
        {
            this.Conditions.Add(new PermissionCondition("permissions.remove"));
        }

        public override string Name { get; } = "permissions remove";

        public override CommandRunMode RunMode { get; } = CommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService.SingleServerInfoProviderService>().Guild;
            var match = Regex.Match(parameter, @"^<@&(\d+)>\s(\S+)\s*$");
            try
            {
                if (match.Success)
                {
                    Permissions.RemoveRolePermission(guild.GetRole(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                else
                {
                    match = Regex.Match(parameter, @"^<@!?(\d+)>\s(\S+)\s*$");
                    Permissions.RemoveUserPermission(guild.GetUser(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }

                await message.Channel.SendMessageAsync($"{message.Author.Mention} permission removed.");
            }
            catch (Exception ex) when (ex.Message == "Permission doesn't exist!")
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} permission doesn't exist!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid parameters!");
            }
        }
    }
}