// -----------------------------------------------------------------------
// <copyright file="PermissionService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.PermissionService.API.Features;
using TrickyBot.Services.PermissionService.DiscordCommands;

namespace TrickyBot.Services.PermissionService
{
    /// <summary>
    /// Сервис обработки разрешений.
    /// </summary>
    public class PermissionService : ServiceBase<PermissionServiceConfig>
    {
        /// <inheritdoc/>
        public override Priority Priority => Priorities.CoreService;

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new IDiscordCommand[]
        {
            new AddPermission(),
            new RemovePermission(),
            new ListPermissions(),
        };

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = Array.Empty<IConsoleCommand>();

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(PermissionService),
            Author = "TrickyBot Team",
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