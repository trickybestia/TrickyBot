// -----------------------------------------------------------------------
// <copyright file="PermissionServiceConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Features;

namespace TrickyBot.Services.PermissionService
{
    /// <summary>
    /// Конфиг <see cref="TrickyBot.Services.PermissionService.PermissionService"/>.
    /// </summary>
    public class PermissionServiceConfig : AlwaysEnabledConfig
    {
        /// <summary>
        /// Получает или задает словарь разрешений пользователей.
        /// </summary>
        public Dictionary<ulong, HashSet<string>> UserPermissions { get; set; } = new Dictionary<ulong, HashSet<string>>();

        /// <summary>
        /// Получает или задает словарь разрешений ролей.
        /// </summary>
        public Dictionary<ulong, HashSet<string>> RolePermissions { get; set; } = new Dictionary<ulong, HashSet<string>>();
    }
}