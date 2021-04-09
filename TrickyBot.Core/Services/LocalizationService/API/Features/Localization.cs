// -----------------------------------------------------------------------
// <copyright file="Localization.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Features;

namespace TrickyBot.Services.LocalizationService.API.Features
{
    /// <summary>
    /// API для <see cref="TrickyBot.Services.LocalizationService.LocalizationService"/>.
    /// </summary>
    public static class Localization
    {
        /// <inheritdoc cref="TrickyBot.Services.LocalizationService.LocalizationService.LocalizationTables"/>
        public static IReadOnlyList<LocalizationTable> LocalizationTables => ServiceManager.GetService<LocalizationService>().LocalizationTables;

        /// <inheritdoc cref="TrickyBot.Services.LocalizationService.LocalizationServiceConfig.Localizations"/>
        public static List<string> Localizations => ServiceManager.GetService<LocalizationService>().Config.Localizations;
    }
}