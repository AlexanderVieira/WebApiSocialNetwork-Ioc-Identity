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
    public class StatesController : Controller
    {
        // GET: States
        public ActionResult Index()
        {
            IEnumerable<StateViewModel> states;
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/states/all").Result;

            if (response.IsSuccessStatusCode)
            {
                states = response.Content.ReadAsAsync<IEnumerable<StateViewModel>>().Result;
                return View(states.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: States/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/states/state/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var state = response.Content.ReadAsAsync<StateViewModel>().Result;
                return View(state);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: States/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Flag")] StateViewModel state, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                state.Flag = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PostAsJsonAsync("api/states/save/", state).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        state = response.Content.ReadAsAsync<StateViewModel>().Result;
                        TempData["SuccessMessage"] = "State created successfully";
                        return RedirectToAction("Details", state);
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
            return View(state);
        }

        // GET: States/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/states/state/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var state = response.Content.ReadAsAsync<StateViewModel>().Result;
                return View(state);
            }
            else
            {
                return View();
            }
        }

        // POST: States/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Flag")] StateViewModel state, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                state.Flag = photoUrl.Substring(1, photoUrl.Length - 2);

                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PutAsJsonAsync("api/states/update/", state).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        state = response.Content.ReadAsAsync<StateViewModel>().Result;
                        TempData["SuccessMessage"] = "State updated successfully";
                        return RedirectToAction("Details", state);
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
            return View(state);
        }

        // GET: States/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/states/state/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var state = response.Content.ReadAsAsync<StateViewModel>().Result;
                return View(state);
            }
            else
            {
                return View();
            }
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.DeleteAsync("api/states/state/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var state = response.Content.ReadAsAsync<StateViewModel>().Result;
                        TempData["SuccessMessage"] = "State deleted successfully";
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
        //POST: States/Upload
        public String Upload(HttpPostedFileBase file)
        {
            if (Session["Token"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        const String URI_ADDRESS = "api/states/upload";
                        var client = GlobalWebApiClient.GetClientRegion();

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
