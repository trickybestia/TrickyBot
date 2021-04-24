// -----------------------------------------------------------------------
// <copyright file="PermissionAlreadyExistsException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService.API.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что разрешение уже присутствует у пользователя или у роли.
    /// </summary>
    public class PermissionAlreadyExistsException : PermissionException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PermissionAlreadyExistsException"/>.
        /// </summary>
        /// <param name="permission">Разрешение.</param>
        public PermissionAlreadyExistsException(string permission)
            : base(permission, $"Разрешение \"{permission}\" уже существует!")
        {
        }
    }
}