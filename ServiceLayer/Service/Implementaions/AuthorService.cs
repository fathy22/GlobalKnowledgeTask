using AutoMapper;
using DomainLayer.DTO;
using DomainLayer.Models;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Implementaions
{
    public class AuthorService: IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AuthorDto>> GetAllAuthors()
        {
            var authors=await _unitOfWork.GetRepository<Author>().GetAll();
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorById(int id)
        {
            var author = await _unitOfWork.GetRepository<Author>().GetById(id);
            return _mapper.Map<AuthorDto>(author);

        }

        public async Task AddAuthor(AuthorDto author)
        {
            var aut = _mapper.Map<Author>(author);
            await _unitOfWork.GetRepository<Author>().Add(aut);
            _unitOfWork.Save();
        }

        public async Task UpdateAuthor(AuthorDto author)
        {
            var aut = _mapper.Map<Author>(author);
            await _unitOfWork.GetRepository<Author>().Update(aut);
            _unitOfWork.Save();
        }

        public async Task DeleteAuthor(int id)
        {
            var author=await GetAuthorById(id);
            var aut = _mapper.Map<Author>(author);
            await _unitOfWork.GetRepository<Author>().Delete(aut);
            _unitOfWork.Save();
        }
    }
}
