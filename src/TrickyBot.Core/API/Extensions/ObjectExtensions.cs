// -----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

namespace TrickyBot.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Копирует значения свойств объекта <paramref name="source"/> свойствам объекта <paramref name="target"/>.
        /// </summary>
        /// <param name="source">Объект, свойства которого будут скопированы.</param>
        /// <param name="target">Объект, свойствам которого будут значения свойств исходного объекта.</param>
        public static void CopyPropertiesTo(this object source, object target)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target.GetType() != source.GetType())
            {
                throw new InvalidOperationException($"{nameof(source)} и {nameof(target)} дожны иметь одинаковый тип.");
            }

            foreach (var property in source.GetType().GetRuntimeProperties())
            {
                property.SetValue(target, property.GetValue(source));
            }
        }
    }
}