using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.API.Dtos
{
    public class UserForRegisterDto
    {
        // It makes sense to do data validations inside the dto
        // as this is the object that is utilised to validate the user.
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters!")]        
        public string Password { get; set; }
    }
}