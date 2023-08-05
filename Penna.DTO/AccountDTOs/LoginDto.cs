namespace Penna.DTO.AccountDTOs
{
    public class LoginDto
    {
        //[Required(ErrorMessage = "Lütfen epostanızı giriniz")]
        //[EmailAddress]
        //[Display(Name = "Epostanız")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Şifreniz")]
        public string Password { get; set; }

        //[Display(Name = "Beni hatırla?")]
        public bool RememberMe { get; set; }
    }
}
