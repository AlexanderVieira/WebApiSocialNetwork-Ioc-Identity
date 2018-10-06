using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPI.MVC.Models;
using WebAPI.MVC.Configurations;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAPI.MVC.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            IEnumerable<AnnouncementViewModel> announcements;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/announcements/all").Result;

            if (response.IsSuccessStatusCode)
            {
                announcements = response.Content.ReadAsAsync<IEnumerable<AnnouncementViewModel>>().Result;
                return View(announcements.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Announcement/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/announcements/announcement/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                return View(announcement);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcement/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Price,Actived,CreationDate," +
                                    "UpdateDate")] AnnouncementViewModel announcement, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                announcement.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);
                   
                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("api/announcements/save/", announcement).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                        TempData["SuccessMessage"] = "Announcement created successfully";
                        return RedirectToAction("Details", announcement);
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
            return View(announcement);
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/announcements/announcement/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                return View(announcement);
            }
            else
            {
                return View();
            }
        }

        // POST: Announcement/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Price,Actived,CreationDate," +
                                    "UpdateDate")] AnnouncementViewModel announcement, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                announcement.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("api/announcements/update/", announcement).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                        TempData["SuccessMessage"] = "Group updated successfully";
                        return RedirectToAction("Details", announcement);
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
            return View(announcement);
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/announcements/announcement/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                return View(announcement);
            }
            else
            {
                return View();
            }
        }

        // POST: Announcement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("api/announcements/announcement/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var announcement = response.Content.ReadAsAsync<AnnouncementViewModel>().Result;
                        TempData["SuccessMessage"] = "Group deleted successfully";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Announcement/upload
        public String Upload(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    const String URI_ADDRESS = "api/Announcements/upload";
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
            return null;
        }

    }
}
