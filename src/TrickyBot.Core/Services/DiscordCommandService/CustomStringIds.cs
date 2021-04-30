// -----------------------------------------------------------------------
// <copyright file="CustomStringIds.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.DiscordCommandService
{
    /// <summary>
    /// Идентификаторы кастомных строк для <see cref="TrickyBot.Services.DiscordCommandService.DiscordCommandService"/>.
    /// </summary>
    internal static class CustomStringIds
    {
        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном изменении префикса.
        /// </summary>
        public const string PrefixChanged = "TrickyBot.Services.DiscordCommandService.PrefixChanged";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об ошибке во время изменения префикса.
        /// </summary>
        public const string InvalidPrefix = "TrickyBot.Services.DiscordCommandService.InvalidPrefix";
    }
}