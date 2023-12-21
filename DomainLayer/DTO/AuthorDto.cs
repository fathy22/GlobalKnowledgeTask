using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

namespace DomainLayer.DTO
{
    public class AuthorDto
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public  IFormFile image { get; set; }


        public int? ProfileImageId { get; set; }

        public  ProfileImageDto ProfileImage { get; set; }

        public virtual List<BlogDto> Blogs { get; set; }
    }
}
