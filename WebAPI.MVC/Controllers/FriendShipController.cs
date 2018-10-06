using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI.MVC.Configurations;
using WebAPI.MVC.Models;

namespace WebAPI.MVC.Controllers
{
    public class FriendShipController : Controller
    {
        // GET: FriendShip
        public ActionResult Index(String termo = "")
        {
            if (Session["Email"] != null)
            {
                try
                {
                    IEnumerable<FriendShipViewModel> friendShips;
                    if (termo != String.Empty)
                    {
                        var client = GlobalWebApiClient.GetClient();
                        var responseFriendShipByName = client.GetAsync(@"api/friendships/friendship/info/" + termo).Result;

                        if (responseFriendShipByName.IsSuccessStatusCode)
                        {
                            friendShips = responseFriendShipByName.Content.ReadAsAsync<IEnumerable<FriendShipViewModel>>().Result;
                            return View(friendShips.ToList());
                        }
                    }
                    else
                    {
                        var client = GlobalWebApiClient.GetClient();
                        var response = client.GetAsync("api/friendships/all").Result;

                        if (response.IsSuccessStatusCode)
                        {
                            friendShips = response.Content.ReadAsAsync<IEnumerable<FriendShipViewModel>>().Result;
                            return View(friendShips.ToList());
                        }
                    }                   
                        
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }                
            }            
            return View();
        }

        // GET: FriendShip/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: FriendShip/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: FriendShip/CreateFriendShip
        [HttpGet]        
        public ActionResult CreateFriendShip(Guid requestedToId)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (requestedToId.Equals(Session["UserId"]))
            {
                return RedirectToAction("Details", "Profiles", routeValues: new { id = requestedToId });
            }

            var friendShip = new FriendShipViewModel
            {
                RequestedToId = requestedToId
            };

            //JObject jObj = new JObject
            //{
            //    ["requestedToId"] = requestedToId
            //};

            var client = GlobalWebApiClient.GetClient();
            var response = client.PostAsJsonAsync("api/friendships/createfriendship/", friendShip).Result;

            if (response.IsSuccessStatusCode)
            {
				client = GlobalWebApiClient.GetClient();
				var requestedTo = client.GetAsync(@"api/profiles/profile/info/" + requestedToId.ToString()).Result;

				//var fsResult = JsonConvert.DeserializeObject<IEnumerable<FriendShipViewModel>>(client.GetStringAsync(@"api/friendships/all").Result);                
				
				if (response.IsSuccessStatusCode)
				{
					var pvm = response.Content.ReadAsAsync<ProfileViewModel>().Result;
					//var result = response.Content.ReadAsStringAsync().Result;
					return RedirectToAction("Details", "Profiles", pvm);
				}
            }

            return View("Error");
        }

        // GET: FriendShip/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: FriendShip/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendShip/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FriendShip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: FriendShip/AcceptFriendship
        public ActionResult RemoveFriendship(Guid requestedToId)
        {
            if (Session["Email"] == null)
                return RedirectToAction("Login", "Account");

            var client = GlobalWebApiClient.GetClient();            

            JObject jObj = new JObject
            {
                ["requestedToId"] = requestedToId                
            };

            var response = client.PostAsJsonAsync("api/friendships/removefriendship/", jObj).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Home", "Profiles");
            }
            return View("Error");
        }
    }
}
