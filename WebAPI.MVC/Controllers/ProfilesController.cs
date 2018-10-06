using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebAPI.MVC.Configurations;
using WebAPI.MVC.Models;
using WebAPI.MVC.Utility;

namespace WebAPI.MVC.Controllers
{

	[SessionState(SessionStateBehavior.Default)]
	public class ProfilesController : Controller
	{
		//GET: Profiles/Home
		public ActionResult Home()
		{
			var client = GlobalWebApiClient.GetClient();
			//var response = client.GetAsync("api/Account/LoggedUser").Result;

			var response = client.GetAsync(@"api/profiles/profile/" + Session["Email"].ToString().EncodeBase64()).Result;

			if (response.IsSuccessStatusCode)
			{
				var userLogged = response.Content.ReadAsAsync<ProfileViewModel>().Result;

				var loggedUserId = Guid.Parse(userLogged.Id.ToString());
				var userId = Guid.Parse(Session["UserId"].ToString());
				if ((Session["Email"] == null) || !(loggedUserId.Equals(userId)))
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					return View(userLogged);

				}
			}
			return RedirectToAction("Create", "Profiles");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddProfilePost(PostViewModel pvm)
		{
			if (pvm.Content == null)
			{
				TempData["IsPostEmpty"] = true;
				return RedirectToAction("Home", "Profiles");
			}
			pvm.PostTime = DateTime.Now;

			var client = GlobalWebApiClient.GetClient();
			var postResponse = client.PostAsJsonAsync("api/profiles/addprofilepost", pvm).Result;

			if (postResponse.IsSuccessStatusCode)
			{
				return RedirectToAction("Home", "Profiles");
			}
			return View("Error");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeletePost(Guid postId)
		{
			if (Session["Email"] == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var client = GlobalWebApiClient.GetClient();


			var response = client.GetAsync("api/Account/LoggedUser").Result;

			if (response.IsSuccessStatusCode)
			{
				var userLogged = response.Content.ReadAsAsync<ProfileViewModel>().Result;

				JObject jObj = new JObject
				{
					["postId"] = postId
				};

				foreach (var post in userLogged.Posts)
					if (post.Id == postId)
					{
						response = client.DeleteAsync("api/posts/del" + postId).Result;
						return RedirectToAction("Home", "Profiles");
					}
			}
			return View("Error");
		}

		//GET: Profiles
		public ActionResult Index(String termo = "")
		{
			if (Session["Email"] != null)
			{
				try
				{
					IEnumerable<ProfileViewModel> profiles;
					var client = GlobalWebApiClient.GetClient();
					var response = client.GetAsync("api/profiles/all").Result;

					if (response.IsSuccessStatusCode)
					{
						profiles = response.Content.ReadAsAsync<IEnumerable<ProfileViewModel>>().Result;
						var profile = profiles.ToList().FirstOrDefault(p => p.Email == Session["Email"].ToString());
						if (profile == null)
						{
							return RedirectToAction("Create", "Profiles");
						}
						else if (termo != String.Empty)
						{
							client = GlobalWebApiClient.GetClient();
							response = client.GetAsync(@"api/profiles/info/" + termo).Result;

							if (response.IsSuccessStatusCode)
							{
								profiles = response.Content.ReadAsAsync<IEnumerable<ProfileViewModel>>().Result;
								return View(profiles.ToList());
							}
							else
							{
								ViewBag.Error = "Server Error. Please contact administrator!";
							}
						}
						else
						{
							return View(profiles.ToList());
						}
					}
					else
					{
						ViewBag.Error = "Server Error. Please contact administrator!";
					}


				}
				catch (Exception ex)
				{
					var result = ex.Message;
				}
				return View("Error");
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}

		}

		//GET: Profiles/Details/5
		public ActionResult Details(Guid? id)
		{
			ProfileViewModel pvm = null;
			if (id == null)
			{
				if (Session["Email"] == null)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					try
					{
						var client = GlobalWebApiClient.GetClientRegion();

						var response = client.GetAsync(@"api/profiles/profile/" + Session["Email"].ToString().EncodeBase64()).Result;

						if (response.IsSuccessStatusCode)
						{
							pvm = response.Content.ReadAsAsync<ProfileViewModel>().Result;
							id = pvm.Id;
						}
						else
						{
							ViewBag.Result = "Server Error. Please contact administrator!";
						}
					}
					catch (Exception ex)
					{
						var result = ex.Message;
					}
				}
			}
			else
			{			                

				try
				{
					var client1 = GlobalWebApiClient.GetClient();
					var responseProfile = client1.GetAsync("api/profiles/profile/info/" + id.ToString()).Result;
					//var fsResult = JsonConvert.DeserializeObject<IEnumerable<FriendShipViewModel>>(client.GetStringAsync(@"api/friendships/all").Result);

					if (responseProfile.IsSuccessStatusCode)
					{
						var profileViewModel = responseProfile.Content.ReadAsAsync<ProfileViewModel>().Result;
						var userId = Guid.Parse(Session["UserId"].ToString());

						if (profileViewModel.Id == userId)
						{
							var friends = client1.GetAsync("api/friendships/friends/" + userId.ToString()).Result;

							ProfileViewModel myFriend = null;
							bool isMyFriend = false;

							if (friends.IsSuccessStatusCode)
							{
								var myFriendShips = friends.Content.ReadAsAsync<IEnumerable<ProfileViewModel>>().Result;

								if (myFriendShips.Count() > 0)
								{
									myFriend = myFriendShips.FirstOrDefault(p => p.Id.Equals(p.Id));

									var friendResult = client1.GetAsync("api/profiles/profile/info/" + myFriend.Id.ToString()).Result;
									var pvmFriend = friendResult.Content.ReadAsAsync<ProfileViewModel>().Result;

									isMyFriend = myFriendShips.Any(p => p.Id == pvmFriend.Id);

									var friendShip = new FriendShipViewModel();
									if (isMyFriend)
									{
										//ViewBag.IsFriend = isMyFriend;
										friendShip.Status = StatusEnumViewModel.Accepted;
									}

									var friendId = Guid.Parse(myFriend.Id.ToString());
									foreach (var myFriendShip in myFriendShips)
									{
										if (myFriendShip.Id.Equals(friendId) && !friendShip.Status.Equals(StatusEnumViewModel.Accepted))
										{
											ViewBag.IsFriend = isMyFriend;
											return View(profileViewModel);
										}
										else if (myFriendShip.Id.Equals(friendId) && friendShip.Status.Equals(StatusEnumViewModel.Accepted))
										{
											ViewBag.FriendsCount = myFriendShips.ToList().Count;
											ViewBag.Friends = myFriendShips.ToList();
											ViewBag.IsFriend = isMyFriend;
											return View(profileViewModel);
										}
									}
								}

								ViewBag.FriendsCount = myFriendShips.Count();
								ViewBag.Friends = myFriendShips.ToList();
								ViewBag.IsFriend = isMyFriend;
								return View(profileViewModel);
							}
						}

						var client2 = GlobalWebApiClient.GetClient();
						var friendsResult = client2.GetAsync("api/friendships/friends/" + id.ToString()).Result;

						if (friendsResult.IsSuccessStatusCode)
						{
							var myFriendShips = friendsResult.Content.ReadAsAsync<IEnumerable<ProfileViewModel>>().Result;
                            
                            bool isMyFriend = myFriendShips.Any(p => p.Id == userId);

							var friendShip = new FriendShipViewModel();

							if (isMyFriend)
							{
								//ViewBag.IsFriend = isMyFriend;
								friendShip.Status = StatusEnumViewModel.Accepted;

								foreach (var myFriendShip in myFriendShips)
								{
									if (myFriendShip.Id.Equals(userId) && !friendShip.Status.Equals(StatusEnumViewModel.Accepted))
									{
										ViewBag.IsFriend = isMyFriend;
										return View(profileViewModel);
									}
									else if (myFriendShip.Id.Equals(userId) && friendShip.Status.Equals(StatusEnumViewModel.Accepted))
									{
										ViewBag.FriendsCount = myFriendShips.Count();
										ViewBag.Friends = myFriendShips.ToList();
										ViewBag.IsFriend = isMyFriend;
										return View(profileViewModel);
									}
								}
							}
						}							
						return View(profileViewModel);
					}
					else
					{
						ViewBag.Result = "Server Error. Please contact administrator!";
					}
				}
				catch (Exception ex)
				{
					var result = ex.Message;
				}
				
			}
			return View("Error");
		}

		//GET: Profiles/Create
		public ActionResult Create()
		{
			var clientUser = GlobalWebApiClient.GetClient();
			var result = clientUser.GetAsync("api/Account/LoggedUser").Result;
			//var result = clientUser.GetAsync(@"api/profiles/profile/" + Session["Email"].ToString().EncodeBase64()).Result;

			if (result.IsSuccessStatusCode)
			{
				var user = result.Content.ReadAsAsync<ProfileViewModel>();

				var userId = user.Result.Id.ToString();
				if ((Session["Email"] == null) || !(userId.Equals(Session["UserId"].ToString())))
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					var client = GlobalWebApiClient.GetClientRegion();
					var responseCountry = client.GetAsync("api/countries/all").Result;
					var responseState = client.GetAsync("api/states/all").Result;

					if (responseCountry.IsSuccessStatusCode && responseState.IsSuccessStatusCode)
					{
						var countries = responseCountry.Content.ReadAsAsync<IEnumerable<CountryViewModel>>().Result;
						var states = responseState.Content.ReadAsAsync<IEnumerable<CountryViewModel>>().Result;

						ViewBag.Countries = countries.ToList();
						ViewBag.States = states.ToList();

						return View();
					}
					else
					{
						return RedirectToAction("Login", "Account");
					}

				}
			}
			return RedirectToAction("Login", "Account");
		}

		//POST: Profiles/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Guid countryId, Guid stateId, ProfileViewModel profile, HttpPostedFileBase file)
		{
			if (Session["Email"] != null)
			{
				if (ModelState.IsValid)
				{
					var photoUrl = Upload(file);

					try
					{
						var clientRegion = GlobalWebApiClient.GetClientRegion();
						var CountryResult = clientRegion.GetAsync("api/countries/country/info/" + countryId.ToString()).Result;
						var StateResult = clientRegion.GetAsync("api/states/state/info/" + stateId.ToString()).Result;

						if (CountryResult.IsSuccessStatusCode && StateResult.IsSuccessStatusCode)
						{
							var cvm = CountryResult.Content.ReadAsAsync<CountryViewModel>().Result;
							var svm = StateResult.Content.ReadAsAsync<StateViewModel>().Result;

							profile.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);
							profile.Country = cvm;
							profile.State = svm;
						}

						var client = GlobalWebApiClient.GetClient();
						var response = client.PostAsJsonAsync("api/profiles/save/", profile).Result;

						if (response.IsSuccessStatusCode)
						{
							profile = response.Content.ReadAsAsync<ProfileViewModel>().Result;
							TempData["SuccessMessage"] = "Profile created successfully";

							return RedirectToAction("Details", profile);
						}
					}
					catch (Exception ex)
					{
						var result = ex.Message;
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
				}
			}
			return RedirectToAction("Login", "Account");
		}



		//GET: Profiles/Edit/5
		public ActionResult Edit(Guid? id)
		{
			if ((Session["Email"] == null) || !(id.ToString().Equals(Session["UserId"].ToString())))
			{
				//return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				return RedirectToAction("Login", "Account");
			}
			else
			{
				try
				{
					var clientRegion = GlobalWebApiClient.GetClientRegion();
					var CountryResult = clientRegion.GetAsync("api/countries/all").Result;
					var StateResult = clientRegion.GetAsync("api/states/all").Result;

					if (CountryResult.IsSuccessStatusCode && StateResult.IsSuccessStatusCode)
					{
						var countries = CountryResult.Content.ReadAsAsync<IEnumerable<CountryViewModel>>().Result;
						var states = StateResult.Content.ReadAsAsync<IEnumerable<CountryViewModel>>().Result;

						ViewBag.Countries = countries.ToList();
						ViewBag.States = states.ToList();
					}

					var client = GlobalWebApiClient.GetClient();
					var response = client.GetAsync(@"api/profiles/profile/info/" + id.ToString()).Result;

					if (response.IsSuccessStatusCode)
					{
						var profile = response.Content.ReadAsAsync<ProfileViewModel>().Result;
						return View(profile);
					}

				}
				catch (Exception ex)
				{
					var result = ex.Message;
				}

			}
			return View("Error");
		}

		//POST: Profiles/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid countryId, Guid stateId, ProfileViewModel profile, HttpPostedFileBase file)
		{
			if (Session["Email"] != null)
			{
				if (ModelState.IsValid)
				{
					var photoUrl = Upload(file);

					try
					{
						var clientRegion = GlobalWebApiClient.GetClientRegion();
						var CountryResult = clientRegion.GetAsync("api/countries/country/info/" + countryId.ToString()).Result;
						var StateResult = clientRegion.GetAsync("api/states/state/info/" + stateId.ToString()).Result;

						if (CountryResult.IsSuccessStatusCode && StateResult.IsSuccessStatusCode)
						{
							var cvm = CountryResult.Content.ReadAsAsync<CountryViewModel>().Result;
							var svm = StateResult.Content.ReadAsAsync<StateViewModel>().Result;

							profile.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);
							profile.Country = cvm;
							profile.State = svm;

							var client = GlobalWebApiClient.GetClient();
							var response = client.PutAsJsonAsync("api/profiles/update/", profile).Result;

							if (response.IsSuccessStatusCode)
							{
								profile = response.Content.ReadAsAsync<ProfileViewModel>().Result;
								TempData["SuccessMessage"] = "Profile updated successfully";

								return RedirectToAction("Details", profile);
							}
						}
					}
					catch (Exception ex)
					{
						var result = ex.Message;
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
				}
			}
			return RedirectToAction("Login", "Account");
		}

		//GET: Profiles/Delete/5
		public ActionResult Delete(Guid id)
		{
			if ((Session["Email"] == null) || !(id.ToString().Equals(Session["UserId"].ToString())))
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var client = GlobalWebApiClient.GetClient();
				var response = client.GetAsync(@"api/profiles/profile/info/" + id.ToString()).Result;

				try
				{
					if (response.IsSuccessStatusCode)
					{
						var profile = response.Content.ReadAsAsync<ProfileViewModel>().Result;
						return View(profile);
					}
					else
					{
						return View();
					}
				}
				catch (Exception ex)
				{
					var result = ex.Message;
				}
			}
			return RedirectToAction("Login", "Account");
		}

		//POST: Profiles/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid? id)
		{
			if (Session["Email"] != null)
			{
				if (ModelState.IsValid)
				{
					var client = GlobalWebApiClient.GetClient();
					var response = client.DeleteAsync("api/profiles/profile/del/" + id.ToString()).Result;

					try
					{
						if (response.IsSuccessStatusCode)
						{
							TempData["SuccessMessage"] = "Profile deleted successfully";
							return RedirectToAction("Index");
						}
					}
					catch (Exception ex)
					{
						var result = ex.Message;
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
				}
			}
			return RedirectToAction("Login", "Account");
		}

		//GET: Profiles/Upload
		public ActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		//POST: Profiles/Upload
		public String Upload(HttpPostedFileBase file)
		{
			if (Session["Email"] != null)
			{
				if (ModelState.IsValid)
				{
					try
					{
						const String URI_ADDRESS = "api/profiles/upload";
						var client = GlobalWebApiClient.GetClient();

						using (var content = new MultipartFormDataContent())
						{
							byte[] Bytes = new byte[file.InputStream.Length + 1];
							file.InputStream.Read(Bytes, 0, Bytes.Length);
							var fileContent = new ByteArrayContent(Bytes);
							fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
							{
								FileName = file.FileName
							};
							content.Add(fileContent);

							var response = client.PostAsync(URI_ADDRESS, content).Result;

							if (response.IsSuccessStatusCode)
							{
								var photoUrl = response.Content.ReadAsStringAsync().Result;
								return photoUrl;
							}
							else
							{
								ViewBag.Failed = "Failed! " + response.Content.ToString();
							}
						}
					}
					catch (Exception ex)
					{
						var result = ex.Message;
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
				}
			}

			return null;
		}
	}
}
