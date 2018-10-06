using System.Collections.Generic;
using System.Linq;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public IEnumerable<Group> GetByName(Group group)
        {
            return _ctx.Groups.ToList().Where(g => g.Name == group.Name).OrderBy(g => g.Name);
        }
    }
}
