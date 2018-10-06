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
    public class PostController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            IEnumerable<PostViewModel> posts;
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync("api/posts/all").Result;

            if (response.IsSuccessStatusCode)
            {
                posts = response.Content.ReadAsAsync<IEnumerable<PostViewModel>>().Result;
                return View(posts.ToList());
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/posts/post/info/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                return View(pvm);
            }
            else
            {
                ViewBag.Result = "Server Error. Please contact administrator!";
            }
            return View();
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PostAsJsonAsync("api/posts/save/", pvm).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                        TempData["SuccessMessage"] = "Post created successfully";
                        return RedirectToAction("Details", pvm);
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
            return View(pvm);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/posts/post/info/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                return View(pvm);
            }
            else
            {
                return View();
            }
        }

        // POST: Posts/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.PutAsJsonAsync("/api/posts/update/", pvm).Result;
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                        TempData["SuccessMessage"] = "Post updated successfully";
                        return RedirectToAction("Details", pvm);
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
            return View(pvm);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var client = GlobalWebApiClient.GetClient();
            var response = client.GetAsync($"api/posts/post/del/?id={id.ToString()}").Result;

            if (response.IsSuccessStatusCode)
            {
                var pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                return View(pvm);
            }
            else
            {
                return View();
            }
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = GlobalWebApiClient.GetClient();
                var response = client.DeleteAsync("api/posts/post/del/" + id.ToString()).Result;

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var pvm = response.Content.ReadAsAsync<PostViewModel>().Result;
                        TempData["SuccessMessage"] = "Post deleted successfully";
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