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
    public class AnnouncementService : BaseService<Announcement>, IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IAnnouncementRepository announcementRepository) : base(announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public IEnumerable<Announcement> GetByDate(Announcement ad)
        {
            return _announcementRepository.GetByDate(ad);
        }

        public IEnumerable<Announcement> GetByName(Announcement ad)
        {
            return _announcementRepository.GetByName(ad);
        }
    }
}
