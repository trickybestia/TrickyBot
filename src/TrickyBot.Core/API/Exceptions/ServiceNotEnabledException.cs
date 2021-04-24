// -----------------------------------------------------------------------
// <copyright file="ServiceNotEnabledException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что запрашиваемый сервис не включён.
    /// </summary>
    public class ServiceNotEnabledException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceNotEnabledException"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервиса.</param>
        public ServiceNotEnabledException(Type serviceType)
            : base($"Сервис \"{serviceType.FullName}\" не включён.")
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Получает тип сервиса.
        /// </summary>
        public Type ServiceType { get; }
    }
}