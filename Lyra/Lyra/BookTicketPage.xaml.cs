using Lyra.Models;
using Lyra.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lyra
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookTicketPage : ContentPage
	{
        private NowPlayingMovie _movie;
        private string _bookDate; 
		public BookTicketPage (NowPlayingMovie movie)
		{
            _movie = movie;
			InitializeComponent ();
            ImgMovieCover.Source = movie.CoverImage;
            LblMovieName.Text = movie.MovieName;
            LblPlayingDate.Text = movie.PlayingDate.ToShortDateString();
            LblTime1.Text = movie.ShowTime1.ToShortTimeString();
            LblTime2.Text = movie.ShowTime2.ToShortTimeString();
            LblTime3.Text = movie.ShowTime3.ToShortTimeString();
            SpanAmount.Text = movie.TicketPrice;
            SpanTotalPrice.Text = movie.TicketPrice;
            SpanQty.Text = "1";

		}

        private void TapTime1_Tapped(object sender, EventArgs e)
        {
            Frame1.BackgroundColor = Color.LightGray;
            Frame2.BackgroundColor = Color.FromHex("#FF5722");
            Frame3.BackgroundColor = Color.FromHex("#FF5722");
            _bookDate = LblTime1.Text;
        }

        private void TapTime2_Tapped(object sender, EventArgs e)
        {
            Frame1.BackgroundColor = Color.FromHex("#FF5722");
            Frame2.BackgroundColor = Color.LightGray;
            Frame3.BackgroundColor = Color.FromHex("#FF5722");
            _bookDate = LblTime2.Text;
        }

        private void TapTime3_Tapped(object sender, EventArgs e)
        {
            Frame1.BackgroundColor = Color.FromHex("#FF5722");
            Frame2.BackgroundColor = Color.FromHex("#FF5722");
            Frame3.BackgroundColor = Color.LightGray;
            _bookDate = LblTime3.Text;
        }

        private async void BtnBookTicket_Clicked(object sender, EventArgs e)
        {
            var bookTicket = new BookTicket
            {
                CustomerName = EntName.Text,
                Email = EntEmail.Text,
                Phone = EntPhone.Text,
                Qty = SpanQty.Text,
                MovieName = LblMovieName.Text,
                TotalPayment = SpanTotalPrice.Text,
                BookingDate = _bookDate
            };

            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.Order(bookTicket);
            if (response)
            {
                await DisplayAlert("Hi", "Your ticket has been reserved...", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong...", "OK");
            }
        }

        private void PickerQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var qty = PickerQty.Items[PickerQty.SelectedIndex];
            SpanQty.Text = qty;
            double price = Convert.ToDouble(SpanAmount.Text);
            double totalPrice = Convert.ToDouble(qty) * price;
            SpanTotalPrice.Text = totalPrice.ToString();
        }
    }
}