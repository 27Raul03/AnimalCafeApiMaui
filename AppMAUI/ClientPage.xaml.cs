using AppMAUI.Models;
using AppMAUI.Data;

namespace AppMAUI
{
    public partial class ClientPage : ContentPage
    {
        private readonly IRestService _restService;

        public ClientPage(IRestService restService)
        {
            InitializeComponent();
            _restService = restService;
            LoadClients();
        }

        private async void LoadClients()
        {
            try
            {
                var clients = await _restService.GetClientsAsync();
                ClientList.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load clients: {ex.Message}", "OK");
            }
        }

        private async void OnAddClientClicked(object sender, EventArgs e)
        {
            try
            {
                var client = new Client
                {
                    Name = "New Client",
                    Email = "newclient@example.com",
                    PhoneNumber = "0722-123-123"
                };

                if (await _restService.AddClientAsync(client))
                {
                    await DisplayAlert("Success", "Client added successfully!", "OK");
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add client: {ex.Message}", "OK");
            }
        }

        private async void OnClientSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Client selectedClient)
            {
                var action = await DisplayActionSheet("Client Options", "Cancel", null, "Edit", "Delete");

                if (action == "Edit")
                {
                    await EditClient(selectedClient);
                }
                else if (action == "Delete")
                {
                    await DeleteClient(selectedClient);
                }

                ClientList.SelectedItem = null;
            }
        }

        private async Task EditClient(Client client)
        {
            try
            {
                client.Name = await DisplayPromptAsync("Edit Client", "Enter new name:", initialValue: client.Name);
                client.Email = await DisplayPromptAsync("Edit Client", "Enter new email:", initialValue: client.Email);
                client.PhoneNumber = await DisplayPromptAsync("Edit Client", "Enter new phone number:", initialValue: client.PhoneNumber);

                if (await _restService.UpdateClientAsync(client))
                {
                    await DisplayAlert("Success", "Client updated successfully!", "OK");
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to edit client: {ex.Message}", "OK");
            }
        }

        private async Task DeleteClient(Client client)
        {
            try
            {
                var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {client.Name}?", "Yes", "No");
                if (confirm)
                {
                    if (await _restService.DeleteClientAsync(client.ID))
                    {
                        await DisplayAlert("Success", "Client deleted successfully!", "OK");
                        LoadClients();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete client: {ex.Message}", "OK");
            }
        }
    }
}

