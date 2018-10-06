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

namespace WebAPI.MVC.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            IEnumerable<GroupViewModel> groups;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/groups/all").Result;

            if (response.IsSuccessStatusCode)
            {
                groups = response.Content.ReadAsAsync<IEnumerable<GroupViewModel>>().Result;
                return View(groups.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Group/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/groups/group/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var group = response.Content.ReadAsAsync<GroupViewModel>().Result;
                return View(group);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "Id,Name,Description,CreationDate,UpdateDate,Actived")] GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("/api/groups/save/", group).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        group = response.Content.ReadAsAsync<GroupViewModel>().Result;
                        TempData["SuccessMessage"] = "Group created successfully";
                        return RedirectToAction("Details", group);
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
            return View(group);
        }

        // GET: Group/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/groups/group/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var group = response.Content.ReadAsAsync<GroupViewModel>().Result;
                return View(group);
            }
            else
            {
                return View();
            }
        }

        // POST: Group/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreationDate,UpdateDate,Actived")] GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("/api/groups/update/", group).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        group = response.Content.ReadAsAsync<GroupViewModel>().Result;
                        TempData["SuccessMessage"] = "Group updated successfully";
                        return RedirectToAction("Details", group);
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
            return View(group);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("/api/groups/group/info/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var group = response.Content.ReadAsAsync<GroupViewModel>().Result;
                return View(group);
            }
            else
            {
                return View();
            }
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("/api/groups/group/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var group = response.Content.ReadAsAsync<GroupViewModel>().Result;
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
        
    }
}
