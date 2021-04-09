// -----------------------------------------------------------------------
// <copyright file="PermissionAlreadyExistsException.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService
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