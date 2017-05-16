using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Enviroment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
    [TestClass]
    public class FileLoggerTest
    {
        [TestMethod]
        public void LogTest()
        {
            var logger = new FileLogger();

            logger.Log("test", "test message");
        }
    }
}
