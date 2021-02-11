// -----------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Enum of log levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// A message is info message.
        /// </summary>
        Info,

        /// <summary>
        /// A message is debug message.
        /// </summary>
        Debug,

        /// <summary>
        /// A message is warn message.
        /// </summary>
        Warn,

        /// <summary>
        /// A message is error message.
        /// </summary>
        Error,
    }
}