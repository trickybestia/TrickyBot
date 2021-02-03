// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;

namespace TrickyBot.API.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        Task ExecuteAsync(IMessage message, string parameter);
    }
}