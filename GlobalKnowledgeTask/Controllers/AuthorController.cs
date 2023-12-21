using DomainLayer.DTO;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;

namespace GlobalKnowledgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IProfileImageService _profileImageService;

        public AuthorController(IAuthorService authorService, IProfileImageService profileImageService)
        {
            _authorService = authorService;
            _profileImageService = profileImageService;
        }

        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors =await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet]
        [Route("GetAuthorById/id")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author =await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public  async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            await _authorService.AddAuthor(authorDto);

            return Ok("Author created successfully");
        }

        [HttpPut]
        [Route("UpdateAuthor/id")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
        {
            authorDto.AuthorId = id;
           await _authorService.UpdateAuthor(authorDto);

            return Ok("Author updated successfully");
        }

        [HttpDelete]
        [Route("DeleteAuthor/id")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = _authorService.GetAuthorById(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

           await _authorService.DeleteAuthor(id);

            return Ok("Author deleted successfully");
        }

        [HttpPost("upload-image/{authorId}")]
        public async Task<IActionResult> UploadImage(int authorId, IFormFile file)
        {
            var imageId =await _profileImageService.UploadImage(file);

            if (imageId == -1)
            {
                // Handle invalid file
                return BadRequest("Invalid file");
            }

           await _profileImageService.UpdateAuthorImage(authorId, imageId);

            return Ok("Image uploaded and associated with the author.");
        }
    }

}
