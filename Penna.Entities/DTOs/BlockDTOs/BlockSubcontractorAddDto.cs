using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Penna.Entities.DTOs
{
    public class BlockSubcontractorAddDto : IDto
    {
        [Required(ErrorMessage = "Hesap seçiniz.")]
        public int CurrentAccountId { get; set; }
        public string CompanyRepresentative { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "iş grubu seçiniz.")]
        public BusinessGroupEnum[] BusinessGroupId { get; set; }

    }
}
