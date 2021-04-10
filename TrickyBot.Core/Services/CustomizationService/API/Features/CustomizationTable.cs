// -----------------------------------------------------------------------
// <copyright file="CustomizationTable.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TrickyBot.Services.CustomizationService.API.Features
{
    /// <summary>
    /// Класс, содержащий сведения о таблице кастомизации.
    /// </summary>
    public class CustomizationTable
    {
        private CustomizationTable(IReadOnlyDictionary<string, string> strings)
        {
            this.Strings = strings;
        }

        /// <summary>
        /// Получает словарь, где ключ - id кастомной строки, значение - текстовое представление этой строки.
        /// </summary>
        public IReadOnlyDictionary<string, string> Strings { get; }

        /// <summary>
        /// Парсит текстовый поток в таблицу кастомизации.
        /// </summary>
        /// <param name="stream">Поток, содержащий текстовые данные.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public static async Task<CustomizationTable> FromStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var strings = new Dictionary<string, string>();
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var splittedLine = line.Split('=');
                strings.Add(splittedLine[0], splittedLine[1]);
            }

            return new CustomizationTable(strings);
        }
    }
}