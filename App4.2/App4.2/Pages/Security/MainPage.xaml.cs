using App4_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace App4._2
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterPageItem item = e.SelectedItem as MasterPageItem;

            if (item != null)
            {
                switch (item.Name)
                {
                    default:

                        Type targetType = item.TargetType;
                      
                        if (targetType != null)
                        {
                            Detail = new NavigationPage((Xamarin.Forms.Page)Activator.CreateInstance(targetType))
                            {
                                BarBackgroundColor = Color.Navy,
                                BarTextColor = Color.White
                            };
                        }

                        break;

                    case "Logout":

                        SessionClose();

                        break;
                }


                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
       
        public async void SetInitialPage()
        {
            Detail = new NavigationPage((Xamarin.Forms.Page)Activator.CreateInstance(typeof(ProductoListPage)))
            {
                BarBackgroundColor = Color.Navy,
                BarTextColor = Color.White
            };
        }

        public async void SessionClose()
        {
            var answer = await DisplayAlert("Cerrar Sesión", "¿Está seguro que desea cerrar la sesión?", "Si", "No");

            if (answer)
                App.Current.SessionClose();
        }
    }
}
