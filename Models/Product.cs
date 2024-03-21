namespace PatronusBazar.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double DiscountPercentage { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Thumbnail { get; set; }
        public List<string> Images { get; set; } // List to hold image URLs

        public Product()
        {
            Images = new List<string>();
        }
    }
}
