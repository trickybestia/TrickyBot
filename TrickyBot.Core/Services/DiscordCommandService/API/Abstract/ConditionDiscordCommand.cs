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
    /// Команда, которая выполняется при успешном выполнении всех условий.
    /// </summary>
    public abstract class ConditionDiscordCommand : IDiscordCommand
    {
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract DiscordCommandRunMode RunMode { get; }

        /// <summary>
        /// Получает список условий, необходимых для выполнения команды.
        /// </summary>
        public IList<IDiscordCommandCondition> Conditions { get; } = new List<IDiscordCommandCondition>();

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
        /// Метод, вызываемый при выполнении команды после успешной проверки всех условий.
        /// </summary>
        /// <param name="message">Сообщение, которое вызвало команду.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        protected abstract Task Execute(IMessage message, string parameter);
    }
}