// using System;
// using System.Net;
// using Newtonsoft.Json;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Net.Http;
// using System.Web;

// namespace CSHttpClientSample
// {
//     static class Program
//     {
//         static void Main(string[] args)
//         {
//             MakeRequest();
//             Console.WriteLine("Hit ENTER to exit...");
//             Console.ReadLine();
//             string KEY = "AIzaSyBmajWtbWJ2Mcfp_fJjZ3ObyFGW9ZtiLNM";
//             // string[] args = Environment.GetCommandLineArgs();
//             foreach (string argumento in args)
//             {
//                 // Console.WriteLine("Busqueda: {0}", argumento);
//                 string urlGoogle = "https://www.googleapis.com/customsearch/v1?key="+KEY+"&cx=017576662512468239146:omuauf_lfve&q="+argumento;
//                 var jsonGoogle = new WebClient().DownloadString(urlGoogle);
//                 string urlBing = "https://api.cognitive.microsoft.com/bing/v7.0/search"+argumento;
//                 var jsonBing = new WebClient().DownloadString(urlGoogle);
//                 // Console.WriteLine(jsonGoogle);
               
//                 dynamic resGoogle = JsonConvert.DeserializeObject(jsonGoogle);
//                 dynamic resBing = JsonConvert.DeserializeObject(jsonBing);
                
//                 // Recorrer el obejto del request de Google
//                 foreach (var i in resGoogle.queries.request)
//                 {
//                     Console.WriteLine("{0}: Google {1}", argumento ,i.totalResults);
//                 }
//             }
//             Console.WriteLine("By Juan Carlos");
//         }
        
//         static async void MakeRequest()
//         {
//             var client = new HttpClient();
//             var queryString = HttpUtility.ParseQueryString(string.Empty);

//             // Request headers
//             client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b74e5e4d56054fa1af5550867f9cf78a");

//             // Request parameters
//             queryString["q"] = "bill gates";
//             queryString["count"] = "10";
//             queryString["offset"] = "0";
//             queryString["mkt"] = "en-us";
//             queryString["safesearch"] = "Moderate";
//             var uri = "https://api.cognitive.microsoft.com/bing/v7.0/search?" + queryString;
//             // var jsonBing = new WebClient().DownloadString(uri);
//             var response = await client.GetAsync(uri);
//             if (response?.Content != null)
//             {
//                 var responseString = await response.Content.ReadAsStringAsync();
//                 dynamic resBing = JsonConvert.DeserializeObject(responseString);
//                 Console.WriteLine(resBing.webPages.totalEstimatedMatches);    
//             }
//         }
//     }
// }