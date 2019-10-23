using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartUrl.Web.Controllers
{
    [Produces("text/html")]
    [Route("/")]
    public class NavigateController : Controller
    {
        [ResponseCache(Duration = 60 * 60 * 48, Location = ResponseCacheLocation.Client)]
        [HttpGet("/{key}")]
        public IActionResult NavigateTo(string key)
        {
            //var data = await _database.GetData(key);
            //if (data != null && (!data.ExpiresUtc.HasValue || DateTime.UtcNow < data.ExpiresUtc.Value))
            //{
            //    await _database.LogAccess(key, Request.HttpContext.Connection.RemoteIpAddress);
            //    var url = UrlKeyReplacementRegex.Replace(data.Url, key);
            //    return Redirect(url);
            //}

            return View("UrlNotFound", key);
        }
    }
}