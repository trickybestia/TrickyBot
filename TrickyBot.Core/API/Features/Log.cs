// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

using TrickyBot.API.Extensions;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// API для логирования.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Логирует отладочное сообщение.
        /// </summary>
        /// <param name="sender">Объект или тип объекта, вызвавшего метод.</param>
        /// <param name="message">Сообщение.</param>
        public static void Debug(object sender, string message) => Send(sender, message, LogLevel.Debug);

        /// <summary>
        /// Логирует информационное сообещние.
        /// </summary>
        /// <param name="sender">Объект или тип объекта, вызвавшего метод.</param>
        /// <param name="message">Сообщение.</param>
        public static void Info(object sender, string message) => Send(sender, message, LogLevel.Info);

        /// <summary>
        /// Логирует предупреждающее сообщение.
        /// </summary>
        /// <param name="sender">Объект или тип объекта, вызвавшего метод.</param>
        /// <param name="message">Сообщение.</param>
        public static void Warn(object sender, string message) => Send(sender, message, LogLevel.Warn);

        /// <summary>
        /// Логирует сообщение об ошибке.
        /// </summary>
        /// <param name="sender">Объект или тип объекта, вызвавшего метод.</param>
        /// <param name="message">Сообщение.</param>
        public static void Error(object sender, string message) => Send(sender, message, LogLevel.Error);

        /// <summary>
        /// Логирует сообщение.
        /// </summary>
        /// <param name="sender">Объект или тип объекта, вызвавшего метод.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="logLevel">"Уровень" сообщения..</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Send(object sender, string message, LogLevel logLevel)
        {
            if (sender is null)
            {
                throw new ArgumentException("Значение не может быть null.", nameof(sender), new NullReferenceException());
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Строка не может быть null или пустой.", nameof(message));
            }

            var previousColor = Console.ForegroundColor;
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            Console.ForegroundColor = logLevel switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            {
                LogLevel.Debug => ConsoleColor.Green,
                LogLevel.Info => ConsoleColor.Cyan,
                LogLevel.Warn => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
            };
            string senderName;
            if (sender is Type senderType)
            {
                senderName = senderType.FullName;
            }
            else
            {
                senderName = sender.GetType().FullName;
            }

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{logLevel.ToLocalString().ToUpper()}] [{senderName}] {message}");
            Console.ForegroundColor = previousColor;
        }
    }
}