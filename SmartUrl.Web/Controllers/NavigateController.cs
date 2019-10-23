using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUrl.Services;

namespace SmartUrl.Web.Controllers
{
    [Produces("text/html")]
    [Route("/")]
    public class NavigateController : Controller
    {
        private readonly IShortUrlService _shortUrlService;

        public NavigateController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [ResponseCache(Duration = 60 * 60 * 48, Location = ResponseCacheLocation.Client)]
        [HttpGet("/{key}")]
        public async Task<IActionResult> NavigateTo(string key)
        {
            var data = await _shortUrlService.GetSmartUrl(key);

            if (data != null)
            {
                return Redirect(data.Url);
            }

            return View("UrlNotFound", key);
        }
    }
}