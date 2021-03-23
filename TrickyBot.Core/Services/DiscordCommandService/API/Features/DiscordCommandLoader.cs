// -----------------------------------------------------------------------
// <copyright file="DiscordCommandLoader.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using TrickyBot.Services.DiscordCommandService.API.Interfaces;

namespace TrickyBot.Services.DiscordCommandService.API.Features
{
    /// <summary>
    /// API для загрузки дискорд-команд через рефлексию.
    /// </summary>
    public static class DiscordCommandLoader
    {
        /// <summary>
        /// Создаёт экземпляры классов, реализующих интерфейс <see cref="IDiscordCommand"/>, и находящихся в определённой сборке.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая классы загружаемых команд.</param>
        /// <returns>Список экземпляров загруженных команд.</returns>
        public static List<IDiscordCommand> GetCommands(Assembly assembly)
        {
            var commands = new List<IDiscordCommand>();
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsAssignableTo(typeof(IDiscordCommand)))
                {
                    var constructor = type.GetConstructor(Array.Empty<Type>());
                    commands.Add((IDiscordCommand)constructor.Invoke(null));
                }
            }

            return commands;
        }
    }
}