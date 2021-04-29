// -----------------------------------------------------------------------
// <copyright file="RemovePermission.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
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
    internal class RemovePermission : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RemovePermission"/>.
        /// </summary>
        public RemovePermission()
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
            var guild = SSIP.Guild;
            var match = PatternMatcher.Match(parameter, TokenType.RoleMention, TokenType.Text);
            try
            {
                if (match.Success)
                {
                    Permissions.RemoveRolePermission(guild.GetRole((ulong)match.Values[0]), (string)match.Values[1]);
                }
                else
                {
                    match = PatternMatcher.Match(parameter, TokenType.UserMention, TokenType.Text);
                    Permissions.RemoveUserPermission(guild.GetUser((ulong)match.Values[0]), (string)match.Values[1]);
                }

                await message.Channel.SendMessageAsync($"{message.Author.Mention} разрешение удалено.");
            }
            catch (PermissionNotExistsException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} разрешение не существует!");
            }
            catch (ArgumentException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} неправильные аргументы!");
            }
        }
    }
}