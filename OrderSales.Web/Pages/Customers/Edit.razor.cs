using Microsoft.AspNetCore.Components;
using MudBlazor;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Services;
using OrderSales.Web.Services;

namespace OrderSales.Web.Pages.Customers
{
    public class EditCustomerPage : ComponentBase
    {
        #region PROPRIEDADES
        [Parameter]
        public string Id { get; set; } = string.Empty;
        public CustomerUpdateRequest InputModel { get; set; } = new();
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
                var result = await CustomerService.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/customer/list");
                    Snackbar.Add(result.Message ?? "cliente atualizado com sucesso !", Severity.Success);
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


        protected override async Task OnInitializedAsync()
        {
            CustomerGetByIdRequest? request = null;
            try
            {
                request = new CustomerGetByIdRequest { Id = Guid.Parse(Id) };
            }
            catch
            {
                Snackbar.Add("Parâmetro inválido", Severity.Error);

            }
            if (request is null)
                return;

            try
            {
                var response = await CustomerService.GetByIdAsync(request);
                if (response.IsSuccess)
                {
                    InputModel = new CustomerUpdateRequest
                    {
                        Id = response.Data?.Id ?? Guid.Empty,
                        Name = response.Data?.Name ?? string.Empty,
                        Address = response.Data?.Address ?? string.Empty,
                        Email = response.Data?.Email ?? string.Empty,
                        Phone = response.Data?.Phone ?? string.Empty
                    };
                }
                else
                {
                    Snackbar.Add(response.Message ?? "Erro ao carregar cliente", Severity.Error);
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
