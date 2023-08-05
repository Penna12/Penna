﻿namespace Penna.Entities.DTOs
{
    public class SignUpUserModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public int TenantId { get; set; }
        public string Role { get; set; }
    }
}
