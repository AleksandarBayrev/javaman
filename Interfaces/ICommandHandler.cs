using System.Threading.Tasks;
using JavaMan.Types;

namespace JavaMan.Interfaces
{
    public interface ICommandHandler
    {
        Task<TaskResult> Execute(string[] args);
    }
}