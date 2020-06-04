using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using static System.Console;

namespace IdentityServer.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5001");
            if (disco.IsError)
            {
                WriteLine("Disco Error:"+disco.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "clientCredentials",
                ClientSecret = "ClientCredentialsSecret",
                Scope = "api1"                
            });

            if (tokenResponse.IsError)
            {
                WriteLine($"Token Error:{tokenResponse.Error}");
                return;
            }

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var apiResponse = await apiClient.GetAsync("http://localhost:6001/identity");
            if (!apiResponse.IsSuccessStatusCode)
            {
                WriteLine(apiResponse.StatusCode);
            }
            else
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                WriteLine(JArray.Parse(content));
            }
            ReadLine();
        }
    }
}
