// -----------------------------------------------------------------------
// <copyright file="PermissionException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.Services.PermissionService
{
    public abstract class PermissionException : Exception
    {
        public PermissionException(string permission, string message)
            : base(message)
        {
            this.Permission = permission;
        }

        public string Permission { get; }
    }
}