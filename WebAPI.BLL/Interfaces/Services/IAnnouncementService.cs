using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IAnnouncementService : IBaseService<Announcement>
    {
        IEnumerable<Announcement> GetByName(Announcement ad);
        IEnumerable<Announcement> GetByDate(Announcement ad);
    }
}
