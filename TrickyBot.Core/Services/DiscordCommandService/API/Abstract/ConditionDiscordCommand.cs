// -----------------------------------------------------------------------
// <copyright file="ConditionDiscordCommand.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Features;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.DiscordCommandService.API.Abstract
{
    /// <summary>
    /// A command that executes when all conditions are successfully checked.
    /// </summary>
    public abstract class ConditionDiscordCommand : IDiscordCommand
    {
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract DiscordCommandRunMode RunMode { get; }

        /// <summary>
        /// Gets a list of conditions associated with this command.
        /// </summary>
        public List<IDiscordCommandCondition> Conditions { get; } = new List<IDiscordCommandCondition>();

        /// <inheritdoc/>
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

        /// <summary>
        /// A method that executes when all conditions are successfully checked.
        /// </summary>
        /// <param name="message">The message that invoked the command.</param>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>A task that represents the asynchronous execution operation.</returns>
        protected abstract Task Execute(IMessage message, string parameter);
    }
}