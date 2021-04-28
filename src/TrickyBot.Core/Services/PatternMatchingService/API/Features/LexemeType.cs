// -----------------------------------------------------------------------
// <copyright file="LexemeType.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Перечисление, содержащее все типы лексем.
    /// </summary>
    public enum LexemeType
    {
        /// <summary>
        /// Тип лексемы, представляющей слово.
        /// </summary>
        Word,

        /// <summary>
        /// Тип лексемы, представляющей один или несколько пробелов.
        /// </summary>
        Whitespace,

        /// <summary>
        /// Тип лексемы, представляющей один или несколько переносов строки.
        /// </summary>
        LineBreak,

        /// <summary>
        /// Тип лексемы, представляющей набор символов, которые не должны быть подвержены синтаксическому анализу.
        /// </summary>
        String,
    }
}