// -----------------------------------------------------------------------
// <copyright file="ServiceNotLoadedException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что запрашиваемый сервис не загружен.
    /// </summary>
    public class ServiceNotLoadedException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceNotLoadedException"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервиса.</param>
        public ServiceNotLoadedException(Type serviceType)
            : base($"Сервис \"{serviceType.FullName}\" не загружен.")
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Получает тип сервиса.
        /// </summary>
        public Type ServiceType { get; }
    }
}