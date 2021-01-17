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
    [Authorize(Roles = "Admin")]
    public class PlaylistsController : ControllerBase
    {

        private readonly IPlaylistService _playlistsService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistsService = playlistService;
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<PlaylistDetail>))]
        [ProducesResponseType(404)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _playlistsService.GetSinglePlaylistAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound(); 
        }

        [ProducesResponseType(200, Type = typeof(CollectionResponse<PlaylistDetail>))]
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _playlistsService.GetAllPlaylists(pageNumber, pageSize);
            return Ok(result); 
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<PlaylistDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PlaylistDetail>))]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PlaylistDetail model)
        {
            var result = await _playlistsService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<PlaylistDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PlaylistDetail>))]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(PlaylistDetail model)
        {
            var result = await _playlistsService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result); 
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<PlaylistDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<PlaylistDetail>))]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _playlistsService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<string>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<string>))]
        [HttpPost("AssignOrRemoveVideo")]
        public async Task<IActionResult> AssignOrRemoveVideo([FromBody]PlaylistVideoRequest model)
        {
            var result = await _playlistsService.AssignOrRemoveVideoFromPlaylistAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result); 
        }

    }
}
