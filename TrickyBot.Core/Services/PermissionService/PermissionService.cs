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
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.PermissionService.API.Features;
using TrickyBot.Services.PermissionService.DiscordCommands;

namespace TrickyBot.Services.PermissionService
{
    public class PermissionService : ServiceBase<PermissionServiceConfig>
    {
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = new List<IDiscordCommand>()
        {
            new AddPermission(),
            new RemovePermission(),
            new ListPermissions(),
        };

        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = new List<IConsoleCommand>();

        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Name = nameof(PermissionService),
            Author = "TrickyBot Team",
            Version = Bot.Instance.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        internal bool HasPermission(IGuildUser user, string permission)
        {
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (this.Config.UserPermissions.ContainsKey(user.Id))
            {
                foreach (var parentPermission in this.Config.UserPermissions[user.Id])
                {
                    if (IsSubpermission(parentPermission, permission))
                    {
                        return true;
                    }
                }
            }

            foreach (var roleId in user.RoleIds.Intersect(this.Config.RolePermissions.Keys))
            {
                foreach (var parentPermission in this.Config.RolePermissions[roleId])
                {
                    if (IsSubpermission(parentPermission, permission))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal void AddUserPermission(IGuildUser user, string permission)
        {
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.UserPermissions.ContainsKey(user.Id))
            {
                this.Config.UserPermissions.Add(user.Id, new HashSet<string>());
            }

            if (!this.Config.UserPermissions[user.Id].Add(permission))
            {
                throw new PermissionAlreadyExistsException(permission);
            }
        }

        internal void RemoveUserPermission(IGuildUser user, string permission)
        {
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.UserPermissions.ContainsKey(user.Id) || !this.Config.UserPermissions[user.Id].Remove(permission))
            {
                throw new PermissionNotExistsException(permission);
            }

            if (this.Config.UserPermissions[user.Id].Count == 0)
            {
                this.Config.UserPermissions.Remove(user.Id);
            }
        }

        internal void AddRolePermission(IRole role, string permission)
        {
            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.RolePermissions.ContainsKey(role.Id))
            {
                this.Config.RolePermissions.Add(role.Id, new HashSet<string>());
            }

            if (!this.Config.RolePermissions[role.Id].Add(permission))
            {
                throw new PermissionAlreadyExistsException(permission);
            }
        }

        internal void RemoveRolePermission(IRole role, string permission)
        {
            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.RolePermissions.ContainsKey(role.Id) || !this.Config.RolePermissions[role.Id].Remove(permission))
            {
                throw new PermissionNotExistsException(permission);
            }

            if (this.Config.RolePermissions[role.Id].Count == 0)
            {
                this.Config.RolePermissions.Remove(role.Id);
            }
        }

        protected override Task OnStart()
        {
            Bot.Instance.Client.RoleDeleted += this.OnRoleDeleted;
            Bot.Instance.Client.UserLeft += this.OnUserLeft;
            return Task.CompletedTask;
        }

        protected override Task OnStop()
        {
            Bot.Instance.Client.RoleDeleted -= this.OnRoleDeleted;
            Bot.Instance.Client.UserLeft -= this.OnUserLeft;
            return Task.CompletedTask;
        }

        private static bool IsSubpermission(ReadOnlySpan<char> parent, ReadOnlySpan<char> child)
        {
            for (int i = 0; i < parent.Length && i < child.Length; i++)
            {
                if (parent[i] == '*')
                {
                    return true;
                }

                if (parent[i] != child[i])
                {
                    break;
                }
            }

            if (parent.Length == child.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
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