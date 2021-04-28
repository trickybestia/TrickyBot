// -----------------------------------------------------------------------
// <copyright file="PatternMatcher.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// API для <see cref="TrickyBot.Services.PatternMatchingService.PatternMatchingService"/>.
    /// </summary>
    public static class PatternMatcher
    {
        /// <summary>
        /// Проводит сопоставление параметра команды с набором типов токенов.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <param name="pattern">Массив типов токенов, представляющий паттерн.</param>
        /// <returns>Результат сопоставления.</returns>
        public static PatternMatch Match(string parameter, params TokenType[] pattern)
        {
            var lexemes = Lexer.Analyze(parameter);
            var tokens = Parser.Parse(lexemes);

            if (tokens.Count != pattern.Length)
            {
                return new PatternMatch(false, null);
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type != pattern[i])
                {
                    return new PatternMatch(false, null);
                }
            }

            return new PatternMatch(true, tokens.Select(token => token.Value).ToList());
        }
    }
}