using Microsoft.AspNetCore.Components;
using MudBlazor;
using OrderSales.Core.Models;
using OrderSales.Core.Requests.Customers;
using OrderSales.Core.Requests.OrderItem;
using OrderSales.Core.Requests.Orders;
using OrderSales.Core.Requests.Products;
using OrderSales.Core.Services;


namespace OrderSales.Web.Pages.Orders
{
    public partial class CreateOrderPage : ComponentBase
    {

        #region PROPRIEDADES

        [Parameter]
        public Guid CustomerId { get; set; } = Guid.Empty;

        public OrderCreateRequest InputModel { get; set; } = new()
        {
            Customer = new Customer()
        };

        public List<Product> Products { get; set; } = new();

        // Para mapear a quantidade de cada produto por Id
        public Dictionary<Guid, int> ProductQuantities { get; set; } = new();

        public bool ShowItemsSection { get; set; } = false;
        public bool ShowHeaderSection { get; set; } = false;
        public bool ShowSummarySection { get; set; } = false;

        public bool IsLoading = true;
        public MudForm _form = default!;
        #endregion

        #region SERVIÇOS
        [Inject]
        public IOrderService OrderService { get; set; } = null!;

        [Inject]
        public ICustomerService CustomerService { get; set; } = null!;

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
                          

                // (2) Normaliza CustomerId sem NRE
                if (InputModel.Customer != null)
                    InputModel.CustomerId = InputModel.Customer.Id;

                // (3) Monta o request já filtrando itens inválidos (equivalente ao RemoveAll)
                var request = new OrderCreateRequest
                {
                    CustomerId = InputModel.Customer?.Id ?? InputModel.CustomerId,
                    OrderItems = (Products ?? Enumerable.Empty<Product>())
                        .Where(p => p.Amount > 0) 
                        .Select(p => new OrderItemCreateRequest
                        {
                            ProductId = p.Id,
                            Amount = p.Amount
                        })
                        .ToList()
                };
            
          
            try
            {
               
                var result = await OrderService.CreateAsync(request);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/order/list");
                    Snackbar.Add(result.Message ?? "Pedido criado com sucesso!", Severity.Success);
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao criar pedido", Severity.Error);
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
            ShowHeaderSection = true;

            await GetCustomer(CustomerId);
            await GetProducts();
        }

        public async Task GetCustomer(Guid id)
        {
            CustomerGetByIdRequest? request = null;
            try
            {
                request = new CustomerGetByIdRequest { Id = id };
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
                    InputModel = new OrderCreateRequest
                    {

                        Customer = response.Data

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

        public async Task GetProducts()
        {
            try
            {
                var request = new ProductGetAllRequest();
                var result = await ProductService.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Products = result.Data ?? [];

                    // Inicializa quantidades com zero
                    foreach (var product in Products)
                    {
                        if (!ProductQuantities.ContainsKey(product.Id))
                            ProductQuantities[product.Id] = 0;
                    }
                }
                else
                {
                    Snackbar.Add(result.Message ?? "Erro ao carregar produtos.", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Erro ao carregar produtos - {ex.Message}", Severity.Error);
            }
        }


        public void ToggleHeader()
        {
            ShowHeaderSection = true;
            ShowItemsSection = false;
            ShowSummarySection = false;
        }

        public void ToggleItems() {

            ShowHeaderSection = false;
            ShowItemsSection = true;
            ShowSummarySection = false;
        }


        public void ToggleSummary()
        {
            ShowHeaderSection = false;
            ShowItemsSection = false;
            ShowSummarySection = true;
        }

        public void IncreaseQuantity(Guid productId)
        {
            foreach (var item in Products) {
                if (item.Id == productId)
                {
                    item.Amount++;
                }
            }


                
        }

        public void DecreaseQuantity(Guid productId)
        {
            foreach (var item in Products)
            {
                if (item.Id == productId)
                {
                    item.Amount--;
                }
            }
        }


        #endregion

    }
}
