// -----------------------------------------------------------------------
// <copyright file="PermissionServiceConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Features;

namespace TrickyBot.Services.PermissionService
{
    public class PermissionServiceConfig : AlwaysEnabledConfig
    {
        public Dictionary<ulong, HashSet<string>> UserPermissions { get; set; } = new Dictionary<ulong, HashSet<string>>();

        public Dictionary<ulong, HashSet<string>> RolePermissions { get; set; } = new Dictionary<ulong, HashSet<string>>();
    }
}