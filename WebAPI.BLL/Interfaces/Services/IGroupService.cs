using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IGroupService : IBaseService<Group>
    {
        IEnumerable<Group> GetByName(Group group);
    }
}
