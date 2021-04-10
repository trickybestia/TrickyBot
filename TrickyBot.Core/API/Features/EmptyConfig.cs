// -----------------------------------------------------------------------
// <copyright file="EmptyConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
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