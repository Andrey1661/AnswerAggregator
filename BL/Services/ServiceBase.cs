using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace BL.Services
{
    public abstract class ServiceBase : IDisposable
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            if (UnitOfWork != null)
                UnitOfWork.Dispose();
        }
    }
}
