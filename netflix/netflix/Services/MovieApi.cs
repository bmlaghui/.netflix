using RestSharp;
using System;
using DOTNETFLIX.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using netflix;
using netflix.Models;

namespace DOTNETFLIX.Services
{
    public static class MovieApi
    {
        private const string HOST = "movie-database-alternative.p.rapidapi.com";
        private const string KEY = "8fa6e9cbd0msh0f6d64f2e92480dp122188jsn2ee48f6b6699";
        public static async Task<SearchResults> SearchAsync(string query)
        {
            var request = CreateRequest(Method.GET);
            request.AddQueryParameter("s", query);

            IRestResponse response = await App.Client.ExecuteAsync(request);
           
            return System.Text.Json.JsonSerializer.Deserialize<SearchResults>(response.Content);

        }
        
        private static RestRequest CreateRequest(Method method)
        {
            var request = new RestRequest(method);


            request.AddHeader("x-rapidapi-host", HOST);

            request.AddHeader("x-rapidapi-key", KEY);

            return request;

        }

        public static async Task<Movie> SeachByIdAsync(string imdb)
        {

            var request = CreateRequest(Method.GET);
            request.AddQueryParameter("i", imdb);

            IRestResponse response = await App.Client.ExecuteAsync(request);

            return System.Text.Json.JsonSerializer.Deserialize<Movie>(response.Content);

        }
    }

    
        
    
}
