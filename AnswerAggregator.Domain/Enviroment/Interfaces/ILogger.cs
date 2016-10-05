using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnswerAggregator.Domain.Enviroment.Interfaces
{
    public interface ILogger
    {
        void Log(string component, string message);
    }
}
