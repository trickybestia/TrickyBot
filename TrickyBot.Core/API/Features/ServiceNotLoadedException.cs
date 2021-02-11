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
    /// An exception that indicates whether a service is not loaded.
    /// </summary>
    public class ServiceNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotLoadedException"/> class.
        /// </summary>
        /// <param name="serviceType">The type of service.</param>
        public ServiceNotLoadedException(Type serviceType)
            : base($"Service \"{serviceType.FullName}\" is not loaded!")
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the type of service.
        /// </summary>
        public Type ServiceType { get; }
    }
}