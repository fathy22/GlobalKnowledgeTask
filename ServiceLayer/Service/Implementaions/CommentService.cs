﻿using AutoMapper;
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

        public async Task AddComment(CommentDto Comment)
        {
            var aut = _mapper.Map<Comment>(Comment);
            await _unitOfWork.GetRepository<Comment>().Add(aut);
            _unitOfWork.Save();
        }

        public async Task UpdateComment(CommentDto Comment)
        {
            var aut = _mapper.Map<Comment>(Comment);
            await _unitOfWork.GetRepository<Comment>().Update(aut);
            _unitOfWork.Save();
        }

        public async Task DeleteComment(int id)
        {
            var Comment=await GetCommentById(id);
            var aut = _mapper.Map<Comment>(Comment);
            await _unitOfWork.GetRepository<Comment>().Delete(aut);
            _unitOfWork.Save();
        }
    }
}
