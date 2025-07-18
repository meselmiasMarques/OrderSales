using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Services;

namespace OrderSales.Web.Pages.Customers
{
    public partial class CreateCustomerPage : ComponentBase
    {

        #region PROPRIEDADES

        public CustomerCreateRequest InputModel { get; set; } = new();

        public bool IsLoading = true;

        public MudForm _form = default!;
        #endregion

        #region SERVIÇOS

        [Inject]
        public ICustomerService CustomerService { get; set; } = null!;

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
                var result = await CustomerService.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/customer/list");
                    Snackbar.Add(result.Message ?? "cliente criado com sucesso !", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao criar cliente", Severity.Error);
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
            NavigationManager.NavigateTo("/customer/list");
        }

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }

        #endregion
    
    }
}
