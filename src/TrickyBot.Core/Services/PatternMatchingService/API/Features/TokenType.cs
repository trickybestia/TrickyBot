// -----------------------------------------------------------------------
// <copyright file="TokenType.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Перечисление, содержащее все типы токенов.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Тип токена, содержащего строковый литерал.
        /// </summary>
        Text,

        /// <summary>
        /// Тип токена, содержащего id (<see cref="ulong"/>) упомянутого пользователя.
        /// </summary>
        UserMention,

        /// <summary>
        /// Тип токена, содержащего id (<see cref="ulong"/>) упомянутой роли.
        /// </summary>
        RoleMention,

        /// <summary>
        /// Тип токена, содержащего id (<see cref="ulong"/>) упомянутого канала.
        /// </summary>
        ChannelMention,

        /// <summary>
        /// Тип токена, содержащего целое знаковое 64-битное число (<see cref="long"/>).
        /// </summary>
        Int64,

        /// <summary>
        /// Тип токена, содержащего цвет (<see cref="Discord.Color"/>), заданный в шестнадцатеричной системе счисления.
        /// </summary>
        Color,
    }
}