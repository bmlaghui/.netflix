using DOTNETFLIX.Models;
using DOTNETFLIX.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace netflix
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnSearch.Clicked += OnSearch;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.Red;
        }


        private async void OnSearch(object sender, EventArgs e)
        {
            await ToogleActivityIndicator(true);
            scrollViewMovies.Content = null;
            scrollViewSeries.Content = null;

            lblMovies.IsVisible = false;
            lblSeries.IsVisible = false;

            var searchResults = await MovieApi.SearchAsync(entrySearch.Text);
            var movies = searchResults.Searches.FindAll(search => search.Type == "movie");
            var series = searchResults.Searches.FindAll(search => search.Type == "series");

            StackLayout stackLayoutMovies = await FillStackLayout(movies);
            StackLayout stackLayoutSeries = await FillStackLayout(series);
            if(stackLayoutMovies != null)
            {
                scrollViewMovies.Content = stackLayoutMovies;
                lblMovies.IsVisible = true;

            }
            if (stackLayoutSeries != null)
            {
                scrollViewSeries.Content = stackLayoutSeries;
                lblSeries.IsVisible = true;

            }

            await ToogleActivityIndicator(false);
        }

        private async Task<StackLayout> FillStackLayout(List<Search> searches)
        {
            StackLayout stackLayout = null;

            if(searches.Count == 0)
            {
                return stackLayout;
            }

            await Task.Run(() =>
            {
                stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };


                foreach (Search search in searches)
                {
                    var tapGestureReconizer = new TapGestureRecognizer();
                    tapGestureReconizer.Tapped += async (s, ev) => 
                    {
                        var moviePage = await MoviePage.Create(search.imdbID);
                        await Navigation.PushAsync(moviePage);
                    };
                    var image = new Image
                    {
                        Source = search.Poster
                    };

                    image.GestureRecognizers.Add(tapGestureReconizer);
                    stackLayout.Children.Add(image);
                }
            });
            return stackLayout;
        }

        private async Task ToogleActivityIndicator(bool isBusy)
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityIndicator.IsRunning = isBusy;
                    activityIndicator.IsVisible = isBusy;
                }
                );
            }
            );
        }
    }
}
