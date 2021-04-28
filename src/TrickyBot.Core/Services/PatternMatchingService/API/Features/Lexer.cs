// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Анализатор, представляющий строку в виде списка лексем.
    /// </summary>
    public static class Lexer
    {
        private static readonly List<char> NewLineSymbols = new List<char> { '\r', '\n' };

        /// <summary>
        /// Представляет строку в виде списка лексем.
        /// </summary>
        /// <param name="text">Строка, которую необходимо разобрать.</param>
        /// <returns>Список лексем, полученный при разборе строки.</returns>
        public static List<Lexeme> Analyze(string text)
        {
            var lexemes = new List<Lexeme>();
            StringBuilder currentLexeme = new StringBuilder();
            LexemeType? currentLexemeType = null;
            bool isEscaped = false;

            void TryFlushCurrentLexeme()
            {
                if (currentLexemeType != null)
                {
                    lexemes.Add(new Lexeme(currentLexemeType.Value, currentLexeme.ToString()));
                    currentLexeme.Clear();
                }
            }

            void UpdateCurrentLexeme(LexemeType type, char appendedSymbol)
            {
                if (currentLexemeType != type)
                {
                    TryFlushCurrentLexeme();
                    currentLexemeType = type;
                }

                currentLexeme.Append(appendedSymbol);
            }

            foreach (var symbol in text)
            {
                if (isEscaped)
                {
                    currentLexeme.Append(symbol);
                }
                else if (symbol == '\\')
                {
                    isEscaped = true;
                    continue;
                }
                else if (currentLexemeType == LexemeType.String && symbol == '"')
                {
                    TryFlushCurrentLexeme();
                    currentLexemeType = null;
                }
                else if (symbol == '"')
                {
                    TryFlushCurrentLexeme();
                    currentLexemeType = LexemeType.String;
                }
                else if (currentLexemeType == LexemeType.String)
                {
                    currentLexeme.Append(symbol);
                }
                else if (char.IsWhiteSpace(symbol))
                {
                    UpdateCurrentLexeme(LexemeType.Whitespace, symbol);
                }
                else if (NewLineSymbols.Contains(symbol))
                {
                    UpdateCurrentLexeme(LexemeType.LineBreak, symbol);
                }
                else
                {
                    UpdateCurrentLexeme(LexemeType.Word, symbol);
                }

                isEscaped = false;
            }

            TryFlushCurrentLexeme();

            return lexemes;
        }
    }
}