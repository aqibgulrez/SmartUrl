using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartUrl.Entities.Domain;
using SmartUrl.Services;

namespace SmartUrl.Api.Controllers
{
    public class SmartUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public SmartUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("create/{id}")]
        public async Task<IActionResult> Create([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            
            var objShortUrl = await _shortUrlService.CreateSmartUrl(id);

            if (objShortUrl != null)
                return Ok(objShortUrl);

            return NotFound();
        }

    }
}
