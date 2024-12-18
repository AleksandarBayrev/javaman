using JavaMan.Enums;

namespace JavaMan.Types
{
    public struct TaskResult
    {
        private string _message;
        private StatusCodes _code;

        public TaskResult(string message, StatusCodes code)
        {
            _message = message;
            _code = code;
        }

        public string Message => _message;
        public StatusCodes Code => _code;
    }
}