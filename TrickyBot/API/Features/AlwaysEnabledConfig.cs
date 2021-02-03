using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    public class AlwaysEnabledConfig : IConfig
    {
        public bool IsEnabled
        {
            get
            {
                return true;
            }
            set { }
        }
    }
}
