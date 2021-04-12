// -----------------------------------------------------------------------
// <copyright file="AlwaysEnabledConfig.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Конфиг всегда включённого сервиса.
    /// </summary>
    public class AlwaysEnabledConfig : IConfig
    {
        /// <inheritdoc/>
        [JsonIgnore]
        public bool IsEnabled
        {
            get { return true; }
            set { }
        }
    }
}