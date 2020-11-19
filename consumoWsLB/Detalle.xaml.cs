using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace consumoWsLB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalle : ContentPage
    {
        public int Id;
        public string Nombre;
        public string Codigo;
        public int Stock;
        public int Activo;
        public double Precio;
        public Detalle(int id, string nombre, string codigo, int stock, int activo, double precio)
        {
            InitializeComponent();
            this.Id = id;
            /*
            this.nombre = nombre;
            this.codigo = codigo;
            this.stock = stock;
            this.activo = activo;
            this.precio = precio;
            */
            lblId.Text = id.ToString();
            txtNombre.Text = nombre;
            txtCodigo.Text = codigo;
            txtStock.Text = stock.ToString();
            txtPrecio.Text = precio.ToString();
            if(activo == 0)
            {
                swActivar.IsToggled = false;
            }
            else
            {
                swActivar.IsToggled = true;
            }
            
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            string mensaje;

            Ws.Item objeto = new Ws.Item();
            objeto.id = Id;
            objeto.nombre = txtNombre.Text;
            objeto.codigo = txtCodigo.Text;
            objeto.stock = Convert.ToInt32(txtStock.Text);
            objeto.precio = Convert.ToDouble(txtPrecio.Text);
            if (swActivar.IsToggled)
            {
                objeto.activo = 1;
                mensaje = "Producto actualizado correctamente.";
            }
            else
            {
                objeto.activo = 0;
                mensaje = "Producto eliminado correctamente.";
            }

            
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://192.168.1.5/API/actualizar-producto/"+Id);
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    await DisplayAlert("Ok", mensaje, "ok");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    mensaje = "Problema: " + response.StatusCode;
                    await DisplayAlert("Error", mensaje, "ok");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex;
                await DisplayAlert("Error", mensaje, "ok");
            }


        }
    }
}