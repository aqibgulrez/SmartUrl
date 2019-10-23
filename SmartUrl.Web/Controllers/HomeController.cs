using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUrl.Entities.Domain;
using SmartUrl.Services;
using SmartUrl.Web.Models;

namespace SmartUrl.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortUrlService _shortUrlService;

        public HomeController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(HomeCreateRequestModel objHomeCreateUrlModel)
        {
            if (ModelState.IsValid)
            {
                var objShortUrl = await _shortUrlService.CreateSmartUrl(objHomeCreateUrlModel.Url, Request.Scheme, Request.Host.Value, Request.PathBase);

                return View("ShortUrlSuccess", new HomeCreateResponseModel() {
                    Url = objShortUrl.Url,
                    ShortUrl = objShortUrl.ShortUrl,
                    IsShortUrl = objShortUrl.IsShortUrl,
                    IsSuccess = objShortUrl.IsSuccess
                });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
