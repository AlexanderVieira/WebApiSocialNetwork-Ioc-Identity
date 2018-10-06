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
    public class FriendShipProcedureService : IFriendShipService
    {
        private readonly IFriendShipRepository _friendShipRepository;
		private readonly IProfileRepository _profileRepository;

		public FriendShipProcedureService(IFriendShipRepository friendShipRepository, 
											IProfileRepository profileRepository)
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

        public void Delete(Guid id)
        {
            _friendShipRepository.Delete(id);
        }

        public void Dispose()
        {
            _friendShipRepository.Dispose();
        }

        public IEnumerable<FriendShip> GetAll()
        {
            return _friendShipRepository.GetAll();
        }

        public FriendShip GetById(Guid id)
        {
            return _friendShipRepository.GetById(id);
        }

        public IEnumerable<Profile> GetFriendsOf(Guid id)
        {
            var friendShips = _friendShipRepository.GetAll();

			var profile = new Profile();
			var profiles = new List<Profile>();

            foreach (var friendShip in friendShips)
            {
				if (friendShip.RequestedById == id)
				{
					profile = _profileRepository.GetById(friendShip.RequestedToId);
					profiles.Add(profile);
				}

				if (friendShip.RequestedToId == id)
				{
					profile = _profileRepository.GetById(friendShip.RequestedById);
					profiles.Add(profile);
				}                    
            }

            return profiles;
        }

        public void RemoveFriendship(Guid requestedById, Guid requestedToId)
        {
            var friendships = _friendShipRepository.GetAll();

            var friendshipToBeRemoved = new FriendShip();
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

        public void Save(FriendShip obj)
        {
            _friendShipRepository.Save(obj);
        }

        public void Update(FriendShip obj)
        {
            _friendShipRepository.Update(obj);
        }
    }
}
