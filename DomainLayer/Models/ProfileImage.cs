using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ProfileImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public virtual Author Author { get; set; }
    }
}
