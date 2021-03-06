using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        // Like what happened in the controller, create a constructor in order to have access to the DataContext
        // so you can access and query the database.
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user =  await _context.Users.FirstOrDefaultAsync( x => x.Username == username );
            
            if (user == null)
                return null;

            if (!verifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // We create the hmac value using the password salt provided.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // Then we create the computed hash using the hmac value.
                // and compare it to the password hash provided.
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            // For the hash and the salt, we want to pass them in as a reference, not as the value.
            // This is done by using the `out` keyword.
            // If you pass them in only as a value, their values wont be updated, so you have to be sure to pass them in as a reference.
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            // Note: press ctrl + . to stub out this method.

            // Store the hash and salt in the reference variables.
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Add and save the new user to the database.
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // The value stored in hmac gives us the functioality we need to create the password hash and salt.
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                // We need to pass the password in as a byte array.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync( x => x.Username == username ))
                return true;

            return false;
        }
    }
}