using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;

namespace OrderSales.Web.Pages.Products
{
    public partial class CreateProductsPage : ComponentBase
    {
        #region PROPRIEDADES
        public ProductCreateRequest InputModel { get; set; } = new();

        public bool IsLoading = true;

        public MudForm _form = default!;

        public string? ErrorMessage;
        #endregion

        #region SERVIÇOS

        [Inject]
        public IProductService ProductService { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion


        #region Metodos

        public async Task OnValidSubmitAsync()
        {
          
            try
            {
                var result = await ProductService.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/products/list");
                    Snackbar.Add(result.Message ?? "Produto criado com sucesso !", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao criar produto", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsLoading = false;
            }
           
        }

        public void Voltar(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("/products/list");
        }

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }

        #endregion
    }
}
