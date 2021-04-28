// -----------------------------------------------------------------------
// <copyright file="PatternMatch.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TrickyBot.Services.PatternMatchingService.API.Features
{
    /// <summary>
    /// Результат сопоставления набора токенов с паттерном.
    /// </summary>
    public class PatternMatch
    {
        private readonly IReadOnlyList<object> values;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PatternMatch"/>.
        /// </summary>
        /// <param name="success">Успешен ли результат.</param>
        /// <param name="values">Список значений токенов. Может быть <c>null</c>, если <c>success == false</c>.</param>
        public PatternMatch(bool success, IReadOnlyList<object> values = null)
        {
            this.Success = success;
            if (success)
            {
                if (values is null)
                {
                    throw new ArgumentException(null, nameof(values), new NullReferenceException());
                }

                this.values = values;
            }
        }

        /// <summary>
        /// Получает значение, показывающее, успешно ли выполнилась операция.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Получает список значений токенов.
        /// </summary>
        public IReadOnlyList<object> Values
        {
            get
            {
                if (this.Success)
                {
                    return this.values;
                }
                else
                {
                    throw new InvalidOperationException("Можно получить результат только успешной операции.");
                }
            }
        }
    }
}