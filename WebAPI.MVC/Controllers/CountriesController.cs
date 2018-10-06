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
    public class CountriesController : Controller
    {

        // GET: Countries
        public ActionResult Index()
        {
            IEnumerable<CountryViewModel> countries;
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/countries/all").Result;

            if (response.IsSuccessStatusCode)
            {
                countries = response.Content.ReadAsAsync<IEnumerable<CountryViewModel>>().Result;
                return View(countries.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Countries/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/countries/country/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                return View(country);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Flag")] CountryViewModel country, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = Upload(file);
                country.Flag = photoUrl.Substring(1, photoUrl.Length-2);

                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PostAsJsonAsync("api/countries/save/", country).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                        TempData["SuccessMessage"] = "Country created successfully";
                        return RedirectToAction("Details", country.Id);
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
            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/countries/country/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                return View(country);
            }
            else
            {
                return View();
            }
        }

        // POST: Countries/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Flag")] CountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PutAsJsonAsync("/api/countries/update/", country).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                        TempData["SuccessMessage"] = "Country updated successfully";
                        return RedirectToAction("Details", country);
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/countries/country/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                return View(country);
            }
            else
            {
                return View();
            }
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.DeleteAsync("api/countries/country/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var country = response.Content.ReadAsAsync<CountryViewModel>().Result;
                        TempData["SuccessMessage"] = "Country deleted successfully";
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
        //POST: Countries/Upload
        public String Upload(HttpPostedFileBase file)
        {
            if (Session["Token"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        const String URI_ADDRESS = "api/countries/upload";
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
