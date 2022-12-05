using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App4._2.ViewModels
{
    public class ProductoViewModel : BindableObject
    {
        int id; public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        string producto1; public string Producto1 { get { return producto1; } set { producto1 = value; OnPropertyChanged("Producto1"); } }
        decimal precio; public decimal Precio { get { return precio; } set { precio = value; OnPropertyChanged("Precio"); } }
        int idMarca; public int IdMarca { get { return idMarca; } set { idMarca = value; OnPropertyChanged("IdMarca"); } }
        string marca; public string Marca { get { return marca; } set { marca = value; OnPropertyChanged("Marca"); } }
        //bool activo; public bool Activo { get { return activo; } set { activo = value; OnPropertyChanged("Activo"); } }

        byte[] imagen; public byte[] Imagen { get { return imagen; } set { imagen = value; OnPropertyChanged("Imagen"); } }
    }
}
