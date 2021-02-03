// -----------------------------------------------------------------------
// <copyright file="Condition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.API.Features
{
    public delegate bool Condition(IMessage message, string parameter);
}