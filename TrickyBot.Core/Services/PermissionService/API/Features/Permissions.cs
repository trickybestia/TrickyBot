// -----------------------------------------------------------------------
// <copyright file="Permissions.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Discord;

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
        /// Определяет, имеет ли пользователь определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        /// <returns>Имеет ли пользователь определённое разрешение.</returns>
        public static bool HasPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            return service.HasPermission(user, permission);
        }

        /// <summary>
        /// Добавляет пользователю определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        public static void AddUserPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.AddUserPermission(user, permission);
        }

        /// <summary>
        /// Добавляет роли определённое разрешение.
        /// </summary>
        /// <param name="role">Роль.</param>
        /// <param name="permission">Разрешение.</param>
        public static void AddRolePermission(IRole role, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.AddRolePermission(role, permission);
        }

        /// <summary>
        /// Удаляет у пользователя определённое разрешение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="permission">Разрешение.</param>
        public static void RemoveUserPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.RemoveUserPermission(user, permission);
        }

        /// <summary>
        /// Удаляет у роли определённое разрешение.
        /// </summary>
        /// <param name="role">Роль.</param>
        /// <param name="permission">Разрешение.</param>
        public static void RemoveRolePermission(IRole role, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.RemoveRolePermission(role, permission);
        }
    }
}