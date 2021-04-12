// -----------------------------------------------------------------------
// <copyright file="PermissionException.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.Services.PermissionService
{
    /// <summary>
    /// Базовый класс для всех исключений, связанных с разрешениями.
    /// </summary>
    public abstract class PermissionException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PermissionException"/>.
        /// </summary>
        /// <param name="permission">Разрешение.</param>
        /// <param name="message">Более подробное описание ошибки.</param>
        public PermissionException(string permission, string message)
            : base(message)
        {
            this.Permission = permission;
        }

        /// <summary>
        /// Получает разрешение, с которым связано исключение.
        /// </summary>
        public string Permission { get; }
    }
}