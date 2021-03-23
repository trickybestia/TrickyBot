// -----------------------------------------------------------------------
// <copyright file="IDiscordCommandCondition.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.Services.DiscordCommandService.API.Interfaces
{
    /// <summary>
    /// Условие выполнения дискорд-команды.
    /// </summary>
    public interface IDiscordCommandCondition
    {
        /// <summary>
        /// Проверяет условие на истинность.
        /// </summary>
        /// <param name="message">Сообщение, которое вызвало команду.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Значение, указывающее на истинность или ложность условия.</returns>
        bool CanExecute(IMessage message, string parameter);
    }
}