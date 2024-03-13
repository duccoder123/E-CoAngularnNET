

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _db;
        private readonly ITokenService _token;
        public AccountController(DataContext db, ITokenService token)
        {
            _db= db;
            _token = token;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            if(await UserExist(registerDto.UserName)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _db.AppUsers.Add(user);
            await _db.SaveChangesAsync();
            return new UserDto{
                Username = user.UserName,
                Token = _token.CreateToken(user)
            };
        }
      

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _db.AppUsers.SingleOrDefaultAsync( x=> x.UserName == loginDto.UserName);
            if(user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var completedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i =0; i < completedHash.Length; i++){
                if(completedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto{
                Username = user.UserName,
                Token = _token.CreateToken(user)
            };
        }
        public async Task<bool> UserExist(string username){
            return await _db.AppUsers.AnyAsync(x=> x.UserName.ToLower().Equals( username.ToLower()));
        }
    }
}    