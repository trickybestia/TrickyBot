// -----------------------------------------------------------------------
// <copyright file="AddPermission.cs" company="The TrickyBot Team">
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
    /// Команда добавления разрешения.
    /// </summary>
    internal class AddPermission : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddPermission"/>.
        /// </summary>
        public AddPermission()
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
            var guild = SSIP.Guild;
            var match = PatternMatcher.Match(parameter, TokenType.RoleMention, TokenType.Text);
            try
            {
                if (match.Success)
                {
                    Permissions.AddRolePermission(guild.GetRole((ulong)match.Values[0]), (string)match.Values[1]);
                }
                else
                {
                    match = PatternMatcher.Match(parameter, TokenType.UserMention, TokenType.Text);
                    Permissions.AddUserPermission(guild.GetUser((ulong)match.Values[0]), (string)match.Values[1]);
                }

                await message.Channel.SendMessageAsync($"{message.Author.Mention} разрешение добавлено.");
            }
            catch (PermissionAlreadyExistsException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} разрешение уже существует!");
            }
            catch (ArgumentException)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention} неправильные аргументы!");
            }
        }
    }
}