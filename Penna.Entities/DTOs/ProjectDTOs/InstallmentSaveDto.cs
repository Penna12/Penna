using System;

namespace Penna.Entities.DTOs
{
    public class InstallmentSaveDto
    {
        public byte TaksitNo { get; set; }
        public DateTime TaksitTarihi { get; set; }
        public string Aciklama { get; set; }
        public double TaksitTutari { get; set; }
    }

    public class InstallmentListDto
    {
        public byte TaksitNo { get; set; }
        public DateTime TaksitTarihi { get; set; }
        public string Aciklama { get; set; }
        public double TaksitTutari { get; set; }
    }
}
