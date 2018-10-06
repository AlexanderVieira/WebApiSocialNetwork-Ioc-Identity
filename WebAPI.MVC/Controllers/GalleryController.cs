using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebAPI.MVC.Configurations;
using WebAPI.MVC.Models;

namespace WebAPI.MVC.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class GalleryController : Controller
    {
        //GET: Gallery
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<GalleryViewModel> galleries;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/galleries/all").Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    galleries = response.Content.ReadAsAsync<IEnumerable<GalleryViewModel>>().Result;
                    return View(galleries.ToList());
                }
                else
                {
                    ViewBag.Error = "Server Error. Please contact administrator!";
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return View();
        }

        // GET: Gallery/Details/5
        [HttpGet]
        public ActionResult Details(Guid? id)
        {
            try
            {
                var client = GlobalWebApiClient.GetClient();
                var responseGallery = client.GetAsync("api/galleries/gallery/info/" + id.ToString()).Result;

                if (responseGallery.IsSuccessStatusCode)
                {
                    var gallery = responseGallery.Content.ReadAsAsync<GalleryViewModel>().Result;

                    //var client = GlobalWebApiClient.GetClient();
                    var response = client.GetAsync("/api/images/imagesbyuser").Result;
                    var responseUser = client.GetAsync("/api/Account/LoggedUser").Result;

                    if (responseUser.IsSuccessStatusCode && response.IsSuccessStatusCode)
                    {
                        IEnumerable<ImageViewModel> userImages;
                        userImages = response.Content.ReadAsAsync<IEnumerable<ImageViewModel>>().Result;
                        var loggedUser = responseUser.Content.ReadAsAsync<ProfileViewModel>().Result;                        

                        if (loggedUser.Id.ToString().Equals(Session["UserId"].ToString()))
                        {                            
                            ViewBag.PhotoCount = userImages.ToList().Count;
                            ViewBag.ImagesByUser = userImages.ToList();
                            ViewBag.LoggedUser = true;

                            var transport = new TransportImageViewModel
                            {
                                DetailsGVM = gallery
                            };
                            

                            return View(transport);
                        }
                        else
                        {
                            ViewBag.LoggedUser = false;
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return View();
        }

        // GET: Gallery/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryViewModel gallery, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {                
                var client = GlobalWebApiClient.GetClient();
               
                var photoUrl = Upload(file);
                gallery.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);
                gallery.ProfileId = Guid.Parse(Session["UserId"].ToString());

                var response = client.PostAsJsonAsync("api/galleries/save/", gallery).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        gallery = response.Content.ReadAsAsync<GalleryViewModel>().Result;

                        var transport = new TransportImageViewModel
                        {
                            DetailsGVM = gallery
                        };

                        TempData["SuccessMessage"] = "Gallery created successfully";
                        return RedirectToAction("Details", transport.DetailsGVM);
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
            return View(gallery);
        }

        // GET: Gallery/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/galleries/gallery/info/" + id.ToString()).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var gallery = response.Content.ReadAsAsync<GalleryViewModel>().Result;
                    //gallery.ProfileId = Guid.Parse(Session["ProfileId"].ToString());
                    return View(gallery);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message; ;
            }
            return View();
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryViewModel gallery, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                gallery.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("api/galleries/update/", gallery).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        gallery = response.Content.ReadAsAsync<GalleryViewModel>().Result;
                        TempData["SuccessMessage"] = "Gallery updated successfully";
                        return RedirectToAction("Details", gallery);
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
            return View(gallery);
        }

        // GET: Gallery/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/galleries/gallery/info/" + id.ToString()).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var gallery = response.Content.ReadAsAsync<GalleryViewModel>().Result;
                    return View(gallery);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message; ;
            }
            return View();
        }

        // POST: Gallery/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid? id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("api/galleries/gallery/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Gallery deleted successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    var result = ex.Message; ;
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View();
        }

        // POST: Gallery/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public String Upload(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    const String URI_ADDRESS = "api/galleries/upload/";
                    var client = GlobalWebApiClient.GetClient();

                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] Bytes = new byte[file.InputStream.Length + 1];
                        file.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
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
