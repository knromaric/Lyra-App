using Lyra.Models;
using Lyra.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lyra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LatestMoviesPage : ContentPage
	{
        public ObservableCollection<LatestMovie> LatestMoviesCollection;
        private bool _isLatestMoviesLoaded = false;
        public LatestMoviesPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!_isLatestMoviesLoaded)
            {
                ApiServices apiService = new ApiServices();
                Console.WriteLine("ENNNTER THIS PAGE Latest movies: ");
                Debug.WriteLine("ENNNTER THIS PAGE Latest movies: ");
                var latestMovies = await apiService.GetLatestMovies();
                Console.WriteLine("Latest movies: " + latestMovies.Count);
                Debug.WriteLine("Latest movies: " + latestMovies.Count);
                LatestMoviesCollection = new ObservableCollection<LatestMovie>(latestMovies);

                _isLatestMoviesLoaded = true;
            }

            LvLatestMovies.ItemsSource = LatestMoviesCollection;
            LatestIndicator.IsRunning = false;
        }
        private async void LvLatestMovies_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var movie = e.ItemData as LatestMovie;
            if (movie != null)
            {
                await Navigation.PushAsync(new VideoPage(movie.MovieTrailor));
            }
        }
    }
}