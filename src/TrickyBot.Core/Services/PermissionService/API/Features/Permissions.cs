// -----------------------------------------------------------------------
// <copyright file="Permissions.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Discord;
using TrickyBot.API.Features;

namespace TrickyBot.Services.PermissionService.API.Features
{
    /// <summary>
    /// API для <see cref="PermissionService"/>.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// Проверяет формат разрешения.
        /// </summary>
        /// <param name="permission">Разрешение для проверки.</param>
        /// <returns>Значение, указывающее, является ли <paramref name="permission"/> верным разрешением.</returns>
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

        /// <summary>
        /// Проверяет, является ли <paramref name="child"/> дочерним расширением <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">Родительское разрешение.</param>
        /// <param name="child">Дочернее разрешение.</param>
        /// <returns>Значение, указывающее, является ли <paramref name="child"/> дочерним расширением <paramref name="parent"/>.</returns>
        public static bool IsSubpermission(ReadOnlySpan<char> parent, ReadOnlySpan<char> child)
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

        /// <summary>
        /// Определяет, имеет ли пользователь определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        /// <returns>Имеет ли пользователь определённое разрешение.</returns>
        public static bool HasPermission(IGuildUser user, string permission)
        {
            var service = ServiceManager.GetService<PermissionService>();

            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
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

        /// <summary>
        /// Добавляет пользователю определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        public static void AddUserPermission(IGuildUser user, string permission)
        {
            var service = ServiceManager.GetService<PermissionService>();

            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!service.Config.UserPermissions.ContainsKey(user.Id))
            {
                service.Config.UserPermissions.Add(user.Id, new HashSet<string>());
            }

            if (!service.Config.UserPermissions[user.Id].Add(permission))
            {
                throw new PermissionAlreadyExistsException(permission);
            }
        }

        /// <summary>
        /// Добавляет роли определённое разрешение.
        /// </summary>
        /// <param name="role">Роль.</param>
        /// <param name="permission">Разрешение.</param>
        public static void AddRolePermission(IRole role, string permission)
        {
            var service = ServiceManager.GetService<PermissionService>();

            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!service.Config.RolePermissions.ContainsKey(role.Id))
            {
                service.Config.RolePermissions.Add(role.Id, new HashSet<string>());
            }

            if (!service.Config.RolePermissions[role.Id].Add(permission))
            {
                throw new PermissionAlreadyExistsException(permission);
            }
        }

        /// <summary>
        /// Удаляет у пользователя определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        public static void RemoveUserPermission(IGuildUser user, string permission)
        {
            var service = ServiceManager.GetService<PermissionService>();

            if (user is null)
            {
                throw new ArgumentException(null, nameof(user), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!service.Config.UserPermissions.ContainsKey(user.Id) || !service.Config.UserPermissions[user.Id].Remove(permission))
            {
                throw new PermissionNotExistsException(permission);
            }

            if (service.Config.UserPermissions[user.Id].Count == 0)
            {
                service.Config.UserPermissions.Remove(user.Id);
            }
        }

        /// <summary>
        /// Удаляет у роли определённое разрешение.
        /// </summary>
        /// <param name="role">Роль.</param>
        /// <param name="permission">Разрешение.</param>
        public static void RemoveRolePermission(IRole role, string permission)
        {
            var service = ServiceManager.GetService<PermissionService>();

            if (role is null)
            {
                throw new ArgumentException(null, nameof(role), new NullReferenceException());
            }

            if (!Permissions.IsValidPermission(permission))
            {
                throw new ArgumentException(null, nameof(permission), new InvalidPermissionException(permission));
            }

            if (!service.Config.RolePermissions.ContainsKey(role.Id) || !service.Config.RolePermissions[role.Id].Remove(permission))
            {
                throw new PermissionNotExistsException(permission);
            }

            if (service.Config.RolePermissions[role.Id].Count == 0)
            {
                service.Config.RolePermissions.Remove(role.Id);
            }
        }
    }
}