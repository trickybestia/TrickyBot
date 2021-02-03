using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    public class EmptyConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}
