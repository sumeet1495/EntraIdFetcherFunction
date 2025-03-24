using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using EntraIdFetcher.Interfaces;

namespace EntraIdFetcher.Helpers
{
    public class GraphServiceClientProvider : IGraphAuthProvider
    {
        private readonly IConfiguration _configuration;

        public GraphServiceClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GraphServiceClient GetGraphServiceClient()
        {
            var tenantId = _configuration["AzureAd:TenantId"];
            var clientId = _configuration["AzureAd:ClientId"];
            var clientSecret = _configuration["AzureAd:ClientSecret"];

            // Null check to avoid runtime crash
            if (string.IsNullOrWhiteSpace(tenantId) ||
                string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new InvalidOperationException("Azure AD credentials (TenantId, ClientId, or ClientSecret) are missing in configuration.");
            }

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            return new GraphServiceClient(credential);
        }
    }
}
