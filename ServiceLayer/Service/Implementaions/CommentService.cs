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

namespace ServiceLayer.Service.Implementaions
{
    public class CommentService: ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CommentDto>> GetAllComments()
        {
            var Comments = await _unitOfWork.GetRepository<Comment>().GetAll().Result
               .AsQueryable()
               .Include(c => c.Author)
               .Include(c => c.Blog)
               .ToListAsync();
            return _mapper.Map<List<CommentDto>>(Comments);
        }

        public async Task<CommentDto> GetCommentById(int id)
        {
            var Comment = await _unitOfWork.GetRepository<Comment>().GetById(id);
            return _mapper.Map<CommentDto>(Comment);

        }

        public async Task AddComment(CreateCommentDto Comment)
        {
            var aut = _mapper.Map<Comment>(Comment);
            await _unitOfWork.GetRepository<Comment>().Add(aut);
            _unitOfWork.Save();
        }

        public async Task UpdateComment(UpdateCommentDto comment)
        {
            try
            {
                var existingComment = await _unitOfWork.GetRepository<Comment>().GetById(comment.CommentId);

                if (existingComment == null)
                {
                    return;
                }
                _mapper.Map(comment, existingComment);

                await _unitOfWork.GetRepository<Comment>().Update(existingComment);

                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task DeleteComment(int id)
        {
            var existingComment = await _unitOfWork.GetRepository<Comment>().GetById(id);

            if (existingComment == null)
            {
                return;
            }
            await _unitOfWork.GetRepository<Comment>().Delete(existingComment);
            _unitOfWork.Save();
        }
    }
}
