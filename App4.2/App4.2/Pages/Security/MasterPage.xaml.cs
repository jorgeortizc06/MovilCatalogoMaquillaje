using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using App4_2.Models;
using App4_2.Pages.Marca;

namespace App4._2
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        
        public MasterPage()
        {
            InitializeComponent();  
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            this.UserName.Text = "Paulina Cabrera";
            this.OrganizationName.Text = "CATALOGOS DE MAQUILLAJE";

            await ApplicationOptions();
        }

        private async Task ApplicationOptions()
        {
            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Productos",
                IconSource = "cart.png",
                Name = "Productos",
                TargetType = typeof(ProductoListPage)
            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Marcas",
                IconSource = "save.png",
                Name = "Marcas",
                TargetType = typeof(MarcaListPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Cerrar Sesión",
                IconSource = "logout.png",
                Name = "Logout",
                TargetType = typeof(LoginPage)
            });

            listView.ItemsSource = masterPageItems;
        }
    }
}
