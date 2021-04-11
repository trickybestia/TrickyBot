// -----------------------------------------------------------------------
// <copyright file="SingleServerInfoProviderServiceConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Features;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    /// <summary>
    /// Конфиг <see cref="TrickyBot.Services.SingleServerInfoProviderService.SingleServerInfoProviderService"/>.
    /// </summary>
    public class SingleServerInfoProviderServiceConfig : AlwaysEnabledConfig
    {
        /// <summary>
        /// Получает или задает Id "привязанного" к боту сервера.
        /// </summary>
        public ulong GuildId { get; set; }
    }
}