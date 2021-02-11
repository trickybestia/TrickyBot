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
    /// An exception that indicates whether a service is not enabled.
    /// </summary>
    public class ServiceNotEnabledException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotEnabledException"/> class.
        /// </summary>
        /// <param name="serviceType">The type of service.</param>
        public ServiceNotEnabledException(Type serviceType)
            : base($"Service \"{serviceType.FullName}\" is not enabled!")
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the type of service.
        /// </summary>
        public Type ServiceType { get; }
    }
}