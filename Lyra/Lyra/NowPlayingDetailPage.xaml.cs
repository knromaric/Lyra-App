using Lyra.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lyra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NowPlayingDetailPage : ContentPage
	{
        private NowPlayingMovie _movie;

        public NowPlayingDetailPage (NowPlayingMovie movie)
        { 
            _movie = movie;
            InitializeComponent ();
            BindingContext = _movie;
        }

        private async void PlayVideo_Tapped(object sender, EventArgs e)
        {
            var videoPage = new VideoPage(_movie.MovieTrailor);
            await Navigation.PushAsync(videoPage);
        }

        private async void OnBookTicket(object sender, EventArgs e)
        {
            var bookTicketPage = new BookTicketPage(_movie);

            await Navigation.PushAsync(bookTicketPage);
        }
    }
}