using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JsonParsing : ContentPage
    {
        public ObservableCollection<PizzaBoy> items;
        public JsonParsing()
        {
            InitializeComponent();
            GetJson();
        }

        public async void GetJson()
        {
            if(NetworkCheck.IsInternet())
            {
                var client = new HttpClient();
                var response = await client.GetAsync("http://www.pizzaboy.de/app/pizzaboy.json");
                await DisplayAlert("Alert", "Connection Established", "Ok");
                string pizza = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(pizza);
                items = new ObservableCollection<PizzaBoy>();
                if (pizza != "")
                {
                    items = JsonConvert.DeserializeObject<ObservableCollection<PizzaBoy>>(pizza);                    
                }
                PizzaList.ItemsSource = items;
            }
            else
            {
                await DisplayAlert("Alert", "No Network", "Ok");
            }
        }
    }
}