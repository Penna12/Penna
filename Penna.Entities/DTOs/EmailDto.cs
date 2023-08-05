using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class EmailDto
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
