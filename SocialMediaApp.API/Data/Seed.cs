using System.Collections.Generic;
using Newtonsoft.Json;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            // Read the json file.
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            // Deserialise the json and turn it into a .NET object, which will be a list of users
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                // Create a password hash and salt for the users
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _context.Add(user);
            }

            _context.SaveChanges();

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
    }
}