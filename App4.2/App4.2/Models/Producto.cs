using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App4_2.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Producto1 { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCambio { get; set; }
        public byte[] Imagen { get; set; }
        public int IdMarca { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
        public ImageSource ImagenSource
        {
            get
            {
                if (Imagen != null)
                    return ImageSource.FromStream(() => new MemoryStream(Imagen));

                return null;
            }
        }
    
}
}
