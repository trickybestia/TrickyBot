// -----------------------------------------------------------------------
// <copyright file="ServiceNotLoadedException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    public class ServiceNotLoadedException : Exception
    {
        public ServiceNotLoadedException(Type serviceType)
            : base($"Service \"{serviceType.FullName}\" is not loaded!")
        {
            this.ServiceType = serviceType;
        }

        public Type ServiceType { get; }
    }
}