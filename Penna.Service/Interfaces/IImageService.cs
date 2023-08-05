using Microsoft.AspNetCore.Http;
using Penna.Entities.DTOs;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IImageService
    {
        Task<ResponseImageDto> UploadImageAsync(string path, IFormFile file); // Image upload yap, yeni ismi geri dön
        void DeleteImage(string path, string imgName);
        void DeleteImage(string path);


        Task<ResponseImageDto> UploadProfileImageAsync(IFormFile file, string oldName); // Image upload yap, yeni ismi geri dön
        void DeleteProfileImage(string imgName);
        
    }
}
