// -----------------------------------------------------------------------
// <copyright file="Lexeme.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Лексема, полученная в процессе анализа параметра команды.
    /// </summary>
    public class Lexeme
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Lexeme"/>.
        /// </summary>
        /// <param name="type">Тип лексемы.</param>
        /// <param name="value">Строковое значение лексемы.</param>
        public Lexeme(LexemeType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Получает тип лексемы.
        /// </summary>
        public LexemeType Type { get; }

        /// <summary>
        /// Получает строковое значение лексемы.
        /// </summary>
        public string Value { get; }
    }
}