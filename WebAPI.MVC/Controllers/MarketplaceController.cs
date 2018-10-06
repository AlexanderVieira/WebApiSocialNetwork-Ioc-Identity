using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAPI.MVC.Models;
using WebAPI.MVC.Configurations;
using System.Net.Http;

namespace WebAPI.MVC.Controllers
{
    public class MarketplaceController : Controller
    {       

        // GET: Marketplace
        public ActionResult Index()
        {
            IEnumerable<MarketplaceViewModel> marketplaces;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/marketplaces/all").Result;

            if (response.IsSuccessStatusCode)
            {
                marketplaces = response.Content.ReadAsAsync<IEnumerable<MarketplaceViewModel>>().Result;
                return View(marketplaces.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Marketplace/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/marketplaces/marketplace/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                return View(marketplace);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Marketplace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marketplace/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,CreationDate,UpdateDate")] MarketplaceViewModel marketplace)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("/api/marketplaces/save/", marketplace).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                        TempData["SuccessMessage"] = "Marketplace created successfully";
                        return RedirectToAction("Details", marketplace);
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
            return View(marketplace);
        }

        // GET: Marketplace/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/marketplaces/marketplace/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                return View(marketplace);
            }
            else
            {
                return View();
            }
        }

        // POST: Marketplace/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,CreationDate,UpdateDate")] MarketplaceViewModel marketplace)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("/api/marketplaces/update/", marketplace).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                        TempData["SuccessMessage"] = "Marketplace updated successfully";
                        return RedirectToAction("Details", marketplace);
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
            return View(marketplace);
        }

        // GET: Marketplace/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/marketplaces/marketplace/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                return View(marketplace);
            }
            else
            {
                return View();
            }
        }

        // POST: Marketplace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("/api/marketplaces/marketplace/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var marketplace = response.Content.ReadAsAsync<MarketplaceViewModel>().Result;
                        TempData["SuccessMessage"] = "Marketplace deleted successfully";
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
