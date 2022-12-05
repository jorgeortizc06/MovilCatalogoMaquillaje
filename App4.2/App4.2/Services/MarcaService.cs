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
    class MarcaService
    {
        HttpClient client;

        string url = "https://10.0.2.2:7108/";
        //string url = "http://zeus.apolosoftware.com.ec/Test/";
        public MarcaService()
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
            try
            {
                Uri uri = new Uri(url + "api/Marcas");

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    
                    return JsonConvert.DeserializeObject<List<Marca>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new List<Marca>();
        }

        public async Task<Marca> MarcaGetAsync(int IdMarca)
        {
            Uri uri = new Uri(url + "api/Marcas/" + IdMarca);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { WriteIndented = true };

                return JsonConvert.DeserializeObject<Marca>(content);
            }

            return null;
        }

        public async Task<bool> MarcaInsertAsync(Marca model)
        {
            Uri uri = new Uri(url + "api/Marcas");
            string json = System.Text.Json.JsonSerializer.Serialize(model);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> MarcaUpdateAsync(Marca model)
        {
            try
            {
                Uri uri = new Uri(url + "api/Marcas/" + model.Id);

                string json = System.Text.Json.JsonSerializer.Serialize(model);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(uri, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> MarcaDeleteAsync(Marca model)
        {
            Uri uri = new Uri(url + "api/Marcas/Delete");

            string json = JsonConvert.SerializeObject(model);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

    }
}
