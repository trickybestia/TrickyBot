// -----------------------------------------------------------------------
// <copyright file="SetCommandPrefix.cs" company="The TrickyBot Team">
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

namespace TrickyBot.Services.DiscordCommandService.DiscordCommands
{
    /// <summary>
    /// Команда установки префикса дискорд-бота.
    /// </summary>
    internal class SetCommandPrefix : ConditionDiscordCommand
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SetCommandPrefix"/>.
        /// </summary>
        public SetCommandPrefix()
        {
            this.Conditions.Add(new DiscordCommandPermissionCondition("commands.prefix.set"));
        }

        /// <inheritdoc/>
        public override string Name { get; } = "commands prefix set";

        /// <inheritdoc/>
        public override DiscordCommandRunMode RunMode => DiscordCommandRunMode.Sync;

        /// <inheritdoc/>
        protected override async Task Execute(IMessage message, string parameter)
        {
            try
            {
                TrickyBot.Services.DiscordCommandService.API.Features.DiscordCommands.CommandPrefix = parameter;

                await message.Channel.SendMessageAsync(embed: new EmbedBuilder().WithDescription(new CustomString(CustomStringIds.PrefixChanged).Format(("callerMention", message.Author.Mention), ("prefix", parameter))).Build());
            }
            catch (ArgumentException)
            {
                await message.Channel.SendMessageAsync(embed: new EmbedBuilder().WithDescription(new CustomString(CustomStringIds.InvalidPrefix).Format(("callerMention", message.Author.Mention), ("prefix", parameter))).Build());
            }
        }
    }
}