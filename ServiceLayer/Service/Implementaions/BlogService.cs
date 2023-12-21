using AutoMapper;
using DomainLayer.DTO;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceLayer.Service.Implementaions
{
    public class BlogService: IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PagedResultDto<BlogDto>> GetAllBlogs(int page = 1, int pageSize = 10)
        {
            var query = _unitOfWork.GetRepository<Blog>().GetAll().Result
                .AsQueryable()
                .Include(blog => blog.Author);

            var totalCount = await query.CountAsync();
            var blogs = await query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var blogDtos = _mapper.Map<List<BlogDto>>(blogs);

            return new PagedResultDto<BlogDto>
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = blogDtos
            };
        }

        public async Task<BlogDto> GetBlogById(int id)
        {
            var Blog = await _unitOfWork.GetRepository<Blog>().GetById(id);
            return _mapper.Map<BlogDto>(Blog);

        }

        public async Task AddBlog(CreateBlogDto Blog)
        {
            var aut = _mapper.Map<Blog>(Blog);
            await _unitOfWork.GetRepository<Blog>().Add(aut);
            _unitOfWork.Save();
        }

        public async Task UpdateBlog(UpdateBlogDto blog)
        {
            try
            {
                var existingBlog = await _unitOfWork.GetRepository<Blog>().GetById(blog.BlogId);

                if (existingBlog == null)
                {
                    return;
                }
                _mapper.Map(blog, existingBlog);

                await _unitOfWork.GetRepository<Blog>().Update(existingBlog);

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task DeleteBlog(int id)
        {
            var Blog=await GetBlogById(id);
            var aut = _mapper.Map<Blog>(Blog);
            await _unitOfWork.GetRepository<Blog>().Delete(aut);
            _unitOfWork.Save();
        }
        public async Task<List<BlogDto>> GetBlogsWithAuthorAsync()
        {
            var blogs= await _unitOfWork.GetRepository<Blog>().GetAll().Result
                .AsQueryable()
                .Include(blog => blog.Author)
                .ToListAsync();
            return _mapper.Map<List<BlogDto>>(blogs);
        }
        public async Task<List<CommentDto>> GetBlogCommentsAsync(int blogId)
        {
            var comments = await _unitOfWork.GetRepository<Comment>().GetAll().Result
              .AsQueryable()
              .Where(c => c.BlogId == blogId)
              .Include(c => c.Author)
              .ToListAsync();
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<List<Blog>> FilterBlogsAsync(string keyword, int? authorId, string sortByComments)
        {
            var blogs = _unitOfWork.GetRepository<Blog>().GetAll().Result
                .AsQueryable()
                .Include(blog => blog.Author)
                .Include(blog => blog.Comments)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                blogs = blogs.Where(b => b.Title.Contains(keyword) || b.Content.Contains(keyword));
            }

            if (authorId.HasValue)
            {
                blogs = blogs.Where(b => b.AuthorId == authorId.Value);
            }

            if (!string.IsNullOrEmpty(sortByComments))
            {
                blogs = sortByComments.ToLower() == "asc"
                    ? blogs.OrderBy(b => b.Comments.Count)
                    : blogs.OrderByDescending(b => b.Comments.Count);
            }

            return await blogs.ToListAsync();
        }
    }
}
