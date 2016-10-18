using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Repositories.Interfaces;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class SettingsService : ISettingsService
    {
        protected readonly IUnitOfWork UnitOfWork;

        public SettingsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public Task<OperationResult> ChangeSetting(string settingName, object value)
        {
            throw new NotImplementedException();
        }
    }
}
