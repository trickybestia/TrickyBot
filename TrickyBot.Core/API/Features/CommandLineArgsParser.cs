// -----------------------------------------------------------------------
// <copyright file="CommandLineArgsParser.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// A class that provides API for parsing command line args.
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
                Log.Error(typeof(CommandLineArgsParser), $"Exception throw while parsing command line args: {ex}");
                Environment.Exit(1);
            }

            Args = parsedArgs;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> of parsed command line args.
        /// </summary>
        public static IReadOnlyDictionary<string, string> Args { get; }
    }
}