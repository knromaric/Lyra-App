using Lyra.Models;
using Lyra.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lyra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NowPlayingMoviesPage : ContentPage
	{
        public ObservableCollection<NowPlayingMovie> NowPlayingMoviesCollection;
        private bool isNowPlayingMoviesLoaded = false;
		public NowPlayingMoviesPage ()
		{
			InitializeComponent (); 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!isNowPlayingMoviesLoaded)
            {
                ApiServices apiServices = new ApiServices();
                var nowPlayingMovies = await apiServices.GetNowPlayingMovies();

                NowPlayingMoviesCollection = new ObservableCollection<NowPlayingMovie>(nowPlayingMovies);

                isNowPlayingMoviesLoaded = true;
            }

            LvNowPlaying.ItemsSource = NowPlayingMoviesCollection;
            BusyIndicator.IsRunning = false;

        }

        private async void OnMovieSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedMovie = e.SelectedItem as NowPlayingMovie;
            LvNowPlaying.SelectedItem = null;
            
            var detailPage = new NowPlayingDetailPage(selectedMovie);
            await Navigation.PushAsync(detailPage);     
        }
    }
}