using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Service.Interfaces;
using ServiceLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLayer.Service.Implementaions
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _uploadFolderPath = "wwwroot/images"; // Path to the folder where images will be stored

        public ProfileImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // Handle invalid file
                return -1;
            }

            // Generate a unique filename to avoid conflicts
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(_uploadFolderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Save image information to the Image table
            var image = new ProfileImage
            {
                ImageName = uniqueFileName
            };
            await _unitOfWork.GetRepository<ProfileImage>().Add(image);

            return image.ImageId;
        }

        public async Task UpdateAuthorImage(int authorId, int imageId)
        {
            var author = await _unitOfWork.GetRepository<Author>().GetById(authorId);
            if (author != null)
            {
                author.ProfileImageId = imageId;
                await _unitOfWork.GetRepository<Author>().Update(author);
            }
        }
    }

}
