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
        const string IP_IDENTITYSERVER = "172.30.238.2";
        const string URL_API_PASSENGERS = "http://172.30.238.3/PassengerDetails";
        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            //var disco = await DiscoveryClient.GetAsync(URL);


            var discoveryClient = new DiscoveryClient($"http://{IP_IDENTITYSERVER}");
            discoveryClient.Policy = new DiscoveryPolicy { RequireHttps = false };
            var disco = await discoveryClient.GetAsync();

            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenClient = new TokenClient($"{URL}/connect/token", "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            //TODO: http://docs.identityserver.io/en/release/quickstarts/2_resource_owner_passwords.html
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
            var response = await client.GetAsync(URL_API_PASSENGERS);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[Client FALLA] Verifique/modifique el IP Authority en Startup para el controller");
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
