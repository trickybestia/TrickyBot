using Discord;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TrickyBot
{
    public class Bot
    {
        public static Bot Instance { get; private set; }
        public DiscordSocketClient Client { get; }
        public ServiceManager ServiceManager { get; }
        public Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

        public Bot()
        {
            Instance = this;
            Client = new DiscordSocketClient();
            ServiceManager = new ServiceManager();
        }
        public async Task Start(string token)
        {
            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();
            await ServiceManager.StartAsync();
        }
        public async Task Stop()
        {
            await ServiceManager.StopAsync();
            await Client.LogoutAsync();
            await Client.StopAsync();
        }
    }
}