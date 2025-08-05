using Microsoft.AspNetCore.Components;
using MudBlazor;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;

namespace OrderSales.Web.Pages.Products
{
    public class EditProductPage : ComponentBase
    {
        #region PROPRIEDADES

        [Parameter]
        public string Id { get; set; } = string.Empty;
        public ProductUpdateRequest InputModel { get; set; } = new();
        public bool IsLoading = true;

        public MudForm _form = default!;
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
                var result = await ProductService.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/product/list");
                    Snackbar.Add(result.Message ?? "Produto atualizado com sucesso !", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao atualizar produto", Severity.Error);
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


        protected override async Task OnInitializedAsync()
        {
            ProductGetByIdRequest? request = null;
            try
            {
                request = new ProductGetByIdRequest { Id = Guid.Parse(Id) };
            }
            catch
            {
                Snackbar.Add("Parâmetro inválido", Severity.Error);

            }
            if (request is null)
                return;

            try
            {
                var response = await ProductService.GetByIdAsync(request);
                if (response.IsSuccess)
                {
                    InputModel = new ProductUpdateRequest
                    {
                        Id = response.Data?.Id ?? Guid.Empty,
                        Name = response.Data?.Name ?? string.Empty,
                        Price = response.Data?.Price ?? 0,
                        Stock = response.Data?.Stock ?? 0,
                        Active = 1
                    };
                }
                else
                {
                    Snackbar.Add(response.Message ?? "Erro ao carregar produtos", Severity.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion
    }
}
