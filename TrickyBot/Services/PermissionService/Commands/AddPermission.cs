using Discord;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrickyBot.API.Abstract;
using TrickyBot.API.Conditions;

namespace TrickyBot.Services.PermissionService.Commands
{
    internal class AddPermission : ConditionCommand
    {
        public override string Name { get; } = "permissions add";

        public AddPermission()
        {
            Conditions.Add(new PermissionCondition("permissions.add"));
        }
        protected override async Task Execute(IMessage message, string parameter)
        {
            var service = Bot.Instance.ServiceManager.GetService<PermissionService>();
            var guild = Bot.Instance.ServiceManager.GetService<SingleServerInfoProviderService.SingleServerInfoProviderService>().Guild;
            var match = Regex.Match(parameter, @"<@&(\d+)>\s(.+)");
            try
            {
                if (match.Success)
                {
                    service.AddRolePermission(guild.GetRole(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                else if ((match = Regex.Match(parameter, @"<@!(\d+)>\s(.+)")).Success)
                {
                    service.AddUserPermission(guild.GetUser(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission added.");
            }
            catch (Exception ex) when (ex.Message == "Permission already exists!")
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission already exists!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> invalid parameters!");
            }
        }
    }
}
