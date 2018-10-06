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
using WebAPI.MVC.Utility;

namespace WebAPI.MVC.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            IEnumerable<ImageViewModel> images;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/images/all").Result;

            if (response.IsSuccessStatusCode)
            {
                images = response.Content.ReadAsAsync<IEnumerable<ImageViewModel>>().Result;
                return View(images.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Image
        public ActionResult GetImagesByUserId()
        {
            var client = GlobalWebApiClient.GetClient();
            //var response = client.GetAsync("api/Account/LoggedUser").Result;

            var responseProfile = client.GetAsync(@"api/profiles/profile/" + Session["Email"].ToString().EncodeBase64()).Result;

            if (responseProfile.IsSuccessStatusCode)
            {
                var userLogged = responseProfile.Content.ReadAsAsync<ProfileViewModel>().Result;

                if ((Session["Email"] == null) || !(userLogged.Id.ToString().Equals(Session["UserId"].ToString())))
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    IEnumerable<ImageViewModel> userImages;
                    //var client = GlobalWebApiClient.GetClient();
                    var response = client.GetAsync("/api/images/imagesbyuser").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        userImages = response.Content.ReadAsAsync<IEnumerable<ImageViewModel>>().Result;
                        ViewBag.PhotoCount = userImages.ToList().Count;
                        return PartialView(@"~/Views/Shared/_ImagesParcial.cshtml", userImages.ToList());
                    }
                    
                    ViewBag.Result = "Server Error. Please contact administrator!";
                    //return RedirectToAction("Index", "Gallery");
                    return PartialView(@"~/Views/Shared/_ImagesParcial.cshtml", ViewBag.Result);

                }
                
            }
            return RedirectToAction("Login", "Account");

        }

        // GET: Image/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/images/image/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                return View(image);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Image/Create
        public ActionResult CreateImage()
        {
            return View();
        }

        // POST: Image/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImage(TransportImageViewModel transport, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                transport.DetailsIVM.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);
                transport.DetailsIVM.GalleryId = transport.DetailsGVM.Id;
                transport.DetailsIVM.ProfileId = Guid.Parse(Session["UserId"].ToString());

                var client = GlobalWebApiClient.GetClient();
                
                var response = client.PostAsJsonAsync("/api/images/save/", transport.DetailsIVM).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //var gallery = responseGallery.Content.ReadAsAsync<TransportImageViewModel>().Result;
                        var ivm = response.Content.ReadAsAsync<ImageViewModel>().Result;                       

                        TempData["SuccessMessage"] = "Image created successfully";

                        return RedirectToAction("Details", "Gallery", transport.DetailsGVM);
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
            return RedirectToAction("Index", "Gallery");
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhotoUrl,Name,Type,Description,CreationDate," +
                                        "UpdateDate")] ImageViewModel image, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                image.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("/api/images/save/", image).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                        TempData["SuccessMessage"] = "Image created successfully";
                        return RedirectToAction("Details", image);
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
            return View(image);
        }

        // GET: Image/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/images/image/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                return View(image);
            }
            else
            {
                return View();
            }
        }

        // POST: Image/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhotoUrl,Name,Type,Description,CreationDate," +
                                    "UpdateDate")] ImageViewModel image, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                image.PhotoUrl = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("/api/images/update/", image).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                        TempData["SuccessMessage"] = "Image updated successfully";
                        return RedirectToAction("Details", image);
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
            return View(image);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/images/image/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                return View(image);
            }
            else
            {
                return View();
            }
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("/api/images/image/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var image = response.Content.ReadAsAsync<ImageViewModel>().Result;
                        TempData["SuccessMessage"] = "Image deleted successfully";
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
        //POST: Image/Upload
        public String Upload(HttpPostedFileBase file)
        {
            if (Session["Email"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        const String URI_ADDRESS = "api/images/upload";
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
            }

            return null;
        }
    }
}
