using TrickyBot.API.Features;

namespace TrickyBot.Services.SingleServerInfoProviderService
{
    public class SingleServerInfoProviderServiceConfig : AlwaysEnabledConfig
    {
        public ulong GuildId { get; set; }
    }
}
