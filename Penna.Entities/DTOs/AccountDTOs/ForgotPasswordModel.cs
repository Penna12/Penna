using System.ComponentModel.DataAnnotations;

namespace Penna.Entities.DTOs
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress, Display(Name ="Registered email address")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
