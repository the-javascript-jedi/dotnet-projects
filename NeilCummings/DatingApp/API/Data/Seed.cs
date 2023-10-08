using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            // PropertyNameCaseInsensitive - even if object casing is not matching
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // move from json to c# object
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}