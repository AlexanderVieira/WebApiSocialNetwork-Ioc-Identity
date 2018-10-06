using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using WebAPI.MVC.Configurations;
using WebAPI.MVC.Models;

namespace WebAPI.MVC.Controllers
{
    public class AddressesController : Controller
    {
        // GET: Addresses
        public ActionResult Index()
        {
            IEnumerable<AddressViewModel> addresses;
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/addresses/all").Result;

            if (response.IsSuccessStatusCode)
            {
                addresses = response.Content.ReadAsAsync<IEnumerable<AddressViewModel>>().Result;
                return View(addresses.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Addresses/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/addresses/address/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                return View(address);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Street,Number,Neighborhood,City,PostalCode")] AddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PostAsJsonAsync("api/addresses/save/", address).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                        TempData["SuccessMessage"] = "Address created successfully";
                        return RedirectToAction("Details", address);
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
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/addresses/address/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                return View(address);
            }
            else
            {
                return View();
            }
        }

        // POST: Addresses/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Street,Number,Neighborhood,City,PostalCode")] AddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.PutAsJsonAsync("api/addresses/update/", address).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                        TempData["SuccessMessage"] = "Address updated successfully";
                        return RedirectToAction("Details", address);
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClientRegion();
            var response = client.GetAsync("api/addresses/address/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                return View(address);
            }
            else
            {
                return View();
            }
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClientRegion();
                var response = client.DeleteAsync("api/addresses/address/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var address = response.Content.ReadAsAsync<AddressViewModel>().Result;
                        TempData["SuccessMessage"] = "Address deleted successfully";
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