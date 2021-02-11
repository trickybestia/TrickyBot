// -----------------------------------------------------------------------
// <copyright file="IConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Interfaces
{
    /// <summary>
    /// A service config.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether a service is enabled.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}