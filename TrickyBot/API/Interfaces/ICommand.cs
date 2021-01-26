using Discord;
using System.Threading.Tasks;

namespace TrickyBot.API.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        Task ExecuteAsync(IMessage message, string parameter);
    }
}
