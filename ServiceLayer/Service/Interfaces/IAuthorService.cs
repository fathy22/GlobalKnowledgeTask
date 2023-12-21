using DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllComments();
        Task<CommentDto> GetCommentById(int id);
        Task AddComment(CreateCommentDto Comment);
        Task UpdateComment(UpdateCommentDto Comment);
        Task DeleteComment(int id);
    }
}
