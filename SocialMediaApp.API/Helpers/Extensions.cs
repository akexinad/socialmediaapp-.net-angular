using Microsoft.AspNetCore.Http;

namespace SocialMediaApp.API.Helpers
{
    // Static means that we dont need to instantiate the class in order to use one of its methods.
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // Adding new headers in an http response
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}