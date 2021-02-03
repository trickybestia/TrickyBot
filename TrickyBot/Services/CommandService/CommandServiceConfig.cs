using TrickyBot.API.Features;

namespace TrickyBot.Services.CommandService
{
    public class CommandServiceConfig : AlwaysEnabledConfig
    {
        public string CommandPrefix { get; set; } = "!";
        public bool AllowDMCommands { get; set; } = false;
    }
}
