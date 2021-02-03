// -----------------------------------------------------------------------
// <copyright file="ServiceNotEnabledException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    public class ServiceNotEnabledException : Exception
    {
        public ServiceNotEnabledException(Type serviceType)
            : base($"Service \"{serviceType.FullName}\" is not enabled!")
        {
            this.ServiceType = serviceType;
        }

        public Type ServiceType { get; }
    }
}