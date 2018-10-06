using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/announcements")]
    public class AnnouncementController : ApiController
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IBlobStorageService _blobStorageService;

        //private readonly IImageService _imageService;

        public AnnouncementController(IAnnouncementService announcementService, 
                                        IBlobStorageService blobStorageService)
        {
            _announcementService = announcementService;
            //_imageService = imageService;
            _blobStorageService = blobStorageService;
        }



        //GET: api/announcements/all
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var announcements = Mapper.Map<IEnumerable<Announcement>,
                                    IEnumerable<AnnouncementBindingModel>>(_announcementService.GetAll());

            return Ok(announcements.ToList());
        }

        // GET: api/announcements/announcement/info/5
        [HttpGet]
        [Route("announcement/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var announcementBindingModel = Mapper
                    .Map<Announcement, AnnouncementBindingModel>(_announcementService.GetById(id));

            if (announcementBindingModel != null)
            {
                return Ok(announcementBindingModel);
            }
            return Content(HttpStatusCode.NotFound, "Announcement not found!");
        }

        //POST: api/announcements/save
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(AnnouncementBindingModel abm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var announcement = Mapper.Map<AnnouncementBindingModel, Announcement>(abm);

                    _announcementService.Save(announcement);

                    //profileModel = _profileService.GetAll().FirstOrDefault(p => p.Id == profileModel.Id);
                    announcement = _announcementService.GetById(announcement.Id);

                    abm = Mapper.Map<Announcement, AnnouncementBindingModel>(announcement);
                    return Ok(abm);
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

        //PUT: api/announcements/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(AnnouncementBindingModel abm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var announcement = Mapper.Map<AnnouncementBindingModel, Announcement>(abm);
                    
                    _announcementService.Update(announcement);

                    announcement = _announcementService.GetAll().FirstOrDefault(p => p.Id == announcement.Id);

                    abm = Mapper.Map<Announcement, AnnouncementBindingModel>(announcement);

                    return Ok(abm);
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

        //DELETE: api/announcements/announcement/del/5
        [HttpDelete]
        [Route("announcement/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var abm = new AnnouncementBindingModel()
                    {
                        Id = id
                    };
                    var announcement = Mapper.Map<AnnouncementBindingModel, Announcement>(abm);
                    _announcementService.Delete(announcement.Id);

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

        //POST: api/announcements/upload
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
                            .UploadImage("announcements", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
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
    }
}
