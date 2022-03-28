using FrontendMovil.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrontendMovil
{
    public partial class MainPage : ContentPage
    {
        //private string _url = "https://10.0.0.8:45455/v1/Products";
        public MainPage()
        {
            InitializeComponent();
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            var prod = new List<Product>();
            prod.Add(new Product() { Name = "Manzana", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Durazno", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Cerveza", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Pera", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Fresa", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Zanahoria", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Naranja", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Mnmadrina", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Kiwi", Price = (decimal?)12.77 });
            prod.Add(new Product() { Name = "Limón", Price = (decimal?)12.77 });
            //HttpClient client = new HttpClient();
            //HttpResponseMessage response= await client.GetAsync(_url);

            //string responseBody = await response.Content.ReadAsStringAsync();   

            //prod =JsonConvert.DeserializeObject<List<Product>>(responseBody);

            ListView.ItemsSource = prod;

        }
    }
}
