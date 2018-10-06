using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;
using WebAPI.BLL.Entities;
using System.Linq;
using System.IO;
using Microsoft.AspNet.Identity;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/images")]
    public class ImageController : ApiController
    {
        private readonly IImageService _imageService;
        private readonly IBlobStorageService _blobStorageService;

        public ImageController(IImageService imageService, IBlobStorageService blobStorageService)
        {
            _imageService = imageService;
            _blobStorageService = blobStorageService;
        }

        //GET: api/images/all        
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var appUser = Guid.Parse(User.Identity.GetUserId());            

            var images = Mapper.Map<IEnumerable<Image>,
                            IEnumerable<ImageBindingModel>>(_imageService.GetAll());

            return Ok(images.ToList());
        }

        //GET: api/images/all        
        [HttpGet]
        [Route("imagesbyuser")]
        public IHttpActionResult GetImagesByUserId()
        {
            var userIdLogged = Guid.Parse(User.Identity.GetUserId());

            var images = Mapper.Map<IEnumerable<Image>,
                            IEnumerable<ImageBindingModel>>(_imageService.GetImagesByUserId(userIdLogged));

            return Ok(images.ToList());
        }

        //GET: api/images/image/info/5
        [HttpGet]
        [Route("image/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var imageBM = Mapper.Map<Image, ImageBindingModel>(_imageService.GetById(id));
            if (imageBM != null)
            {
                return Ok(imageBM);
            }
            return Content(HttpStatusCode.NotFound, "Friend not found!");
        }

        //POST: api/images/save        
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(ImageBindingModel imageBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var image = Mapper.Map<ImageBindingModel, Image>(imageBM);

                    image.Id = Guid.NewGuid();
                    _imageService.Save(image);

                    var imageSaved = _imageService.GetById(image.Id);

                    imageBM = Mapper.Map<Image, ImageBindingModel>(imageSaved);
                    return Ok(imageBM);
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

        //PUT: api/images/update/5
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(ImageBindingModel imageBM)
        {
            try
            {
                var image = Mapper.Map<ImageBindingModel, Image>(imageBM);
                _imageService.Update(image);

                imageBM = Mapper.Map<Image, ImageBindingModel>(image);
                return Ok(imageBM);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //DELETE: api/images/del/5
        [HttpDelete]
        [Route("image/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var imageBM = new ImageBindingModel()
                {
                    Id = id
                };
                var image = Mapper.Map<ImageBindingModel, Image>(imageBM);
                _imageService.Delete(image.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //POST: api/images/upload
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
                            .UploadImage("images", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
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
