using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CRUD_Application.Models;
using System.Text;

namespace CRUD_Application.Controllers
{
    public class AccountsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Account> accounts = new List<Account>();
            string apiUrl = "http://localhost:5188/api/BankAPI/";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    accounts = JsonConvert.DeserializeObject<List<Account>>(result);
                }
                else
                {
                    ViewBag.ErrorMessage = $"Unable to retrieve data from the API. Status Code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Create(Account account)
        {
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            string data = JsonConvert.SerializeObject(account, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd" // Format for DateTime
            });
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(apiUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Account successfully added.";
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Account account= new Account();
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            HttpResponseMessage response =  _httpClient.GetAsync(apiUrl+id).Result;
            if (response.IsSuccessStatusCode)
                {
                    string result =  response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Account>(result);
                    if(data!=null){
                        account=data;
                    }
                }

            return View(account);
        }

         [HttpPost]
        public  IActionResult Edit(Account account)
        {
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            string data = JsonConvert.SerializeObject(account, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd" // Format for DateTime
            });
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(apiUrl+account.AccountNumber, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Account Updated added.";
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Account account= new Account();
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            HttpResponseMessage response =  _httpClient.GetAsync(apiUrl+id).Result;
            if (response.IsSuccessStatusCode)
                {
                    string result =  response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Account>(result);
                    if(data!=null){
                        account=data;
                    }
                }

            return View(account);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Account account= new Account();
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            HttpResponseMessage response =  _httpClient.GetAsync(apiUrl+id).Result;
            if (response.IsSuccessStatusCode)
                {
                    string result =  response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Account>(result);
                    if(data!=null){
                        account=data;
                    }
                }

            return View(account);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            string apiUrl = "http://localhost:5188/api/BankAPI/";
            HttpResponseMessage response =  _httpClient.DeleteAsync(apiUrl+id).Result;
            if (response.IsSuccessStatusCode)
                {
                    TempData["delete_message"] = "Account Deleted added.";
                    return RedirectToAction("Index");
                }

            return View();
        }

    }
}
