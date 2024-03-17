using System;

namespace PatronusBazar.Models
{
    public class CartItemDetails
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Add this property
        public decimal ProductPrice { get; set; } // Add this property
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
