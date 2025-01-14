namespace AppMAUI
{
    public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnExploreNowClicked(object sender, EventArgs e)
    {
        // Ensure navigation works with Shell routing
        await Shell.Current.GoToAsync("///ProductPage");
    }
}


}
