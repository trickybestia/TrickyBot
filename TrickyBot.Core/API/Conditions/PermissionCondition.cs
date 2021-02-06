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
    /// <summary>
    /// A condition that can be used for permission check.
    /// </summary>
    public class PermissionCondition : ICondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionCondition"/> class.
        /// </summary>
        /// <param name="permission">The name of permission.</param>
        public PermissionCondition(string permission)
        {
            this.Permission = permission;
        }

        /// <summary>
        /// Gets the name of permission.
        /// </summary>
        public string Permission { get; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => PermissionService.HasPermission((IGuildUser)message.Author, this.Permission);
    }
}