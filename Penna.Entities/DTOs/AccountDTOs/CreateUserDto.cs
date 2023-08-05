using Penna.Core.Entities;
using Penna.Entities.Models;

namespace Penna.Entities.DTOs
{
    public class CreateUserDto : IDto
    {
        public AppUser AppUser { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
