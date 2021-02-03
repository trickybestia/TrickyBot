// -----------------------------------------------------------------------
// <copyright file="Bot.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace TrickyBot
{
    public class Bot
    {
        public Bot()
        {
            Instance = this;
            this.Client = new DiscordSocketClient();
            this.ServiceManager = new ServiceManager();
        }

        public static Bot Instance { get; private set; }

        public DiscordSocketClient Client { get; }

        public ServiceManager ServiceManager { get; }

        public Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        public async Task Start(string token)
        {
            await this.Client.LoginAsync(TokenType.Bot, token);
            await this.Client.StartAsync();
            await this.ServiceManager.StartAsync();
        }

        public async Task Stop()
        {
            await this.ServiceManager.StopAsync();
            await this.Client.LogoutAsync();
            await this.Client.StopAsync();
        }
    }
}