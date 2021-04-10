// -----------------------------------------------------------------------
// <copyright file="DiscordCommandPermissionCondition.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.PermissionService.API.Features;

namespace TrickyBot.Services.DiscordCommandService.API.Features.Conditions
{
    /// <summary>
    /// Условие, проверяющее наличие определённого разрешения у пользователя.
    /// </summary>
    public class DiscordCommandPermissionCondition : IDiscordCommandCondition
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DiscordCommandPermissionCondition"/>.
        /// </summary>
        /// <param name="permission">Название разрешения.</param>
        public DiscordCommandPermissionCondition(string permission)
        {
            this.Permission = permission;
        }

        /// <summary>
        /// Получает название разрешения.
        /// </summary>
        public string Permission { get; }

        /// <inheritdoc/>
        public bool CanExecute(IMessage message, string parameter) => Permissions.HasPermission((IGuildUser)message.Author, this.Permission);
    }
}