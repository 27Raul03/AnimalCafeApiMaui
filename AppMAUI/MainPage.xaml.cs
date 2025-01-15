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
        await Shell.Current.GoToAsync("///ProductPage");
    }
}


}
