// -----------------------------------------------------------------------
// <copyright file="CustomStringIds.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.Services.PermissionService
{
    /// <summary>
    /// Идентификаторы кастомных строк для <see cref="TrickyBot.Services.PermissionService.PermissionService"/>.
    /// </summary>
    internal static class CustomStringIds
    {
        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном добавлении разрешения роли.
        /// </summary>
        public const string RolePermissionAdded = "TrickyBot.Services.PermissionService.RolePermissionAdded";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном удалении разрешения у роли.
        /// </summary>
        public const string RolePermissionRemoved = "TrickyBot.Services.PermissionService.RolePermissionRemoved";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение о том, что заданное разрешение не существует у роли.
        /// </summary>
        public const string RolePermissionNotExists = "TrickyBot.Services.PermissionService.RolePermissionNotExists";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном добавлении разрешения роли.
        /// </summary>
        public const string RolePermissionAlreadyExists = "TrickyBot.Services.PermissionService.RolePermissionAlreadyExists";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном добавлении разрешения пользователю.
        /// </summary>
        public const string UserPermissionAdded = "TrickyBot.Services.PermissionService.UserPermissionAdded";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном удалении разрешения у пользователя.
        /// </summary>
        public const string UserPermissionRemoved = "TrickyBot.Services.PermissionService.UserPermissionRemoved";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение о том, что заданное разрешение не существует у пользователя.
        /// </summary>
        public const string UserPermissionNotExists = "TrickyBot.Services.PermissionService.UserPermissionNotExists";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение об успешном добавлении разрешения пользователю.
        /// </summary>
        public const string UserPermissionAlreadyExists = "TrickyBot.Services.PermissionService.UserPermissionAlreadyExists";

        /// <summary>
        /// Идентификатор кастомной строки, содержащей сообщение о том, что команде были переданы неверные параметры.
        /// </summary>
        public const string InvalidParameters = "TrickyBot.Services.PermissionService.InvalidParameters";
    }
}