using System;
using System.Collections.Generic;

namespace BagAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<CartItem>? CartItems { get; set; } = new List<CartItem>(); 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}