using Lyra.Models;
using Lyra.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lyra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpComingMoviesPage : ContentPage
	{
        public ObservableCollection<UpComingMovie> UpComingMovies;
        private bool _isMoviesLoaded = false;
		public UpComingMoviesPage ()
		{
			InitializeComponent ();
            
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!_isMoviesLoaded)
            {
                ApiServices apiService = new ApiServices();
                var comingMovies = await apiService.GetUpcomingMovies();
                UpComingMovies = new ObservableCollection<UpComingMovie>(comingMovies);
                _isMoviesLoaded = true;
            }
            
            LvUpComing.ItemsSource = UpComingMovies;
            UpComingIndicator.IsRunning = false; 
        }

        private async void LvUpComing_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var upComingMovie = e.ItemData as UpComingMovie;
            if(upComingMovie != null)
            {
                string trailorLink = upComingMovie.MovieTrailor;
                await Navigation.PushAsync(new VideoPage(trailorLink));
            }
            
        }
    }
}