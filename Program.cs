using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace Program
{   
    class Google{
        public long GetResults(string busqueda, string KEY, string EngineID){
            string urlGoogle = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx="+EngineID+"&q="+busqueda;
               
            var jsonGoogle = new WebClient().DownloadString(urlGoogle);
            // Convitiendo el string a objects
            dynamic resGoogle = JsonConvert.DeserializeObject(jsonGoogle);
            
            // Recorrer el obejto del request de Google
            long resultsQuantity = 0;
            foreach (var google in resGoogle.queries.request)
            {
                
                    // Console.WriteLine("__________________________________________________");
                    // Console.WriteLine("");
                    // Console.WriteLine("{0}: Google => {1}", busqueda ,google.totalResults);
                    resultsQuantity = google.totalResults;
                
            }
            Console.WriteLine("{0}: Google => {1}",busqueda, resultsQuantity);
            return resultsQuantity;
        }
    }
    class Bing{
        public long GetResults(string busqueda, string KEY, string EngineID){
            string urlBing = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx="+EngineID+"&q="+busqueda;
               
            var jsonBing = new WebClient().DownloadString(urlBing);
            // Convitiendo el string a objects
            dynamic resBing = JsonConvert.DeserializeObject(jsonBing);
            
            // Recorrer el obejto del request de Bing
            long resultsQuantity = 0;
            foreach (var bing in resBing.queries.request)
            {
                resultsQuantity = bing.totalResults;
            }
            Console.WriteLine("{0}: Bing   => {1}",busqueda, resultsQuantity);
            return resultsQuantity;
        }
    }
    static class Program
    {
        static void Main(string[] args)
        {
            long iBing = 0;
            long iGoogle = 0;
            string KEY = "AIzaSyAJuxgY1dQGQV45L1nIOoxLjJd7Cy-xA1M";
            string EngineIdG = "010556043604774410724:xvljiggke1i";
            string EngineIdB = "010556043604774410724:fzoekgp3pmq";
            var searchGoogle = new Google();
            var searchBing = new Bing();
            Console.WriteLine("__________________________________________________");
            foreach (string argumento in args)
            {
                
                long resultsGoogle = searchGoogle.GetResults(argumento, KEY, EngineIdG);
                long resultsBing = searchBing.GetResults(argumento, KEY, EngineIdB);
                if (resultsGoogle>resultsBing)
                {
                    iGoogle++;
                }else if (resultsGoogle<resultsBing)
                {
                    iBing++;
                }
                
            }
            Console.WriteLine("============================");
            if (iBing>iGoogle)
            {
                Console.WriteLine("Bing winner");
            }else if(iBing<iGoogle){
                Console.WriteLine("Google winner");
            }else{
                Console.WriteLine("Obtubieron un empate");
            }
            Console.WriteLine("============================");

        }
        
        // this function isn't  use
        // it's just a demo for Microsoft's API
        static async void BingSearch(string busqueda)
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