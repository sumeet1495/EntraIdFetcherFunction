using Microsoft.Graph; // way to connect and interact with Microsoft Graph API
using EntraIdFetcher.Interfaces; // importing interface for the implementation
using EntraIdFetcher.Models; // importing model class to hold the user data
using System.Threading.Tasks;  // provides support for asynchronous programming (async/await)

namespace EntraIdFetcher.Services
{
    public class UserService : IUserService
    {
        private readonly GraphServiceClient _graphClient; // use for storing an instance of Microsoft Graph client for invoking graph api functions

        // constructor invokation to use Azure.Identity library for authentication
        public UserService(GraphServiceClient graphClient)
        {
            _graphClient = graphClient;
        }

        // method to fetch user details based on unique object ID
        public async Task<UserDetailsModel> GetUserDetailsByIdAsync(string objectId)   // Example of "I" - Interfaces Segregation
        {
            var user = await _graphClient.Users[objectId].GetAsync();  // No .Request() required after v5.0
           // Console.WriteLine(user);

            // mapping response to model 
            return new UserDetailsModel
            {
                ObjectId = user.Id,
                DisplayName = user.DisplayName,
                FirstName = user.GivenName,
                LastName = user.Surname,
                UserPrincipalName = user.UserPrincipalName,
                OfficeLocation = user.OfficeLocation,  
                MobilePhone = user.MobilePhone
            };
        }
        
    }
}
