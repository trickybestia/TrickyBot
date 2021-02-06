// -----------------------------------------------------------------------
// <copyright file="CommandLoader.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// A class that provides API for loading commands from assembly.
    /// </summary>
    public static class CommandLoader
    {
        /// <summary>
        /// Searchs for classes which implements <see cref="ICommand"/> interface in the provided <see cref="Assembly"/> and instantiates them.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> which contains commands.</param>
        /// <returns>The <see cref="List{T}"/> containing loaded commands.</returns>
        public static List<ICommand> GetCommands(Assembly assembly)
        {
            var commands = new List<ICommand>();
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsAssignableTo(typeof(ICommand)))
                {
                    var constructor = type.GetConstructor(Array.Empty<Type>());
                    commands.Add((ICommand)constructor.Invoke(null));
                }
            }

            return commands;
        }
    }
}