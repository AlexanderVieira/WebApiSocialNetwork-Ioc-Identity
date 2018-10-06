using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using WebAPI.MVC.Configurations;
using WebAPI.MVC.Models;
using WebAPI.MVC.Utility;

namespace WebAPI.MVC.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "/Token";
                
                try
                {
                    var client = GlobalWebApiClient.GetClient();

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", model.Email),
                        new KeyValuePair<string, string>("password", model.Password),
                    });

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();
                        GlobalWebApiClient.StoreToken(tokenResponse);

                        if (tokenResponse != null)
                        {                            
                            Session["Email"] = tokenResponse.Username.ToString();
                                                        
                            var clientUser = GlobalWebApiClient.GetClient();

                            var resultLoggedUser = clientUser.GetAsync("api/Account/LoggedUser").Result;
                            var resultLoggedEmail = clientUser.GetAsync(@"api/profiles/profile/" + Session["Email"].ToString().EncodeBase64()).Result;

                            if (!resultLoggedEmail.IsSuccessStatusCode)
                            {
                                if (resultLoggedUser.IsSuccessStatusCode)
                                {
                                    if (resultLoggedUser != null)
                                    {
                                        var user = await resultLoggedUser.Content.ReadAsAsync<ProfileViewModel>();
                                        Session["UserId"] = user.Id;
                                        return RedirectToAction("Create", "Profiles");
                                    }
                                }
                            }
                            else
                            {
                                var user = await resultLoggedEmail.Content.ReadAsAsync<ProfileViewModel>();
                                Session["UserId"] = user.Id;
                                return RedirectToAction("Home", "Profiles");
                            }
                        }
                        return View(model);
                    }

                    return View(model);
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }
            }

            return View(model);
        }

        // GET: /Account/Register        
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "api/Account/Register";

                try
                {
                    var client = GlobalWebApiClient.GetClient();
                    var dataJson = JsonConvert.SerializeObject(model);
                    var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("SuccessEmail", "Account");
                    }
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }
            }
            return View(model);
        }

        //GET: /Account/SuccessEmail        
        public ActionResult SuccessEmail()
        {
            ViewBag.Message = "Please check your e-mail and confirm your e-mail address.";
            return View();
        }

        //GET: /Account/ConfirmEmail        
        public ActionResult ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var confirmEmail = new ConfirmEmailViewModel()
            {
                UserId = userId,
                Code = code
            };

            return View();
        }

        // POST: /Account/ConfirmEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "api/Account/ConfirmEmail";

                try
                {
                    var client = GlobalWebApiClient.GetClient();
                    var dataJson = JsonConvert.SerializeObject(model);
                    var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var confirmEmailResponse = await response.Content.ReadAsAsync<ConfirmEmailViewModel>();
                        return RedirectToAction("Login", "Account");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //GET: /Account/ForgotPassword        
        public ActionResult ForgotPassword()
        {
            return View();
        }


        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "api/Account/ForgotPassword";

                try
                {
                    var client = GlobalWebApiClient.GetClient();
                    var dataJson = JsonConvert.SerializeObject(model);
                    var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var ForgotResponse = await response.Content.ReadAsAsync<ForgotPasswordViewModel>();
                        //return RedirectToAction("Login", "Account");
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET: /Account/ForgotPasswordConfirmation        
        public ActionResult ForgotPasswordConfirmation()
        {
            ViewBag.Message = "Please check your e-mail and confirm your e-mail address.";
            return View();
        }



        // GET: /Account/ResetPassword        
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "api/Account/ResetPassword";

                try
                {
                    var client = GlobalWebApiClient.GetClient();
                    var dataJson = JsonConvert.SerializeObject(model);
                    var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var ResetPasswordResponse = await response.Content.ReadAsAsync<ResetPasswordViewModel>();
                        return RedirectToAction("ResetPasswordConfirmation", "Account");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return View(model);
        }

        //GET: /Account/ResetPasswordConfirmation
        public ActionResult ResetPasswordConfirmation()
        {
            ViewBag.Message = "Password reset successfully!";
            return View();
        }

        //POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            if (ModelState.IsValid)
            {
                const String URI_ADDRESS = "api/Account/Logout";

                try
                {
                    var client = GlobalWebApiClient.GetClient();

                    var content = new StringContent("application/json");

                    var response = await client.PostAsync(URI_ADDRESS, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Session.Clear();
                        return RedirectToAction("Login", "Account");
                    }
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }
            }
            return View();
        }

    }
}