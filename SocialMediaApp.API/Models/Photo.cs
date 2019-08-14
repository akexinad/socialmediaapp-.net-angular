using System;

namespace SocialMediaApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        // PUBLIC ID: Found in the cloudinary respone when we have uploaded a photo.
        public string PublicId { get; set; }
        
        // RELATIONSHIPS BETWEEN MODELS
        public User User { get; set; }
        public int UserId { get; set; }
    }
}