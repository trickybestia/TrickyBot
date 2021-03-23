﻿// -----------------------------------------------------------------------
// <copyright file="AlwaysEnabledConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Конфиг всегда включённого сервиса.
    /// </summary>
    public class AlwaysEnabledConfig : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled
        {
            get { return true; }
            set { }
        }
    }
}