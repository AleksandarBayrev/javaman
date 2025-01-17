using System.Threading.Tasks;
using JavaMan.Interfaces;
using JavaMan.Types;

namespace JavaMan.Handlers
{
    public class JavaListCandidatesCommandHandler : ICommandHandler
    {
        public Task<TaskResult> Execute(string[] args)
        {
            return Task.FromResult(new TaskResult());
        }
    }
}