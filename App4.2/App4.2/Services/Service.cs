using App4_2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App4_2.Services
{
    public class Service
    {
        HttpClient client;

        string url = "https://10.0.2.2:7108/";
        //string url = "http://zeus.apolosoftware.com.ec/Test/";
        public Service()
        {
            HttpClientHandler insecureHandler = GetInsecureHandler();
            client = new HttpClient(insecureHandler); 
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<List<Marca>> MarcaQueryAsync()
        {
            Uri uri = new Uri(url + "api/Marcas");

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { WriteIndented = true };

                return JsonConvert.DeserializeObject<List<Marca>>(content);
            }

            return new List<Marca>();
        }

        public async Task<List<Producto>> ProductoQueryAsync()
        {
            try
            {
                Uri uri = new Uri(url + "api/Productos");

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var result = JsonConvert.DeserializeObject<List<Producto>>(content);
                    return JsonConvert.DeserializeObject<List<Producto>>(content);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            return new List<Producto>();
        }


        public async Task<Producto> ProductoGetAsync(int IdProducto)
        {
            Uri uri = new Uri(url + "api/Productos/" + IdProducto);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { WriteIndented = true };

                return JsonConvert.DeserializeObject<Producto>(content);
            }

            return null;
        }

        public async Task<bool> ProductoInsertAsync(Producto model)
        {
            Uri uri = new Uri(url + "api/Productos");
            string json = System.Text.Json.JsonSerializer.Serialize(model);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ProductoUpdateAsync(Producto model)
        {
            try
            {
                Uri uri = new Uri(url + "api/Productos/" + model.Id);

                string json = System.Text.Json.JsonSerializer.Serialize(model);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(uri, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> ProductoDeleteAsync(Producto model)
        {
            Uri uri = new Uri(url + "api/Productos/"+model.Id);


            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
