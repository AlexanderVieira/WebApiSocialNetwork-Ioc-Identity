using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public void AddProfilePost(Guid id, Post post)
        {
            _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Posts.Add(post);            
        }

        public void AddProfileImage(Guid id, Image img)
        {
            _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Images.Add(img);
        }

        public void AddProfileGallery(Guid id, Gallery gallery)
        {            
            _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Galleries.Add(gallery);
        }

        public void AddProfileGroup(Guid id, Group group)
        {
            _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Groups.Add(group);
        }

        public void AddProfileAnnouncement(Guid id, Announcement ad)
        {
            _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Announcements.Add(ad);
        }

        public Profile GetByEmail(String email)
        {
            return _ctx.Profiles.ToList().Find(p => p.Email.ToLower() == email.ToLower());
        }

        public IEnumerable<Profile> GetByName(String termo)
        {            
            return _ctx.Profiles.ToList().Where(p => p.FirstName.ToLower().Contains(termo.ToLower()) || 
                                                p.Email.ToLower().Contains(termo.ToLower())).OrderBy(p => p.FirstName).Take(25);
        }

        public IEnumerable<FriendShip> GetFriends(Guid id)
        {
            return _ctx.Profiles.SingleOrDefault(p => p.Id.Equals(id)).Friends.ToList();
        }
        
    }
}