using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/friendships")]
    public class FriendShipController : ApiController
    {
        private readonly IFriendShipService _friendShipService;        
        private readonly INotificationService _notificationService;


        public FriendShipController(IFriendShipService friendShipService, 
                                    INotificationService notificationService)
        {
            _friendShipService = friendShipService;
            _notificationService = notificationService;
        }

        public FriendShipController()
        {

        }

        // GET: api/FriendShips
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var friendShips = Mapper.Map<IEnumerable<FriendShip>,
                                    IEnumerable<FriendShipBindingModel>>(_friendShipService.GetAll());

            return Ok(friendShips.ToList());
        }

		// GET: api/friendships/friends/5
		[HttpGet]
		[Route("friends/{id}")]
		[ResponseType(typeof(IEnumerable<FriendShip>))]
		public IHttpActionResult GetFriendsOf(Guid id)
		{			
			var friends = Mapper.Map<IEnumerable<BLL.Entities.Profile>,
									IEnumerable<ProfileBindingModel>>(_friendShipService.GetFriendsOf(id));
			if (friends == null)
			{
				return NotFound();
			}

			return Ok(friends.ToList());			
		}


		// GET: api/FriendShips/friendship/info/{id}
		[HttpGet]
        [Route("friendship/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            try
            {
                var friendShipBindingModel = Mapper
                    .Map<FriendShip, FriendShipBindingModel>(_friendShipService.GetById(id));

                if (friendShipBindingModel != null)
                {
                    return Ok(friendShipBindingModel);
                }                
            }
            catch (Exception ex)
            {
                var resul = ex.Message;
            }
            return Content(HttpStatusCode.NotFound, "Profile not found!");
        }

        // POST: api/FriendShips/createfrindship
        [HttpPost]
        [Route("createfriendship")]
        public IHttpActionResult CreateFriendShip(FriendShipBindingModel friendShip)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var freindShipModel = Mapper.Map<FriendShipBindingModel, FriendShip>(friendShip);                    

                    var requestedToId = freindShipModel.RequestedToId;

                    freindShipModel.RequestedById = Guid.Parse(User.Identity.GetUserId());
                    var requestedById = freindShipModel.RequestedById;

                    _friendShipService.CreateFriendship(requestedById, requestedToId);

                    return Ok();                    
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        // PUT: api/FriendShips/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(FriendShipBindingModel friendShip)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var freindShipModel = Mapper.Map<FriendShipBindingModel, FriendShip>(friendShip);

                    var appUser = User.Identity.GetUserId();
                    freindShipModel.Id = Guid.Parse(appUser);

                    _friendShipService.Update(freindShipModel);
                    
                    var friendShipUpdated = _friendShipService.GetById(freindShipModel.Id);

                    var friendShipBindingModel = Mapper.Map<FriendShip, FriendShipBindingModel>(friendShipUpdated);
                    return Ok(friendShipBindingModel);
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        // DELETE: api/FriendShips/friendship/del/{id}
        [HttpDelete]
        [Route("friendship/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var friendShipBindingModel = new FriendShipBindingModel()
                    {
                        Id = id
                    };
                    var freindShipModel = Mapper.Map<FriendShipBindingModel, FriendShip>(friendShipBindingModel);
                    _friendShipService.Delete(freindShipModel.Id);

                    return Ok();
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //POST: api/FriendShips/removefriendship
        [HttpPost]
        [Route("removefriendship")]
        public IHttpActionResult RemoveFriendship(JObject jObj)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var requestedById = Guid.Parse(User.Identity.GetUserId());
                    var requestedToId = Guid.Parse(jObj["requestedToId"].ToString());                                       

                    _friendShipService.RemoveFriendship(requestedById, requestedToId);

                    return Ok();
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));

        }
    }
}
