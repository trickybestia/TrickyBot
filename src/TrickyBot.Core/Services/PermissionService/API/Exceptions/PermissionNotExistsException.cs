// -----------------------------------------------------------------------
// <copyright file="PermissionNotExistsException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService.API.Exceptions
{
    /// <summary>
    /// Исключение, указывающее на то, что разрешение не существует у пользователя или у роли.
    /// </summary>
    public class PermissionNotExistsException : PermissionException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PermissionNotExistsException"/>.
        /// </summary>
        /// <param name="permission">Разрешение.</param>
        public PermissionNotExistsException(string permission)
            : base(permission, $"Разрешение \"{permission}\" не существует!")
        {
        }
    }
}