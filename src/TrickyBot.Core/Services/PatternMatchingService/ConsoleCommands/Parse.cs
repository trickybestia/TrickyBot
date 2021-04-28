// -----------------------------------------------------------------------
// <copyright file="Parse.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using TrickyBot.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.PatternMatchingService.API.Features;

namespace TrickyBot.Services.PatternMatchingService.ConsoleCommands
{
    /// <summary>
    /// Команда, разбирающая параметр на набор токенов.
    /// </summary>
    internal class Parse : IConsoleCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "parse";

        /// <inheritdoc/>
        public ConsoleCommandRunMode RunMode => ConsoleCommandRunMode.Sync;

        /// <inheritdoc/>
        public Task ExecuteAsync(string parameter)
        {
            foreach (var token in Parser.Parse(Lexer.Analyze(parameter)))
            {
                Log.Debug(this, $"[{token.Type}] \"{token.Value}\"");
            }

            return Task.CompletedTask;
        }
    }
}