using slunaSVI.Models;
using slunaSVI.Services;

namespace slunaSVI.Views;

public partial class ProductView : ContentPage
{

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
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        //if (!string.IsNullOrWhiteSpace(NameEntry.Text) && !string.IsNullOrWhiteSpace(SurnameEntry.Text))
        //{
        //    Person person;
        //    if (_selectedPersonId.HasValue)
        //    {
        //        person = await _personRepository.GetPersonAsync(_selectedPersonId.Value);
        //        if (person != null)
        //        {
        //            person.Name = NameEntry.Text;
        //            person.Surname = SurnameEntry.Text;
        //            await _personRepository.SavePersonAsync(person);
        //            await DisplayAlert("Éxito", "La persona ha sido actualizada exitosamente.", "OK");
        //        }
        //    }
        //    else
        //    {
        //        person = new Person
        //        {
        //            Name = NameEntry.Text,
        //            Surname = SurnameEntry.Text
        //        };
        //        await _personRepository.SavePersonAsync(person);
        //        await DisplayAlert("Éxito", "La persona ha sido creada exitosamente.", "OK");
        //    }

        //    Cleaner();
        //    LoadData();
        //}
    }

    private void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        //if (e.CurrentSelection.FirstOrDefault() is Person selectedPerson)
        //{
        //    _selectedPersonId = selectedPerson.Id;
        //    NameEntry.Text = selectedPerson.Name;
        //    SurnameEntry.Text = selectedPerson.Surname;
        //}
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {

    }
}