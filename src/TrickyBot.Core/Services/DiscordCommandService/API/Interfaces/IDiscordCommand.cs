// -----------------------------------------------------------------------
// <copyright file="IDiscordCommand.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Discord;
using TrickyBot.Services.DiscordCommandService.API.Features;

namespace TrickyBot.Services.DiscordCommandService.API.Interfaces
{
    /// <summary>
    /// Дискорд-команда.
    /// </summary>
    public interface IDiscordCommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Получает способ выполнения команды.
        /// </summary>
        DiscordCommandRunMode RunMode { get; }

        /// <summary>
        /// Асинхронно выполняет команду.
        /// </summary>
        /// <param name="message">Сообщение, которое вызвало команду.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        Task ExecuteAsync(IMessage message, string parameter);
    }
}