namespace AppMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("ProductPage", typeof(ProductPage));
            Routing.RegisterRoute("AnimalPage", typeof(AnimalPage));
            Routing.RegisterRoute("ClientPage", typeof(ClientPage));
        }
    }
}
