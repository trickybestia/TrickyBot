using System;
using System.Collections.Generic;
using System.Reflection;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    public static class CommandLoader
    {
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