// -----------------------------------------------------------------------
// <copyright file="EmptyConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Пустой конфиг сервиса.
    /// </summary>
    public class EmptyConfig : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;
    }
}