using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class FriendShipService : BaseService<FriendShip>, IFriendShipService
    {
        private readonly IFriendShipRepository _friendShipRepository;
        private readonly IProfileRepository _profileRepository;
        

        public FriendShipService(IFriendShipRepository friendShipRepository, 
                                 IProfileRepository profileRepository) : base(friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
            _profileRepository = profileRepository;            
        }

        public void CreateFriendship(Guid requestedById, Guid requestedToId)
        {

			var friendShip = new FriendShip
			{
				Id = Guid.NewGuid(),

				RequestedById = requestedById,
                RequestedToId = requestedToId,
                RequestTime = DateTime.Now,
                Status = StatusEnum.Pendent
            };

            _friendShipRepository.Save(friendShip);            
        }

		public IEnumerable<Profile> GetFriendsOf(Guid id)
		{			
			var friendShips = _friendShipRepository.GetAll();
            
            var profiles = new List<Profile>();

            foreach (var friendShip in friendShips)
            {
                if (friendShip.RequestedById == id)
                    profiles.Add(friendShip.RequestedTo);
                if (friendShip.RequestedToId == id)
                    profiles.Add(friendShip.RequestedBy);
            }

            return profiles;
		}

		public void RemoveFriendship(Guid requestedById, Guid requestedToId)
        {
            var friendships = _friendShipRepository.GetAll();
            
            var  friendshipToBeRemoved = new FriendShip();
            foreach (var friendship in friendships)
            {
                if (
                    (friendship.RequestedById == requestedById
                    && friendship.RequestedToId == requestedToId)
                    ||
                    (friendship.RequestedById == requestedById
                    && friendship.RequestedToId == requestedById)
                   )
                {
                    friendshipToBeRemoved = friendship;
                    _friendShipRepository.Delete(friendshipToBeRemoved.Id);
                    return;
                }
            }
        }

    }
}
