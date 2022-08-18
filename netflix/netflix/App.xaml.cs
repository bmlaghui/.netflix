using RestSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace netflix
{
    public partial class App : Application
    {
        public const string API_BASE_URL = "https://movie-database-alternative.p.rapidapi.com";
        public static RestClient Client;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
           
        }

        protected override void OnStart()
        {
            Client = new RestClient(API_BASE_URL);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
