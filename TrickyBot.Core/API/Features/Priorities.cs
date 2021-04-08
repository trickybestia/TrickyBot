// -----------------------------------------------------------------------
// <copyright file="Priorities.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Набор стандартных приоритетов.
    /// </summary>
    public static class Priorities
    {
        /// <summary>
        /// Приоритет <see cref="TrickyBot.Services.BotService.BotService"/>.
        /// </summary>
        public static readonly Priority BotService = new Priority(10000);

        /// <summary>
        /// Приоритет встроенного сервиса.
        /// </summary>
        public static readonly Priority CoreService = new Priority(1000);

        /// <summary>
        /// Приоритет загруженного сервиса.
        /// </summary>
        public static readonly Priority DynamicService = new Priority(0);
    }
}