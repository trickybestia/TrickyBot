// -----------------------------------------------------------------------
// <copyright file="IConfig.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Interfaces
{
    public interface IConfig
    {
        bool IsEnabled { get; set; }
    }
}