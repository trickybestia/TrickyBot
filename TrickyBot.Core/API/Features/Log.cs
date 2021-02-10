// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// A class that provides API for logging.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Logs the debug message.
        /// </summary>
        /// <param name="sender">A sender of the message.</param>
        /// <param name="message">The message to log.</param>
        public static void Debug(object sender, string message) => Send(sender, message, LogLevel.Debug);

        /// <summary>
        /// Logs the info message.
        /// </summary>
        /// <param name="sender">A sender of the message.</param>
        /// <param name="message">The message to log.</param>
        public static void Info(object sender, string message) => Send(sender, message, LogLevel.Info);

        /// <summary>
        /// Logs the warn message.
        /// </summary>
        /// <param name="sender">A sender of the message.</param>
        /// <param name="message">The message to log.</param>
        public static void Warn(object sender, string message) => Send(sender, message, LogLevel.Warn);

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="sender">A sender of the message.</param>
        /// <param name="message">The message to log.</param>
        public static void Error(object sender, string message) => Send(sender, message, LogLevel.Error);

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="sender">A sender of the message.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="logLevel"><see cref="LogLevel"/> of the message.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Send(object sender, string message, LogLevel logLevel)
        {
            if (sender is null)
            {
                throw new ArgumentException("Value can not be null!", nameof(sender), new NullReferenceException());
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("String can not be null or empty!", nameof(message));
            }

            var previousColor = Console.ForegroundColor;
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            Console.ForegroundColor = logLevel switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            {
                LogLevel.Debug => ConsoleColor.Green,
                LogLevel.Info => ConsoleColor.Cyan,
                LogLevel.Warn => ConsoleColor.Magenta,
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

            Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{logLevel.ToString().ToUpper()}] [{senderName}] {message}");
            Console.ForegroundColor = previousColor;
        }
    }
}