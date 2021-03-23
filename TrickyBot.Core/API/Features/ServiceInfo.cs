// -----------------------------------------------------------------------
// <copyright file="ServiceInfo.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Newtonsoft.Json;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Содержит в себе информацию о сервисе.
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Получает название сервиса.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Получает имя автора сервиса.
        /// </summary>
        public string Author { get; init; } = string.Empty;

        /// <summary>
        /// Получает ссылку на GitHub-репозиторий сервиса.
        /// </summary>
        public string GithubRepositoryUrl { get; init; } = string.Empty;

        /// <summary>
        /// Получает версию сервиса.
        /// </summary>
        public Version Version { get; init; } = new Version(1, 0, 0, 0);

        /// <inheritdoc/>
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}