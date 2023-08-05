using Penna.Core.Utilities.Enums;
using System;

namespace Penna.Entities.DTOs
{
    public class OfferCloseDto
    {
        public int PurchaseTenderId { get; set; }
        public int PurchaseId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public double InvoiceAmount { get; set; }
        public PurchaseTypeEnum PurchaseType { get; set; }
        public string CompanyName { get; set; }
    }
}
