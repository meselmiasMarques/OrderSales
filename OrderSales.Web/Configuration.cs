using MudBlazor;
using MudBlazor.Utilities;

namespace OrderSales.Web
{
    public static class Configuration
    {

        public const string HttpClientName = "ordersales";
        public static string BackendUrl { get; set; } = "https://localhost:5001";



        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },

            PaletteLight = new PaletteLight
            {
                // Manter cores padrão, mas garantir contraste no menu
                DrawerBackground = Colors.Green.Darken4,    // fundo claro, mas com leve contraste
                DrawerText = Colors.Green.Darken4,           // texto escuro e legível
                DrawerIcon = Colors.Green.Darken4,        // ícones escuros também
                AppbarBackground = Colors.Green.Default,     // para manter o topo em azul padrão
                AppbarText = Colors.Shades.Black            // texto branco no topo
            },

            // Deixe o dark padrão, ou personalize se quiser
            PaletteDark = new PaletteDark()
        };

    }
}
