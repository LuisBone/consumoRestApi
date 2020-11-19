using System;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace consumoWsLB
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGet_Clicked(object sender, EventArgs e)
        {
            string mensaje;
            try
            {
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://192.168.1.5/API/listado-productos");
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //lblError.Text = content;
                    //var resultado = JsonConvert.DeserializeObject<List<Ws.Root>>(content);
                    Ws.Root resultado = JsonConvert.DeserializeObject<Ws.Root>(content);
                    List < Ws.Item > result = resultado.info.items;

                    myListView.ItemsSource = result;
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

        private async void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalle = e.Item as Ws.Item;
            //await DisplayAlert("prueba", "prueba: "+ detalle.nombre, "ok");
            await Navigation.PushAsync(new Detalle(detalle.id,detalle.nombre,detalle.codigo,detalle.stock,detalle.activo,detalle.precio));
        }
    }
}
