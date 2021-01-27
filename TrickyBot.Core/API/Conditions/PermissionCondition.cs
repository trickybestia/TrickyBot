// -----------------------------------------------------------------------
// <copyright file="PermissionCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

using TrickyBot.API.Interfaces;
using TrickyBot.Services.PermissionService;

namespace TrickyBot.API.Conditions
{
    public class PermissionCondition : ICondition
    {
        public PermissionCondition(string permission)
        {
            this.Permission = permission;
        }

        public string Permission { get; }

        public bool CanExecute(IMessage message, string parameter) => PermissionService.HasPermission((IGuildUser)message.Author, this.Permission);
    }
}