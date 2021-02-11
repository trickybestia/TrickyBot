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
    /// A class that provides API for the <see cref="PermissionService"/>.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// Validates permission.
        /// </summary>
        /// <param name="permission">Permission to validate.</param>
        /// <returns>Whether or not permission is valid.</returns>
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
        /// Determines whether or not user has a permission.
        /// </summary>
        /// <param name="user">A user.</param>
        /// <param name="permission">A permission.</param>
        /// <returns>Whether or not user has a permission.</returns>
        public static bool HasPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            return service.HasPermission(user, permission);
        }

        /// <summary>
        /// Adds specified permission to user.
        /// </summary>
        /// <param name="user">A user.</param>
        /// <param name="permission">A permission.</param>
        public static void AddUserPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.AddUserPermission(user, permission);
        }

        /// <summary>
        /// Adds specified permission to role.
        /// </summary>
        /// <param name="role">A role.</param>
        /// <param name="permission">A permission.</param>
        public static void AddRolePermission(IRole role, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.AddRolePermission(role, permission);
        }

        /// <summary>
        /// Removes specified permission from user.
        /// </summary>
        /// <param name="user">A user.</param>
        /// <param name="permission">A permission.</param>
        public static void RemoveUserPermission(IGuildUser user, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.RemoveUserPermission(user, permission);
        }

        /// <summary>
        /// Removes specified permission from role.
        /// </summary>
        /// <param name="role">A role.</param>
        /// <param name="permission">A permission.</param>
        public static void RemoveRolePermission(IRole role, string permission)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            service.RemoveRolePermission(role, permission);
        }
    }
}