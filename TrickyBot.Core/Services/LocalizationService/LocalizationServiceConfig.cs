// -----------------------------------------------------------------------
// <copyright file="LocalizationServiceConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Features;

namespace TrickyBot.Services.LocalizationService
{
    /// <summary>
    /// Конфиг <see cref="TrickyBot.Services.LocalizationService.LocalizationService"/>.
    /// </summary>
    public class LocalizationServiceConfig : AlwaysEnabledConfig
    {
        /// <summary>
        /// Получает или задает список используемых ботом локализаций, отсортированных по убыванию приоритета.
        /// </summary>
        public List<string> Localizations { get; set; } = new List<string>() { "russian" };
    }
}