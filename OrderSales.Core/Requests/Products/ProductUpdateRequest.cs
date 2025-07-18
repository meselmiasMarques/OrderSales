using System.ComponentModel.DataAnnotations;

namespace OrderSales.Core.Requests.Products;

public class ProductUpdateRequest
{
    public Guid Id { get; set; }

    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
    [Required(ErrorMessage = "Campo Nome obrigatório")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Defina um preço para o produto")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Campo Estoque obrigatório")]
    public int Stock { get; set; }

    public int Active { get; set; } = 1; // Default to active (1)
}