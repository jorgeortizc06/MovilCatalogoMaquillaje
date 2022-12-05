using App4_2.Models;
using App4_2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4._2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoListPage : ContentPage
    {
        List<Producto> productos = new List<Producto>();

        public ProductoListPage()
        {
            InitializeComponent();

            listView.RefreshCommand = new Command(async () => await LoadData());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadData();
        }

        private async Task LoadData()
        {
            Service service = new Service();

            productos  = await service.ProductoQueryAsync();
            listView.ItemsSource = productos;

            listView.IsRefreshing = false;
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {                 
            this.Navigation.PushAsync(new ProductoPage());
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            var item = menuItem.CommandParameter as Producto;

            if (item != null)
            {
                Service service = new Service();

                var data = await service.ProductoDeleteAsync(item);

                await LoadData();
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = productos.Where(p => p.Producto1.Contains(e.NewTextValue)).ToList();
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Producto;

            if (item != null)
            {
                Service service = new Service();

                var data  = await service.ProductoGetAsync(item.Id);

                if (data != null)
                {
                    var page = new ProductoPage();
                    page.LoadData(data);

                    await this.Navigation.PushAsync(page);
                }
            }
        }
    }
}