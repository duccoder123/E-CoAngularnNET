using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public  class SeedingData
    {
        public async static Task SeedUsers (DataContext context){
            if(await context.AppUsers.AnyAsync()) return;
            var userData = await File.ReadAllTextAsync("Data/userSeedData.json");
            var option =new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData,option);
            foreach(var user in users){
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("This is Pas$w0RD"));
                user.PasswordSalt = hmac.Key;
                context.AppUsers.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}