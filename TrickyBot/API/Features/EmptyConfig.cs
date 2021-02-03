// -----------------------------------------------------------------------
// <copyright file="EmptyConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    public class EmptyConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}