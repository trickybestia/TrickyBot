using Discord;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrickyBot.API.Abstract;
using TrickyBot.API.Conditions;

namespace TrickyBot.Services.PermissionService.Commands
{
    internal class RemovePermission : ConditionCommand
    {
        public override string Name { get; } = "permissions remove";

        public RemovePermission()
        {
            Conditions.Add(new PermissionCondition("permissions.remove"));
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
                    service.RemoveRolePermission(guild.GetRole(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                else if ((match = Regex.Match(parameter, @"<@!(\d+)>\s(.+)")).Success)
                {
                    service.RemoveUserPermission(guild.GetUser(ulong.Parse(match.Result("$1"))), match.Result("$2"));
                }
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission removed.");
            }
            catch (Exception ex) when (ex.Message == "Permission doesn't exist!")
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> permission doesn't exist!");
            }
            catch
            {
                await message.Channel.SendMessageAsync($"<@!{message.Author.Id}> invalid parameters!");
            }
        }
    }
}
