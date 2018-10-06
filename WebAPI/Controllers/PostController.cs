using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Post/all
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var pbm = Mapper.Map<IEnumerable<Post>,
                                    IEnumerable<PostBindingModel>>(_postService.GetAll());

            return Ok(pbm.ToList());
        }

        // GET: api/Posts/Post/Info/5
        [Route("post/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var pbm = Mapper.Map<Post, PostBindingModel>(_postService.GetById(id));

            if (pbm != null)
            {
                return Ok(pbm);
            }
            return Content(HttpStatusCode.NotFound, "Notification not found!");
        }

        // POST: api/Posts/Save
        [Route("save")]
        public IHttpActionResult Save(PostBindingModel pbm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = Mapper.Map<PostBindingModel, Post>(pbm);

                    _postService.Save(post);

                    post = _postService.GetById(post.Id);

                    var pbmSave = Mapper.Map<Post, PostBindingModel>(post);
                    return Ok(pbmSave);
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

        // PUT: api/Notification/Update
        [Route("update")]
        public IHttpActionResult Update(PostBindingModel pbm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = Mapper.Map<PostBindingModel, Post>(pbm);

                    _postService.Update(post);

                    post = _postService.GetById(post.Id);

                    var pbmUp = Mapper.Map<Post, PostBindingModel>(post);
                    return Ok(pbmUp);
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

        // DELETE: api/Posts/post/del
        [Route("post/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pbm = new PostBindingModel
                    {
                        Id = id
                    };
                    var post = Mapper.Map<PostBindingModel, Post>(pbm);
                    _postService.Delete(post.Id);

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
