using App4._2.ViewModels;
using App4_2.Models;
using App4_2.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4._2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoPage : ContentPage
    {
        ProductoViewModel CurrentViewModel { get { return (ProductoViewModel)this.BindingContext; } }

        List<Item> marcaList { get; set; }

        public ProductoPage()
        {
            InitializeComponent();

            BindingContext = new ViewModels.ProductoViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await InitPickers();

            marcaPicker.SelectedItem = marcaList.Where(p => p.Id == CurrentViewModel.IdMarca).FirstOrDefault();

            await CargarImagen();
        }

        public async Task InitPickers()
        {
            Service service = new Service();

            List<Marca> marcas = await service.MarcaQueryAsync();

            marcaList = marcas.Select(p =>
            new Item
            {
                Id = p.Id,
                Description = p.Marca1
            }).ToList();

            marcaPicker.ItemsSource = marcaList;
        }

        public void LoadData(Producto producto)
        {
            CurrentViewModel.Id = producto.Id;
            CurrentViewModel.Producto1 = producto.Producto1;
            CurrentViewModel.Precio = producto.Precio;
            CurrentViewModel.IdMarca = producto.IdMarca;
            CurrentViewModel.Imagen = producto.Imagen;
        }

        private  async void saveButton_Clicked(object sender, EventArgs e)
        {
            Producto producto = new Producto();

            producto.Id = CurrentViewModel.Id;
            producto.Producto1 = CurrentViewModel.Producto1;
            producto.Precio = CurrentViewModel.Precio;
            producto.Imagen = CurrentViewModel.Imagen;

            Item itemMarca = marcaPicker.SelectedItem as Item;

            if (itemMarca != null)
                producto.IdMarca = itemMarca.Id;

            Service service = new Service();

            bool result = false;

            if(producto.Id == 0)
                result = await service.ProductoInsertAsync(producto);
            else
                result = await service.ProductoUpdateAsync(producto);

            if (result)
            {
                await this.Navigation.PopAsync();
            }
        }

        async void Capturar_Clicked(System.Object sender, System.EventArgs e)
        {
            await CapturarImagen();
        }

        private async Task CargarImagen()
        {
            try
            {
                if (CurrentViewModel.Imagen != null && CurrentViewModel.Imagen.Length > 0)
                    ImageData.Source = ImageSource.FromStream(() => new MemoryStream(CurrentViewModel.Imagen));
                else
                    ImageData.Source = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task CapturarImagen()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Error", "Camara no disponible", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 92
                });

                if (file != null)
                {
                    var stream = file.GetStream();

                    MemoryStream ms = new MemoryStream();

                    stream.CopyTo(ms);

                    CurrentViewModel.Imagen = ms.ToArray();

                    await CargarImagen();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}