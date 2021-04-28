// -----------------------------------------------------------------------
// <copyright file="Parser.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Discord;

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Анализатор, представляющий список лексем в виде списка токенов.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Представляет список лексем в виде списка токенов.
        /// </summary>
        /// <param name="lexemes">Список лексем, который необходимо разобрать.</param>
        /// <returns>Список токенов, полученный при разборе списка лексем.</returns>
        public static List<Token> Parse(IEnumerable<Lexeme> lexemes)
        {
            var tokens = new List<Token>();
            Match match;

            void AddToken(TokenType type, object value)
            {
                tokens.Add(new Token(type, value));
            }

            foreach (var lexeme in lexemes)
            {
                if (lexeme.Type is LexemeType.Whitespace or LexemeType.LineBreak)
                {
                    continue;
                }
                else if (lexeme.Type == LexemeType.String)
                {
                    AddToken(TokenType.Text, lexeme.Value);
                }
                else if ((match = Regex.Match(lexeme.Value, @"#([0-9a-fA-F]{6})")).Success)
                {
                    var hex = match.Result("$1");
                    var red = Convert.ToByte(hex[0..2], 16);
                    var green = Convert.ToByte(hex[2..4], 16);
                    var blue = Convert.ToByte(hex[4..6], 16);

                    AddToken(TokenType.Color, new Color(red, green, blue));
                }
                else if ((match = Regex.Match(lexeme.Value, @"<@!?(\d+)>")).Success)
                {
                    AddToken(TokenType.UserMention, ulong.Parse(match.Result("$1")));
                }
                else if ((match = Regex.Match(lexeme.Value, @"<@&(\d+)>")).Success)
                {
                    AddToken(TokenType.RoleMention, ulong.Parse(match.Result("$1")));
                }
                else if ((match = Regex.Match(lexeme.Value, @"<#(\d+)>")).Success)
                {
                    AddToken(TokenType.ChannelMention, ulong.Parse(match.Result("$1")));
                }
                else if ((match = Regex.Match(lexeme.Value, @"(\d+)")).Success)
                {
                    AddToken(TokenType.Int64, long.Parse(match.Result("$1")));
                }
                else
                {
                    AddToken(TokenType.Text, lexeme.Value);
                }
            }

            return tokens;
        }
    }
}