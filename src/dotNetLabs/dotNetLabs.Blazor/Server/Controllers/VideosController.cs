using dotNetLabs.Server.Services;
using dotNetLabs.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {

        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]VideoDetail model)
        {
            var result = await _videosService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result); 
        }

    }
}
