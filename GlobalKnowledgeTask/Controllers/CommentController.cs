using DomainLayer.DTO;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Implementaions;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;

namespace GlobalKnowledgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _CommentService;
        private readonly IProfileImageService _profileImageService;

        public CommentController(ICommentService CommentService, IProfileImageService profileImageService)
        {
            _CommentService = CommentService;
            _profileImageService = profileImageService;
        }

        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var Comments =await _CommentService.GetAllComments();
            return Ok(Comments);
        }

        [HttpGet]
        [Route("GetCommentById/id")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var Comment =await _CommentService.GetCommentById(id);

            if (Comment == null)
            {
                return NotFound();
            }

            return Ok(Comment);
        }

        [HttpPost]
        [Route("CreateComment")]
        public  async Task<IActionResult> CreateComment([FromBody] CreateCommentDto CommentDto)
        {
            await _CommentService.AddComment(CommentDto);

            return Ok("Comment created successfully");
        }

        [HttpPut]
        [Route("UpdateComment/id")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto comment)
        {
            comment.CommentId = id;
            await _CommentService.UpdateComment(comment);

            return Ok("Comment updated successfully");
        }

        [HttpDelete]
        [Route("DeleteComment/id")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var existingComment = await _CommentService.GetCommentById(id);

            if (existingComment == null)
            {
                return NotFound();
            }

           await _CommentService.DeleteComment(id);

            return Ok("Comment deleted successfully");
        }
    }

}
