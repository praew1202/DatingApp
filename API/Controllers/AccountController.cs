using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseAPIController, iTokenService
    {
        private readonly DataContext _context;
        private readonly iTokenService _tokenService;
        public AccountController(DataContext context, iTokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDTO>> Register(RegisterDTO resgister)
        {
            if (await UserExists(resgister.username)) return BadRequest("username is taken");
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(resgister.password));
            var passwordSalt = hmac.Key;
            var user = new AppUser()
            {
                UserName = resgister.username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt

            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return new UserTokenDTO{
                username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
            
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.User.AnyAsync(db => db.UserName == username.ToLower());
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDTO>> Login(LoginDTO login)
        {
            var user = await _context.User.SingleOrDefaultAsync(user => user.UserName == login.username);
            if (user == null) return Unauthorized("Username invalid");

            var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.
            GetBytes(login.password));

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i]) return Unauthorized("password invalid");

            }
              return new UserTokenDTO{
                username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        public string CreateToken(AppUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}