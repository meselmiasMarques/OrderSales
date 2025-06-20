using System.ComponentModel.DataAnnotations;

namespace OrderSales.Core.Requests.Customers;

public class CustomerCreateRequest
{
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
    [Required(ErrorMessage = "Campo Nome obrigatório")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Campo Email obrigatório")]
    [EmailAddress(ErrorMessage = "Digite um Email válido")]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(11, ErrorMessage = "Telefone deve ter no Maxímo 11 caracteres")]
    public string Phone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Campo Endereço é obrigatório")]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string Address { get; set; } = string.Empty;
}