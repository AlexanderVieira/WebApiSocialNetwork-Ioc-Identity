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
    public class NotificationController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            IEnumerable<NotificationViewModel> notifications;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/notifications/all").Result;

            if (response.IsSuccessStatusCode)
            {
                notifications = response.Content.ReadAsAsync<IEnumerable<NotificationViewModel>>().Result;
                return View(notifications.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Notifications/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/notifications/notification/info/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var nvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                return View(nvm);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotificationViewModel nvm)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("api/notifications/save/", nvm).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        nvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                        TempData["SuccessMessage"] = "Notification created successfully";
                        return RedirectToAction("Details", nvm);
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
            return View(nvm);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/notifications/notification/info/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var nvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                return View(nvm);
            }
            else
            {
                return View();
            }
        }

        // POST: Notifications/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NotificationViewModel nvm)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("/api/notifications/update/", nvm).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        nvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                        TempData["SuccessMessage"] = "Notification updated successfully";
                        return RedirectToAction("Details", nvm);
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
            return View(nvm);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/notifications/notification/del/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var nvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                return View(nvm);
            }
            else
            {
                return View();
            }
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("api/notifications/notification/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var pvm = response.Content.ReadAsAsync<NotificationViewModel>().Result;
                        TempData["SuccessMessage"] = "Notification deleted successfully";
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
            return View();
        }
    }
}