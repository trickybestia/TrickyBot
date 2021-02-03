// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrickyBot.API.Interfaces
{
    public interface IService<out TConfig>
        where TConfig : IConfig
    {
        List<ICommand> Commands { get; }

        TConfig Config { get; }

        string Name { get; }

        string Author { get; }

        Version Version { get; }

        Task StartAsync();

        Task StopAsync();
    }
}