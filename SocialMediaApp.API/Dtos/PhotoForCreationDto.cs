using System;
using Microsoft.AspNetCore.Http;

namespace SocialMediaApp.API.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }

        // This is the file that we are sending with the HTTP request.
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}