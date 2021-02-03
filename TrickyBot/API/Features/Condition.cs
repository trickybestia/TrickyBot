using Discord;

namespace TrickyBot.API.Features
{
    public delegate bool Condition(IMessage message, string parameter);
}
