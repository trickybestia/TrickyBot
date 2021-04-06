// -----------------------------------------------------------------------
// <copyright file="PermissionAlreadyExistsException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService
{
    public class PermissionAlreadyExistsException : PermissionException
    {
        public PermissionAlreadyExistsException(string permission)
            : base(permission, $"Разрешение \"{permission}\" уже существует!")
        {
        }
    }
}