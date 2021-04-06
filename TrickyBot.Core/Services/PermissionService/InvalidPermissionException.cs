// -----------------------------------------------------------------------
// <copyright file="InvalidPermissionException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService
{
    public class InvalidPermissionException : PermissionException
    {
        public InvalidPermissionException(string permission)
            : base(permission, $"\"{permission}\" - неверное разрешение!")
        {
        }
    }
}