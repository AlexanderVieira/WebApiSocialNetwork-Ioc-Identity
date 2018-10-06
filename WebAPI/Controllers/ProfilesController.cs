using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/profiles")]
    public class ProfilesController : ApiController
    {        
        private readonly IProfileService _profileService;        
        private readonly IImageService _imageService;
        private readonly IBlobStorageService _blobStorageService;

        public ProfilesController(IProfileService profileService, 
                                  IImageService imageService, IBlobStorageService blobStorageService)
        {            
            _profileService = profileService;            
            _imageService = imageService;
            _blobStorageService = blobStorageService;            
        }

        public ProfilesController()
        {

        }

        //GET: api/Profiles/all
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var profiles = Mapper.Map<IEnumerable<BLL.Entities.Profile>,
                                    IEnumerable<ProfileBindingModel>>(_profileService.GetAll());

            return Ok(profiles.ToList());
        }

        // GET: api/profiles/info/termo
        [HttpGet]
        [Route("info/{termo}")]
        public IHttpActionResult GetByName(String termo)
        {
            var profileBindingModel = Mapper.Map<IEnumerable<BLL.Entities.Profile>,
                                    IEnumerable<ProfileBindingModel>>(_profileService.GetByName(termo));

            return Ok(profileBindingModel.ToList());
        }

        // GET: api/Profiles/profile/info/5
        [HttpGet]
        [Route("profile/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var profileBindingModel = Mapper
                    .Map<BLL.Entities.Profile, ProfileBindingModel>(_profileService.GetById(id));

            if (profileBindingModel != null)
            {
                return Ok(profileBindingModel);
            }
            return Content(HttpStatusCode.NotFound, "Profile not found!");
        }

        // GET: api/Profiles/info/email
        [HttpGet]
        [Route("profile/{email}")]
        public IHttpActionResult GetByEmail(String email)
        {
            email = email.DecodeBase64();
            var profileBindingModel = Mapper
                    .Map<BLL.Entities.Profile, ProfileBindingModel>(_profileService.GetByEmail(email));

            if (profileBindingModel != null)
            {
                return Ok(profileBindingModel);
            }
            return Content(HttpStatusCode.NotFound, "Profile not found!");
        }

        //POST: api/Profiles/save
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(ProfileBindingModel profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profileModel = Mapper.Map<ProfileBindingModel, BLL.Entities.Profile>(profile);

                    var appUser = User.Identity.GetUserId();
                    profileModel.Id = Guid.Parse(appUser);

                    _profileService.Save(profileModel);

                    //profileModel = _profileService.GetAll().FirstOrDefault(p => p.Id == profileModel.Id);
                    profileModel = _profileService.GetById(profileModel.Id);

                    var profileBM = Mapper.Map<BLL.Entities.Profile, ProfileBindingModel>(profileModel);
                    return Ok(profileBM);
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

        //PUT: api/profiles/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(ProfileBindingModel profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profileModel = Mapper.Map<ProfileBindingModel, BLL.Entities.Profile>(profile);                    
                    _profileService.Update(profileModel);

                    profileModel = _profileService.GetAll().FirstOrDefault(p => p.Id == profile.Id);                    

                    var profileBM = Mapper.Map<BLL.Entities.Profile, ProfileBindingModel>(profileModel);
                    return Ok(profileBM);
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

        //DELETE: api/Profiles/profile/del/5
        [HttpDelete]
        [Route("profile/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profileBindingModel = new ProfileBindingModel()
                    {
                        Id = id
                    };
                    var profileModel = Mapper.Map<ProfileBindingModel, BLL.Entities.Profile>(profileBindingModel);
                    _profileService.Delete(profileModel.Id);
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

        //POST: api/Profiles/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> Upload()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var photoUrl = String.Empty;
                    
                    var files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        var imageFile = files[i];
                        photoUrl = await _blobStorageService
                            .UploadImage("profiles", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
                                          imageFile.InputStream, imageFile.ContentType);                        
                    }
                    return Ok(photoUrl);

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
        
        public IEnumerable<FriendShip> GetFriends(Guid id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            return _profileService.GetFriends(userId);
        }

        //POST: api/profiles/addprofilepost
        [HttpPost]
        [Route("addprofilepost")]
        public IHttpActionResult AddProfilePost(PostBindingModel pbm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = Guid.Parse(User.Identity.GetUserId());

                    var post = Mapper.Map<PostBindingModel, BLL.Entities.Post>(pbm);
                    _profileService.AddProfilePost(userId, post);
                    return Ok();
                }
                catch (Exception ex)
                {
                    var results = ex.Message;
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
