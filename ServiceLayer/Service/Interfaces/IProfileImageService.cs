using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Interfaces
{
    public interface IProfileImageService
    {
         Task<int> UploadImage(IFormFile file);
        Task UpdateAuthorImage(int authorId, int imageId);
    }
}
