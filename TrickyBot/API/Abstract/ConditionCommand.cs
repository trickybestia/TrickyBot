// -----------------------------------------------------------------------
// <copyright file="ConditionCommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Abstract
{
    public abstract class ConditionCommand : ICommand
    {
        public abstract string Name { get; }

        public List<ICondition> Conditions { get; } = new List<ICondition>();

        public Task ExecuteAsync(IMessage message, string parameter)
        {
            foreach (var condition in this.Conditions)
            {
                if (!condition.CanExecute(message, parameter))
                {
                    return Task.CompletedTask;
                }
            }

            return this.Execute(message, parameter);
        }

        protected abstract Task Execute(IMessage message, string parameter);
    }
}