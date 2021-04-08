// -----------------------------------------------------------------------
// <copyright file="BotServiceConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;
using TrickyBot.API.Features;

namespace TrickyBot.Services.BotService
{
    /// <summary>
    /// Конфиг <see cref="TrickyBot.Services.BotService.BotService"/>.
    /// </summary>
    public class BotServiceConfig : AlwaysEnabledConfig
    {
        /// <summary>
        /// Получает или задает тип токена, используемого для аутентификации.
        /// </summary>
        public TokenType TokenType { get; set; } = TokenType.Bot;

        /// <summary>
        /// Получает или задает токен, используемый для аутентификации.
        /// </summary>
        public string Token { get; set; } = "XXXXXXXXXXXXXXXXXXXXXXXX.XXXXXX.XXXXXXXXXXXXXXXXXXXXXXXXXXX";
    }
}