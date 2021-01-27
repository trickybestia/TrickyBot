// -----------------------------------------------------------------------
// <copyright file="Paths.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace TrickyBot.API.Features
{
    public static class Paths
    {
        public static string BotCore { get; internal set; }

        public static string ServicesRoot { get; internal set; }

        public static string ServiceDlls => Path.Combine(ServicesRoot, "Services");

        public static string Configs => Path.Combine(ServicesRoot, "Configs");
    }
}