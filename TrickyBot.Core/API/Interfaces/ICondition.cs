// -----------------------------------------------------------------------
// <copyright file="ICondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.API.Interfaces
{
    public interface ICondition
    {
        bool CanExecute(IMessage message, string parameter);
    }
}