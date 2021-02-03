using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrickyBot.API.Abstract;
using TrickyBot.API.Interfaces;
using TrickyBot.Services.PermissionService.Commands;

namespace TrickyBot.Services.PermissionService
{
    public class PermissionService : ServiceBase<PermissionServiceConfig>
    {
        public override string Name { get; } = "Permissions";
        public override List<ICommand> Commands { get; } = new List<ICommand>()
        {
            new AddPermission(),
            new RemovePermission(),
            new ListPermission()
        };
        public override string Author { get; } = "TrickyBot Team";
        public override Version Version { get; } = Bot.Instance.Version;

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
                        return IsValidPermission(permission[(i + 1)..]);
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

            if (!Config.UserPermissions.ContainsKey(user.Id))
            {
                Config.UserPermissions.Add(user.Id, new HashSet<string>());
            }

            if (!Config.UserPermissions[user.Id].Add(permission))
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

            if (!Config.UserPermissions.ContainsKey(user.Id) || !Config.UserPermissions[user.Id].Remove(permission))
            {
                throw new Exception("Permission doesn't exist!");
            }

            if (Config.UserPermissions[user.Id].Count == 0)
            {
                Config.UserPermissions.Remove(user.Id);
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
            if (!Config.RolePermissions.ContainsKey(role.Id))
            {
                Config.RolePermissions.Add(role.Id, new HashSet<string>());
            }

            if (!Config.RolePermissions[role.Id].Add(permission))
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
            if (!Config.RolePermissions.ContainsKey(role.Id) || !Config.RolePermissions[role.Id].Remove(permission))
            {
                throw new Exception("Permission doesn't exist!");
            }

            if (Config.RolePermissions[role.Id].Count == 0)
            {
                Config.RolePermissions.Remove(role.Id);
            }
        }
        protected override Task OnStart()
        {
            Bot.Instance.Client.RoleDeleted += OnRoleDeleted;
            Bot.Instance.Client.UserLeft += OnUserLeft;
            return Task.CompletedTask;
        }
        protected override Task OnStop()
        {
            Bot.Instance.Client.RoleDeleted -= OnRoleDeleted;
            Bot.Instance.Client.UserLeft -= OnUserLeft;
            return Task.CompletedTask;
        }
        private Task OnUserLeft(SocketGuildUser user)
        {
            Config.UserPermissions.Remove(user.Id);
            return Task.CompletedTask;
        }
        private Task OnRoleDeleted(SocketRole role)
        {
            Config.RolePermissions.Remove(role.Id);
            return Task.CompletedTask;
        }
    }
}