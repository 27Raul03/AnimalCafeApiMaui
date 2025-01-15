using System;

namespace AppMAUI
{
    public partial class LoginPage : ContentPage
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "Admin1!";

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim();
            var password = PasswordEntry.Text;

            if (email == AdminEmail && password == AdminPassword)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                ErrorMessage.Text = "Invalid email or password!";
                ErrorMessage.IsVisible = true;
            }
        }
    }
}
