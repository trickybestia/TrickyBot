// -----------------------------------------------------------------------
// <copyright file="DiscordCommandCondition.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Discord;

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// Делегат, представляющий условие.
    /// </summary>
    /// <param name="message">Сообщение, которое вызвало команду.</param>
    /// <param name="parameter">Параметр команды.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public delegate bool DiscordCommandCondition(IMessage message, string parameter);
}