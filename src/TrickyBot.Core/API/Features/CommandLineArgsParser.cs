// -----------------------------------------------------------------------
// <copyright file="CommandLineArgsParser.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Парсер аргументов командной строки.
    /// </summary>
    public static class CommandLineArgsParser
    {
        static CommandLineArgsParser()
        {
            var parsedArgs = new Dictionary<string, string>();
            var rawArgs = Environment.GetCommandLineArgs();
            try
            {
                for (int i = 1; i < rawArgs.Length; i++)
                {
                    int parameterNameStartIndex = 0;
                    while (rawArgs[i][parameterNameStartIndex] == '-')
                    {
                        parameterNameStartIndex++;
                    }

                    parsedArgs.Add(rawArgs[i][parameterNameStartIndex..], rawArgs[i + 1]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обработке параметров командной строки.", ex);
            }

            Args = parsedArgs;
        }

        /// <summary>
        /// Получает список спарсенных аргументов командной строки.
        /// </summary>
        public static IReadOnlyDictionary<string, string> Args { get; }
    }
}