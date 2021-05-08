// -----------------------------------------------------------------------
// <copyright file="AddUserPermission.cs" company="The TrickyBot Team">
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
    /// Команда добавления разрешения пользователю.
    /// </summary>
    internal class AddUserPermission : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddUserPermission"/>.
        /// </summary>
        public AddUserPermission()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("permissions.add"));
        }

        /// <inheritdoc/>
        public override string Name { get; } = "permissions add";

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
                    Permissions.AddUserPermission(target, permission);

                    await message.Channel.SendMessageAsync(embed: new EmbedBuilder().WithDescription(new CustomString(CustomStringIds.UserPermissionAdded).Format(("callerMention", message.Author.Mention), ("targetMention", target.Mention), ("permission", permission))).Build());
                }
                catch (PermissionAlreadyExistsException)
                {
                    await message.Channel.SendMessageAsync(embed: new EmbedBuilder().WithDescription(new CustomString(CustomStringIds.UserPermissionAlreadyExists).Format(("callerMention", message.Author.Mention), ("targetMention", target.Mention), ("permission", permission))).Build());
                }
                catch (ArgumentException)
                {
                    await message.Channel.SendMessageAsync(embed: new EmbedBuilder().WithDescription(new CustomString(CustomStringIds.InvalidParameters).Format(("callerMention", message.Author.Mention))).Build());
                }
            }
        }
    }
}