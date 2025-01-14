using AppMAUI.Data;
using AppMAUI.Models;

namespace AppMAUI
{
    public partial class AnimalPage : ContentPage
    {
        private readonly IRestService _restService;

        public AnimalPage(IRestService restService)
        {
            InitializeComponent();
            _restService = restService;
            LoadAnimals();
        }

        private async void LoadAnimals()
        {
            try
            {
                var animals = await _restService.GetAnimalsAsync();
                AnimalList.ItemsSource = animals;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load animals: {ex.Message}", "OK");
            }
        }

        private async void OnAddAnimalClicked(object sender, EventArgs e)
        {
            try
            {
                var animal = new Animal
                {
                    Name = "New Animal",
                    Breed = "Unknown",
                    Age = 0,
                    Description = "New Description",
                    Health = "Healthy"
                };

                if (await _restService.AddAnimalAsync(animal))
                {
                    await DisplayAlert("Success", "Animal added successfully!", "OK");
                    LoadAnimals();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add animal: {ex.Message}", "OK");
            }
        }

        private async void OnAnimalSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Animal selectedAnimal)
            {
                var action = await DisplayActionSheet("Animal Options", "Cancel", null, "Edit", "Delete");

                if (action == "Edit")
                {
                    await EditAnimal(selectedAnimal);
                }
                else if (action == "Delete")
                {
                    await DeleteAnimal(selectedAnimal);
                }

                AnimalList.SelectedItem = null;
            }
        }

        private async Task EditAnimal(Animal animal)
        {
            try
            {
                animal.Name = await DisplayPromptAsync("Edit Animal", "Enter new name:", initialValue: animal.Name);
                animal.Breed = await DisplayPromptAsync("Edit Animal", "Enter new breed:", initialValue: animal.Breed);

                if (int.TryParse(await DisplayPromptAsync("Edit Animal", "Enter new age:", initialValue: animal.Age.ToString()), out var age))
                {
                    animal.Age = age;
                }

                animal.Description = await DisplayPromptAsync("Edit Animal", "Enter new description:", initialValue: animal.Description);
                animal.Health = await DisplayPromptAsync("Edit Animal", "Enter new health status:", initialValue: animal.Health);

                if (await _restService.UpdateAnimalAsync(animal))
                {
                    await DisplayAlert("Success", "Animal updated successfully!", "OK");
                    LoadAnimals();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to edit animal: {ex.Message}", "OK");
            }
        }

        private async Task DeleteAnimal(Animal animal)
        {
            try
            {
                var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {animal.Name}?", "Yes", "No");
                if (confirm)
                {
                    if (await _restService.DeleteAnimalAsync(animal.ID))
                    {
                        await DisplayAlert("Success", "Animal deleted successfully!", "OK");
                        LoadAnimals();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete animal: {ex.Message}", "OK");
            }
        }
    }
}
