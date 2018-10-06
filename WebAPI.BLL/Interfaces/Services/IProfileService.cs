using System;
using System.Collections.Generic;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IProfileService : IBaseService<Profile>
    {
        IEnumerable<FriendShip> GetFriends(Guid id);
        IEnumerable<Profile> GetByName(String termo);
        Profile GetByEmail(String email);        
        void AddProfilePost(Guid id, Post post);
        void AddProfileImage(Guid id, Image img);
        void AddProfileGallery(Guid id, Gallery gallery);
    }
}
