// -----------------------------------------------------------------------
// <copyright file="ServiceNotLoadedException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
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