using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Penna.Business.Interfaces;
using Penna.Core.Utilities.Constants;
using Penna.Entities.DTOs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteImage(string path, string imgName)
        {
            if (File.Exists(Path.Combine(path, imgName)))
            {
                File.Delete(Path.Combine(path, imgName));
            }
        }
        public void DeleteImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public async Task<ResponseImageDto> UploadImageAsync(string path, IFormFile file)
        {
            if (file != null)
            {
                path = _webHostEnvironment.WebRootPath + path;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName); // file.ContentType.Trim(); debug ile bak
                FileStream stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
                stream.Dispose();

                return new ResponseImageDto() { ImageUrl = path, NewName = newFileName, RealName = file.FileName, ContentType = Path.GetExtension(file.FileName) };
            }
            return null;
        }


        public void DeleteProfileImage(string imgName)
        {
            string serverFolder = _webHostEnvironment.WebRootPath + SD.ProfileImagePath;
            this.DeleteImage(Path.Combine(serverFolder, imgName));
        }

        public async Task<ResponseImageDto> UploadProfileImageAsync(IFormFile file, string oldName)
        {
            if (!string.IsNullOrEmpty(oldName))
            {
                this.DeleteProfileImage(oldName); // eski resmi sil
            }
            
            return await this.UploadImageAsync(SD.ProfileImagePath, file);
        }
    }
}
