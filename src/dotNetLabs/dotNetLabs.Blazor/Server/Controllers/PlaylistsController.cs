using dotNetLabs.Server.Services;
using dotNetLabs.Shared;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class PlaylistsController : ControllerBase
    {

        private readonly IPlaylistService _playlistsService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistsService = playlistService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _playlistsService.GetAllPlaylists(pageNumber, pageSize);
            return Ok(result); 
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(PlaylistDetail model)
        {
            var result = await _playlistsService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(PlaylistDetail model)
        {
            var result = await _playlistsService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result); 
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _playlistsService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
