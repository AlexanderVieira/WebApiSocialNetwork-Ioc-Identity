using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
    {
        public IEnumerable<Announcement> GetByName(Announcement ad)
        {            
            return _ctx.Announcements.ToList().Where(a => a.Title == ad.Title).OrderBy(a => a.Title);
        }
        public IEnumerable<Announcement> GetByDate(Announcement ad)
        {            
            return _ctx.Announcements.ToList().Where(a => a.CreationDate == ad.CreationDate).OrderBy(a => a.CreationDate);
        }
    }
}
