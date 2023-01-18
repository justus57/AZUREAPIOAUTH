using Microsoft.IdentityModel.Clients.ActiveDirectory;
using RestSharp;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAuthorizationToken();
            Console.WriteLine(AzureDetails.AccessToken);
            var client = new RestClient("https://api.businesscentral.dynamics.com/v2.0/4dfedb10-35ca-4e46-9c2a-0fa40d6968c0/Sandbox2/WS/NGO%20Demo/Codeunit/WebPortal");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer "+AzureDetails.AccessToken);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        public static void GetAuthorizationToken()
        {
            ClientCredential cc = new ClientCredential(AzureDetails.ClientID, AzureDetails.ClientSecret);
           // var context = new AuthenticationContext("https://login.microsoftonline.com/" + AzureDetails.TenantID);
            var context = new AuthenticationContext("https://login.windows.net/" + AzureDetails.TenantID);
            var result = context.AcquireTokenAsync("https://api.businesscentral.dynamics.com", cc);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the Access token");
            }
            AzureDetails.AccessToken = result.Result.AccessToken;
        }
    }
}
