// -----------------------------------------------------------------------
// <copyright file="Analyze.cs" company="The TrickyBot Team">
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
    /// Команда, разбирающая параметр на набр лексем.
    /// </summary>
    internal class Analyze : IConsoleCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "analyze";

        /// <inheritdoc/>
        public ConsoleCommandRunMode RunMode => ConsoleCommandRunMode.Sync;

        /// <inheritdoc/>
        public Task ExecuteAsync(string parameter)
        {
            foreach (var lexeme in Lexer.Analyze(parameter))
            {
                Log.Debug(this, $"[{lexeme.Type}] \"{lexeme.Value}\"");
            }

            return Task.CompletedTask;
        }
    }
}