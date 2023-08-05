namespace Penna.Web.Models
{
    public class ErrorViewModel
    {
        public string PageTitle { get; set; }
        public string Email { get; set; }
        public int StatusCode { get; set; }
        public string ExceptionPath { get; set; }
        public string ExceptionMessage { get; set; }
        public string Stacktrace { get; set; }

    }
}
