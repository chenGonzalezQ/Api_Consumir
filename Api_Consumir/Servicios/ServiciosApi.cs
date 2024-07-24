using Api_Consumir.Models;
using Api_Consumir.NewFolder;

using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace Api_Consumir.Servicios
{
    public class ServiciosApi : IServiciosApi
    {
        private static string _baseUrl;

        public ServiciosApi() {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task<List<Producto>> Lista()
        {


                List<Producto> lista = new List<Producto>();
                var cliente = new HttpClient();
               // string url = "http://www.productoschen.somee.com/api/Producto/lista";
                cliente.BaseAddress = new Uri(_baseUrl);
                var respuesta = await cliente.GetAsync("api/Producto/lista");

                if (respuesta.IsSuccessStatusCode)
                {
                    var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var l = JsonConvert.DeserializeObject<List<Producto>>(jsonRespuesta);
                    lista = l;
                
                }

                 return lista;
        }
    

        public async Task<bool> Borrar(int idProducto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress=new Uri(_baseUrl);

            var respuesta = await cliente.DeleteAsync($"api/Producto/Borrar/{idProducto}");
            if (respuesta.IsSuccessStatusCode)
            {
                resultado = true;
            }
            return resultado;
        }

        public async Task<bool>Editar(Producto producto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var contenido = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var respuesta = await cliente.PutAsync("api/Producto/Editar/",contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                resultado = true;
            }
            return resultado;
        }

        public async Task<bool> Guardar(Producto producto)
        {
            bool resultado = false;
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var contenido = new StringContent(JsonConvert.SerializeObject(producto),Encoding.UTF8,"application/json");
            var respuesta = await cliente.PostAsync("api/Producto/Guardar/",contenido);
            if (respuesta.IsSuccessStatusCode) {
                resultado = true;
            }
            return resultado;
        }

       

        public async Task<Producto> Obtener(int idProducto)
        {
            Producto producto = new Producto();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var respuesta = await cliente.GetAsync($"api/Producto/obtener/{idProducto}");
            if (respuesta.IsSuccessStatusCode) {
                var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
             var p = JsonConvert.DeserializeObject<Root>(jsonRespuesta);
                producto = p.producto;
            
            }
            return producto;
        }
    }
}
