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
    /// <summary>
    /// A class that wraps <see cref="DiscordSocketClient"/>.
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        public Bot()
        {
            Instance = this;
            this.Client = new DiscordSocketClient();
            this.ServiceManager = new ServiceManager();
        }

        /// <summary>
        /// Gets the instance of <see cref="Bot"/> class.
        /// </summary>
        public static Bot Instance { get; private set; }

        /// <summary>
        /// Gets the <see cref="DiscordSocketClient"/> associated with this bot.
        /// </summary>
        public DiscordSocketClient Client { get; }

        /// <summary>
        /// Gets the <see cref="TrickyBot.ServiceManager"/> associated with this bot.
        /// </summary>
        public ServiceManager ServiceManager { get; }

        /// <summary>
        /// Gets the version of the bot.
        /// </summary>
        public Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Starts a bot asynchronously.
        /// </summary>
        /// <param name="token">The bot token to login.</param>
        /// <returns>A task that represents the asynchronous start operation.</returns>
        public async Task Start(string token)
        {
            await this.Client.LoginAsync(TokenType.Bot, token);
            await this.Client.StartAsync();
            await this.ServiceManager.StartAsync();
        }

        /// <summary>
        /// Stops a bot asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous stop operation.</returns>
        public async Task Stop()
        {
            await this.ServiceManager.StopAsync();
            await this.Client.LogoutAsync();
            await this.Client.StopAsync();
        }
    }
}