using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App4_2.Models;
using App4_2.Services;
using App4._2;

namespace App4_2.Pages.Marca
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MarcaListPage : ContentPage
	{
		List<App4_2.Models.Marca> marcas = new List<App4_2.Models.Marca>();
		public MarcaListPage ()
		{
			InitializeComponent ();
            listView.RefreshCommand = new Command(async () => await LoadData());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadData();
        }

        private async Task LoadData()
        {
            MarcaService service = new MarcaService();

            marcas = await service.MarcaQueryAsync();
            listView.ItemsSource = marcas;

            listView.IsRefreshing = false;
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MarcaPage());
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);

            var item = menuItem.CommandParameter as App4_2.Models.Marca;

            if (item != null)
            {
                MarcaService service = new MarcaService();

                var data = await service.MarcaDeleteAsync(item);

                await LoadData();
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = marcas.Where(p => p.Marca1.Contains(e.NewTextValue)).ToList();
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as App4_2.Models.Marca;

            if (item != null)
            {
                MarcaService service = new MarcaService();

                var data = await service.MarcaGetAsync(item.Id);

                if (data != null)
                {
                    var page = new MarcaPage();
                    page.LoadData(data);

                    await this.Navigation.PushAsync(page);
                }
            }
        }
    }
}