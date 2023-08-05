using System.ComponentModel.DataAnnotations;

namespace Penna.DTO.WebDTOs
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        
        [Display(Name = "Stok Miktarı")]
        public int Stock { get; set; }
    }
}
