using Discord;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Abstract
{
    public abstract class ConditionCommand : ICommand
    {
        public abstract string Name { get; }
        public List<ICondition> Conditions { get; } = new List<ICondition>();

        public Task ExecuteAsync(IMessage message, string parameter)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.CanExecute(message, parameter))
                {
                    return Task.CompletedTask;
                }
            }
            return Execute(message, parameter);
        }
        protected abstract Task Execute(IMessage message, string parameter);
    }
}
