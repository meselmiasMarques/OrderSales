using System.Text.Json.Serialization;

namespace OrderSales.Core.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    [JsonIgnore]
    public int active { get; set; } = 1; // 1 for active, 0 for inactive
}