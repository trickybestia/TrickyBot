// -----------------------------------------------------------------------
// <copyright file="Paths.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Предоставляет доступ к набору директорий бота.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Получает путь к директории, в которой находится исполняемый файл бота.
        /// </summary>
        public static string BotCore { get; internal set; }

        /// <summary>
        /// Получает путь к директории данных бота.
        /// </summary>
        public static string Data { get; internal set; }

        /// <summary>
        /// Получает путь к директории, которая содержит в себе библиотеки сервисов.
        /// </summary>
        public static string Services => Path.Combine(Data, "Services");

        /// <summary>
        /// Получает путь к директории, которая содержит в себе конфигурационные файлы.
        /// </summary>
        public static string Configs => Path.Combine(Data, "Configs");

        /// <summary>
        /// Создаёт все директории, если они не существуют.
        /// </summary>
        internal static void Init()
        {
            Directory.CreateDirectory(BotCore);
            Directory.CreateDirectory(Data);
            Directory.CreateDirectory(Configs);
            Directory.CreateDirectory(Services);
        }
    }
}