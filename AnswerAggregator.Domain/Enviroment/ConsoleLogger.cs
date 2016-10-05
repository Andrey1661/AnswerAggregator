using System;
using AnswerAggregator.Domain.Enviroment.Interfaces;

namespace AnswerAggregator.Domain.Enviroment
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string component, string message)
        {
            var time = DateTime.Now;
            var result = string.Format("{0}: ({1}): {2}\n", time.ToShortTimeString(), component, message);

            Console.WriteLine(result);
        }
    }
}
