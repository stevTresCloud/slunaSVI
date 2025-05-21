using slunaSVI.Models;
using slunaSVI.Services;

namespace slunaSVI.Views;

public partial class ProductView : ContentPage
{

    private ProductModel? _selectedProductModel;

    public ProductView()
	{
		InitializeComponent();
        ClearAll();
        LoadData();
    }

    public async void LoadData()
    {
        ProductService _productService = new ProductService();
        List<ProductModel> products = new List<ProductModel>();
        try
        {
            products = await _productService.GetProducts(); // Removed `.Result` to correctly await the asynchronous method
            if (products != null)
            {
                ProductsCollectionView.ItemsSource = products;
            }
            else
            {
                await DisplayAlert("Error", "Failed to load products", "OK"); // Added `await` to properly handle the asynchronous call
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK"); // Added error handling for robustness
        }
    }

    public void ClearAll()
    {
        ProductNameEntry.Text = string.Empty;
        ProductDescriptionEntry.Text = string.Empty;
        UnitPriceEntry.Text = string.Empty;
        ProductsCollectionView.ItemsSource = null;
        _selectedProductModel = null;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ProductNameEntry.Text) || string.IsNullOrWhiteSpace(ProductDescriptionEntry.Text) || string.IsNullOrWhiteSpace(UnitPriceEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        if (_selectedProductModel != null)
        {
            _selectedProductModel.ProductName = ProductNameEntry.Text;
            _selectedProductModel.ProductDescription = ProductDescriptionEntry.Text;
            _selectedProductModel.UnitPrice = double.TryParse(UnitPriceEntry.Text, out double unitPrice) ? unitPrice : (double?)null;
            // Call the update method from the service
            var productService = new ProductService();
            await productService.UpdateProduct(_selectedProductModel.Id, _selectedProductModel);
            DisplayAlert("Success", "Product EDITED successfully", "OK");
        }
        else
        {
            var newProduct = new ProductModel
            {
                ProductName = ProductNameEntry.Text,
                ProductDescription = ProductDescriptionEntry.Text,
                UnitPrice = double.TryParse(UnitPriceEntry.Text, out double unitPrice) ? unitPrice : (double?)null
            };
            // Call the create method from the service
            var productService = new ProductService();
            await productService.CreateProduct(newProduct);
            DisplayAlert("Success", "Product CREATED successfully", "OK");
        }
        
        ClearAll();
        LoadData();
    }

    private void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ProductModel selectedProduct)
        {
            // Ensure _selectedProductModel is initialized before accessing its properties
            _selectedProductModel = new ProductModel
            {
                Id = selectedProduct.Id,
                ProductName = selectedProduct.ProductName,
                ProductDescription = selectedProduct.ProductDescription,
                UnitPrice = selectedProduct.UnitPrice
            };

            ProductNameEntry.Text = selectedProduct.ProductName;
            ProductDescriptionEntry.Text = selectedProduct.ProductDescription;
            UnitPriceEntry.Text = selectedProduct.UnitPrice.ToString();
        }
        else
        {
            ClearAll();
            LoadData();
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this product?", "Yes", "No");
        if (result)
        {
            var button = sender as Button;
            if (button?.CommandParameter is string id)
            {
                var productService = new ProductService();
                await productService.DeleteProduct(id);

                DisplayAlert("Success", "Product deleted successfully", "OK");

                ClearAll();
                LoadData();
            }
            else
            {
                await DisplayAlert("Error", "No product ¨to deletion", "OK");
            }
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        ClearAll();
        LoadData();
    }
}