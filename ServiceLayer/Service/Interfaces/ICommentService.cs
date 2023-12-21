﻿using DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAuthors();
        Task<AuthorDto> GetAuthorById(int id);
        Task AddAuthor(AuthorDto author);
        Task UpdateAuthor(AuthorDto author);
        Task DeleteAuthor(int id);
    }
}
