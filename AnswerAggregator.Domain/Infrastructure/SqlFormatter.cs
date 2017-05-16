using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace AnswerAggregator.Domain.Infrastructure
{
    internal class SqlFormatter : DatabaseLogFormatter
    {
        public SqlFormatter(Action<string> writeAction)
            : base(writeAction)
        {
        }

        public SqlFormatter(DbContext context, Action<string> writeAction)
            : base(context, writeAction)
        {
        }

        private static bool _markSet;
        private static bool _commandLogged;

        public override void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            if (!_markSet && !_commandLogged)
            {
                _markSet = true;
                Write(string.Format("{0}{1}", "!--", Environment.NewLine));
            }

            base.Opened(connection, interceptionContext);
        }

        public override void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            _commandLogged = true;
            base.LogCommand(command, interceptionContext);
        }

        public override void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            base.Closed(connection, interceptionContext);

            if (_markSet && _commandLogged)
            {
                _markSet = false;
                _commandLogged = false;
                Write(string.Format("{0}{1}", "--!", Environment.NewLine));
            }
        }
    }
}
