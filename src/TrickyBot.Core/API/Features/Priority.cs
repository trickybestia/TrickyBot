// -----------------------------------------------------------------------
// <copyright file="Priority.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Представляет приоритет чего-либо.
    /// </summary>
    public struct Priority : IComparable<Priority>
    {
        /// <summary>
        /// Значение приоритета. Чем оно выше, тем раньше операция будет выполнена.
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Инициализирует новый экземпляр структуры <see cref="Priority"/>.
        /// </summary>
        /// <param name="value">Значение приоритета.</param>
        public Priority(int value)
        {
            this.Value = value;
        }

        /// <inheritdoc/>
        public int CompareTo(Priority other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}