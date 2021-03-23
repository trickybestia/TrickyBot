// -----------------------------------------------------------------------
// <copyright file="ServiceNotEnabledException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Exception, указывающий на то, что запрашиваемый сервис не включён.
    /// </summary>
    public class ServiceNotEnabledException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceNotEnabledException"/>.
        /// </summary>
        /// <param name="serviceType">Тип сервиса.</param>
        public ServiceNotEnabledException(Type serviceType)
            : base($"Service \"{serviceType.FullName}\" is not enabled!")
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Получает тип сервиса.
        /// </summary>
        public Type ServiceType { get; }
    }
}