// -----------------------------------------------------------------------
// <copyright file="Paths.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Provides paths to bot directories.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets bot executable folder.
        /// </summary>
        public static string BotCore { get; internal set; }

        /// <summary>
        /// Gets data folder path.
        /// </summary>
        public static string Data { get; internal set; }

        /// <summary>
        /// Gets services folder.
        /// </summary>
        public static string Services => Path.Combine(Data, "Services");

        /// <summary>
        /// Gets configs folder.
        /// </summary>
        public static string Configs => Path.Combine(Data, "Configs");

        /// <summary>
        /// Creates all directories.
        /// </summary>
        internal static void Init()
        {
            Directory.CreateDirectory(BotCore);
            Directory.CreateDirectory(Data);
            Directory.CreateDirectory(Configs);
            Directory.CreateDirectory(Services);
        }
    }
}