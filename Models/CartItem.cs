namespace PatronusBazar.Models
{
    public class CartItem
    {
        public int ProductId { get; set; } // Id of the product
        public int UserId { get; set; } // Id of the user
        public int Quantity { get; set; } // Quantity of the product in the cart
    }
}
