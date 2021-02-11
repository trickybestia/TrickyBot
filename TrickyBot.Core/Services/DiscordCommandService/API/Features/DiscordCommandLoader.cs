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
    /// A class that provides API for loading commands from assembly.
    /// </summary>
    public static class DiscordCommandLoader
    {
        /// <summary>
        /// Searchs for classes which implements <see cref="IDiscordCommand"/> interface in the provided <see cref="Assembly"/> and instantiates them.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> which contains commands.</param>
        /// <returns>The <see cref="List{T}"/> containing loaded commands.</returns>
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