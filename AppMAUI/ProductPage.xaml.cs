using AppMAUI.Models;
using AppMAUI.Data;

namespace AppMAUI
{
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
                ProductList.ItemsSource = products;
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
                var product = new Product
                {
                    Name = "New Product",
                    Description = "Default Description",
                    Price = 0,
                    Category = "Default Category"
                };


                if (await _restService.AddProductAsync(product))
                {
                    await DisplayAlert("Success", "Product added successfully!", "OK");
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add product: {ex.Message}", "OK");
            }
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product selectedProduct)
            {
                var action = await DisplayActionSheet("Product Options", "Cancel", null, "Edit", "Delete");

                if (action == "Edit")
                {
                    await EditProduct(selectedProduct);
                }
                else if (action == "Delete")
                {
                    await DeleteProduct(selectedProduct);
                }

                ProductList.SelectedItem = null;
            }
        }

        private async Task EditProduct(Product product)
        {
            try
            {
                product.Name = await DisplayPromptAsync("Edit Product", "Enter new name:", initialValue: product.Name);
                product.Description = await DisplayPromptAsync("Edit Product", "Enter new description:", initialValue: product.Description);

                var priceInput = await DisplayPromptAsync("Edit Product", "Enter new price:", initialValue: product.Price.ToString());
                if (int.TryParse(priceInput, out var newPrice))
                {
                    product.Price = newPrice;
                }

                if (await _restService.UpdateProductAsync(product))
                {
                    await DisplayAlert("Success", "Product updated successfully!", "OK");
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to edit product: {ex.Message}", "OK");
            }
        }

        private async Task DeleteProduct(Product product)
        {
            try
            {
                var confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {product.Name}?", "Yes", "No");
                if (confirm)
                {
                    if (await _restService.DeleteProductAsync(product.ID))
                    {
                        await DisplayAlert("Success", "Product deleted successfully!", "OK");
                        LoadProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete product: {ex.Message}", "OK");
            }
        }
    }
}
