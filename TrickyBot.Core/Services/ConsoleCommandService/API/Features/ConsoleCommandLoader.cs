// -----------------------------------------------------------------------
// <copyright file="ConsoleCommandLoader.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using TrickyBot.Services.ConsoleCommandService.API.Interfaces;

namespace TrickyBot.Services.ConsoleCommandService.API.Features
{
    /// <summary>
    /// API для загрузки консольных серверных команд через рефлексию.
    /// </summary>
    public static class ConsoleCommandLoader
    {
        /// <summary>
        /// Создаёт экземпляры классов, реализующих интерфейс <see cref="IConsoleCommand"/>, и находящихся в определённой сборке.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая классы загружаемых команд.</param>
        /// <returns>Список экземпляров загруженных команд.</returns>
        public static List<IConsoleCommand> GetCommands(Assembly assembly)
        {
            var commands = new List<IConsoleCommand>();
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsAssignableTo(typeof(IConsoleCommand)))
                {
                    var constructor = type.GetConstructor(Array.Empty<Type>());
                    commands.Add((IConsoleCommand)constructor.Invoke(null));
                }
            }

            return commands;
        }
    }
}