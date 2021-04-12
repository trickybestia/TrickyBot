// -----------------------------------------------------------------------
// <copyright file="UnhandledExceptionHandler.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Обработчик необработанных исключений.
    /// </summary>
    internal static class UnhandledExceptionHandler
    {
        /// <summary>
        /// Инициализирует статические поля класса <see cref="UnhandledExceptionHandler"/>.
        /// </summary>
        static UnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        /// <summary>
        /// Метод-заглушка, необходимый для вызова статического конструктора.
        /// </summary>
        public static void Init()
        {
        }

        /// <summary>
        /// Обработчик события необработанного исключения.
        /// </summary>
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(typeof(UnhandledExceptionHandler), $"Необработанное исключение (terminating: {e.IsTerminating}):\n{e.ExceptionObject}");
            if (e.IsTerminating)
            {
                Environment.ExitCode = 1;
            }
        }
    }
}