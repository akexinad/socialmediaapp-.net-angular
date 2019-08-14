using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public interface IDatingRepository
    {
        // Here we are using what is known as a generic.
        // The type T is GENERIC and can either be a type of User or a type of Photo, which are our Entities.
        // Thus, we can use this method to add photos as well as users.
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();

        // IEnumerable<User> = a list of type user
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
    }
}