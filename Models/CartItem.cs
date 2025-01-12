namespace BagAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int? CartId { get; set; } 

        public int? ProductId { get; set; } 

        public int Quantity { get; set; } 

        public decimal TotalPrice => Quantity * UnitPrice; 

        public decimal UnitPrice { get; set; } 
    }
}
