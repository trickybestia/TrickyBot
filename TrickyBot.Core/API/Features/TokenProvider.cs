// -----------------------------------------------------------------------
// <copyright file="TokenProvider.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace TrickyBot.API.Features
{
    internal static class TokenProvider
    {
        public static string GetToken()
        {
            var tokenProviderType = CommandLineArgsParser.Args["tokenprovidertype"];
            string token = tokenProviderType switch
            {
                "envvar" => Environment.GetEnvironmentVariable(CommandLineArgsParser.Args["tokenenvvarname"]),
                "commandlinearg" => CommandLineArgsParser.Args["token"],
                _ => throw new Exception("Unrecognized token provider type."),
            };
            if (token is null)
            {
                throw new Exception("Can't find token.");
            }

            return token;
        }
    }
}