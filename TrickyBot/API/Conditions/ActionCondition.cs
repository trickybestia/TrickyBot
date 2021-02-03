using Discord;
using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Conditions
{
    public class ActionCondition : ICondition
    {
        public Condition Condition { get; set; }

        public bool CanExecute(IMessage message, string parameter) => Condition(message, parameter);
    }
}
