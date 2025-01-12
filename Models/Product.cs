using BagAPI.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; } // Make this optional

    public Stock? Stock { get; set; } // Make this optional
    public ICollection<Review>? Reviews { get; set; } = new List<Review>();
}
