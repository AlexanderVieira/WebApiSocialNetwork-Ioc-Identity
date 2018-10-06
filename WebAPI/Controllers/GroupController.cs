using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [RoutePrefix("api/groups")]
    public class GroupController : ApiController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        //GET: api/groups/all        
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var Groups = Mapper.Map<IEnumerable<Group>,
                            IEnumerable<GroupBindingModel>>(_groupService.GetAll());

            return Ok(Groups.ToList());
        }

        //GET: api/groups/group/info/5
        [HttpGet]
        [Route("group/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var GroupBM = Mapper.Map<Group, GroupBindingModel>(_groupService.GetById(id));
            if (GroupBM != null)
            {
                return Ok(GroupBM);
            }
            return Content(HttpStatusCode.NotFound, "Group not found!");
        }

        //POST: api/groups/save        
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(GroupBindingModel GroupBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var group = Mapper.Map<GroupBindingModel, Group>(GroupBM);
                    _groupService.Save(group);

                    GroupBM = Mapper.Map<Group, GroupBindingModel>(group);
                    return Ok(GroupBM);
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

        //PUT: api/groups/update/5
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(GroupBindingModel GroupBM)
        {
            try
            {
                var group = Mapper.Map<GroupBindingModel, Group>(GroupBM);
                _groupService.Update(group);

                GroupBM = Mapper.Map<Group, GroupBindingModel>(group);
                return Ok(GroupBM);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //DELETE: api/group/del/5
        [HttpDelete]
        [Route("group/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var GroupBM = new GroupBindingModel()
                {
                    Id = id
                };
                var group = Mapper.Map<GroupBindingModel, Image>(GroupBM);
                _groupService.Delete(group.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }
    }
}
