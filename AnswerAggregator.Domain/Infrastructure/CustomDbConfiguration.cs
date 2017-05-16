using System.Data.Entity;

namespace AnswerAggregator.Domain.Infrastructure
{
    public class CustomDbConfiguration : DbConfiguration
    {
        public CustomDbConfiguration()
        {
            SetDatabaseLogFormatter((context, writeAction) => new SqlFormatter(context, writeAction));
        }
    }
}
