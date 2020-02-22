using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace Program
{
    static class Program
    {
        static void Main(string[] args)
        {
            
            string KEY = "AIzaSyBmajWtbWJ2Mcfp_fJjZ3ObyFGW9ZtiLNM";
            // string[] args = Environment.GetCommandLineArgs();
            foreach (var item in args)
            {
                    BingShare(item);
                
            }
            Console.WriteLine("");
            foreach (string argumento in args)
            {
                string urlGoogle = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx=017576662512468239146:omuauf_lfve&q="+argumento;
                var jsonGoogle = new WebClient().DownloadString(urlGoogle);
                string urlBing = "https://api.cognitive.microsoft.com/bing/v7.0/search"+argumento;
                var jsonBing = new WebClient().DownloadString(urlGoogle);
                dynamic resGoogle = JsonConvert.DeserializeObject(jsonGoogle);
                dynamic resBing = JsonConvert.DeserializeObject(jsonBing);
                
                // Recorrer el obejto del request de Google
                foreach (var i in resGoogle.queries.request)
                {
                    Console.WriteLine("{0}: Google {1}", argumento ,i.totalResults);
                }
            }

            Console.ReadLine();
        }
        
        static async void BingShare(string busqueda)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "714ebc5f53934dbf896c514dbf3db2cd");

            // Request parameters
            queryString["q"] = busqueda;
            queryString["count"] = "10";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safesearch"] = "Moderate";
            var uri = "https://api.cognitive.microsoft.com/bing/v7.0/search?" + queryString;
            // var jsonBing = new WebClient().DownloadString(uri);
            var response = await client.GetAsync(uri);
            if (response?.Content != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic resBing = JsonConvert.DeserializeObject(responseString);
                Console.WriteLine("{0}: Bing {1}",busqueda, resBing.webPages.totalEstimatedMatches);  
            }
        }
    }
}