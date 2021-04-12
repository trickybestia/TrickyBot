// -----------------------------------------------------------------------
// <copyright file="DiscordCommands.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Discord;
using TrickyBot.API.Features;

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// API для <see cref="TrickyBot.Services.DiscordCommandService.DiscordCommandService"/>.
    /// </summary>
    public static class DiscordCommands
    {
        /// <summary>
        /// Получает или задает префикс дискорд-команд.
        /// </summary>
        public static string CommandPrefix
        {
            get
            {
                return ServiceManager.GetService<DiscordCommandService>().Config.CommandPrefix;
            }

            set
            {
                if (string.IsNullOrEmpty(value)
                || char.IsWhiteSpace(value[0])
                || char.IsWhiteSpace(value[^1])
                || value.Contains('\r')
                || value.Contains('\n'))
                {
                    throw new ArgumentException("Строка не должна быть пустой, начинаться или заканчиваться пробелом, содержать символы '\\r' или '\\n'.");
                }

                ServiceManager.GetService<DiscordCommandService>().Config.CommandPrefix = value;
            }
        }

        /// <summary>
        /// Определяет, является ли <paramref name="message"/> командой.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Значение, указывающее, является ли <paramref name="message"/> командой.</returns>
        internal static bool IsCommand(IMessage message)
        {
            var service = ServiceManager.GetService<DiscordCommandService>();

            if (message.Channel is IDMChannel && !service.Config.AllowDMCommands)
            {
                return false;
            }

            return message is IUserMessage && !message.Author.IsBot && message.Content.StartsWith(service.Config.CommandPrefix);
        }
    }
}