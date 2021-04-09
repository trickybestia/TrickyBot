// -----------------------------------------------------------------------
// <copyright file="LocalizedString.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace TrickyBot.Services.LocalizationService.API.Features
{
    /// <summary>
    /// Локализованная строка.
    /// </summary>
    public class LocalizedString
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LocalizedString"/>.
        /// </summary>
        /// <param name="id">Идентификатор строки.</param>
        public LocalizedString(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Получает идентификатор строки в виде "ServiceName.SomeText".
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Форматирует текущую строку.
        /// </summary>
        /// <param name="keywords">Список кортежей ключ-значение, значения которого будут использоваться при форматировании.</param>
        /// <returns>Отформатированная строка.</returns>
        public string Format(params (string key, object value)[] keywords)
        {
            var value = this.ToString();
            foreach (var keyword in keywords)
            {
                value = value.Replace($"{{{keyword.key}}}", keyword.value.ToString());
            }

            return value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            foreach (var localization in Localization.Localizations)
            {
                var localizationTable = Localization.LocalizationTables.FirstOrDefault(table => table.Language == localization);
                if (localizationTable is not null && localizationTable.Strings.TryGetValue(this.Id, out string value))
                {
                    return value;
                }
            }

            return this.Id;
        }
    }
}