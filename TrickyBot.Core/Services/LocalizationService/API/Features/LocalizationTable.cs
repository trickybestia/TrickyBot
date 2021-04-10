// -----------------------------------------------------------------------
// <copyright file="LocalizationTable.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TrickyBot.Services.LocalizationService.API.Features
{
    /// <summary>
    /// Класс, содержащий сведения о таблице локализации.
    /// </summary>
    public class LocalizationTable
    {
        private LocalizationTable(string language, IReadOnlyDictionary<string, string> strings)
        {
            this.Language = language;
            this.Strings = strings;
        }

        /// <summary>
        /// Получает язык таблицы локализации.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Получает словарь, где ключ - id локализованной строки, значение - текстовое представление локализованной строки.
        /// </summary>
        public IReadOnlyDictionary<string, string> Strings { get; }

        /// <summary>
        /// Парсит текстовый поток в таблицу локализации.
        /// </summary>
        /// <param name="stream">Поток, содержащий текстовые данные.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public static async Task<LocalizationTable> FromStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var language = (await reader.ReadLineAsync()).Split('=', 2)[1];
            var strings = new Dictionary<string, string>();
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var splittedLine = line.Split('=');
                strings.Add(splittedLine[0], splittedLine[1]);
            }

            return new LocalizationTable(language, strings);
        }
    }
}