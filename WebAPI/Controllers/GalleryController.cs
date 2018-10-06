using AutoMapper;
using Microsoft.AspNet.Identity;
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

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/galleries")]
    public class GalleryController : ApiController
    {
        private readonly IGalleryService _galleryService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IImageService _imageService;
        //private readonly ITableStorageService _tableStorageService;        

        public GalleryController(IGalleryService galleryService, IBlobStorageService blobStorageService, IImageService imageService)
        {
            _galleryService = galleryService;
            _blobStorageService = blobStorageService;
            _imageService = imageService;
            //_tableStorageService = tableStorageService;
        }

        //GET: api/Galleries
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get()
        {
            var galleries = Mapper.Map<IEnumerable<Gallery>,
                                        IEnumerable<GalleryBindingModel>>(_galleryService.GetAll());

            return Ok(galleries.ToList());
        }

        //GET: api/galleries/gallery/info/5
        [HttpGet]
        [Route("gallery/info/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var galleryBindingModel = Mapper.Map<Gallery, GalleryBindingModel>(_galleryService.GetById(id));
            if (galleryBindingModel != null)
            {
                return Ok(galleryBindingModel);
            }
            return Content(HttpStatusCode.NotFound, "Gallery not found!");
        }

        //POST: api/galleries/save
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Post(GalleryBindingModel gallery)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var galleryModel = Mapper.Map<GalleryBindingModel, Gallery>(gallery);
                    galleryModel.Id = Guid.NewGuid();
                    _galleryService.Save(galleryModel);

                    var galleryModelSaved = _galleryService.GetById(galleryModel.Id);
                    

                    var galleryBM = Mapper.Map<Gallery, GalleryBindingModel>(galleryModelSaved);
                    return Ok(galleryBM);
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

        //PUT: api/galleries/update
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Put(GalleryBindingModel gallery)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var galleryModel = Mapper.Map<GalleryBindingModel, Gallery>(gallery);
                    _galleryService.Update(galleryModel);
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

        //DELETE: api/galleries/gallery/del/5
        [HttpDelete]
        [Route("gallery/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var galleryBindingModel = new GalleryBindingModel()
                {
                    Id = id
                };
                var galleryModel = Mapper.Map<GalleryBindingModel, Gallery>(galleryBindingModel);
                _galleryService.Delete(galleryModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                var result = ex.Message; ;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //POST: api/galleries/upload
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
                            .UploadImage("gallery", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
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