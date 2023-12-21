using DomainLayer.DTO;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;

namespace GlobalKnowledgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _BlogService;
        private readonly IProfileImageService _profileImageService;

        public BlogController(IBlogService BlogService, IProfileImageService profileImageService)
        {
            _BlogService = BlogService;
            _profileImageService = profileImageService;
        }

        [HttpGet]
        [Route("GetAllBlogs")]
        public async Task<IActionResult> GetAllBlogs(int page = 1, int pageSize = 10)
        {
            var blogs = await _BlogService.GetAllBlogs(page, pageSize);

            return Ok(blogs);
        }

        [HttpGet]
        [Route("GetBlogById/id")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var Blog = _BlogService.GetBlogById(id);

            if (Blog == null)
            {
                return NotFound();
            }

            return Ok(Blog);
        }

        [HttpPost]
        [Route("CreateBlog")]
        public  async Task<IActionResult> CreateBlog([FromBody] BlogDto BlogDto)
        {
            await _BlogService.AddBlog(BlogDto);

            return Ok("Blog created successfully");
        }

        [HttpPut]
        [Route("UpdateBlog/id")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogDto BlogDto)
        {
            var existingBlog = _BlogService.GetBlogById(id);

            if (existingBlog == null)
            {
                return NotFound();
            }
            BlogDto.BlogId = id;
           await _BlogService.UpdateBlog(BlogDto);

            return Ok("Blog updated successfully");
        }

        [HttpDelete]
        [Route("DeleteBlog/id")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var existingBlog = _BlogService.GetBlogById(id);

            if (existingBlog == null)
            {
                return NotFound();
            }

           await _BlogService.DeleteBlog(id);

            return Ok("Blog deleted successfully");
        }
        [HttpDelete]
        [Route("Search")]
        public async Task<IActionResult> FilterBlogs([FromQuery] string keyword,[FromQuery] int? authorId,[FromQuery] string sortByComments)
        {
            var blogs=await _BlogService.FilterBlogsAsync(keyword, authorId, sortByComments);
            return Ok(blogs);
        }

    }

}
