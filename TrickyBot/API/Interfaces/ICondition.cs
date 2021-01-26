using Discord;

namespace TrickyBot.API.Interfaces
{
    public interface ICondition
    {
        bool CanExecute(IMessage message, string parameter);
    }
}
