// -----------------------------------------------------------------------
// <copyright file="PermissionService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.WebSocket;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.PermissionService.DiscordCommands;

namespace TrickyBot.Services.PermissionService
{
    /// <summary>
    /// Сервис обработки разрешений.
    /// </summary>
    public class PermissionService : ServiceBase<PermissionServiceConfig>, IDiscordCommandService<PermissionServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new IDiscordCommand[]
        {
            new AddUserPermission(),
            new RemoveUserPermission(),
            new AddRolePermission(),
            new RemoveRolePermission(),
            new ListPermissions(),
        };

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(PermissionService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <inheritdoc/>
        protected override Task OnStart()
        {
            Bot.Client.RoleDeleted += this.OnRoleDeleted;
            Bot.Client.UserLeft += this.OnUserLeft;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task OnStop()
        {
            Bot.Client.RoleDeleted -= this.OnRoleDeleted;
            Bot.Client.UserLeft -= this.OnUserLeft;
            return Task.CompletedTask;
        }

        private Task OnUserLeft(SocketGuildUser user)
        {
            this.Config.UserPermissions.Remove(user.Id);
            return Task.CompletedTask;
        }

        private Task OnRoleDeleted(SocketRole role)
        {
            this.Config.RolePermissions.Remove(role.Id);
            return Task.CompletedTask;
        }
    }
}