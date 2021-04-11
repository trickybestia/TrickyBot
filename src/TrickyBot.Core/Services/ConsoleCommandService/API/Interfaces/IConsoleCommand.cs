// -----------------------------------------------------------------------
// <copyright file="IConsoleCommand.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.Services.ConsoleCommandService.API.Features;

namespace TrickyBot.Services.ConsoleCommandService.API.Interfaces
{
    /// <summary>
    /// Серверная консольная команда.
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Получает название команды.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Получает способ выполнения команды.
        /// </summary>
        ConsoleCommandRunMode RunMode { get; }

        /// <summary>
        /// Выполняет команду асинхронно.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ExecuteAsync(string parameter);
    }
}