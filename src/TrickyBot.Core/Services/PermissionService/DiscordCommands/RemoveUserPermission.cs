// -----------------------------------------------------------------------
// <copyright file="RemoveUserPermission.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.CustomizationService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Abstract;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Features.Conditions;
using TrickyBot.Services.PatternMatchingService.API.Features;
using TrickyBot.Services.PermissionService.API.Exceptions;
using TrickyBot.Services.PermissionService.API.Features;
using TrickyBot.Services.SingleServerInfoProviderService.API.Features;

using TokenType = TrickyBot.Services.PatternMatchingService.API.Features.TokenType;

namespace TrickyBot.Services.PermissionService.DiscordCommands
{
    /// <summary>
    /// Команда удаления разрешения.
    /// </summary>
    internal class RemoveUserPermission : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RemoveUserPermission"/>.
        /// </summary>
        public RemoveUserPermission()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("permissions.remove"));
        }

        /// <inheritdoc/>
        public override string Name { get; } = "permissions remove";

        /// <inheritdoc/>
        public override DiscordCommandRunMode RunMode { get; } = DiscordCommandRunMode.Sync;

        /// <inheritdoc/>
        protected override async Task Execute(IMessage message, string parameter)
        {
            var match = PatternMatcher.Match(parameter, TokenType.UserMention, TokenType.Text);

            if (match.Success)
            {
                var target = SSIP.Guild.GetUser((ulong)match.Values[0]);
                var permission = (string)match.Values[1];

                try
                {
                    Permissions.RemoveUserPermission(target, permission);

                    await message.Channel.SendMessageAsync(new CustomString(CustomStringIds.UserPermissionRemoved).Format(("callerMention", message.Author.Mention), ("targetMention", target.Mention), ("permission", permission)));
                }
                catch (PermissionNotExistsException)
                {
                    await message.Channel.SendMessageAsync(new CustomString(CustomStringIds.UserPermissionNotExists).Format(("callerMention", message.Author.Mention), ("targetMention", target.Mention), ("permission", permission)));
                }
                catch (ArgumentException)
                {
                    await message.Channel.SendMessageAsync(new CustomString(CustomStringIds.InvalidParameters).Format(("callerMention", message.Author.Mention)));
                }
            }
        }
    }
}