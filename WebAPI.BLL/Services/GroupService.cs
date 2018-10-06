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
    public class GroupService : BaseService<Group>, IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository) : base(groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IEnumerable<Group> GetByName(Group group)
        {
            return _groupRepository.GetByName(group);
        }
    }
}
