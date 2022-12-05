using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App4_2.ViewModels
{
    class MarcaViewModel: BindableObject
    {
        int id; public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        string marca1; public string Marca1 { get { return marca1; } set { marca1 = value; OnPropertyChanged("Marca1"); } }
        DateTime fechaCambio; public DateTime FechaCambio { get { return fechaCambio; } set { fechaCambio = value; OnPropertyChanged("FechaCambio"); } }
        int usrIng; public int UsrIng { get { return usrIng; } set { usrIng = value; OnPropertyChanged("UsrIng"); } }
        int activo; public int Activo { get { return activo; } set { activo = value; OnPropertyChanged("Activo"); } }
    }
}
