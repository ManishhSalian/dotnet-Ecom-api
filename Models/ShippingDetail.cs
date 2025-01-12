namespace BagAPI.Models
{
    public class ShippingDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; } 

        public string? Address { get; set; }

        public string? City { get; set; } 

        public string? PostalCode { get; set; } 

        public string? Country { get; set; }

        public string? ShippingMethod { get; set; } 

        public Order? Order { get; set; }
    }
}