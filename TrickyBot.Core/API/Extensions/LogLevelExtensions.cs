// -----------------------------------------------------------------------
// <copyright file="LogLevelExtensions.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using TrickyBot.API.Features;

namespace TrickyBot.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="LogLevel"/>.
    /// </summary>
    public static class LogLevelExtensions
    {
        /// <summary>
        /// Возвращает русскоязычное представление <see cref="LogLevel"/>.
        /// </summary>
        /// <param name="logLevel">Значение <see cref="LogLevel"/>.</param>
        /// <returns>Русскоязычное представление <see cref="LogLevel"/>.</returns>
        public static string ToLocalString(this LogLevel logLevel)
        {
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            return logLevel switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            {
                LogLevel.Debug => "Отладка",
                LogLevel.Info => "Информация",
                LogLevel.Warn => "Предупреждение",
                LogLevel.Error => "Ошибка",
            };
        }
    }
}