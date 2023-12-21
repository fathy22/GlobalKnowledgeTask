
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        public string Text { get; set; }
        public int AuthorId { get; set; }   

        public AuthorDto Author { get; set; }
        public int BlogId { get; set; }

        public virtual BlogDto Blog { get; set; }
    }
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
    }

    public class UpdateCommentDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
    }
}
