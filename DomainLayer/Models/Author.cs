using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DomainLayer.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public int? ProfileImageId { get; set; }

        [ForeignKey("ProfileImageId")]
        public virtual ProfileImage ProfileImage { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
