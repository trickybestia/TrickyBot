using Discord;
using TrickyBot.API.Interfaces;
using TrickyBot.Services.PermissionService;

namespace TrickyBot.API.Conditions
{
    public class PermissionCondition : ICondition
    {
        public string Permission { get; }

        public PermissionCondition(string permission)
        {
            Permission = permission;
        }
        public bool CanExecute(IMessage message, string parameter) => PermissionService.HasPermission((IGuildUser)message.Author, Permission);
    }
}
