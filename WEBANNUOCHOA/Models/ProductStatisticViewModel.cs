//New

namespace WEBANNUOCHOA.Models
{
    public class ProductStatisticViewModel
    {
        public int ProductId { get; set; }
        public int TotalQuantity { get; set; }

        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime? PurchaseDate { get; set; }
    
    }
}
