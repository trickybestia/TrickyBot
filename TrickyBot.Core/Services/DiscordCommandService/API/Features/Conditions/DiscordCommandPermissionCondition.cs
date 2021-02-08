// -----------------------------------------------------------------------
// <copyright file="DiscordCommandPermissionCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.PermissionService.API.Features;

namespace TrickyBot.Services.DiscordCommandService.API.Features.Conditions
{
    /// <summary>
    /// A condition that can be used for permission check.
    /// </summary>
    public class DiscordCommandPermissionCondition : IDiscordCommandCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordCommandPermissionCondition"/> class.
        /// </summary>
        /// <param name="permission">The name of permission.</param>
        public DiscordCommandPermissionCondition(string permission)
        {
            this.Permission = permission;
        }

        /// <summary>
        /// Gets the name of permission.
        /// </summary>
        public string Permission { get; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => Permissions.HasPermission((IGuildUser)message.Author, this.Permission);
    }
}