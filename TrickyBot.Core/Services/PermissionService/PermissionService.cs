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
using TrickyBot.API.Interfaces;
using TrickyBot.Services.PermissionService.Commands;

namespace TrickyBot.Services.PermissionService
{
    public class PermissionService : ServiceBase<PermissionServiceConfig>
    {
        public override List<ICommand> Commands { get; } = new List<ICommand>()
        {
            new AddPermission(),
            new RemovePermission(),
            new ListPermissions(),
        };

        public override ServiceInfo Info { get; } = new ServiceInfo()
        {
            Name = "Permissions",
            Author = "TrickyBot Team",
            Version = Bot.Instance.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        public static bool IsValidPermission(ReadOnlySpan<char> permission)
        {
            if (permission.IsEmpty)
            {
                return false;
            }

            bool hasWildcard = false;
            for (int i = 0; i < permission.Length; i++)
            {
                if (permission[i] == '*')
                {
                    if (hasWildcard)
                    {
                        return false;
                    }

                    hasWildcard = true;
                }
                else if (permission[i] == '.')
                {
                    if (hasWildcard)
                    {
                        return false;
                    }
                    else
                    {
                        return IsValidPermission(permission.Slice(i + 1));
                    }
                }
            }

            return true;
        }

        public static bool HasPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (service.Config.UserPermissions.ContainsKey(user.Id))
            {
                foreach (var parentPermission in service.Config.UserPermissions[user.Id])
                {
                    if (IsSubpermission(parentPermission, permission))
                    {
                        return true;
                    }
                }
            }

            foreach (var roleId in user.RoleIds.Intersect(service.Config.RolePermissions.Keys))
            {
                foreach (var parentPermission in service.Config.RolePermissions[roleId])
                {
                    if (IsSubpermission(parentPermission, permission))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void AddUserPermission(IGuildUser user, string permission)
        {
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.UserPermissions.ContainsKey(user.Id))
            {
                this.Config.UserPermissions.Add(user.Id, new HashSet<string>());
            }

            if (!this.Config.UserPermissions[user.Id].Add(permission))
            {
                throw new Exception("Permission already exists!");
            }
        }

        public void RemoveUserPermission(IGuildUser user, string permission)
        {
            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.UserPermissions.ContainsKey(user.Id) || !this.Config.UserPermissions[user.Id].Remove(permission))
            {
                throw new Exception("Permission doesn't exist!");
            }

            if (this.Config.UserPermissions[user.Id].Count == 0)
            {
                this.Config.UserPermissions.Remove(user.Id);
            }
        }

        public void AddRolePermission(IRole role, string permission)
        {
            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.RolePermissions.ContainsKey(role.Id))
            {
                this.Config.RolePermissions.Add(role.Id, new HashSet<string>());
            }

            if (!this.Config.RolePermissions[role.Id].Add(permission))
            {
                throw new Exception("Permission already exists!");
            }
        }

        public void RemoveRolePermission(IRole role, string permission)
        {
            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!this.Config.RolePermissions.ContainsKey(role.Id) || !this.Config.RolePermissions[role.Id].Remove(permission))
            {
                throw new Exception("Permission doesn't exist!");
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