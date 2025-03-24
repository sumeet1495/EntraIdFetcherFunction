namespace EntraIdFetcher.Models
{   // comment by sumeet - This class holds basic user details fetched from Microsoft Graph
    public class UserDetailsModel
    {
        public string ObjectId { get; set; }            // It holds the User ID
        public string DisplayName { get; set; }        // Display name
        public string FirstName { get; set; }          // First name of the user
        public string LastName { get; set; }           // Last name of the user
        public string UserPrincipalName { get; set; }  // Email or login in entra ID 
        public string OfficeLocation { get; set; }    // Office location (address)
        public string MobilePhone { get; set; }       // Mobile number
     
    }
}
