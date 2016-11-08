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
    }
}
