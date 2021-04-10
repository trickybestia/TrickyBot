// -----------------------------------------------------------------------
// <copyright file="Customization.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

using TrickyBot.API.Features;

namespace TrickyBot.Services.CustomizationService.API.Features
{
    /// <summary>
    /// API для <see cref="TrickyBot.Services.CustomizationService.CustomizationService"/>.
    /// </summary>
    public static class Customization
    {
        /// <inheritdoc cref="TrickyBot.Services.CustomizationService.CustomizationService.CustomizationTables"/>
        public static IReadOnlyList<CustomizationTable> CustomizationTables => ServiceManager.GetService<CustomizationService>().CustomizationTables;
    }
}