// -----------------------------------------------------------------------
// <copyright file="ActionCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Conditions
{
    public class ActionCondition : ICondition
    {
        public Condition Condition { get; set; }

        public bool CanExecute(IMessage message, string parameter) => this.Condition(message, parameter);
    }
}