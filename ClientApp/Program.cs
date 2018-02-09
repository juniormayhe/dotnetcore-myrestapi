using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientApp
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        static string URL = "http://172.30.225.93";
        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            //var disco = await DiscoveryClient.GetAsync(URL);


            //var discoveryClient = new DiscoveryClient(URL);
            ////var disco = await DiscoveryClient.GetAsync(Contants.Constants.Authority);
            //discoveryClient.Policy = new DiscoveryPolicy { RequireHttps = false };
            //var disco = await discoveryClient.GetAsync();

            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}

            // request token
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenClient = new TokenClient($"{URL}/connect/token", "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine("[FALLA]");
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("[OK]");
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            const string URL_API_DESEADA = "172.30.234.150";
            string controller = "PassengerDetails";
            var response = await client.GetAsync($"http://{URL_API_DESEADA}/{controller}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[Client FALLA]");
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                Console.WriteLine("[Client OK]");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadKey();
        }
    }
}
