// -----------------------------------------------------------------------
// <copyright file="InvalidPermissionException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService
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