using dotNetLabs.Server.Services;
using dotNetLabs.Shared;
using Microsoft.AspNetCore.Authorization;
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
    public class CommentsController : ControllerBase
    {

        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService; 
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<CommentDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CommentDetail>))]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CommentDetail model)
        {
            var result = await _commentsService.CreateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<CommentDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CommentDetail>))]
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CommentDetail model)
        {
            var result = await _commentsService.EditAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<CommentDetail>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<CommentDetail>))]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _commentsService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
