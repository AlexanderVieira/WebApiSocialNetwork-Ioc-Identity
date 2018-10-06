using AutoMapper;
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
    [RoutePrefix("api/notifications")]
    public class NotificationController : ApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notifications/all
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var nbm = Mapper.Map<IEnumerable<Notification>,
                                    IEnumerable<NotificationBindingModel>>(_notificationService.GetAll());

            return Ok(nbm.ToList());
        }

        // GET: api/Notifications/Notification/Info/5
        [Route("notification/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var nbm = Mapper.Map<Notification, NotificationBindingModel>(_notificationService.GetById(id));

            if (nbm != null)
            {
                return Ok(nbm);
            }
            return Content(HttpStatusCode.NotFound, "Notification not found!");
        }

        // POST: api/Notification/Save
        [Route("save")]
        public IHttpActionResult Save(NotificationBindingModel nbm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = Mapper.Map<NotificationBindingModel, Notification>(nbm);

                    _notificationService.Save(notification);
                    
                    notification = _notificationService.GetById(notification.Id);

                    var nbmSave = Mapper.Map<Notification, NotificationBindingModel>(notification);
                    return Ok(nbmSave);
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
        public IHttpActionResult Update(NotificationBindingModel nbm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = Mapper.Map<NotificationBindingModel, Notification>(nbm);

                    _notificationService.Update(notification);

                    notification = _notificationService.GetById(notification.Id);

                    var nbmUp = Mapper.Map<Notification, NotificationBindingModel>(notification);
                    return Ok(nbmUp);
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

        // DELETE: api/Notifications/Notification/del
        [Route("notification/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nbm = new NotificationBindingModel
                    {
                        Id = id
                    };
                    var notification = Mapper.Map<NotificationBindingModel, Notification>(nbm);
                    _notificationService.Delete(notification.Id);

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

        // DELETE: api/Notifications/Notification/del
        [Route("notification/del")]
        public IHttpActionResult DeleteNotification(JObject jObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = jObj["notificationId"].ToString();
                    var notificationId = Guid.Parse(notification);

                    _notificationService.Delete(notificationId);

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
