using Lyra.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lyra.Services
{
    public class ApiServices
    {
        private readonly string _nowPlayingMoviesUrl = "http://colosseum.somee.com/api/NowPlayingMovies";
        private readonly string _upcomingMovieUrl = "http://colosseum.somee.com/api/UpComingMovies";
        private readonly string _orderApiUrl = "http://colosseum.somee.com/api/Orders";
        public async Task<List<NowPlayingMovie>> GetNowPlayingMovies()
        {
            var client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, _nowPlayingMoviesUrl);
            requestMessage.Headers.Add("ApiKey", "30bc5986-e889-4e88-844e-ac88cde87406");
            var responseMessage = await client.SendAsync(requestMessage);
            var movieResponse = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<NowPlayingMovie>>(movieResponse);
        }

        public async Task<List<UpComingMovie>> GetUpcomingMovies()
        {
            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _upcomingMovieUrl);
            requestMessage.Headers.Add("ApiKey", "30bc5986-e889-4e88-844e-ac88cde87406");
            var responseMessage = await client.SendAsync(requestMessage);
            var movieResponse = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UpComingMovie>>(movieResponse);
        }

        public async Task<bool> Order(BookTicket bookTicket)
        {
            var client = new HttpClient();
            var jsonTicket = JsonConvert.SerializeObject(bookTicket);
            var content = new StringContent(jsonTicket, Encoding.UTF8, "application/json");
            content.Headers.Add("ApiKey", "30bc5986-e889-4e88-844e-ac88cde87406");
            var response = await client.PostAsync(_orderApiUrl, content);
            return response.IsSuccessStatusCode; 
        }
    }
}
