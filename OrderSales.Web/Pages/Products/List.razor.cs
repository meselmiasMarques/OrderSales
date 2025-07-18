
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;
using System.Security.Cryptography;
using System.Threading.Tasks;


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

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

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

        public async Task EditProduct(Guid id)
        {
           
        }

        public async Task DeleteProduct(Guid id)
        {
            try
            {
                var request = new ProductDeleteRequest { Id = id };
                var result = await ProductService.DeleteAsync(request);
                if (result.IsSuccess)
                {
                    Products.RemoveAll(p => p.Id == id);
                    Snackbar.Add(result.Message ?? "Produto excluído com sucesso !", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message ?? " Erro ao Excluir produtos", Severity.Error);

                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public void CreateProduct()
        {
            NavigationManager.NavigateTo("/products/create");
        }
    }
}
