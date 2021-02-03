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

using TrickyBot.API.Abstract;
using TrickyBot.API.Conditions;

namespace TrickyBot.Services.PermissionService.Commands
{
    internal class RemovePermission : ConditionCommand
    {
        public RemovePermission()
        {
            this.Conditions.Add(new PermissionCondition("permissions.remove"));
        }

        public override string Name { get; } = "permissions remove";

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService.SingleServerInfoProviderService>().Guild;
            var match = Regex.Match(parameter, @"<@&(\d+)>\s(.+)");
            try
            {
                if (match.Success)
                {
                    service.RemoveRolePermission(guild.GetRole(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                else if ((match = Regex.Match(parameter, @"<@!(\d+)>\s(.+)")).Success)
                {
                    service.RemoveUserPermission(guild.GetUser(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }

                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission removed.");
            }
            catch (Exception ex) when (ex.Message == "Permission doesn't exist!")
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission doesn't exist!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> invalid parameters!");
            }
        }
    }
}