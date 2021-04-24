// -----------------------------------------------------------------------
// <copyright file="InvalidPermissionException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService.API.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что разрешение неверно.
    /// </summary>
    public class InvalidPermissionException : PermissionException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InvalidPermissionException"/>.
        /// </summary>
        /// <param name="permission">Разрешение.</param>
        public InvalidPermissionException(string permission)
            : base(permission, $"\"{permission}\" - неверное разрешение!")
        {
        }
    }
}