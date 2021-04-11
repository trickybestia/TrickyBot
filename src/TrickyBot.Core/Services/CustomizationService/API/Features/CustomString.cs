// -----------------------------------------------------------------------
// <copyright file="CustomString.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.CustomizationService.API.Features
{
    /// <summary>
    /// Изменяемая пользователем строка.
    /// </summary>
    public class CustomString
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CustomString"/>.
        /// </summary>
        /// <param name="id">Идентификатор строки.</param>
        public CustomString(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Получает идентификатор строки.
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
            foreach (var customizationTable in Customization.CustomizationTables)
            {
                if (customizationTable.Strings.TryGetValue(this.Id, out string value))
                {
                    return value;
                }
            }

            return this.Id;
        }
    }
}