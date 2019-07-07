using System.Threading.Tasks;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    // By convention, Interfaces always start off with a capital I.
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}