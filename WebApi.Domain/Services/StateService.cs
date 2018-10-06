using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Domain.Interfaces.Services;

namespace WebAPI.Domain.Services
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
