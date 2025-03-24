using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;
using EntraIdFetcher.Interfaces;

namespace EntraIdFetcherFunctionApp
{
    public class UserFetchFunction
    {
        private readonly ILogger<UserFetchFunction> _logger;
        private readonly IUserService _userService;

        // Local private class is created to read input body sent in the post request of http through postman
        private class RequestModel
        {
            public string ObjectId { get; set; }
        }

        public UserFetchFunction(ILogger<UserFetchFunction> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Function("UserFetchFunction")]
        // Function level authorisation is used for creating HTTP trigger
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequestData req) // used post request where user will sent object id in body to get details
        {
            _logger.LogInformation("UserFetchFunction triggered.");
            try
            {
                // Reading and parsing request body sent in the post request
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var requestData = JsonSerializer.Deserialize<RequestModel>(requestBody);
                Console.WriteLine($"Body received - {requestBody}");
                Console.WriteLine($"Parsed ObjectId - {requestData?.ObjectId}");
                // for debugging


                if (requestData == null || string.IsNullOrEmpty(requestData.ObjectId))
                {
                    _logger.LogWarning("Invalid request payload or ObjectId missing.");
                    return new BadRequestObjectResult("Please provide a valid 'objectId' in the request body.");
                }

                var userDetails = await _userService.GetUserDetailsByIdAsync(requestData.ObjectId);

                _logger.LogInformation($"User fetched: {userDetails?.DisplayName}");
                return new OkObjectResult(userDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL error - {ex}");
                _logger.LogError($"Unhandled exception - {ex}");
                return new ObjectResult("Internal server error") { StatusCode = 500 };
            }
        }


    }
}
