using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Text { get; set; }
        public int AuthorId { get; set; }   

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
        public int BlogId { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
    }
}
