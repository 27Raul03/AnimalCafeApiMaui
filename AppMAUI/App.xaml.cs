using AppMAUI.Data;

namespace AppMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IRestService, RestService>();

            MainPage = new NavigationPage(new AppShell());
        }
    }
}
