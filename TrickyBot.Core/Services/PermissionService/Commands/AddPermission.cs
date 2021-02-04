// -----------------------------------------------------------------------
// <copyright file="AddPermission.cs" company="TrickyBot Team">
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
    internal class AddPermission : ConditionCommand
    {
        public AddPermission()
        {
            this.Conditions.Add(new PermissionCondition("permissions.add"));
        }

        public override string Name { get; } = "permissions add";

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService.SingleServerInfoProviderService>().Guild;
            var match = Regex.Match(parameter, @"^<@&(\d+)>\s(\S+)\s*$");
            try
            {
                if (match.Success)
                {
                    service.AddRolePermission(guild.GetRole(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                else
                {
                    match = Regex.Match(parameter, @"^<@!?(\d+)>\s(\S+)\s*$");
                    service.AddUserPermission(guild.GetUser(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }

                await message.Channel.SendMessageAsync($"{message.Author.Mention} permission added.");
            }
            catch (Exception ex) when (ex.Message == "Permission already exists!")
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} permission already exists!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid parameters!");
            }
        }
    }
}