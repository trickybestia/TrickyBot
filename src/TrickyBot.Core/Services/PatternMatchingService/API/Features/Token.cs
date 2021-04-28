// -----------------------------------------------------------------------
// <copyright file="Token.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Токен, полученный в результате анализа лексем.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Token"/>.
        /// </summary>
        /// <param name="type">Тип токена.</param>
        /// <param name="value">Значение токена.</param>
        public Token(TokenType type, object value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Получает тип токена.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// Получает значение токена.
        /// </summary>
        public object Value { get; }
    }
}