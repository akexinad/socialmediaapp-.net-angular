namespace SocialMediaApp.API.Dtos
{
    // These properties are the only properties the user is allowed to update,
    // so its a good idea to create a dto for this so we don't transfer too much data
    // in the http requests.
    public class UserForUpdateDto
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}