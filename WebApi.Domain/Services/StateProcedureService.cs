using System;
using System.Collections.Generic;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Domain.Interfaces.Services;

namespace WebApi.Domain.Services
{
    public class StateProcedureService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateProcedureService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public void Delete(Guid id)
        {
            _stateRepository.Delete(id);
        }

        public void Dispose()
        {
            _stateRepository.Dispose();
        }

        public IEnumerable<State> GetAll()
        {
            return _stateRepository.GetAll();
        }

        public State GetById(Guid id)
        {
            return _stateRepository.GetById(id);
        }

        public void Save(State obj)
        {
            _stateRepository.Save(obj);
        }

        public void Update(State obj)
        {
            _stateRepository.Update(obj);
        }
    }
}
