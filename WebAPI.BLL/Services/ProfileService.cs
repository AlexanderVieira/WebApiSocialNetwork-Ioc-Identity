using System;
using System.Collections.Generic;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class ProfileService : BaseService<Profile>, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository) : base(profileRepository)
        {
            _profileRepository = profileRepository;
        }        

        public void AddProfileGallery(Guid id, Gallery gallery)
        {
            _profileRepository.AddProfileGallery(id, gallery);
        }

        public void AddProfileImage(Guid id, Image img)
        {
            _profileRepository.AddProfileImage(id, img);
        }

        public void AddProfilePost(Guid id, Post post)
        {
            _profileRepository.AddProfilePost(id, post);
        }

        public Profile GetByEmail(String email)
        {
            return _profileRepository.GetByEmail(email);
        }

        public IEnumerable<Profile> GetByName(String termo)
        {
            return _profileRepository.GetByName(termo);
        }

        public IEnumerable<FriendShip> GetFriends(Guid id)
        {
            return _profileRepository.GetFriends(id);
        }
        
    }
}
