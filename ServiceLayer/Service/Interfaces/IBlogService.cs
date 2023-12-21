using DomainLayer.DTO;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Interfaces
{
    public interface IBlogService
    {
        Task<PagedResultDto<BlogDto>> GetAllBlogs(int page = 1, int pageSize = 10);
        Task<BlogDto> GetBlogById(int id);
        Task AddBlog(CreateBlogDto Blog);
        Task UpdateBlog(UpdateBlogDto Blog);
        Task DeleteBlog(int id);
        Task<List<BlogDto>> GetBlogsWithAuthorAsync();
        Task<List<CommentDto>> GetBlogCommentsAsync(int blogId);
        Task<List<Blog>> FilterBlogsAsync(string keyword, int? authorId, string sortByComments);
    }
}
