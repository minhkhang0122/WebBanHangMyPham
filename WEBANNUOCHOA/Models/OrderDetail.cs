using System.ComponentModel.DataAnnotations;

namespace WEBANNUOCHOA.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]

        public decimal Price { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
