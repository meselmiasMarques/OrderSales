
using Microsoft.AspNetCore.Components;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;
using System.Security.Cryptography;


namespace OrderSales.Web.Pages.Products
{
    public partial class ListProductsPage : ComponentBase 
    {

        #region PROPRIEDADES
        public List<Product> Products { get; set; } = new ();

        public bool IsLoading = true;
        public string? ErrorMessage;
        #endregion

        #region SERVIÇOS

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        #endregion
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var request = new ProductGetAllRequest();
                var result = await ProductService.GetAllAsync(request);

                if (result.IsSuccess)
                {
                    Products = result.Data ?? [];
                }
                else
                {
                    ErrorMessage = result.Message ?? "Erro ao carregar produtos.";
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erro ao Carregar Produtos - {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void EditProduct(Guid id)
        {
            // Redireciona para página de edição
            // Exemplo: NavigationManager.NavigateTo($"/products/edit/{id}");
        }

        public void DeleteProduct(Guid id)
        {
            // Lógica para exclusão
            // Pode abrir um modal de confirmação
        }

        public void CreateProduct()
        {
            NavigationManager.NavigateTo("/products/create");
        }
    }
}
