// -----------------------------------------------------------------------
// <copyright file="ListPermissions.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using System.Threading.Tasks;

using Discord;
using TrickyBot.API.Features;
using TrickyBot.Services.CustomizationService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;
using TrickyBot.Services.SingleServerInfoProviderService.API.Features;

namespace TrickyBot.Services.PermissionService.DiscordCommands
{
    /// <summary>
    /// Команда вывода списка разрешений.
    /// </summary>
    internal class ListPermissions : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ListPermissions"/>.
        /// </summary>
        public ListPermissions()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("permissions.list"));
        }

        /// <inheritdoc/>
        public override string Name { get; } = "permissions list";

        /// <inheritdoc/>
        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        /// <inheritdoc/>
        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = ServiceManager.GetService<PermissionService>();
            var guild = SSIP.Guild;
            var responseBuilder = new EmbedBuilder();
            var stringBuilder = new StringBuilder();

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

                responseBuilder.AddField("Роли:", stringBuilder.ToString());
                stringBuilder.Clear();
            }

            if (service.Config.UserPermissions.Count != 0)
            {
                foreach (var userInfo in service.Config.UserPermissions)
                {
                    var user = guild.GetUser(userInfo.Key);
                    stringBuilder.AppendLine($"{user.Mention}:");
                    foreach (var rolePermission in userInfo.Value)
                    {
                        stringBuilder.Append(rolePermission).AppendLine(",");
                    }
                }

                stringBuilder.Replace("*", @"\*");

                responseBuilder.AddField("Пользователи:", stringBuilder.ToString());
            }

            await message.Channel.SendMessageAsync(embed: responseBuilder.Build());
        }
    }
}