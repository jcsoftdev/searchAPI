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
            int iBing = 0;
            int iGoogle = 0;
            string KEY = "AIzaSyAJuxgY1dQGQV45L1nIOoxLjJd7Cy-xA1M";
            foreach (string argumento in args)
            {
                // Los links de cosnultas
                string urlGoogle = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx=010556043604774410724:xvljiggke1i&q="+argumento;
                string urlBing = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx=010556043604774410724:fzoekgp3pmq&q="+argumento;
                // Realizando la peticion de links
                var jsonGoogle = new WebClient().DownloadString(urlGoogle);
                var jsonBing = new WebClient().DownloadString(urlGoogle);
                // Convitiendo el string a objects
                dynamic resGoogle = JsonConvert.DeserializeObject(jsonGoogle);
                dynamic resBing = JsonConvert.DeserializeObject(jsonBing);
                
                // Recorrer el obejto del request de Google
                foreach (var google in resGoogle.queries.request)
                {
                    // Recorrer el obejto del request de Bing
                    foreach (var bing in resBing.queries.request)
                    {
                        Console.WriteLine("__________________________________________________");
                        Console.WriteLine("");
                        Console.WriteLine("{0}: Google => {1}, Bing => {2}", argumento ,google.totalResults, bing.totalResults);
                        if (google.totalResults > bing.totalResults)
                        {
                            Console.WriteLine("Google Winner: {0}", argumento);
                            iGoogle ++;
                        }else
                        {
                            Console.WriteLine("Bing Winner: {0}", argumento);
                            iBing ++;
                        }
                        
                    }
                }
                
            }
            Console.WriteLine("__________________________________________________");
            if (iBing>iGoogle)
            {
                Console.WriteLine("Bing winner");
            }else if(iBing<iGoogle){
                Console.WriteLine("Google winner");
            }else{
                Console.WriteLine("Obtubieron un empate");
            }
            Console.WriteLine("======================");

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