// -----------------------------------------------------------------------
// <copyright file="ListPermissions.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;

namespace TrickyBot.Services.PermissionService.DiscordCommands
{
    internal class ListPermissions : ConditionDiscordCommand
    {
        public ListPermissions()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("permissions.list"));
        }

        public override string Name { get; } = "permissions list";

        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService.SingleServerInfoProviderService>().Guild;
            var responseBuilder = new EmbedBuilder();
            var stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(parameter))
            {
                if (service.Config.RolePermissions.Count != 0)
                {
                    foreach (var roleInfo in service.Config.RolePermissions)
                    {
                        var role = guild.GetRole(roleInfo.Key);
                        stringBuilder.AppendLine($"{role.Mention}:");
                        foreach (var rolePermission in roleInfo.Value)
                        {
                            stringBuilder.Append(rolePermission).AppendLine(",");
                        }
                    }

                    stringBuilder.Replace("*", @"\*");

                    responseBuilder.AddField("Roles:", stringBuilder.ToString());
                    stringBuilder.Clear();
                }

                if (service.Config.UserPermissions.Count != 0)
                {
                    foreach (var userInfo in service.Config.UserPermissions)
                    {
                        stringBuilder.AppendLine($"<@{userInfo.Key}>:");
                        foreach (var rolePermission in userInfo.Value)
                        {
                            stringBuilder.Append(rolePermission).AppendLine(",");
                        }
                    }

                    stringBuilder.Replace("*", @"\*");

                    responseBuilder.AddField("Users:", stringBuilder.ToString());
                }

                await message.Channel.SendMessageAsync(embed: responseBuilder.Build());
            }
            else
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} invalid parameters!");
            }
        }
    }
}