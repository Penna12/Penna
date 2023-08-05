using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class ChangePasswordDto : IDto
    {
        public AppUserInfoDto AppUserInfo { get; set; }
        public ChangePasswordModel ChangePassword { get; set; }
    }
}
