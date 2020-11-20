using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tare6JSuntaxi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]


    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.100.5/rest/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Tare6JSuntaxi.Ws.Estudiante> _post;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            var content = await client.GetStringAsync(Url);
            List<Tare6JSuntaxi.Ws.Estudiante> posts = JsonConvert.DeserializeObject<List<Tare6JSuntaxi.Ws.Estudiante>>(content);
            _post = new ObservableCollection<Ws.Estudiante>(posts);

            MyListView.ItemsSource = _post;

            
        }

        private async void btnVistaInsertar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaInsertar());
        }

        private async void btnVistaEditar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new vistaEditar("","","",""));
        }

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Tare6JSuntaxi.Ws.Estudiante)e.SelectedItem;
            var id = Convert.ToString(item.codigo);
            var nombre = item.nombre;
            var apellido = item.apellido;
            var edad = Convert.ToString(item.edad);

            await Navigation.PushAsync(new vistaEditar(id, nombre, apellido, edad));
        }
    }
}
