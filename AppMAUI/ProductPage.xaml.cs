using AppMAUI.Data;
using AppMAUI.Models;
using Newtonsoft.Json;

namespace AppMAUI;

public partial class ProductPage : ContentPage
{
    private readonly IRestService _restService;

    public ProductPage(IRestService restService)
    {
        InitializeComponent();
        _restService = restService;
        LoadProducts();
    }

    private async void LoadProducts()
    {
        try
        {
            var products = await _restService.GetProductsAsync();

            if (products != null && products.Any())
            {
                ProductList.ItemsSource = products;
                Console.WriteLine($"[DEBUG] Products received: {products.Count}");
                foreach (var product in products)
                {
                    Console.WriteLine($"[DEBUG] Product: {product.Name} - {product.Price} - {product.Description}");
                }
            }
            else
            {
                await DisplayAlert("Info", "No products available.", "OK");
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
        }
    }


    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                string.IsNullOrWhiteSpace(priceEntry.Text) ||
                string.IsNullOrWhiteSpace(categoryEntry.Text))
            {
                await DisplayAlert("Error", "All fields must be filled.", "OK");
                return;
            }

            if (!int.TryParse(priceEntry.Text, out var price))
            {
                await DisplayAlert("Error", "Invalid price format.", "OK");
                return;
            }

            var product = new Product
            {
                Name = nameEntry.Text,
                Description = categoryEntry.Text,
                Price = price
            };

            Console.WriteLine($"[DEBUG] Product to Add: {JsonConvert.SerializeObject(product)}");

            if (await _restService.AddProductAsync(product))
            {
                await DisplayAlert("Success", "Product added successfully!", "OK");
                nameEntry.Text = string.Empty;
                priceEntry.Text = string.Empty;
                categoryEntry.Text = string.Empty;
                LoadProducts();
            }
            else
            {
                await DisplayAlert("Error", "Failed to add product.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add product: {ex.Message}", "OK");
            Console.WriteLine($"[ERROR] Exception: {ex.Message}");
        }
    }

    private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Product selectedProduct)
        {
            var action = await DisplayActionSheet("Product Options", "Cancel", null, "Edit", "Delete");

            if (action == "Edit")
            {
                await OnEditProductClicked(selectedProduct);
            }
            else if (action == "Delete")
            {
                await DeleteSelectedProduct(selectedProduct);
            }

            ProductList.SelectedItem = null;
        }
    }
    private async Task DeleteSelectedProduct(Product selectedProduct)
    {
        try
        {
            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {selectedProduct.Name}?", "Yes", "No");
            if (confirm)
            {
                var success = await _restService.DeleteProductAsync(selectedProduct.ID);
                if (success)
                {
                    await DisplayAlert("Success", "Product deleted successfully.", "OK");
                    LoadProducts();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete product.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete product: {ex.Message}", "OK");
        }
    }


    private async Task OnEditProductClicked(Product product)
    {
        try
        {
            product.Name = await DisplayPromptAsync("Edit Product", "Enter new name:", initialValue: product.Name);
            product.Description = await DisplayPromptAsync("Edit Product", "Enter new description:", initialValue: product.Description);

            if (!int.TryParse(await DisplayPromptAsync("Edit Product", "Enter new price:", initialValue: product.Price.ToString()), out var price))
            {
                await DisplayAlert("Error", "Invalid price format.", "OK");
                return;
            }

            product.Price = price;

            if (await _restService.UpdateProductAsync(product))
            {
                await DisplayAlert("Success", "Product updated successfully!", "OK");
                LoadProducts();
            }
            else
            {
                await DisplayAlert("Error", "Failed to update product.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to edit product: {ex.Message}", "OK");
        }
    }
    private async void OnLoadProductsClicked(object sender, EventArgs e)
    {
        try
        {
            var products = await _restService.GetProductsAsync();
            if (products != null && products.Any())
            {
                ProductList.ItemsSource = products;
                await DisplayAlert("Success", "Products loaded successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Info", "No products available.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
        }
    }


    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (ProductList.SelectedItem is Product selectedProduct)
        {
            var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {selectedProduct.Name}?", "Yes", "No");
            if (confirm)
            {
                var success = await _restService.DeleteProductAsync(selectedProduct.ID);
                if (success)
                {
                    await DisplayAlert("Success", "Product deleted successfully.", "OK");
                    LoadProducts();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete product.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "No product selected.", "OK");
        }
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}
