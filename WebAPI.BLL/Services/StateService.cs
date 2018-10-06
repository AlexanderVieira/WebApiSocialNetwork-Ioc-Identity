using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class StateService : BaseService<State>, IStateService
    {
        private readonly IStateRepository _StateRepository;

        public StateService(IStateRepository StateRepository) : base(StateRepository)
        {
            _StateRepository = StateRepository;
        }
    }
}
