using Microsoft.AspNetCore.Components;
using MudBlazor;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;

namespace OrderSales.Web.Pages.Customers
{
    public partial class ListCustomerPage : ComponentBase
    {

        #region PROPRIEDADES
        public List<Customer> Customers { get; set; } = new();

        public bool IsLoading = true;
        public string? ErrorMessage;
        #endregion

        #region SERVIÇOS

        [Inject]
        public ICustomerService CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var request = new CustomerGetAllRequest();
                var result = await CustomerService.GetAllAsync(request);
         
                if (result.IsSuccess)
                {
                    Customers = result.Data ?? [];
                    Console.WriteLine(result.Data);
                }
                else
                {
                    ErrorMessage = result.Message ?? "Erro ao carregar clientes.";
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erro ao Carregar clientes - {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task EditCustomer(Guid id)
        {
            
        }

        public async Task DeleteCustomer(Guid id)
        {
            try
            {
                var request = new CustomerDeleteRequest { Id = id };
                 await CustomerService.DeleteAsync(request);
               
                    Customers.RemoveAll(p => p.Id == id);
                    Snackbar.Add("cliente excluído com sucesso !", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public void CreateCustomer()
        {
            NavigationManager.NavigateTo("/customer/create");
        }
    }
}
