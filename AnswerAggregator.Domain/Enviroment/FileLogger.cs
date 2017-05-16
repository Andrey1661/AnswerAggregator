using System;
using System.IO;
using AnswerAggregator.Domain.Enviroment.Interfaces;

namespace AnswerAggregator.Domain.Enviroment
{
    public class FileLogger : ILogger
    {
        private readonly string _logsRoot;

        private static int _increment;
        private static string _logFileName;
        private static string _logsFolder;

        public FileLogger(string logsRoot)
        {
            _logsRoot = Path.Combine(logsRoot, "EfLogs");

            if (string.IsNullOrWhiteSpace(_logsFolder))
            {
                var folder = DateTime.Now.ToString("G").Replace("/", "-").Replace(":", "-").Replace(" ", "_");
                _logsFolder = Path.Combine(_logsRoot, folder);
            }
                
            if (!Directory.Exists(_logsFolder))
                Directory.CreateDirectory(_logsFolder);
        }

        public void Log(string component, string message)
        {
            while (true)
            {
                if (string.IsNullOrWhiteSpace(message))
                    return;

                if (message.Contains("!--"))
                {
                    SetLogFileName();

                    if (!message.Contains("--!")) return;
                    var messages = message.Split(new[] {"--!"}, StringSplitOptions.RemoveEmptyEntries);
                    File.AppendAllText(_logFileName, messages[0].Replace("!--", ""));

                    if (messages.Length <= 1) return;
                    message = messages[1];
                    continue;
                }

                if (message.Contains("--!"))
                {
                    var messages = message.Split(new[] {"--!"}, StringSplitOptions.RemoveEmptyEntries);
                    File.AppendAllText(_logFileName, messages[0].Replace("!--", ""));

                    if (messages.Length <= 1) return;
                    message = messages[1];
                    continue;
                }

                File.AppendAllText(_logFileName, message);
                break;
            }
        }

        private static void SetLogFileName()
        {
            var time = DateTime.Now.ToString("T").Replace(":", "-");
            _logFileName = Path.Combine(_logsFolder, string.Format("{0}_{1}.txt", time, _increment++));
        }
    }
}
