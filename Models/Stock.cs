public class Stock
{
    public int Id { get; set; } 
    public int? ProductId { get; set; } 
    public Product? Product { get; set; } // Make this optional
    public int StockLevel { get; set; }
}
