using System.Collections.Generic;

namespace BL.Enviroment
{
    public class OperationResult
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; protected set; }

        public OperationResult(bool success)
        {       
            Errors = new List<string>();
            Success = success;
        }

        public OperationResult(string error)
        {
            Success = false;
            Errors = new List<string> {error};
        }

        public OperationResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = new List<string>(errors);
        }
    }
}
