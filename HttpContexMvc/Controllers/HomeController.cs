using HttpContexMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpContexMvc.Controllers
{
    public class HomeController : Controller
    {
        HttpClientHandler _httpClientHandler = new HttpClientHandler();

        public HomeController()
        {
            _httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public IActionResult Index()
        {

            HttpClient httpClient = new HttpClient(_httpClientHandler);

            var response = httpClient.GetAsync(/*$"https://localhost:number/api/Controller"*/).Result;
            List<Users> users = null;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                users = JsonConvert.DeserializeObject<List<Users>>(response.Content.ReadAsStringAsync().Result);
            }

            return View(users);
        }


        public IActionResult Add()
        {
            return View(new Users());
        }

        [HttpPost]
        public IActionResult Add(Users users)
        {
            HttpClient httpClient = new HttpClient(_httpClientHandler);
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var res = httpClient.PostAsync(/*$"https://localhost:number/api/Controller"*/, stringContent).Result;

            if (res.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Error for adding");
            return View();
        }

        public IActionResult Edit(int id)
        {
            HttpClient httpClient = new HttpClient(_httpClientHandler);

            var res = httpClient.GetAsync(/*$"https://localhost:number/api/Controller/{id}"*/).Result;

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var user = JsonConvert.DeserializeObject<Users>(res.Content.ReadAsStringAsync().Result);

                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Users users)
        {
            HttpClient httpClient = new HttpClient(_httpClientHandler);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

            var res = httpClient.PutAsync(/*$"https://localhost:number/api/Controller/{users.Id}"*/, stringContent).Result;

            //if (res.StatusCode == System.Net.HttpStatusCode.NoContent)
            //{

            //}
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int id)
        {
            HttpClient httpClient = new HttpClient(_httpClientHandler);

            var res = httpClient.DeleteAsync(/*$"https://localhost:number/api/Controller/{id}"*/).Result;

            return RedirectToAction("Index");
        }
    }
}
