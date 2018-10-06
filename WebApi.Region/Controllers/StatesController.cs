using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Region.Models;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Services;

namespace WebApi.Region.Controllers
{
    [RoutePrefix("api/states")]
    public class StatesController : ApiController
    {
        private readonly IStateService _stateService;
        private readonly IBlobStorageService _blobStorageService;

        public StatesController(IStateService stateService, IBlobStorageService blobStorageService)
        {
            _stateService = stateService;
            _blobStorageService = blobStorageService;
        }

        // GET: api/States/all
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetStates()
        {
            var stateBM = Mapper.Map<IEnumerable<State>,
                            IEnumerable<StateBindingModel>>(_stateService.GetAll());

            return Ok(stateBM.ToList());
        }

        // GET: api/States/5
        [HttpGet]
        [Route("state/info/{id}")]
        [ResponseType(typeof(State))]
        public IHttpActionResult GetState(Guid id)
        {
            var stateBM = Mapper.Map<State, StateBindingModel>(_stateService.GetById(id));
            if (stateBM != null)
            {
                return Ok(stateBM);
            }
            return Content(HttpStatusCode.NotFound, "State not found!");
        }

        // PUT: api/States/update/5
        [HttpPut]
        [Route("update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutState(StateBindingModel stateBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = Mapper.Map<StateBindingModel, State>(stateBM);
                    _stateService.Update(state);

                    stateBM = Mapper.Map<State, StateBindingModel>(state);
                    return Ok(stateBM);
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

        // POST: api/States/save
        [HttpPost]
        [Route("save")]
        [ResponseType(typeof(State))]
        public IHttpActionResult PostState(StateBindingModel stateBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = Mapper.Map<StateBindingModel, State>(stateBM);
                    _stateService.Save(state);

                    state = _stateService.GetById(state.Id);

                    stateBM = Mapper.Map<State, StateBindingModel>(state);
                    return Ok(stateBM);
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

        // DELETE: api/States/5
        [HttpDelete]
        [Route("state/del/{id}")]
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(Guid id)
        {
            try
            {
                var stateBM = new StateBindingModel()
                {
                    Id = id
                };
                var state = Mapper.Map<StateBindingModel, State>(stateBM);
                _stateService.Delete(state.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //POST: api/States/upload
        [HttpPost]
        [Route("upload")]
        public IHttpActionResult Upload()
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
                        photoUrl = _blobStorageService
                            .UploadImage("states", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stateService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(Guid id)
        {
            return _stateService.GetAll().Count(s => s.Id == id) > 0;
        }
    }
}