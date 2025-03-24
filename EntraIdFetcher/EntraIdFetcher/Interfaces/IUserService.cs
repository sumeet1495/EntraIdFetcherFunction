using EntraIdFetcher.Models;  // allows to use userDetailsModels class in the file
using System.Threading.Tasks; // provides async/await programming 

// below code is the implementation of an interface
// is like agreement that provides what all methods a class must implement, but not how they work.

namespace EntraIdFetcher.Interfaces
{
    public interface IUserService
    {
        //method GetUserDetailsByIdAsync which takes a string object ID and returns user details in asynchronous way
        // return type - return a UserDetailsModel object
        Task<UserDetailsModel> GetUserDetailsByIdAsync(string objectId);
    }
}
