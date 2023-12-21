using System;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class ProfileImageDto
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public AuthorDto Author { get; set; }
    }
    public class CreateProfileImageDto
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
    }
}
