using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IFriendShipService : IBaseService<FriendShip>
    {
        void CreateFriendship(Guid requestedById, Guid requestedToId);
		IEnumerable<Profile> GetFriendsOf(Guid id);
		void RemoveFriendship(Guid requestedById, Guid requestedToId);
    }
}
