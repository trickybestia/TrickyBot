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
    /// A class that contains info about service.
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets the author of the service.
        /// </summary>
        public string Author { get; init; } = string.Empty;

        /// <summary>
        /// Gets the github repository url of the service.
        /// </summary>
        public string GithubRepositoryUrl { get; init; } = string.Empty;

        /// <summary>
        /// Gets the version of the service.
        /// </summary>
        public Version Version { get; init; } = new Version(1, 0, 0, 0);

        /// <inheritdoc/>
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}