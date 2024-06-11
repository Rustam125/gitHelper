using System.Collections.Generic;

namespace GitHelper.Models
{
    public class CommandHandlerResultModel
    {
        public CommandHandlerResultModel(List<string> result, string errors)
        {
            Result = result;
            Errors = errors;
        }

        public List<string> Result { get; }

        public string Errors { get; }

        public bool HasErrors => string.IsNullOrEmpty(Errors) == false;
    }
}