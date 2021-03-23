// -----------------------------------------------------------------------
// <copyright file="InvalidPermissionException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.Services.PermissionService
{
    public class InvalidPermissionException : Exception
    {
        public InvalidPermissionException(string permission)
            : base($"\"{permission}\" - неверное разрешение!")
        {
            this.Permission = permission;
        }

        public string Permission { get; }
    }
}