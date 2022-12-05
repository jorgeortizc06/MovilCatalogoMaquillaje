using App4_2.Models;
using App4_2.Services;
using App4_2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4_2.Pages.Marca
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MarcaPage : ContentPage
	{
		MarcaViewModel CurrentViewModel { get { return (MarcaViewModel)this.BindingContext; } }
		List<Item> marcaList { get; set; }

		public MarcaPage ()
		{
			InitializeComponent ();
			BindingContext = new ViewModels.MarcaViewModel();

        }

        public void LoadData(App4_2.Models.Marca marca)
        {
            CurrentViewModel.Id = marca.Id;
            CurrentViewModel.Marca1 = marca.Marca1;
        }

		private async void saveButton_Clicked(object sender, EventArgs e)
		{
			App4_2.Models.Marca marca = new App4_2.Models.Marca();

			marca.Id = CurrentViewModel.Id;
			marca.Marca1 = CurrentViewModel.Marca1;

			MarcaService service = new MarcaService();

			bool result = false;

			if (marca.Id == 0)
			{
				result = await service.MarcaInsertAsync(marca); ;
			}
			else
			{
				result = await service.MarcaUpdateAsync(marca);
			}

			if (result)
			{
				await this.Navigation.PopAsync();
			}
        }

    }
}