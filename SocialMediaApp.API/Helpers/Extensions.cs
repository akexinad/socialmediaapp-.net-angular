using System;
using Microsoft.AspNetCore.Http;
using SocialMediaApp.API.Models;

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

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            } 

            return age;
        }
    }
}