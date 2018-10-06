using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebAPI.MVC.Models;

namespace WebAPI.MVC.Configurations
{
    public static class GlobalWebApiClient
    {
        public const String URI_BASE = "http://localhost:49640/";
        public const String URI_BASE_REGION = "http://localhost:49753/";
        //public static readonly HttpClient client = new HttpClient();

        public static HttpClient GetClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(URI_BASE)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            var session = HttpContext.Current.Session;
            if (session["Token"] != null)
            {
                TokenResponse tokenResponse = GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.AccessToken);
            }
            return client;
        }

        public static HttpClient GetClientRegion()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(URI_BASE_REGION)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            var session = HttpContext.Current.Session;
            if (session["Token"] != null)
            {
                TokenResponse tokenResponse = GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.AccessToken);
            }
            return client;
        }

        public static void StoreToken(TokenResponse token)
        {
            var session = HttpContext.Current.Session;
            session["Token"] = token;
        }
        public static TokenResponse GetToken()
        {
            var session = HttpContext.Current.Session;
            return (TokenResponse)session["Token"];
        }

    }
}